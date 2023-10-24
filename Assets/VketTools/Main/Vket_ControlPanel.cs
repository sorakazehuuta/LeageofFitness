using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VitDeck.Main.ValidatedExporter;
using VitDeck.Main.ValidatedExporter.GUI;
using VitDeck.Utilities;
using VitDeck.Validator;
using Vket.VketPrefabs;
using VketTools.Language;
using VketTools.Utilities;
using VketTools.Utilities.Networking;
using VketTools.Utilities.SS;
using VRC.Core;
using VRC.SDK3.Components;
using VRC.SDK3.Editor;
using VRC.SDKBase;
using VRC.SDKBase.Editor;
using UdonSharpEditor;
using UdonSharp;
using Assembly = System.Reflection.Assembly;
using AssetUtility = VketTools.Utilities.AssetUtility;
using Status = VketTools.Utilities.SequenceStatus.RunStatus;

namespace VketTools.Main
{
    public class Vket_ControlPanel : EditorWindow
    {
        private static readonly string EventUrl = "https://event.vket.com/";
        private static readonly string CircleMypageUrl = "https://event.vket.com/2023Winter/mypage/management";

        private static string _authorizationCode = "";
        private static NetworkUtility.AccessTokenProvider.Result _authentication;
        private static string _accessToken;
        private static string _vketId;
        private static int _companyCircleId;
        private static Texture2D _noImage;

        private static VketWorldDefinition _worldDefinition;
        private static NetworkUtility.AuthorizationCodeProvider.Result _authorizationResult;
        
        private const string WorldThumbnailFolderPath = "Assets/VketTools/Config/WorldThumbnail";
        private static Texture2D[] _worldThumbnails;

        public static bool IsLogin { get; private set; }
        public static bool IsSelected { get; private set; }
        public static bool IsWaitAuth { get; private set; }

        /// <summary>
        /// 認可コード入力ウィンドウのログインボタンが押されたかを格納
        /// </summary>
        private static bool _isClickLogin = false;
        
        /// <summary>
        /// ワールドのビルド中はtrueになる
        /// </summary>
        private static bool _isBuildWorld;
        
        /// <summary>
        /// タスクキャンセル用
        /// </summary>
        private static CancellationTokenSource _cts;
        
        /// <summary>
        /// 実行中のタスク
        /// </summary>
        private static UniTask _currentUniTask;

        /// <summary>
        /// 入稿シーケンス
        /// </summary>
        private static DraftSequence _currentSeq = DraftSequence.None;

        /// <summary>
        /// スクロール位置
        /// </summary>
        private static Vector2 _selectWindowScrollPos = Vector2.zero;
        private static Vector2 _controlPanelWindowScrollPos = Vector2.zero;
        
        /// <summary>
        /// パッケージタイプ
        /// </summary>
        private static PackageType _packageType;

        /// <summary>
        /// WorldDefinitionに定義されている最大サイズのタグ
        /// </summary>
        private enum MaxSizeType
        {
            SetPass,
            Batches,
        }

        /// <summary>
        /// VketToolsのPackageタイプ
        /// Assets/VketTools/Config/VersionInfo.assetから調整
        /// </summary>
        private enum PackageType
        {
            Stable,
            Company,
            Community,
            Dev,
        }
        
        /// <summary>
        /// イベント名の設定
        /// WorldDefinitionの取得に使用
        /// </summary>
        private enum EventFolder : ushort
        {
            GameVket0 = 0,
            Vket4 = 4,
            Vket5 = 5,
            Vket6 = 6,
            Vket2021 = 7,
            Vket2022Summer = 8,
            Vket2022Winter = 9,
            Vket2023Summer = 10,
            Vket2023Winter = 11,
        }

        #region VRCSDKが開かれた場合にウィンドウを開き、ウィンドウを閉じた場合に相互に閉じる処理
        
        /// <summary>
        /// VRCSDKコントロールパネルを開く 
        /// </summary>
        [MenuItem("VketTools/Show Control Panel", false, 600)]
        private static void OpenSDKWindow()
        {
            var sdkControlPanelType = Type.GetType("VRCSdkControlPanel,VRC.SDKBase.Editor");
            if (sdkControlPanelType == null)
            {
                Debug.LogError("Exception:Not Found VRCSdkControlPanelType");
                return;
            }
            
            MethodInfo info = null;
            try 
            {
                info = sdkControlPanelType.GetMethod("ShowControlPanel", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            }
            catch (Exception) 
            {
                Debug.LogError("Exception:Not Found VRCSdkControlPanel.ShowControlPanel Method");
            }
            if (info != null) 
            {
                info.Invoke(null, null);
            }
            else
            {
                Debug.LogError("Not Found VRCSdkControlPanel.ShowControlPanel Method");
            }
        }
        
        private static Vket_ControlPanel _controlPanel;
        private static VRCSdkControlPanel _vrcSdkWindow;

        private static void OnVrcSdkPanelEnable(object sender, EventArgs e)
        {
            OpenWindowIfClose();
        }
        
        private static void OnVrcSdkPanelDisable(object sender, EventArgs e)
        {
            _vrcSdkWindow = null;
            CloseWindowIfOpen();
        }

        private void OnEnable()
        {
            _controlPanel = this;
            _vrcSdkWindow = VRCSdkControlPanel.window;
        }

        private void OnDisable()
        {
            _controlPanel = null;
            
            // ワールドサムネイルの解放
            ReleaseWorldThumbnail();

            // SDKウィンドウの参照があれば閉じる
            if (_vrcSdkWindow)
            {
                _vrcSdkWindow.Close();
                _vrcSdkWindow = null;
            }
        }

        /// <summary>
        /// SDKパネルが開いた際に呼ぶ想定
        /// </summary>
        private static void OpenWindowIfClose()
        {
            if (!HasOpenInstances<Vket_ControlPanel>() && HasOpenInstances<VRCSdkControlPanel>())
            {
                var window = GetWindow<Vket_ControlPanel>(false, "VketTools");
                window.minSize = new Vector2(230f, 190f + GetFooterHeight(null) + GetToolButtonsHeight());
                window.Show();
            }
        }
        
        /// <summary>
        /// SDKパネルが閉じた際に呼ぶ想定
        /// </summary>
        private static void CloseWindowIfOpen()
        {
            if(_controlPanel)
            {
                _controlPanel.Close();
            }
        }

        #endregion

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            // デバッグモードでない場合、Unity起動時にシンボルにVITDECK_HIDE_MENUITEMが定義されていない場合は定義を追加する。
            // デバッグモードでない場合はメニューにVitDeckを表示しなくなる
            // インポート時、デバッグモードでない場合VITDECK_HIDE_MENUITEMシンボルの追加
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone).Split(';')
                .ToList();
            if (!Utilities.Hiding.HidingUtil.DebugMode)
            {
                if (!symbols.Contains("VITDECK_HIDE_MENUITEM"))
                {
                    symbols.Add("VITDECK_HIDE_MENUITEM");
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, string.Join(";", symbols));

            // InfoScriptableObject群をエディタ上で編集不可に変更
            AssetUtility.SetHideFlags();

            LoginInfo info = AssetUtility.LoginInfoData;

            // 既にログイン中の場合、ValidatorRuleの初期化
            if (IsLogin && !CircleNullOrEmptyCheck())
            {
                UserSettings userSettings = UserSettingUtility.GetUserSettings();
                if (string.IsNullOrEmpty(userSettings.validatorRuleSetType))
                {
                    switch (_packageType)
                    {
                        default:
                            // "Assets/id"
                            userSettings.validatorFolderPath = "Assets/" + info.GetCircle().id;
                            break;
                    }

                    userSettings.validatorRuleSetType = GetValidatorRule();
                }
            }

            // アクセストークンが無い場合ログイン処理
            if (!EditorApplication.isPlaying && info != null && !string.IsNullOrEmpty(info.accessToken))
            {
                SetPackageType();
                Login();
            }

            // 現在のシーンで境界線の描画
            DrawBoundsLimitGizmos(SceneManager.GetActiveScene());

            // プレイモード変更時に実行する関数登録
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

            // シーンを開いたときに実行する関数を登録
            EditorSceneManager.sceneOpened += OnSceneOpened;
            
            // SDKパネルのOpen,Close時の処理を追加
            VRCSdkControlPanel.OnSdkPanelEnable += OnVrcSdkPanelEnable;
            VRCSdkControlPanel.OnSdkPanelDisable += OnVrcSdkPanelDisable;
            
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        private static void Login()
        {
            LoginInfo info = AssetUtility.LoginInfoData;
            info.submissionTerm = NetworkUtility.GetTerm(AssetUtility.VersionInfoData.event_version, "stable");
            switch (_packageType)
            {
                case PackageType.Stable:
                    VketAccountKeepLogin();
                    UpdateLoginInfoData();
                    break;
                case PackageType.Company:
                case PackageType.Community:
                    info.profile = new NetworkUtility.Profile.Result();
                    info.data = new NetworkUtility.UserData();
                    info.accessToken = "hoge";
                    info.vketId = "hoge";
                    info.vrcLaunchFlag = false;
                    IsLogin = true;
                    break;
                case PackageType.Dev:
                    info.profile = new NetworkUtility.Profile.Result();
                    info.data = new NetworkUtility.UserData();
                    info.accessToken = "hoge";
                    info.vketId = "hoge";
                    info.vrcLaunchFlag = false;
                    IsLogin = true;
                    info.world = NetworkUtility.GetWorldData().data.worlds;
                    EditorCoroutineUtility.StartCoroutine(GetTexture(), new object());
                    break;
                default:
                    VketAccountKeepLogin();
                    UpdateLoginInfoData();
                    IsLogin = true;
                    _companyCircleId = 1;
                    break;
            }
        }

        private static void SetPackageType()
        {
            switch (AssetUtility.VersionInfoData.package_type)
            {
                case "stable":
                    _packageType = PackageType.Stable;
                    break;
                case "company":
                    _packageType = PackageType.Company;
                    break;
                case "community":
                    _packageType = PackageType.Community;
                    break;
                default:
                    _packageType = PackageType.Dev;
                    break;
            }
        }

        /// <summary>
        /// シーンが開かれた場合に呼ばれる
        /// </summary>
        /// <param name="scene">開いたシーン</param>
        /// <param name="mode">OpenSceneMode</param>
        private static void OnSceneOpened(Scene scene, OpenSceneMode mode)
        {
            DrawBoundsLimitGizmos(scene);
        }

        /// <summary>
        /// ブースの範囲境界線の描画
        /// </summary>
        /// <param name="scene">チェックするシーン</param>
        private static void DrawBoundsLimitGizmos(Scene scene)
        {
            var info = AssetUtility.LoginInfoData;
            var userSettings = VitDeck.Utilities.UserSettingUtility.GetUserSettings();
            if (scene.name == "" || scene.path == "" || info == null || !info.IsCircleAvailable() || !IsSelected ||
                SceneManager.GetActiveScene() != scene || scene.name != info.GetCircle().id.ToString() ||
                Path.GetDirectoryName(scene.path).Replace("/", @"\") !=
                userSettings.validatorFolderPath.Replace("/", @"\"))
            {
                return;
            }

            var ruleSets = Validator.GetRuleSets().Where(a => a.GetType().Name == userSettings.validatorRuleSetType);
            if (ruleSets.Any())
            {
                var selectedRuleSet = ruleSets.First();
                var baseFolder = AssetDatabase.LoadAssetAtPath<DefaultAsset>(userSettings.validatorFolderPath);
                var target = selectedRuleSet.TargetFinder.Find(AssetDatabase.GetAssetPath(baseFolder), false);
                var type = typeof(BoothBoundsRule);
                foreach (var rule in selectedRuleSet.GetRules())
                {
                    if (rule.GetType() == type)
                    {
                        // BoothBoundsRuleのLogicを呼び出し、BoothRangeIndicatorをシーンに配置することでGizmoを描画する
                        ReflectionUtility.InvokeMethod(type, "Logic", rule, new object[] { target });
                    }
                }
            }
        }

        /// <summary>
        /// LoginInfoの検証
        /// </summary>
        /// <returns>AssetUtility.LoginInfoDataが有効な場合true</returns>
        private bool ValidateInfo()
        {
            LoginInfo info = AssetUtility.LoginInfoData;
            return !(info == null || info.data == null || string.IsNullOrEmpty(info.accessToken) ||
                     string.IsNullOrEmpty(info.vketId));
        }

        /// <summary>
        /// AssetUtility.LoginInfoDataのサークルが選択されているか検証
        /// </summary>
        /// <returns>選択されている場合true</returns>
        private bool ValidateSelect()
        {
            LoginInfo info = AssetUtility.LoginInfoData;
            return info.selectedItemNum != 9999 || info.selectedCircleNum != 9999 || info.selectedWorldId != 9999;
        }

        private void OnGUI()
        {
            // 入稿処理中表示
            if (_currentSeq != DraftSequence.None)
            {
                DraftSequenceWindow();
                Repaint();
                return;
            }
            
            // 設定パネルの操作ができない特殊な状態の表示
            switch (_packageType)
            {
                case PackageType.Dev:
                case PackageType.Stable:
                {
                    // VRCSDKにログインしているか確認
                    if (!APIUser.IsLoggedIn)
                    {
                        EditorGUILayout.LabelField(/* "VRChat SDKにログインしてください。" */ LocalizedMessage.Get("Vket_ControlPanel.OnGUI.SDKLoginMessage"));
                        Repaint();
                        return;
                    }

                    // VRChat.exeのパスが合っているか確認
                    if (!ExistVRCClient())
                    {
                        EditorGUILayout.LabelField(/* "VRChatの起動パスが指定されていません。" */ LocalizedMessage.Get("Vket_ControlPanel.OnGUI.AppPathError.Title"));
                        EditorGUILayout.HelpBox(/* "VRChat SDKのSettingタブを開き、Installed Client PathにVRChat.exeを指定してください。" */ LocalizedMessage.Get("Vket_ControlPanel.OnGUI.AppPathError.Message"), MessageType.Info);
                        Repaint();
                        return;
                    }

                    // VRChat SDKが非表示
                    if (!VRCSdkControlPanel.window)
                    {
                        EditorGUILayout.LabelField(/* "VRChat SDKが表示されていません。「VRChat SDK/Show Control Panel」から表示してください。" */ LocalizedMessage.Get("Vket_ControlPanel.OnGUI.NotOpenSDKPanel.Title"));
                        Repaint();
                        return;
                    }
            
                    // VRChat SDK のBuilderタブが開いていない
                    if (VRCSettings.ActiveWindowPanel != 1)
                    {
                        EditorGUILayout.LabelField(/* "VRChat SDKのBuilderタブが開いていません。" */ LocalizedMessage.Get("Vket_ControlPanel.OnGUI.NotBuilderTab.Title"));
                        EditorGUILayout.HelpBox(/* "VRChat SDKのBuilderタブを開いてください。" */ LocalizedMessage.Get("Vket_ControlPanel.OnGUI.NotBuilderTab.Message"), MessageType.Info);

                        if (GUILayout.Button(/* "Builderタブを開く" */ LocalizedMessage.Get("Vket_ControlPanel.OnGUI.NotBuilderTab.OpenButton")))
                        {
                            // SDKにログインしていること前提
                            OpenSDKBuilderTab();
                        }
                        
                        Repaint();
                        return;
                    }
                }
                    break;
            }

            // タスク実行中はメニューを触れない
            EditorGUI.BeginDisabledGroup(!_currentUniTask.GetAwaiter().IsCompleted);
            
            // 毎フレームする必要があるか要確認
            AssetUtility.SetHideFlags();
            
            if (IsLogin)
            {
                switch (_packageType)
                {
                    case PackageType.Dev:
                    {
                        if (IsSelected)
                        {
                            ControlPanelWindow();
                        }
                        else
                        {
                            SelectWindow();
                        }
                        break;
                    }
                    case PackageType.Stable:
                    {
                        if (IsSelected || int.Parse(AssetUtility.VersionInfoData.event_version) < 7)
                        {
                            ControlPanelWindow();
                        }
                        else
                        {
                            SelectWindow();
                        }
                        break;
                    }
                    default:
                    {
                        ControlPanelWindow();
                        break;
                    }
                }
            }
            // 未ログイン時
            else
            {
                if (IsWaitAuth)
                {
                    // 認可コード入力ウィンドウ
                    WaitAuthWindow();
                }
                else
                {
                    // ログイン画面
                    LoginWindow();
                }
            }

            EditorGUI.EndDisabledGroup();
        }

        private void Update()
        {
            LoginInfo info = AssetUtility.LoginInfoData;
            SetPackageType();

            if (IsLogin)
            {
                if (!ValidateInfo())
                {
                    Logout();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(info.accessToken) && (info.mall_data != null || info.data != null) &&
                    !EditorApplication.isPlaying)
                {
                    _accessToken = info.accessToken;
                    _vketId = info.vketId;
                    Login();
                    IsSelected = ValidateSelect();
                }
            }

            // 認可コード入力時のログインボタン押下後にUpdateでの初期化処理を終えたタイミングで実行
            if (_isClickLogin)
            {
                ClickLogin();
                _isClickLogin = false;
            }

            if (IsSelected && !ValidateSelect())
                IsSelected = false;
            if (AssetUtility.LoginInfoData.profile != null && !IsLogin && !EditorApplication.isPlaying)
                UpdateLoginInfoData();
        }

        /// <summary>
        /// WorldDefinitionの初期化
        /// </summary>
        private static void SetupPlayModeWorldDefinition()
        {
            var info = AssetUtility.LoginInfoData;
            switch (_packageType)
            {
                case PackageType.Stable:
                    break;
                case PackageType.Company:
                case PackageType.Community:
                {
                    if (info != null)
                        _worldDefinition = AssetUtility.AssetLoad<VketWorldDefinition>(GetWorldDefinitionPath(info));
                    break;
                }
                case PackageType.Dev:
                {
                    if (info != null)
                        _worldDefinition = AssetUtility.AssetLoad<VketWorldDefinition>(GetWorldDefinitionPath(info.selectedItemNum));
                    break;
                }
                default:
                {
                    if (info != null)
                        _worldDefinition = AssetUtility.AssetLoad<VketWorldDefinition>($"{AssetUtility.ConfigFolderPath}/WorldDefinitions/0.asset");
                    break;
                }
            }
        }

        private int _SelectCircleId = 0;
        
        /// <summary>
        /// ログイン画面の描画
        /// </summary>
        private void LoginWindow()
        {
            GUILayout.Space(5);
            
            GUIStyle box = new GUIStyle(GUI.skin.box);
            GUIStyle l1 = new GUIStyle(GUI.skin.label);
            GUIStyle l2 = new GUIStyle(GUI.skin.label);

            l1.fontSize = 25;
            l1.fixedHeight = 30;
            l2.fontSize = 15;
            l2.fixedHeight = 23;

            float controlWidth = position.width - position.width / 3;

            LoginInfo info = AssetUtility.LoginInfoData;

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                GUIContent content = new GUIContent( /* ログイン */LocalizedMessage.Get("Vket_ControlPanel.Login"));
                EditorGUILayout.LabelField(content, l1, GUILayout.Width(l1.CalcSize(content).x));
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(30);

            switch (_packageType)
            {
                case PackageType.Company:
                {
                    EditorGUILayout.BeginVertical(box);
                    {
                        EditorGUILayout.Space();

                        _SelectCircleId = EditorGUILayout.IntField("ID", _SelectCircleId);
                        // 0以外を選択させる
                        bool isSelect = _SelectCircleId <= 0;
                        if (isSelect)
                        {
                            EditorGUILayout.HelpBox( /* "※IDを入力してください。" */
                                LocalizedMessage.Get("Vket_ControlPanel.LoginWindow.InputCircleID"), MessageType.Error);
                        }

                        EditorGUI.BeginDisabledGroup(isSelect);

                        // Note: 文字を変えてね
                        DrawCompanyBoothContent(info, controlWidth, _SelectCircleId, "ロンドン(London)", 2);
                        DrawCompanyBoothContent(info, controlWidth, _SelectCircleId, "沖縄(Okinawa)", 3);
                        DrawCompanyBoothContent(info, controlWidth, _SelectCircleId, "渋谷原宿(Shibuya Harajuku)", 4);

                        EditorGUI.EndDisabledGroup();

                        EditorGUILayout.Space();

                        GUILayout.Space(30);

                        GUILayout.FlexibleSpace();
                    }
                    EditorGUILayout.EndVertical();
                    break;
                }
                case PackageType.Community:
                {
                    EditorGUILayout.BeginVertical(box);
                    {
                        EditorGUILayout.Space();

                        _SelectCircleId = EditorGUILayout.IntField("サークルID", _SelectCircleId);
                        // 0以外を選択させる
                        bool isSelect = _SelectCircleId <= 0;
                        if (isSelect)
                        {
                            EditorGUILayout.HelpBox( /* "※IDを入力してください。" */
                                LocalizedMessage.Get("Vket_ControlPanel.LoginWindow.InputCircleID"), MessageType.Error);
                        }

                        EditorGUI.BeginDisabledGroup(isSelect);

                        // Note: 文字を変えてね
                        DrawCompanyBoothContent(info, controlWidth, _SelectCircleId, "プロップ設置プラン(PropPlan)", 1001);
                        DrawCompanyBoothContent(info, controlWidth, _SelectCircleId, "大規模ブースプラン(BigBoothPlan)", 1002);

                        EditorGUI.EndDisabledGroup();

                        EditorGUILayout.Space();

                        GUILayout.Space(30);

                        GUILayout.FlexibleSpace();
                    }
                    EditorGUILayout.EndVertical();
                    break;
                }
                case PackageType.Dev:
                {
                    EditorGUILayout.BeginVertical(box);
                    {
                        EditorGUILayout.Space();

                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.FlexibleSpace();
                            GUIContent content = new GUIContent("テストログイン");
                            EditorGUILayout.LabelField(content, l2, GUILayout.Width(l2.CalcSize(content).x));
                            GUILayout.FlexibleSpace();
                        }
                        EditorGUILayout.EndHorizontal();

                        GUILayout.Space(30);

                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.FlexibleSpace();

                            if (GUILayout.Button( /* ログイン */LocalizedMessage.Get("Vket_ControlPanel.Login"),
                                    GUILayout.Width(controlWidth),
                                    GUILayout.Height(30)))
                            {
                                info.profile = new Utilities.Networking.NetworkUtility.Profile.Result();
                                info.data = new Utilities.Networking.NetworkUtility.UserData();
                                info.accessToken = "hoge";
                                info.vketId = "hoge";
                                info.vrcLaunchFlag = false;
                                info.submissionTerm =
                                    Utilities.Networking.NetworkUtility.GetTerm(
                                        AssetUtility.VersionInfoData.event_version,
                                        "stable");
                                IsLogin = true;
                                info.world = Utilities.Networking.NetworkUtility.GetWorldData().data.worlds;
                                EditorUtility.SetDirty(info);
                                AssetDatabase.SaveAssets();
                                EditorCoroutineUtility.StartCoroutine(GetTexture(), new object());
                            }

                            GUILayout.FlexibleSpace();
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.Space();

                        GUIStyle l3 = new GUIStyle(GUI.skin.label);
                        l3.fontSize = 11;
                        l3.fixedHeight = 16;

                        EditorGUILayout.Space();

                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.FlexibleSpace();
                            UIUtility.EditorGUILink(EventUrl, l3);
                            GUILayout.FlexibleSpace();
                        }
                        EditorGUILayout.EndHorizontal();

                        GUILayout.FlexibleSpace();
                    }
                    EditorGUILayout.EndVertical();
                    break;
                }
                case PackageType.Stable:
                {
                    EditorGUILayout.BeginVertical(box);
                    {
                        EditorGUILayout.Space();

                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.FlexibleSpace();
                            GUIContent content = new GUIContent( /* VketAccountでログイン */
                                LocalizedMessage.Get("Vket_ControlPanel.LoginVketAccount"));
                            EditorGUILayout.LabelField(content, l2, GUILayout.Width(l2.CalcSize(content).x));
                            GUILayout.FlexibleSpace();
                        }
                        EditorGUILayout.EndHorizontal();

                        GUILayout.Space(30);

                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.FlexibleSpace();

                            if (GUILayout.Button( /* ログイン */LocalizedMessage.Get("Vket_ControlPanel.Login"),
                                    GUILayout.Width(controlWidth),
                                    GUILayout.Height(30)))
                            {
                                _authorizationResult = NetworkUtility.GetAuthorizationCode().Result;
                                IsWaitAuth = true;
                            }

                            GUILayout.FlexibleSpace();
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.Space();

                        GUIStyle l3 = new GUIStyle(GUI.skin.label);
                        l3.fontSize = 11;
                        l3.fixedHeight = 16;

                        EditorGUILayout.Space();

                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.FlexibleSpace();
                            UIUtility.EditorGUILink(EventUrl, l3);
                            GUILayout.FlexibleSpace();
                        }
                        EditorGUILayout.EndHorizontal();

                        GUILayout.FlexibleSpace();
                    }
                    EditorGUILayout.EndVertical();
                    break;
                }
            }
            
            GUILayout.Space(5);
        }

        /// <summary>
        /// 企業ブース選択コンテントの描画
        /// </summary>
        /// <param name="info"></param>
        /// <param name="controlWidth"></param>
        /// <param name="circleId"></param>
        /// <param name="worldName"></param>
        /// <param name="worldId"></param>
        private void DrawCompanyBoothContent(LoginInfo info, float controlWidth, int circleId, string worldName,
            int worldId)
        {
            GUILayout.Space(30);

            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();

                DrawGuiCompanyBoothButton(info, controlWidth, circleId, worldName, worldId);

                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 企業ブース選択ボタンの描画
        /// </summary>
        /// <param name="info"></param>
        /// <param name="controlWidth"></param>
        /// <param name="circleId"></param>
        /// <param name="worldName"></param>
        /// <param name="worldId"></param>
        /// <returns>入稿を開始ボタンが押された場合にtrue</returns>
        private bool DrawGuiCompanyBoothButton(LoginInfo info, float controlWidth, int circleId,
            string worldName, int worldId)
        {
            /* "入稿を開始する" */
            var content = new GUIContent($"{LocalizedMessage.Get("Vket_ControlPanel.StartSubmissionButton")}（{worldName}）");
            var clicked = GUILayout.Button(content, UIUtility.GetContentSizeFitStyle(content, GUI.skin.button, controlWidth), GUILayout.Width(controlWidth), GUILayout.Height(30));
            if (clicked)
            {
                info.profile = new Utilities.Networking.NetworkUtility.Profile.Result();
                info.data = new Utilities.Networking.NetworkUtility.UserData();
                info.accessToken = "hoge";
                info.vketId = "hoge";
                info.vrcLaunchFlag = false;
                info.submissionTerm = Utilities.Networking.NetworkUtility.GetTerm(AssetUtility.VersionInfoData.event_version, "stable");
                info.selectedWorldId = worldId;
                _companyCircleId = circleId;
                _worldDefinition = AssetUtility.AssetLoad<VketWorldDefinition>(GetWorldDefinitionPath(info));
                IsLogin = true;
            }

            return clicked;
        }

        /// <summary>
        /// 認可コード入力ウィンドウの描画
        /// </summary>
        private void WaitAuthWindow()
        {
            GUILayout.Space(5);
            
            GUIStyle box = new GUIStyle(GUI.skin.box);
            GUIStyle l1 = new GUIStyle(GUI.skin.label);
            GUIStyle l2 = new GUIStyle(GUI.skin.label);

            l1.fontSize = 25;
            l1.fixedHeight = 30;
            l2.fontSize = 15;
            l2.fixedHeight = 23;

            float controlWidth = position.width - position.width / 3;

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                GUIContent content = new GUIContent( /* ログイン */LocalizedMessage.Get("Vket_ControlPanel.Login"));
                EditorGUILayout.LabelField(content, l1, GUILayout.Width(l1.CalcSize(content).x));
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(30);

            EditorGUILayout.BeginVertical(box);
            {
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    GUIContent content = new GUIContent( /* 表示された認可コードを入力してください */LocalizedMessage.Get("Vket_ControlPanel.WaitAuthWindow.EnterAuthCode"));
                    EditorGUILayout.LabelField(content, l2, GUILayout.Width(l2.CalcSize(content).x));
                    GUILayout.FlexibleSpace();
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(30);

                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    _authorizationCode = EditorGUILayout.TextField(_authorizationCode, GUILayout.Width(controlWidth));
                    GUILayout.FlexibleSpace();
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(30);

                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button( /* ログイン */LocalizedMessage.Get("Vket_ControlPanel.Login"), GUILayout.Width(controlWidth),
                            GUILayout.Height(30)))
                    {
                        _isClickLogin = true;
                    }

                    GUILayout.FlexibleSpace();
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                GUIStyle l3 = new GUIStyle(GUI.skin.label);
                l3.fontSize = 11;
                l3.fixedHeight = 16;

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    UIUtility.EditorGUILink(EventUrl, l3);
                    GUILayout.FlexibleSpace();
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(30);

                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button( /* 戻る */LocalizedMessage.Get("Vket_ControlPanel.WaitAuthWindow.BackButton"), GUILayout.Width(controlWidth * 0.3f),
                            GUILayout.Height(25)))
                    {
                        IsWaitAuth = false;
                        _authorizationCode = "";
                        _authorizationResult = new NetworkUtility.AuthorizationCodeProvider.Result();
                    }

                    GUILayout.FlexibleSpace();
                }
                EditorGUILayout.EndHorizontal();


                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndVertical();
            
            GUILayout.Space(5);
        }

        /// <summary>
        /// 認可コード入力ウィンドウのログインボタン押下後に呼ばれる想定
        /// </summary>
        private async void ClickLogin()
        {
            _authentication = await NetworkUtility.GetAccessToken(_authorizationCode, _authorizationResult);
            VketAccountLogin(_authentication);
        }

        /// <summary>
        /// 入稿ワールド選択ウィンドウの描画
        /// </summary>
        private void SelectWindow()
        {
            GUILayout.Space(5);
            
            LoginInfo info = AssetUtility.LoginInfoData;

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();

            _selectWindowScrollPos = EditorGUILayout.BeginScrollView(_selectWindowScrollPos, GUI.skin.box);
            {
                switch (_packageType)
                {
                    case PackageType.Stable:
                    {
                        // 2022S, 2022W では circle と company の型は同じ
                        var circles = info.GetCirclesOrCompanyCircles();

                        // サークルが登録されていない場合はサークル登録確認画面の描画
                        if (circles == null || circles.Length <= 0 || circles[0].owner_id == 0)
                        {
                            GUILayout.Space(30);

                            EditorGUILayout.BeginHorizontal();
                            {
                                GUILayout.FlexibleSpace();
                                GUIContent content =
                                    new GUIContent(LocalizedMessage.Get("Vket_ControlPanel.SelectWindow.NotFoundCircle") /*"サークルが登録されていません。"*/);
                                GUIStyle textStyle = new GUIStyle();
                                textStyle.alignment = TextAnchor.MiddleCenter;
                                EditorGUILayout.LabelField(content, textStyle);
                                GUILayout.FlexibleSpace();
                            }
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            {
                                GUILayout.FlexibleSpace();
                                GUIContent content =
                                    new GUIContent(
                                        LocalizedMessage.Get("Vket_ControlPanel.SelectWindow.RequestLoginAgain") /*"マイページからサークルを登録のうえ再度ログインしてください。"*/);
                                GUIStyle textStyle = new GUIStyle();
                                textStyle.alignment = TextAnchor.MiddleCenter;
                                EditorGUILayout.LabelField(content, textStyle);
                                GUILayout.FlexibleSpace();
                            }
                            EditorGUILayout.EndHorizontal();

                            GUILayout.Space(30);

                            GUIStyle l3 = new GUIStyle(GUI.skin.label);
                            l3.fontSize = 11;
                            l3.fixedHeight = 16;

                            EditorGUILayout.BeginHorizontal();
                            {
                                GUILayout.FlexibleSpace();
                                UIUtility.EditorGUILink(CircleMypageUrl, l3);
                                GUILayout.FlexibleSpace();
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        else
                        {
                            // ワールド選択ボタンの描画
                            for (int i = 0; i < circles.Length; i++)
                            {
                                foreach (var entryWorld in circles[i].worlds)
                                {
                                    DrawWorldSelectContent(i, entryWorld.id, info, entryWorld.world_type == "quest", entryWorld.booth_type == "item");
                                }
                            }
                        }

                        break;
                    }
                    case PackageType.Dev:
                    {
                        for (int i = 0; i < info.world.Length; i++)
                        {
                            DrawWorldSelectContent(0, info.world[i].id, info);
                        }

                        break;
                    }
                }
            }
            EditorGUILayout.EndScrollView();

            // ログアウトボタンの表示
            EditorGUILayout.BeginHorizontal();
            {
                GUIStyle b2 = new GUIStyle(GUI.skin.button);
                b2.fontSize = 12;
                GUIContent content = new GUIContent( /* ログアウト */LocalizedMessage.Get("Vket_ControlPanel.LogOutButton"));
                if (GUILayout.Button(content, GUILayout.Width(position.width - 10.0f), GUILayout.Height(30.0f)))
                {
                    Logout();
                }
            }
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(5);
        }

        /// <summary>
        /// ワールド選択Contentの描画
        /// </summary>
        /// <param name="circleIndex"></param>
        /// <param name="worldId"></param>
        /// <param name="info"></param>
        private void DrawWorldSelectContent(int circleIndex, int worldId, LoginInfo info, bool isQuest = false, bool isItem = false)
        {
            LoadWorldThumbnail(info);
            var noImage = AssetUtility.GetIcon(null);
            GUIStyle itemStyle = new GUIStyle(GUI.skin.box);
            itemStyle.fixedHeight = 100.0f;
            EditorGUILayout.BeginVertical(itemStyle);
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.BeginVertical();
                    {
                        GUIContent contents = new GUIContent();
                        GUIStyle style = new GUIStyle(GUIStyle.none);
                        if (info.worldIcon != null)
                        {
                            contents.image = GetWorldThumbnail(info, worldId);
                        }
                        
                        if(!contents.image)
                        {
                            contents.image = noImage;
                            style.fixedHeight = 95.0f;
                            style.fixedWidth = 95.0f;
                        }
                        else
                        {
                            style.fixedHeight = 256.0f;
                            style.fixedWidth = 144.0f;
                        }

                        if (GUILayout.Button(contents, style))
                        {
                        }
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginVertical();
                    {
                        if (false)//Utilities.Hiding.HidingUtil.DebugModeは重い処理なのでここでは使用しない
                        {
                            var style = new GUIStyle(GUI.skin.label);
                            if (isItem && isQuest)
                            {
                                style.normal.textColor = Color.red;
                                /* " (Questアイテム入稿)" */
                                EditorGUILayout.LabelField(info.GetWorldName(worldId) + LocalizedMessage.Get("Vket_ControlPanel.DrawWorldSelectContent.QuestItem"), style);
                            }
                            else if (isItem)
                            {
                                style.normal.textColor = Color.cyan;
                                /* " (PCアイテム入稿)" */
                                EditorGUILayout.LabelField(info.GetWorldName(worldId) + LocalizedMessage.Get("Vket_ControlPanel.DrawWorldSelectContent.PCItem"), style);
                            }
                            else if (isQuest)
                            {
                                style.normal.textColor = Color.blue;
                                /* " (Quest入稿)" */
                                EditorGUILayout.LabelField(info.GetWorldName(worldId) + LocalizedMessage.Get("Vket_ControlPanel.DrawWorldSelectContent.Quest"), style);
                            }
                            else
                            {
                                /* " (PC入稿)" */
                                EditorGUILayout.LabelField(info.GetWorldName(worldId) + LocalizedMessage.Get("Vket_ControlPanel.DrawWorldSelectContent.PC"));
                            }
                        }
                        else
                        {
                            if (isItem && isQuest)
                            {
                                /* " (Questアイテム入稿)" */
                                EditorGUILayout.LabelField(info.GetWorldName(worldId) + LocalizedMessage.Get("Vket_ControlPanel.DrawWorldSelectContent.QuestItem"));
                            }
                            else if (isItem)
                            {
                                /* " (PCアイテム入稿)" */
                                EditorGUILayout.LabelField(info.GetWorldName(worldId) + LocalizedMessage.Get("Vket_ControlPanel.DrawWorldSelectContent.PCItem"));
                            }
                            else if (isQuest)
                            {
                                /* " (Quest入稿)" */
                                EditorGUILayout.LabelField(info.GetWorldName(worldId) + LocalizedMessage.Get("Vket_ControlPanel.DrawWorldSelectContent.Quest"));
                            }
                            else
                            {
                                /* " (PC入稿)" */
                                EditorGUILayout.LabelField(info.GetWorldName(worldId) + LocalizedMessage.Get("Vket_ControlPanel.DrawWorldSelectContent.PC"));
                            }
                        }

                        GUIContent contents = new GUIContent();
                        contents.text = LocalizedMessage.Get("Vket_ControlPanel.DrawWorldSelectContent.SelectWorldButton") /* "このワールドに入稿する" */;

                        if (GUILayout.Button(contents, GUILayout.Width(position.width * 0.5f)))
                        {
                            IsSelected = true;
                            info.selectedCircleNum = circleIndex;
                            info.selectedWorldId = worldId;
                            EditorUtility.SetDirty(info);
                            AssetDatabase.SaveAssets();
                            _worldDefinition =
                                AssetUtility.AssetLoad<VketWorldDefinition>(
                                    GetWorldDefinitionPath(info));
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// ワールドアイコンをロードする
        /// </summary>
        /// <param name="info"></param>
        /// <param name="forceUpdate"></param>
        private static void LoadWorldThumbnail(LoginInfo info, bool forceUpdate = false)
        {
            if(!Directory.Exists(WorldThumbnailFolderPath))
                return;
            
            if (_worldThumbnails == null || _worldThumbnails.Length != info.world.Length || forceUpdate)
            {
                ReleaseWorldThumbnail();
                _worldThumbnails = new Texture2D[info.world.Length];
                for(int i=0; i < info.world.Length; i++)
                {
                    var texture = new Texture2D(0, 0);
                    texture.LoadImage(File.ReadAllBytes(Path.Combine(WorldThumbnailFolderPath, info.world[i].id.ToString())));
                    _worldThumbnails[i] = texture;
                }
            }
        }

        private static void ReleaseWorldThumbnail()
        {
            if (_worldThumbnails != null)
            {
                for (int i = 0; i < _worldThumbnails.Length; i++)
                {
                    if (_worldThumbnails[i])
                    {
                        DestroyImmediate(_worldThumbnails[i]);
                        _worldThumbnails[i] = null;
                    }
                }

                _worldThumbnails = null;
            }
        }
        
        /// <summary>
        /// キャッシュしたワールドアイコンを取得する
        /// LoadWorldIcon後に使用する想定
        /// </summary>
        /// <param name="info"></param>
        /// <param name="worldId"></param>
        /// <returns>ワールドアイコン</returns>
        private Texture2D GetWorldThumbnail(LoginInfo info, int worldId)
        {
            if (_worldThumbnails == null)
                return null;
            
            for(int i=0; i < info.world.Length; i++)
            {
                if (info.world[i].id == worldId)
                {
                    if (i < _worldThumbnails.Length)
                    {
                        return _worldThumbnails[i];
                    }

                    return null;
                }
            }

            return null;
        }

        #region コントロールパネルウィンドウ
        
        // ControlPanelで使用するスタイル
        private GUIStyle _controlPanelLabelStyle1;
        private GUIStyle _controlPanelLabelStyle2;
        private GUIStyle _controlPanelButtonStyle1;
        private GUIStyle _controlPanelButtonStyle2;
        private GUIStyle _controlPanelMainFiledStyle;
        
        // ツールボタンの大きさ
        private const float ControlPanelToolButtonHeight = 35f;
        // フッターボタンの大きさ
        private const float ControlPanelFooterButtonHeight = 30f;
        // 最小ウィンドウサイズ
        private const float ControlPanelWindowMinHeight = 600f;
        // 最小サムネイルサイズ
        private const float ControlPanelThumbnailMinSize = 50f;
        
        
        // 入稿シーン作成ボタンなどの最低横幅サイズ
        private const float ToolButtonsMinWidth = 160;
        
        /// <summary>
        /// 標準の空白サイズ
        /// </summary>
        private const float ControlPanelBaseSpace = 10f;

        /// <summary>
        /// コントロールパネル用スタイルの初期化
        /// </summary>
        private void InitControlPanelWindowStyles()
        {
            if(_controlPanelLabelStyle1 != null)
                return;
            
            _controlPanelLabelStyle1 = new GUIStyle(GUI.skin.label);
            _controlPanelLabelStyle2 = new GUIStyle(GUI.skin.label);
            _controlPanelButtonStyle1 = new GUIStyle(GUI.skin.button);
            _controlPanelButtonStyle2 = new GUIStyle(GUI.skin.button);
            
            _controlPanelLabelStyle1.alignment = TextAnchor.MiddleCenter;
            _controlPanelLabelStyle1.fontSize = 15;
            _controlPanelLabelStyle1.fixedHeight = 25;
            _controlPanelLabelStyle1.padding = new RectOffset(0, 0, 0, 0);
            _controlPanelLabelStyle2.alignment = TextAnchor.MiddleCenter;
            _controlPanelLabelStyle2.fontSize = 11;
            _controlPanelLabelStyle2.fixedHeight = 16;
            _controlPanelLabelStyle2.padding = new RectOffset(0, 0, 0, 0);

            _controlPanelButtonStyle1.fontSize = 12;
            _controlPanelButtonStyle2.fontSize = 12;
            
            _controlPanelMainFiledStyle = new GUIStyle(GUI.skin.box){ 
                padding = new RectOffset(),
                margin = new RectOffset()
            };
        }

        /// <summary>
        /// コントロールパネルの描画
        /// </summary>
        private void ControlPanelWindow()
        {
            InitControlPanelWindowStyles();
            
            LoginInfo info = AssetUtility.LoginInfoData;
            bool isItem = info.IsItem();

            if (_worldDefinition)
            {
                _worldDefinition.IsItem = isItem;
            }
            
            var headerHeight = GetHeaderHeight();
            var itemHeight = GetItemSettingHeight(info);
            var footerHeight = GetFooterHeight(info);

            // ウィンドウ最小サイズの更新
            float windowHeight = headerHeight + footerHeight + itemHeight + GetLoginInformationHeight() + GetToolButtonsHeight();
            bool isScrollVertical = windowHeight > ControlPanelWindowMinHeight;
            minSize = new Vector2(230f, isScrollVertical ? ControlPanelWindowMinHeight : windowHeight);
            
            // ヘッダーの描画
            DrawHeader(info);
            
            if (isScrollVertical)
            {
                _controlPanelWindowScrollPos = EditorGUILayout.BeginScrollView(_controlPanelWindowScrollPos, GUIStyle.none, GUI.skin.verticalScrollbar);
            }

            float maxItemHeight = isItem ? 382f : 0f;
            
            // アイコンや情報表示部分のRect
            var mainFiledRect = new Rect(0f, headerHeight, position.width,
                position.height - headerHeight - footerHeight - maxItemHeight);

            // 幅 or 高さ-(はみ出し防止分の値->サムネイルサイズは横幅計算するため、ちょうどいい値を探す必要あり) の 短いほうを取得
            float windowMinSize = Mathf.Min(mainFiledRect.width, mainFiledRect.height - GetLoginInformationHeight() + 160f + maxItemHeight);
            float thumbnailLeftFieldSize = ToolButtonsMinWidth + ControlPanelBaseSpace * 2;
            
            // Windowサイズの幅の短いほうを基準にサムネイルの大きさを計算
            float thumbnailSize = Mathf.Max(windowMinSize - thumbnailLeftFieldSize, ControlPanelThumbnailMinSize);

            // 入稿シーン作成ボタンなどの幅
            // サムネと横並びになると仮定して計算
            // 窓幅 - (空白 + サムネイル + 空白)
            float toolButtonWidth = position.width - thumbnailSize - ControlPanelBaseSpace * 2;

            // 縦長レイアウトになる場合true
            // メインフィールド全体の高さからGetToolButtonsHeight分の空白ができる場合にtrue
            float portraitLayoutThumbnailSize = mainFiledRect.width - ControlPanelBaseSpace * 2;
            bool portraitLayoutFlag = mainFiledRect.height > portraitLayoutThumbnailSize + ControlPanelBaseSpace * 2 +
                GetLoginInformationHeight() + GetToolButtonsHeight()  + ControlPanelBaseSpace;

            // 縦長の場合サムネイルの横幅を最大に
            if (portraitLayoutFlag)
            {
                thumbnailSize = portraitLayoutThumbnailSize;
            }
            
            // メイン部分の描画
            using (new EditorGUILayout.VerticalScope(_controlPanelMainFiledStyle,
                       GUILayout.Width(mainFiledRect.width), GUILayout.Height(mainFiledRect.height)))
            {
                // サークルアイコンの描画
                float circleIconPosY = isScrollVertical ? ControlPanelBaseSpace : headerHeight + ControlPanelBaseSpace;
                EditorGUI.DrawPreviewTexture(
                    new Rect(ControlPanelBaseSpace, circleIconPosY, thumbnailSize,
                        thumbnailSize),
                    info.circleIcon != null ? info.circleIcon : AssetUtility.GetIcon(null));

                // 縦配置
                if (portraitLayoutFlag)
                {
                    // サムネイル分の空白を追加
                    GUILayout.Space(thumbnailSize + ControlPanelBaseSpace * 2);
                }
                // 横配置
                else
                {
                    GUILayout.Space(ControlPanelBaseSpace);

                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(thumbnailSize + ControlPanelBaseSpace * 2);

                        // サムネの横にボタンを表示
                        DrawToolButtons(toolButtonWidth);
                    }
                    EditorGUILayout.EndHorizontal();

                    // ボタンの高さがサムネイルのサイズより小さい場合はその分だけスペースを追加
                    float addSpace = thumbnailSize + ControlPanelBaseSpace - GetToolButtonsHeight();
                    if (0 < addSpace)
                    {
                        GUILayout.Space(addSpace);
                    }
                }

                DrawLoginInformation(info);

                if (portraitLayoutFlag)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(ControlPanelBaseSpace / 2f);
                    DrawToolButtons(position.width - ControlPanelBaseSpace);
                    GUILayout.Space(ControlPanelBaseSpace / 2f);
                    EditorGUILayout.EndHorizontal();

                    // 一番下に空白
                    GUILayout.Space(ControlPanelBaseSpace);
                }
            }

            if (isItem)
            {
                DrawItemSetting(info);
            }
            
            if (isScrollVertical)
            {
                EditorGUILayout.EndScrollView();
            }

            // フッターは下側に配置
            GUILayout.FlexibleSpace();

            // フッターの描画
            DrawFooter(info);
        }
        
        /// <summary>
        /// Headerの高さを取得
        /// </summary>
        /// <returns>Headerの高さ</returns>
        private float GetHeaderHeight()
        {
            // ラベルは実際の高さより2fスペースが入る分大きい
            return 5f + (_controlPanelLabelStyle1.fixedHeight + 2f);
        }
        
        /// <summary>
        /// Headerの表示
        /// サークル名とマイページボタンの描画
        /// </summary>
        private void DrawHeader(LoginInfo info)
        {
            GUILayout.Space(5);
            
            EditorGUILayout.BeginHorizontal();
            {
                GUIContent content1 = new GUIContent($"{ /* サークル名 */LocalizedMessage.Get("Vket_ControlPanel.CircleName")}:");
                GUIContent content2;
                switch (_packageType)
                {
                    case PackageType.Stable:
                    {
                        var circles = info.GetCirclesOrCompanyCircles();
                        content2 = new GUIContent(circles[info.selectedCircleNum].name_ja, circles[info.selectedCircleNum].name_ja);
                        break;
                    }
                    case PackageType.Company:
                    case PackageType.Community:
                    case PackageType.Dev:
                    default:
                        content2 = new GUIContent("", "");
                        break;
                }

                var content3 = new GUIContent(LocalizedMessage.Get("Vket_ControlPanel.DrawHeader.MyPage") /*"マイページ"*/);
                
                // サークル名が表示可能な横幅を計算
                const float space = 20;
                var width = position.width - space - _controlPanelLabelStyle1.CalcSize(content1).x -
                            _controlPanelButtonStyle1.CalcSize(content3).x;
                var style = UIUtility.GetContentSizeFitStyle(content2, _controlPanelLabelStyle1, width);
                
                // サークル名ラベル描画
                EditorGUILayout.LabelField(content1, _controlPanelLabelStyle1,
                    GUILayout.Width(_controlPanelLabelStyle1.CalcSize(content1).x),
                    GUILayout.Height(_controlPanelLabelStyle1.CalcSize(content1).y));
                
                // サークル名の描画
                EditorGUILayout.LabelField(content2, style, GUILayout.Width(style.CalcSize(content2).x),
                    GUILayout.Height(style.CalcSize(content2).y));
                GUILayout.FlexibleSpace();
                
                // マイページボタンの描画
                if (GUILayout.Button(content3, _controlPanelButtonStyle1,
                        GUILayout.Width(_controlPanelButtonStyle1.CalcSize(content3).x),
                        GUILayout.Height(_controlPanelButtonStyle1.CalcSize(content3).y)))
                {
                    switch (_packageType)
                    {
                        case PackageType.Stable:
                            Application.OpenURL(CircleMypageUrl);
                            break;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// フッターの高さを取得
        /// </summary>
        /// <param name="info">LoginInfo</param>
        /// <returns>フッターの高さ</returns>
        private static float GetFooterHeight(LoginInfo info)
        {
            int footerLineCount = 3;
            
            if (!info)
            {
                return (ControlPanelFooterButtonHeight + 2f) * footerLineCount + ControlPanelBaseSpace;
            }
            
            if (info.IsQuest(info.selectedWorldId))
            {
                footerLineCount++;
            }
            
            // ボタンは実際の高さより2fスペースが入る分大きい
            return (ControlPanelFooterButtonHeight + 2f) * footerLineCount + ControlPanelBaseSpace;
        }

        /// <summary>
        /// Footerの表示
        /// </summary>
        /// <param name="info">LoginInfo</param>
        private void DrawFooter(LoginInfo info)
        {
            GUILayout.Space(ControlPanelBaseSpace/2f);

            bool isItem = info.IsItem();

            EditorGUILayout.BeginVertical();
            {
                GUIContent content;
                
                // ボタンを横配置
                EditorGUILayout.BeginHorizontal();
                {
                    int horizontalButtonCount = 3;
                    float buttonWidth = position.width / horizontalButtonCount - 4f;

                    switch (_packageType)
                    {
                        case PackageType.Stable:
                        case PackageType.Dev:
                        {
                            content = new GUIContent(LocalizedMessage.Get("Vket_ControlPanel.DrawFooter.BackWorldSelectButton") /* "ワールド選択に戻る"*/);
                            if (GUILayout.Button(content, UIUtility.GetContentSizeFitStyle(content, _controlPanelButtonStyle2, buttonWidth), GUILayout.Width(buttonWidth), GUILayout.Height(ControlPanelFooterButtonHeight)))
                            {
                                IsSelected = false;
                                info.selectedCircleNum = 9999;
                                info.selectedWorldId = 9999;
                            }
                            break;
                        }
                        default:
                        {
                            horizontalButtonCount = 2;
                            buttonWidth = position.width / horizontalButtonCount - 4f;
                            break;
                        }
                    }

                    // ログアウトボタンの表示
                    content = new GUIContent( /* ログアウト */LocalizedMessage.Get("Vket_ControlPanel.LogOutButton"));
                    if (GUILayout.Button(content,
                            UIUtility.GetContentSizeFitStyle(content, _controlPanelButtonStyle2, buttonWidth),
                            GUILayout.Width(buttonWidth), GUILayout.Height(ControlPanelFooterButtonHeight)))
                    {
                        Logout();
                    }
                    
                    // パッケージボタンの表示
                    content = new GUIContent("Packages");
                    if (GUILayout.Button(content,
                            UIUtility.GetContentSizeFitStyle(content, _controlPanelButtonStyle2, buttonWidth),
                            GUILayout.Width(buttonWidth), GUILayout.Height(ControlPanelFooterButtonHeight)))
                    {
                        PopupWindow.Show(new Rect(Event.current.mousePosition, Vector2.one),
                            new Vket_PackagePopup());
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (!isItem)
                {
                    content = new GUIContent(LocalizedMessage.Get("Vket_ControlPanel.VketPrefabsButton"));
                    if (GUILayout.Button(content,
                            UIUtility.GetContentSizeFitStyle(content, _controlPanelButtonStyle2, position.width - 6f),
                            GUILayout.Width(position.width - 6f), GUILayout.Height(ControlPanelFooterButtonHeight)))
                    {
                        VketPrefabsMainWindow.OpenMainWindow(GetExhibitorID());
                    }
                }

                // Questワールドの場合ASTCに変換するボタンを表示
                if (info.IsQuest(info.selectedWorldId))
                {
                    /* "[Questワールド用]入稿フォルダのテクスチャをASTC 6×6 に変換" */
                    content = new GUIContent(LocalizedMessage.Get("Vket_ControlPanel.ChangeASTCButton"));
                    if (GUILayout.Button(content,
                            UIUtility.GetContentSizeFitStyle(content, _controlPanelButtonStyle2, position.width - 6f),
                            GUILayout.Width(position.width - 6f), GUILayout.Height(ControlPanelFooterButtonHeight)))
                    {
                        QuestTextureConverter.ConvertAstc($"Assets/{GetExhibitorID()}");

                        EditorUtility.DisplayDialog(
                            LocalizedMessage.Get("Vket_ControlPanel.ChangeASTCButtonCompleateDialogTitle"),
                            LocalizedMessage.Get("Vket_ControlPanel.ChangeASTCButtonCompleateDialogMessage"),
                            LocalizedMessage.Get("Vket_ControlPanel.ChangeASTCButtonCompleateDialogOKButton"));
                    }
                }

                switch (_packageType)
                {
                    case PackageType.Company:
                    case PackageType.Community:
                    {
                        EditorGUI.BeginDisabledGroup(CircleNullOrEmptyCheck());
                        {
                            if (GUILayout.Button( /* 入稿ファイルの書き出し */LocalizedMessage.Get("Vket_ControlPanel.DrawFooter.ExportButton"),
                                    _controlPanelButtonStyle2,
                                    GUILayout.Height(ControlPanelFooterButtonHeight)))
                            {
                                BoothExportButton_Click($"Assets/{GetExhibitorID()}", $"Vket_{_packageType.ToString()}_BoothData_{GetExhibitorID()}");
                            }
                        }
                        EditorGUI.EndDisabledGroup();
                        break;
                    }
                    // 入稿ボタンの表示
                    default:
                    {
                        EditorGUI.BeginDisabledGroup(CircleNullOrEmptyCheck());
                        {
                            if (GUILayout.Button( /* 入稿 */LocalizedMessage.Get("Vket_ControlPanel.SubmissionButton"), _controlPanelButtonStyle2,
                                    GUILayout.Height(ControlPanelFooterButtonHeight)))
                            {
                                DraftButton_Click();
                            }
                        }
                        EditorGUI.EndDisabledGroup();
                        break;
                    }
                }
            }
            EditorGUILayout.EndVertical();
            
            GUILayout.Space(ControlPanelBaseSpace/2f);
        }

        private static void BoothExportButton_Click(string baseFolderAssetPath, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = "BoothData";

            if (!Directory.Exists(baseFolderAssetPath))
            {
                EditorUtility.DisplayDialog(LocalizedMessage.Get("Vket_ControlPanel.BoothExportButton_Click.FailedExport.Title"),
                    LocalizedMessage.Get("Vket_ControlPanel.BoothExportButton_Click.FailedExport.Message"), "OK");
            }
            
            var files = new List<string>();
            files.Add(baseFolderAssetPath);
            string[] filePathArray = Directory.GetFileSystemEntries(baseFolderAssetPath, "*", SearchOption.AllDirectories);
            foreach (var filePath in filePathArray)
            {
                var path = filePath.Replace('\\', '/');
                if (path.Contains(".meta")) continue;
                files.Add(path);
            }
            
            AssetDatabase.ExportPackage(files.ToArray(), $"{fileName}.unitypackage", ExportPackageOptions.Interactive);
        }

        /// <summary>
        /// 入稿ボタン押下時に呼ばれる
        /// </summary>
        private static void DraftButton_Click()
        {
            VketAccountKeepLogin();
            LoginInfo info = AssetUtility.LoginInfoData;
            Login();

            bool ignore = false;

            foreach (NetworkUtility.Role role in info.data.user.roles)
            {
                ignore = (role.role == "admin" || role.role == "tester") ? true : ignore;
            }

            if (EditorPlayCheck() || !UpdateCheck())
            {
                return;
            }

            if (info.data.company?[info.selectedCircleNum] != null)
            {
                // 企業なら ignore フラグを無視するのでここでは何もしない
            }
            else if (ignore)
            {
            }
            else
            {
                switch (_packageType)
                {
                    default:
                    {
                        if (!Utilities.Hiding.HidingUtil.DebugMode && !NetworkUtility.TermCheck(AssetUtility.VersionInfoData.event_version, AssetUtility.VersionInfoData.package_type))
                        {
                            EditorUtility.DisplayDialog("Error", /* 入稿期間外です。 */LocalizedMessage.Get("Vket_ControlPanel.DraftButton_Click.OutsideSubmissionPeriod"), "OK");
                            return;
                        }
                        break;
                    }
                }
            }

            var isCompany = info.data.company != null && info.data.company.Length > 0;
            if (info.data.user.id != info.data.circle?[info.selectedCircleNum].owner_id && !isCompany)
            {
                EditorUtility.DisplayDialog("Error", /* 入稿は代表者のみが可能です。{n}入稿を中断します。 */LocalizedMessage.Get("Vket_ControlPanel.DraftButton_Click.CancelSubmission"),
                    "OK");
                return;
            }

            DraftFirstTask().Forget();
        }
        
        /// <summary>
        /// ツールボタン群の高さを取得
        /// </summary>
        /// <returns>ツールボタン群の高さ</returns>
        private static float GetToolButtonsHeight()
        {
            // ボタンの高さ * ボタンの数
            return (ControlPanelToolButtonHeight+2f) * 6f;
        }

        /// <summary>
        /// ツールボタン群の表示
        /// </summary>
        /// <param name="buttonWidth">ボタンの横幅</param>
        private void DrawToolButtons(float buttonWidth)
        {
            EditorGUILayout.BeginVertical();
            {
                GUIContent content = new GUIContent( /* 入稿用シーン作成 */LocalizedMessage.Get("Vket_ControlPanel.CreateSubmissionSceneButton"));
                if (GUILayout.Button(content, UIUtility.GetContentSizeFitStyle(content, _controlPanelButtonStyle1, buttonWidth),
                        GUILayout.Height(ControlPanelToolButtonHeight)))
                {
                    LoadTemplateButton_Click();
                }
                
                content = new GUIContent( /* 入稿用シーンを開く */LocalizedMessage.Get("Vket_ControlPanel.DrawToolButtons.OpenSubmissionSceneButton"));
                if (GUILayout.Button(content, UIUtility.GetContentSizeFitStyle(content, _controlPanelButtonStyle1, buttonWidth),
                        GUILayout.Height(ControlPanelToolButtonHeight)))
                {
                    OpenSubmissionSceneButton_Click();
                }

                content = new GUIContent( /* ブースチェック */LocalizedMessage.Get("Vket_ControlPanel.BoothCheckButton"));
                if (GUILayout.Button(content, UIUtility.GetContentSizeFitStyle(content, _controlPanelButtonStyle1, buttonWidth),
                        GUILayout.Height(ControlPanelToolButtonHeight)))
                {
                    BoothCheckButton_Click();
                }

                content = new GUIContent( /* 容量チェック */LocalizedMessage.Get("Vket_ControlPanel.BuildSizeCheckButton"));
                if (GUILayout.Button(content, UIUtility.GetContentSizeFitStyle(content, _controlPanelButtonStyle1, buttonWidth),
                        GUILayout.Height(ControlPanelToolButtonHeight)))
                {
                    BuildSizeCheckButton_Click();
                }

                content = new GUIContent( /* SetPassチェック */LocalizedMessage.Get("Vket_ControlPanel.SetPassCheckButton"));
                if (GUILayout.Button(content, UIUtility.GetContentSizeFitStyle(content, _controlPanelButtonStyle1, buttonWidth),
                        GUILayout.Height(ControlPanelToolButtonHeight)))
                {
                    SetPassCheckButton_Click();
                }
                
                content = new GUIContent( /* VRChat 動作確認 */LocalizedMessage.Get("Vket_ControlPanel.VRCCheckButton"));
                if (GUILayout.Button(content, UIUtility.GetContentSizeFitStyle(content, _controlPanelButtonStyle1, buttonWidth),
                        GUILayout.Height(ControlPanelToolButtonHeight)))
                {
                    VRCCheckButton_Click();
                }
            }
            EditorGUILayout.EndVertical();
        }

        private static void LoadTemplateButton_Click()
        {
            LoginInfo info = AssetUtility.LoginInfoData;
            Login();

            if (EditorPlayCheck() || !UpdateCheck())
            {
                return;
            }

            Type type = typeof(VitDeck.TemplateLoader.GUI.TemplateLoaderWindow);
            ReflectionUtility.InvokeMethod(type, "Open", null, null);
            var templateIndex = _worldDefinition.TemplateIndex;
            if (!CircleNullOrEmptyCheck())
            {
                ReflectionUtility.SetField(type, "popupIndex", null, templateIndex);
                ReflectionUtility.SetField(type, "templateProperty", null,
                    Assembly.Load("VitDeck.TemplateLoader").GetType("VitDeck.TemplateLoader.TemplateLoader")
                        .GetMethod("GetTemplateProperty", BindingFlags.Static | BindingFlags.Public).Invoke(null,
                            new object[]
                            {
                                ((string[])ReflectionUtility.GetField(type, "templateFolders", null))[templateIndex]
                            }));
                switch (_packageType)
                {
                    case PackageType.Stable:
                    case PackageType.Company:
                    case PackageType.Community:
                    case PackageType.Dev:
                    {
                        ReflectionUtility.SetField(type, "replaceStringList", null,
                            new Dictionary<string, string> { { "CIRCLEID", GetExhibitorID() } });
                        break;
                    }
                    default:
                    {
                        ReflectionUtility.SetField(type, "replaceStringList", null,
                            new Dictionary<string, string> { { "CIRCLEID", "1" } });
                        break;
                    }
                }
            }
        }

        private static string GetExhibitorID()
        {
            string exhibitorID = "";
            LoginInfo info = AssetUtility.LoginInfoData;
            if (!CircleNullOrEmptyCheck())
            {
                switch (_packageType)
                {
                    case PackageType.Company:
                    case PackageType.Community:
                        exhibitorID = _companyCircleId.ToString();
                        break;
                    case PackageType.Dev:
                        exhibitorID = info.selectedWorldId.ToString();
                        break;
                    default:
                        var circles = info.GetCirclesOrCompanyCircles();
                        exhibitorID = circles[info.selectedCircleNum].id.ToString();
                        break;
                }
            }
            return exhibitorID;
        }

        /// <summary>
        /// 入稿シーンを開くボタンの処理
        /// </summary>
        private static void OpenSubmissionSceneButton_Click()
        {
            if (!CircleNullOrEmptyCheck())
            {
                string exhibitorID = GetExhibitorID();
                if (!string.IsNullOrEmpty(exhibitorID))
                {
                    var scenePath = string.Format("Assets/{0}/{0}.unity", exhibitorID);

                    if (File.Exists(scenePath))
                    {
                        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                        {
                            EditorSceneManager.OpenScene(scenePath);
                        }
                    }
                    else
                    {
                        /* "シーンが開けませんでした。" */ /* "入稿用シーンが見つかりません。入稿用シーンを作成してください。" */
                        EditorUtility.DisplayDialog(LocalizedMessage.Get("Vket_ControlPanel.OpenSubmissionSceneButton_Click.ErrorDialog.Title"), LocalizedMessage.Get("Vket_ControlPanel.OpenSubmissionSceneButton_Click.ErrorDialog.Message"), "OK");
                    }
                }
                else
                {
                    /* "シーンが開けませんでした。" */ /* "入稿用シーンが見つかりません。入稿用シーンを作成してください。" */
                    EditorUtility.DisplayDialog(LocalizedMessage.Get("Vket_ControlPanel.OpenSubmissionSceneButton_Click.ErrorDialog.Title"), LocalizedMessage.Get("Vket_ControlPanel.OpenSubmissionSceneButton_Click.ErrorDialog.Message"), "OK");
                }
            }
        }

        private static async void BoothCheckButton_Click()
        {
            LoginInfo info = AssetUtility.LoginInfoData;
            Login();

            bool isItem = info.IsItem();
            if (isItem)
            {
                // ItemType更新
                UpdateItemType(info);
            }

            if (EditorPlayCheck() || !UpdateCheck())
            {
                return;
            }

            try
            {
                _cts = new CancellationTokenSource();
                _currentUniTask = BoothCheckTask(_cts.Token);
                await _currentUniTask;
                Debug.Log("Booth check completed");
            }
            catch (Exception e)
            {
                Debug.Log($"Booth check has been canceled.:{e}");
                EditorUtility.DisplayDialog(/* "キャンセル" */ LocalizedMessage.Get("Vket_ControlPanel.BoothCheckButton_Click.Cancel.Title"), /* "ブースチェックをキャンセルしました。" */ LocalizedMessage.Get("Vket_ControlPanel.BoothCheckButton_Click.Cancel.Message"), "OK");
            }
            finally
            {
                _cts.Dispose();
                _cts = null;
                _currentUniTask = default;
            }
        }

        private static async void BuildSizeCheckButton_Click()
        {
            Login();

            if (EditorPlayCheck() || !UpdateCheck())
            {
                return;
            }
            
            try
            {
                _cts = new CancellationTokenSource();
                _currentUniTask = BuildSizeCheckTask(_cts.Token);
                await _currentUniTask;
                Debug.Log("Capacity check completed");
            }
            catch (Exception e)
            {
                Debug.Log($"Capacity check canceled.:{e}");
                EditorUtility.DisplayDialog(/* "キャンセル" */ LocalizedMessage.Get("Vket_ControlPanel.BuildSizeCheckButton_Click.Cancel.Title"), /* "容量チェックをキャンセルしました。" */ LocalizedMessage.Get("Vket_ControlPanel.BuildSizeCheckButton_Click.Cancel.Message"), "OK");
            }
            finally
            {
                _cts.Dispose();
                _cts = null;
                _currentUniTask = default;
            }
        }

        private static async void SetPassCheckButton_Click()
        {
            Login();

            if (EditorPlayCheck() || !UpdateCheck())
            {
                return;
            }

            try
            {
                Vket_BlinderToolbar.IsBlind = true;
                _cts = new CancellationTokenSource();
                _currentUniTask = SetPassCheckButtonPreprocessingTask(_cts.Token);
                await _currentUniTask;
                Debug.Log("SetPass check preprocessing completed");
            }
            catch (Exception e)
            {
                Vket_BlinderToolbar.IsBlind = false;
                Debug.Log($"Canceled SetPass check.:{e}");
                EditorUtility.DisplayDialog(/* "キャンセル" */ LocalizedMessage.Get("Vket_ControlPanel.SetPassCheckButton_Click.Cancel.Title"), /* "SetPassチェックをキャンセルしました。" */ LocalizedMessage.Get("Vket_ControlPanel.SetPassCheckButton_Click.Cancel.Message"), "OK");
            }
            finally
            {
                _cts.Dispose();
                _cts = null;
                _currentUniTask = default;
            }
        }
        
        private static async void VRCCheckButton_Click()
        {
            Login();
            
            LoginInfo info = AssetUtility.LoginInfoData;
            bool isItem = info.IsItem();
            if (isItem)
            {
                // ItemType更新
                UpdateItemType(info);
            }

            if (EditorPlayCheck() || !UpdateCheck())
            {
                return;
            }

            try
            {
                // ベイク
                _cts = new CancellationTokenSource();
                _currentUniTask = BakeCheckAndRun(false, _cts.Token);
                await _currentUniTask;
                // ビルド&Test
                _currentUniTask = BuildAndTestWorld(false);
                await _currentUniTask;
                Debug.Log("VRC check completed");
            }
            catch (Exception e)
            {
                Debug.Log($"Booth check has been canceled.:{e}");
                EditorUtility.DisplayDialog(/* "キャンセル" */ LocalizedMessage.Get("Vket_ControlPanel.VRCCheckButton_Click.Cancel.Title"), /* "VRCチェックをキャンセルしました。" */ LocalizedMessage.Get("Vket_ControlPanel.VRCCheckButton_Click.Cancel.Message"), "OK");
            }
            finally
            {
                _cts.Dispose();
                _cts = null;
                _currentUniTask = default;
            }
        }

        /// <summary>
        /// 入稿情報の表示
        /// </summary>
        /// <returns></returns>
        private float GetLoginInformationHeight()
        {
            float retVal = 0f;
            
            switch (_packageType)
            {
                case PackageType.Company:
                case PackageType.Community:
                    break;
                default:
                    // 名前
                    retVal += _controlPanelLabelStyle1.fixedHeight +2f;
                    break;
            }
            
            if (CircleNullOrEmptyCheck())
            {
                return retVal;
            }

            switch (_packageType)
            {
                case PackageType.Company:
                case PackageType.Community:
                    break;
                default:
                    // 配置ワールド名
                    retVal += _controlPanelLabelStyle1.fixedHeight + 2f;
                    break;
            }

            // 入稿ルールボタン
            retVal += 22f;
            
            // イベント名
            retVal += _controlPanelLabelStyle1.fixedHeight + 2f;
            
            //入稿期限
            retVal += _controlPanelLabelStyle2.fixedHeight + 2f;

            // 空白
            retVal += 5f;
            
            // 日付, 日付
            retVal += (_controlPanelLabelStyle2.fixedHeight + 2f) * 2f;
            
            // bottomSpace
            retVal += 10f;

            return retVal;
        }
        
        private void DrawLoginInformation(LoginInfo info)
        {
            GUIContent content = null;
            
            switch (_packageType)
            {
                case PackageType.Company:
                case PackageType.Community:
                    break;
                default:
                {
                    // ユーザー名
                    var user = info.data.user;
                    string userName = "";
                    if (user != null)
                    {
                        userName = Application.systemLanguage == SystemLanguage.Japanese ? user.name_ja : user.name_en;
                    }

                    content = new GUIContent($"{ /* 名前 */LocalizedMessage.Get("Vket_ControlPanel.UserName")}:{userName}", $"{userName}");
                    EditorGUILayout.LabelField(content, _controlPanelLabelStyle1,
                        GUILayout.Width(_controlPanelLabelStyle1.CalcSize(content).x > position.width - 10
                            ? position.width - 10
                            : _controlPanelLabelStyle1.CalcSize(content).x),
                        GUILayout.Height(_controlPanelLabelStyle1.fixedHeight));
                }
                    break;
            }

            // サークル情報
            if (!CircleNullOrEmptyCheck())
            {
                switch (_packageType)
                {
                    case PackageType.Stable:
                    case PackageType.Dev:
                        content = new GUIContent(
                            $"{ /* 配置ワールド */LocalizedMessage.Get("Vket_ControlPanel.PlacementWorld")} : {info.GetWorldName()}",
                            info.GetWorldName());
                        break;
                    default:
                        content = null;
                        break;
                }

                if (content != null)
                {
                    // 配置ワールド名の表示
                    EditorGUILayout.LabelField(content,
                        UIUtility.GetContentSizeFitStyle(content, _controlPanelLabelStyle1, position.width - 10),
                        GUILayout.Width(_controlPanelLabelStyle1.CalcSize(content).x > position.width - 10
                            ? position.width - 10
                            : _controlPanelLabelStyle1.CalcSize(content).x),
                        GUILayout.Height(_controlPanelLabelStyle1.fixedHeight));
                }

                EditorGUILayout.BeginHorizontal();
                {
                    var content1 = new GUIContent(/* 入稿方法 */LocalizedMessage.Get("Vket_ControlPanel.SubmissionMethod"));
                    var content2 = new GUIContent( /* 入稿ルール */LocalizedMessage.Get("Vket_ControlPanel.SubmissionRule"));

                    // 右寄せ
                    GUILayout.FlexibleSpace();
                    
                    switch (_packageType)
                    {
                        case PackageType.Company:
                        case PackageType.Community:
                            break;
                        default:
                        {
                            if (GUILayout.Button(content1, _controlPanelButtonStyle1,
                                    GUILayout.Width(_controlPanelButtonStyle1.CalcSize(content2).x),
                                    GUILayout.Height(20f)))
                            {
                                var mouseRect = new Rect(Event.current.mousePosition, Vector2.one);
                                mouseRect.x = 0;

                                // Popupを開く
                                PopupWindow.Show(mouseRect,
                                    new MessagePopup(LocalizedMessage.Get("SubmissionMethodPopup.Message")));
                            }
                        } 
                            break;
                    }

                    if (GUILayout.Button(content2, _controlPanelButtonStyle1, GUILayout.Width(_controlPanelButtonStyle1.CalcSize(content2).x),
                            GUILayout.Height(20f)))
                    {
                        Application.OpenURL(GetRuleURL());
                    }
                }
                EditorGUILayout.EndHorizontal();
                
                // イベント名
                content = new GUIContent(AssetUtility.VersionInfoData.event_name);
                EditorGUILayout.LabelField(content, _controlPanelLabelStyle1, GUILayout.Width(_controlPanelLabelStyle1.CalcSize(content).x),
                    GUILayout.Height(_controlPanelLabelStyle1.fixedHeight));
                
                // 入稿期限
                content = new GUIContent($"{ /* 入稿期限(JST) */LocalizedMessage.Get("Vket_ControlPanel.DrawLoginInformation.SubmissionDeadline")}:");
                EditorGUILayout.LabelField(content, _controlPanelLabelStyle2, GUILayout.Width(_controlPanelLabelStyle2.CalcSize(content).x),
                    GUILayout.Height(_controlPanelLabelStyle2.fixedHeight));

                GUILayout.Space(5);
                
                var d1s = "";
                var d1e = "";
                if (info.submissionTerm != null)
                {
                    d1s = AssetUtility.TimestampToDateString(info.submissionTerm.start);
                    d1e = AssetUtility.TimestampToDateString(info.submissionTerm.end);
                }
                
                var tooltip = $"{d1s} - {d1e}";
                content = new GUIContent($"{d1s}-,", tooltip);
                EditorGUILayout.LabelField(content, _controlPanelLabelStyle2, GUILayout.Width(_controlPanelLabelStyle2.CalcSize(content).x),
                    GUILayout.Height(_controlPanelLabelStyle2.fixedHeight));
                content = new GUIContent($"{d1e}  ", tooltip);
                EditorGUILayout.LabelField(content, _controlPanelLabelStyle2, GUILayout.Width(_controlPanelLabelStyle2.CalcSize(content).x),
                    GUILayout.Height(_controlPanelLabelStyle2.fixedHeight));
                
                GUILayout.Space(10);
            }
        }
        
        private VketItemInfo _itemInfo;

        private static float GetItemSettingHeight(LoginInfo info)
        {
            float labelHeight = EditorGUIUtility.singleLineHeight;
            if (info.IsItem())
            {
                // ラベル, 空白, アイテム設定フィールド
                return labelHeight + 5f + 50f
                       + GetHelpBoxHeight(/* "シーンの「サークルIDオブジェクト」以下に自動で配置されます。" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.DragDrop.Help"))
                       + 5f
                       + labelHeight
                       + GetHelpBoxHeight(/* "Default:見た目のみを表示するアイテムです。\nPickup:アイテムが掴めるようになります。\nAvatar:アバターペデスタル(Blue Print ID)を設定可能です。" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.SelectItemType.Help"))
                       + labelHeight
                       + 5f
                       + labelHeight * 3
                       + 5f
                       + labelHeight * 3
                       + GetHelpBoxHeight(/* "キャプションボードの商品名と販売価格を設定可能です。" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.BordSetting.Help"))
                       + 4f;
            }

            return 0f;
        }
        
        public static float GetHelpBoxHeight(string message, MessageType type = MessageType.Info)
        {
            var style   = new GUIStyle( "HelpBox" );
            var content = new GUIContent( message );
            return Mathf.Max( style.CalcHeight( content, Screen.width - (type != MessageType.None ? 53 : 21)), 40);
        }

        private void DrawItemSetting(LoginInfo info)
        {
            if (!_itemInfo)
            {
                // ここを通るときにはログインしている
                _itemInfo = LoadItemInfo(info);
            }

            if (_itemInfo)
            {
                // アイテム設定ラベル描画
                var content = new GUIContent(/* "アイテム設定" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.Title"));
                EditorGUILayout.LabelField(content, _controlPanelLabelStyle1,
                    GUILayout.Width(_controlPanelLabelStyle1.CalcSize(content).x),
                    GUILayout.Height(EditorGUIUtility.singleLineHeight));
                
                EditorGUILayout.Space(5f);

                var asset = DragAndDropAreaUtility.GetObject<GameObject>(/* "入稿するオブジェクトをここにドラッグ&ドロップで追加してください。" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.DragDrop.FiledMessage"), Color.cyan, Color.black);
                if (asset != null)
                {
                    // Debug.Log($"{asset.name}を取得");
                    // 配置
                    // シーン上に存在しない場合は複製
                    if (asset.scene.name == null)
                    {
                        var copy = Instantiate(asset);
                        copy.name = asset.name;
                        asset = copy;
                        Undo.RegisterCreatedObjectUndo(asset, "Create Prefab");
                    }

                    // Rootの子として設定
                    asset.transform.parent = GetSceneNameObject();
                    asset.transform.localPosition = Vector3.zero;
                    asset.transform.localRotation = Quaternion.identity;
                }

                EditorGUILayout.HelpBox(/* "シーンの「サークルIDオブジェクト」以下に自動で配置されます。" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.DragDrop.Help"), MessageType.Info);
                
                EditorGUILayout.Space(5f);

                EditorGUI.BeginChangeCheck();
                var typeIndex = EditorGUILayout.Popup(/* "アイテムの種類を選択" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.SelectItemType.Title"), (int)_itemInfo._itemType, new[]
                {
                    "Default", "Pickup", "Avatar"
                });
                if (EditorGUI.EndChangeCheck())
                {
                    _itemInfo._itemType = (VketItemInfo.ItemType)typeIndex;
                    EditorUtility.SetDirty(_itemInfo);
                    AssetDatabase.SaveAssets();

                    UpdateItemType(info);
                }
                
                EditorGUILayout.HelpBox(/* "Default:見た目のみを表示するアイテムです。\nPickup:アイテムが掴めるようになります。\nAvatar:アバターペデスタル(Blue Print ID)を設定可能です。" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.SelectItemType.Help"), MessageType.Info);

                EditorGUI.BeginDisabledGroup(typeIndex != 2);
                EditorGUI.BeginChangeCheck();
                _itemInfo._bluePrintID = EditorGUILayout.TextField("Avatar Blue Print ID", _itemInfo._bluePrintID);
                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(_itemInfo);
                    AssetDatabase.SaveAssets();
                }
                EditorGUI.EndDisabledGroup();
                
                EditorGUILayout.Space(5f);
                
                EditorGUILayout.LabelField(LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.SelectPedestal"));
                string[] enum_name = { /* "基本台座" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.PedestalName1"), /* "小物用台座" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.PedestalName2")};
                GUIStyle style_radio = new GUIStyle(EditorStyles.radioButton);
                
                EditorGUI.BeginChangeCheck();
                var select = GUILayout.SelectionGrid(_itemInfo._selectTemplateIndex, enum_name, 1, style_radio);
                if (EditorGUI.EndChangeCheck())
                {
                    _itemInfo._selectTemplateIndex = select;
                    var basic = GameObject.Find("ReferenceObjects/Pedestals/Basic");
                    var smallItems = GameObject.Find("ReferenceObjects/Pedestals/SmallItems");
                    if (basic && smallItems)
                    {
                        basic.SetActive(select == 0);
                        smallItems.SetActive(select == 1);
                    }
                }
                
                EditorGUILayout.Space(5f);
                EditorGUILayout.LabelField(/* "キャプションボードの設定" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.BordSetting.Title"));
                
                EditorGUI.BeginChangeCheck();
                _itemInfo._itemName = EditorGUILayout.TextField(/* "商品名" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.BordSetting.ItemName"), _itemInfo._itemName);
                
                _itemInfo._itemPrice = EditorGUILayout.TextField(/* "販売価格" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.BordSetting.ItemPrice"), _itemInfo._itemPrice);

                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(_itemInfo);
                    AssetDatabase.SaveAssets();
                }
                
                EditorGUILayout.HelpBox(/* "キャプションボードの商品名と販売価格を設定可能です。" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.BordSetting.Help"), MessageType.Info);
                
                // 線
                GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
            }
            else
            {
                EditorGUILayout.HelpBox(/* "アイテムの入稿用シーンが見つかりません。\n入稿用シーンを作成してください。" */ LocalizedMessage.Get("Vket_ControlPanel.DrawItemSetting.NotFoundScene.Help"), MessageType.Error);
            }
        }

        /// <summary>
        /// シーンからシーンと同名のオブジェクトを取得する
        /// </summary>
        /// <returns>シーンと同名のオブジェクト</returns>
        private static Transform GetSceneNameObject()
        {
            Transform root = null;
            foreach (var obj in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (obj.name == SceneManager.GetActiveScene().name)
                {
                    root = obj.transform;
                    break;
                }
            }

            return root;
        }
        
        /// <summary>
        /// 入稿フォルダからItemInfoを読み込む
        /// ログインしている状態で使用すること前提
        /// </summary>
        /// <param name="info"></param>
        /// <returns>入稿フォルダのItemInfo</returns>
        private static VketItemInfo LoadItemInfo(LoginInfo info)
        {
            if (!CircleNullOrEmptyCheck())
            {
                var userSettings = UserSettingUtility.GetUserSettings();
                userSettings.validatorFolderPath = "Assets/" + GetExhibitorID();

                // シーンフォルダから読み込み
                if (userSettings != null)
                {
                    return AssetDatabase.LoadAssetAtPath<VketItemInfo>(
                        $"{userSettings.validatorFolderPath}/Config/VketItemInfo.asset");
                }
            }

            return null;
        }

        /// <summary>
        /// ItemTypeの更新時に呼ぶ想定の処理
        /// </summary>
        /// <param name="info">ログイン情報</param>
        /// <param name="isUploadTiming">入稿タイミングの場合true</param>
        private static void UpdateItemType(LoginInfo info, bool isUploadTiming = false)
        {
            Transform root = GetSceneNameObject();
            var itemInfo = LoadItemInfo(info);
            if (root && itemInfo)
            {
                // 初期化
                RemovePreviewPickup(root);
                RemovePreviewAvatarPedestal(root);
                
                // アップロード処理の場合は追加しない
                if(isUploadTiming)
                    return;
                
                switch (itemInfo._itemType)
                {
                    case VketItemInfo.ItemType.None:
                        break;
                    case VketItemInfo.ItemType.Pickup:
                        AddPreviewPickup(root);
                        break;
                    case VketItemInfo.ItemType.AvatarPedestal:
                        AddPreviewAvatarPedestal(root, itemInfo._bluePrintID);
                        break;
                }
            }
        }
        
        private static void AddPreviewPickup(Transform root)
        {
            AdjustCollider(root);
            root.gameObject.AddComponent<VRCPickup>();
            var rb = root.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        
        private static void RemovePreviewPickup(Transform root)
        {
            if (!root)
            {
                Debug.Log($"Root Object not found.");
                return;
            }
            
            var pickups = root.gameObject.GetComponents<VRCPickup>();
            foreach (var t in pickups)
            {
                DestroyImmediate(t);
            }

            var colliders = root.GetComponentsInChildren<Collider>();
            foreach (var collider in colliders)
            {
                DestroyImmediate(collider);
            }

            var rbs = root.GetComponents<Rigidbody>();
            foreach (var rb in rbs)
            {
                DestroyImmediate(rb);
            }
        }

        private const string AvatarPedestal3DPrefabGuid = "9fffe84a94533884eaf481963546643d";

        /// <summary>
        /// AvatarPedestalの追加
        /// rootがnullの場合はReferenceObjects内に生成する
        /// </summary>
        /// <param name="root"></param>
        /// <param name="bluePrintId"></param>
        private static void AddPreviewAvatarPedestal(Transform root, string bluePrintId)
        {
            var avatarPedestal3DPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(AvatarPedestal3DPrefabGuid));
            var pedestal3D = PrefabUtility.InstantiatePrefab(avatarPedestal3DPrefab) as GameObject;
            if (pedestal3D)
            {
                // ReferenceObjects内に生成
                var referenceObjects = GameObject.Find("ReferenceObjects");
                pedestal3D.transform.SetParent(referenceObjects.transform);

                pedestal3D.transform.position = new Vector3(0f, 1f, 0f);
                var prefabInfo = pedestal3D.GetComponent<VketPrefabInformation>();
                if(prefabInfo)
                    DestroyImmediate(prefabInfo);

                var vketAvatarPedestal = pedestal3D.GetComponent<VketAvatarPedestal>();
                if (vketAvatarPedestal)
                {
                    VketAvatarPedestalEditor.AdjustCapsuleCollider(vketAvatarPedestal, root);
                    var avatarPedestalSerializedObject = new SerializedObject(vketAvatarPedestal);
                    var avatarPedestalProperty = avatarPedestalSerializedObject.FindProperty("avatarPedestal");
                    avatarPedestalSerializedObject.Update();
                    var avatarPedestal = avatarPedestalProperty.objectReferenceValue as VRCAvatarPedestal;
                    if (avatarPedestal)
                    {
                        avatarPedestal.blueprintId = bluePrintId;
                        EditorUtility.SetDirty(avatarPedestal);
                        avatarPedestalSerializedObject.ApplyModifiedProperties();
                    }
                }
            }
        }
        
        private static void RemovePreviewAvatarPedestal(Transform root)
        {
            if (!root)
            {
                Debug.Log($"Root Object not found.");
                return;
            }

            // 入稿IDオブジェクト内のAvatarPedestal3D削除
            foreach (var childTransform in root.GetComponentsInChildren<Transform>())
            {
                if (PrefabUtility.GetPrefabInstanceStatus(childTransform.gameObject) != PrefabInstanceStatus.Connected)
                {
                    continue;
                }
                var prefabObject = PrefabUtility.GetCorrespondingObjectFromOriginalSource(childTransform);
                var path = AssetDatabase.GetAssetPath(prefabObject);
                var guid = AssetDatabase.AssetPathToGUID(path);
                if (guid == AvatarPedestal3DPrefabGuid)
                {
                    DestroyImmediate(childTransform.gameObject);
                    return;
                }
            }
            
            // ReferenceObjects内のAvatarPedestal3D削除
            var referenceObjects = GameObject.Find("ReferenceObjects");
            foreach (var childTransform in referenceObjects.GetComponentsInChildren<Transform>())
            {
                if (PrefabUtility.GetPrefabInstanceStatus(childTransform.gameObject) != PrefabInstanceStatus.Connected)
                {
                    continue;
                }
                var prefabObject = PrefabUtility.GetCorrespondingObjectFromOriginalSource(childTransform);
                var path = AssetDatabase.GetAssetPath(prefabObject);
                var guid = AssetDatabase.AssetPathToGUID(path);
                if (guid == AvatarPedestal3DPrefabGuid)
                {
                    DestroyImmediate(childTransform.gameObject);
                    return;
                }
            }
        }

        private static void AdjustCollider(Transform root)
        {
            if (!root)
            {
                Debug.Log($"Root Object not found.");
                return;
            }
            
            var renderers = root.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
            {
                Debug.Log("Renderer not found.");
                return;
            }

            BoxCollider collider;
            collider = root.GetComponent<BoxCollider>();
            if (!collider)
            {
                collider = root.gameObject.AddComponent<BoxCollider>();
            }
            
            Bounds totalBounds = CalculateBounds(renderers);

            Vector3 pos = root.transform.position;
            Vector3 scale = root.transform.lossyScale;

            Vector3 localCenter = new Vector3(
                (totalBounds.center.x - pos.x) / scale.x,
                (totalBounds.center.y - pos.y) / scale.y,
                (totalBounds.center.z - pos.z) / scale.z
            );
            Vector3 localSize = new Vector3(
                totalBounds.size.x / scale.x,
                totalBounds.size.y / scale.y,
                totalBounds.size.z / scale.z
            );
            
            collider.center = localCenter;
            //collider.height = localSize.y;
            //collider.radius = localSize.x <= localSize.y ? localSize.x * 0.5f : localSize.y * 0.5f;
            collider.size = localSize;

            Debug.Log("Setup Completed.");
        }

        private static Bounds CalculateBounds(Renderer[] renderers)
        {
            Bounds bounds = new Bounds();

            foreach (var renderer in renderers)
            {
                Vector3 min = renderer.bounds.center - (renderer.bounds.size * 0.5f);
                Vector3 max = renderer.bounds.center + (renderer.bounds.size * 0.5f);

                if (bounds.size == Vector3.zero)
                    bounds = new Bounds(renderer.bounds.center, Vector3.zero);

                bounds.Encapsulate(min);
                bounds.Encapsulate(max);
            }

            return bounds;
        }
        
        #endregion // コントロールパネルウィンドウ
        
        #region 入稿シークエンスウィンドウ

        private void DraftSequenceWindow()
        {
            EditorGUILayout.LabelField(/* "入稿処理中です。" */ LocalizedMessage.Get("Vket_ControlPanel.DraftSequenceWindow.Title"), EditorStyles.wordWrappedLabel);
            EditorGUILayout.HelpBox(/* "入稿処理中はこれらのウィンドウを閉じないでください。\n[VRChat SDK], [VketTools], [Game]" */ LocalizedMessage.Get("Vket_ControlPanel.DraftSequenceWindow.Help"), MessageType.Warning);
            
            // 実行中のタスク
            EditorGUILayout.LabelField(VketSequenceDrawer.GetSequenceText(_currentSeq), EditorStyles.wordWrappedLabel);
            
            switch (_currentSeq)
            {
                case DraftSequence.VRChatCheck:
                    if (VRCSdkControlPanel.window.PanelState == SdkPanelState.Building)
                    {
                        EditorGUILayout.LabelField(_isBuildWorld ? 
                            /* "テストの準備中です。しばらくお待ちください。" */ LocalizedMessage.Get("Vket_ControlPanel.DraftSequenceWindow.Build.Message") : 
                            /* "確認ダイアログ表示中です。" */ LocalizedMessage.Get("Vket_ControlPanel.DraftSequenceWindow.Build.Confirm"), EditorStyles.wordWrappedLabel);
                    }
                    break;
            }
            
            VketSequenceDrawer.Draw(position);
            
            // 入稿キャンセルボタン
            if (_cts != null)
            {
                var buttonStyle = new GUIStyle(GUI.skin.button);
                buttonStyle.fontSize = 20;
                if (GUILayout.Button(/* "入稿キャンセル" */ LocalizedMessage.Get("Vket_ControlPanel.DraftSequenceWindow.CancelButton"), buttonStyle, GUILayout.Height(30f)))
                {
                    _cts.Cancel();
                    _cts.Dispose();
                    _cts = null;
                }
            }
        }
        
        #endregion // 入稿シークエンスウィンドウ
        
        #region タスク処理
        
        /// <summary>
        /// ブースチェック
        /// </summary>
        /// <param name="cancellationToken"></param>
        private static async UniTask BoothCheckTask(CancellationToken cancellationToken)
        {
            // ベイク
            await BakeCheckAndRun(false, cancellationToken);
            
            var userSettings = UserSettingUtility.GetUserSettings();
            userSettings.validatorRuleSetType = GetValidatorRule();
            if (!CircleNullOrEmptyCheck())
            {
                userSettings.validatorFolderPath = "Assets/" + GetExhibitorID();
            }

            Type validatorWindowType = typeof(VitDeck.Validator.GUI.ValidatorWindow);
            ReflectionUtility.InvokeMethod(validatorWindowType, "Open", null, null);
            object window = ReflectionUtility.GetField(validatorWindowType, "window", null);

            UserSettingUtility.SaveUserSettings(userSettings);

            if (string.IsNullOrEmpty(userSettings.validatorFolderPath) ||
                (!string.IsNullOrEmpty(userSettings.validatorFolderPath) &&
                 !Directory.Exists(userSettings.validatorFolderPath)))
            {
                Debug.Log(userSettings.validatorFolderPath);
                if (!EditorUtility.DisplayDialog("Warning", /* 入稿フォルダが見つかりませんでした。\n現在開いているシーンをチェックしますか？ */
                        LocalizedMessage.Get("Vket_ControlPanel.BoothCheckFunc.NotFoundFolder"), /* はい */LocalizedMessage.Get("Yes"), /* いいえ */
                        LocalizedMessage.Get("No")))
                {
                    throw new OperationCanceledException("Cancel Booth Check");
                }

                string baseFolderPath = SceneManager.GetActiveScene().path;
                Debug.Log(baseFolderPath);
                if (string.IsNullOrEmpty(baseFolderPath))
                {
                    EditorUtility.DisplayDialog("Error", /* シーンが見つかりませんでした。 */LocalizedMessage.Get("Vket_ControlPanel.BoothCheckFunc.NotFoundScene"), "OK");
                    throw new OperationCanceledException("Not Found Scene");
                }

                ReflectionUtility.SetField(validatorWindowType, "baseFolder", window,
                    AssetDatabase.LoadAssetAtPath<DefaultAsset>(Path.GetDirectoryName(baseFolderPath)));
            }

            ReflectionUtility.InvokeMethod(validatorWindowType, "OnValidate", window, null);
        }

        /// <summary>
        /// ビルドサイズ計算前の処理
        /// 入稿オブジェクト以外を削除する
        /// </summary>
        private static void BuildSizeCheckPreprocessingFunc()
        {
            LoginInfo info = AssetUtility.LoginInfoData;
            Scene scene = SceneManager.GetActiveScene();
            string rootObjectName;

            switch (_packageType)
            {
                case PackageType.Company:
                case PackageType.Community:
                case PackageType.Dev:
                    rootObjectName = scene.name;
                    break;
                default:
                    var circles = info.GetCirclesOrCompanyCircles();
                    rootObjectName = CircleNullOrEmptyCheck()
                        ? scene.name
                        : circles[info.selectedCircleNum].id.ToString();
                    break;
            }

            // 入稿オブジェクト以外を削除
            foreach (var obj in Array.FindAll(Resources.FindObjectsOfTypeAll<GameObject>(),
                         item => item.transform.parent == null))
            {
                if (obj.name != rootObjectName && AssetDatabase.GetAssetOrScenePath(obj).Contains(".unity"))
                {
                    DestroyImmediate(obj);
                }
            }
        }
        
        /// <summary>
        /// 容量確認
        /// </summary>
        /// <param name="cancellationToken"></param>
        private static async UniTask BuildSizeCheckTask(CancellationToken cancellationToken)
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.isDirty)
            {
                if (!EditorUtility.DisplayDialog("Warning", /* シーンが変更されています。\n保存して実行しますか？ */
                        LocalizedMessage.Get("Vket_ControlPanel.BuildSizeCheckFunc.SceneSaveConfirmationDialog"), /* はい */LocalizedMessage.Get("Yes"), /* いいえ */
                        LocalizedMessage.Get("No")))
                {
                    throw new OperationCanceledException("Select Not Save Scene");
                }
            }

            EditorSceneManager.SaveScene(scene);

            // シーンのベイク
            await BakeCheckAndRun(false, cancellationToken);

            BuildSizeCheckPreprocessingFunc();

            // ビルドサイズ確認
            float cmpSize;

            try
            {
                cmpSize = await CalculationCompileSize();
            }
            finally
            {
                // シーンを再度開き元の状態に戻す。
                EditorSceneManager.OpenScene(SceneManager.GetActiveScene().path);
            }

            if (BuildSizeCheck(cmpSize))
            {
                Debug.LogError(
                    $"[<color=green>VketTools</color>] AssetBundle Size <color=#ff0000ff><size=30>{cmpSize}</size></color>MB is over Regulation: {GetBuildMaxSize()}MB");
                if (EditorUtility.DisplayDialog("Error",
                        $"{ /* 容量がオーバーしています。 */LocalizedMessage.Get("Vket_ControlPanel.BuildSizeCheckFunc.OverSize")}\r\n{cmpSize}MB\r\nRegulation {GetBuildMaxSize()}MB", /*入稿ルール*/
                        LocalizedMessage.Get("Vket_ControlPanel.SubmissionRule"), "OK"))
                {
                    Application.OpenURL(GetRuleURL());
                }
                return;
            }
            
            if (cmpSize == -1)
            {
                EditorUtility.DisplayDialog("Error", /* ビルドに失敗しました。 */LocalizedMessage.Get("Vket_ControlPanel.BuildSizeCheckFunc.Failed"), "OK");
            }
            else
            {
                if (EditorUtility.DisplayDialog("Build",
                        $"{ /* ビルド完了！ */LocalizedMessage.Get("Vket_ControlPanel.BuildSizeCheckFunc.Compleate")}\r\nCompressed Size: {cmpSize}MB\r\nRegulation {GetBuildMaxSize()}MB", /*入稿ルール*/
                        LocalizedMessage.Get("Vket_ControlPanel.SubmissionRule"), "OK"))
                {
                    Application.OpenURL(GetRuleURL());
                }
            }
        }

        /// <summary>
        /// SetPassチェック
        /// 前処理
        /// </summary>
        /// <param name="cancellationToken"></param>
        private static async UniTask SetPassCheckButtonPreprocessingTask(CancellationToken cancellationToken)
        {
            // シーンのベイク
            await BakeCheckAndRun(false, cancellationToken);
            
            OpenWindowIfClose();
            EditorUtility.DisplayProgressBar("SetPassCalls and Batches Check.", "Start up...", 0);
            AssetUtility.EditorPlayInfoData.isVketEditorPlay = true;
            AssetUtility.EditorPlayInfoData.isSetPassCheckOnly = true;
            AssetUtility.EditorPlayInfoData.clientSimEnabled = GetClientSimEnabled();
            AssetUtility.SaveAsset(AssetUtility.EditorPlayInfoData);

            // ブラインド解除(Layoutに保存させない)
            Vket_BlinderToolbar.IsBlind = false;
            
            // WindowLayoutを保存
            SaveEditorLayout();

            var assembly = typeof(EditorWindow).Assembly;
            var type = assembly.GetType("UnityEditor.GameView");
            var game = EditorWindow.GetWindow(type);
            game.Close();
            var setpass = EditorWindow.GetWindow(type, true);
            setpass.ShowUtility();
            SetClientSimEnabled(false);
            
            // Playモードに変更
            EditorApplication.isPlaying = true;
        }
        
        /// <summary>
        /// SetPassチェック
        /// 中間の処理
        /// セットパス確認ボタンが押された後、PlayModeに突入したときに呼ばれる
        /// </summary>
        private static async UniTask SetPassCheckButtonIntermediateProcessingTask()
        {
            try
            {
                Vket_BlinderToolbar.IsBlind = true;
                _cts = new CancellationTokenSource();
                _currentUniTask = EditorPlay(_cts.Token, true);
                await _currentUniTask;
                Debug.Log("SetPass check postprocessing completed");
            }
            catch (OperationCanceledException e)
            {
                // キャンセル時に呼ばれる例外
                Debug.Log($"Canceled SetPass check.:{e}");
                EditorUtility.DisplayDialog(/* "キャンセル" */ LocalizedMessage.Get("Vket_ControlPanel.SetPassCheckButtonIntermediateProcessingTask.Cancel.Title"), /* "SetPassチェックをキャンセルしました。" */ LocalizedMessage.Get("Vket_ControlPanel.SetPassCheckButtonIntermediateProcessingTask.Cancel.Message"), "OK");
            }
            finally
            {
                Vket_BlinderToolbar.IsBlind = false;
                
                // プレイモード終了
                EditorApplication.isPlaying = false;
                RestoreClientSimEnabled();
                
                _cts.Dispose();
                _cts = null;
                _currentUniTask = default;
            }
        }

        /// <summary>
        /// SetPassチェック
        /// 後処理
        /// セットパス確認ボタンが押された後、PlayModeからEditorModeに突入したときに呼ばれる
        /// </summary>
        private static void SetPassCheckButtonPostprocessingFunc()
        {
            AssetUtility.EditorPlayInfoData.isSetPassCheckOnly = false;
            AssetUtility.SaveAsset(AssetUtility.EditorPlayInfoData);
        }

        /// <summary>
        /// SDKのBuilderタブを開く
        /// </summary>
        private static async UniTask OpenSDKBuilderTabTask()
        {
            // VRCSdkControlPanelが開くまで待つ
            await UniTask.DelayFrame(2);
            
            // 開かれていない場合はスルーする
            if (_vrcSdkWindow)
            {
                // タイムアウト
                var timeoutController = new TimeoutController();
                var timeoutToken = timeoutController.Timeout(TimeSpan.FromSeconds(7));
                
                // SDKにログインするまで待つ
                try
                {
                    await UniTask.WaitUntil(() => APIUser.IsLoggedIn, cancellationToken : timeoutToken);
                }
                catch (Exception)
                {
                    // 例外を握りつぶす
                    Debug.LogError("Time out OpenSDKBuilderTab");
                    return;
                }

                OpenSDKBuilderTab();
            }
            else
            {
                Debug.LogError("Not Found VRCSdkControlPanel");
            }
        }
        
        /// <summary>
        /// SDKタブを開く
        /// コントロールパネルが開いており、SDKにログインしていること前提の処理
        /// </summary>
        private static void OpenSDKBuilderTab()
        {
            MethodInfo info = null;
            try {
                info = _vrcSdkWindow.GetType().GetMethod("RenderTabs", BindingFlags.NonPublic | BindingFlags.Instance);
            }
            catch (Exception) 
            {
                Debug.LogError("Exception:Not Found VRCSdkControlPanel.RenderTabs Method");
            }
            if (info != null) 
            {
                VRCSettings.ActiveWindowPanel = 1;
                info.Invoke(_vrcSdkWindow, null);
            }
            else
            {
                Debug.LogError("Not Found VRCSdkControlPanel.RenderTabs Method");
            }
        }
        
        #region 入稿処理開始用タスク

        /// <summary>
        /// 入稿タスク1の実行
        /// 入稿ボタンが押されたとき
        /// </summary>
        private static async UniTask DraftFirstTask()
        {
            // 入稿前タスク1の実行
            try
            {
                Vket_BlinderToolbar.IsBlind = true;
                _cts = new CancellationTokenSource();
                _currentUniTask = DraftFunc(_cts.Token);
                await _currentUniTask;
                Debug.Log("入稿タスク1終了");
            }
            catch (OperationCanceledException e)
            {  
                // キャンセル時に呼ばれる例外
                Vket_BlinderToolbar.IsBlind = false;
                Debug.Log($"入稿タスク1をキャンセルしました。{e}");
                EditorUtility.DisplayDialog(/* "キャンセル" */ LocalizedMessage.Get("Vket_ControlPanel.DraftTask.Cancel.Title"), /* "入稿をキャンセルしました。" */ LocalizedMessage.Get("Vket_ControlPanel.DraftTask.Cancel.Message"), "OK");
            }
        }
        
        /// <summary>
        /// 入稿タスク2の実行
        /// SetPassチェックとスクショ撮影のためPlayModeに入ったとき
        /// </summary>
        private static async UniTask DraftSecondTask()
        {
            try 
            {  
                Vket_BlinderToolbar.IsBlind = true;
                _cts = new CancellationTokenSource();
                _currentUniTask = EditorPlay(_cts.Token, false);
                await _currentUniTask;
                Debug.Log("入稿タスク2終了");
            }
            catch (OperationCanceledException e)
            {  
                // キャンセル時に呼ばれる例外
                Vket_BlinderToolbar.IsBlind = false;
                Debug.Log($"入稿タスク2をキャンセルしました。{e}");
                EditorUtility.DisplayDialog(/* "キャンセル" */ LocalizedMessage.Get("Vket_ControlPanel.DraftTask.Cancel.Title"), /* "入稿をキャンセルしました。" */ LocalizedMessage.Get("Vket_ControlPanel.DraftTask.Cancel.Message"), "OK");
            }
        }

        /// <summary>
        /// 入稿タスク3の実行
        /// スクショ撮影後EditorModeに入ったとき
        /// </summary>
        private static async UniTask DraftThirdTask()
        {
            try
            {
                _cts = new CancellationTokenSource();
                await UniTask.Yield(_cts.Token);
                Vket_BlinderToolbar.IsBlind = true;
                _currentUniTask = ExportAndUpload(_cts.Token);
                await _currentUniTask;
                Debug.Log("入稿タスク3終了");
            }
            catch (OperationCanceledException e)
            {  
                // キャンセル時に呼ばれる例外
                Vket_BlinderToolbar.IsBlind = false;
                Debug.Log($"入稿タスク3をキャンセルしました。{e}");
                EditorUtility.DisplayDialog(/* "キャンセル" */ LocalizedMessage.Get("Vket_ControlPanel.DraftTask.Cancel.Title"), /* "入稿をキャンセルしました。" */ LocalizedMessage.Get("Vket_ControlPanel.DraftTask.Cancel.Message"), "OK");
            }
        }
        
        /// <summary>
        /// 入稿処理完了時に初期化する処理
        /// </summary>
        private static void DraftExitFunc()
        {
            Vket_BlinderToolbar.IsBlind = false;
            _currentSeq = DraftSequence.None;
            if (_cts != null)
            {
                _cts.Dispose();
                _cts = null;
            }
            _currentUniTask = default;
            VketSequenceDrawer.ResetSequence();
            
            // Pedestalだった場合は元に戻すため更新する
            LoginInfo info = AssetUtility.LoginInfoData;
            if (info.IsItem())
            {
                UpdateItemType(info);
                // シーン保存
                Scene scene = SceneManager.GetActiveScene();
                EditorSceneManager.SaveScene(scene);
            }
        }
        
        #endregion // 入稿処理開始用タスク

        #region 入稿メインタスク

        /// <summary>
        /// 入稿ボタンが押された後に実行されるタスク
        /// </summary>
        /// <param name="cancellationToken"></param>
        static async UniTask DraftFunc(CancellationToken cancellationToken)
        {
            // セットアップ
            LoginInfo info = AssetUtility.LoginInfoData;

            CopyProxyToUdon(SceneManager.GetActiveScene());
            info.errorFlag = false;
            
            // シーケンス状況の初期化
            VketSequenceDrawer.ResetSequence();
            
            try
            {
                // ①ベイクチェック
                await DraftBakeCheckFunc(cancellationToken, info);
                // ②VRChatチェック
                await DraftVrcCheckFunc(cancellationToken, info);
                // ③ルールチェック
                await DraftRuleCheckFunc(cancellationToken, info);
                // ④ビルドサイズチェック
                await DraftBuildSizeCheckFunc(cancellationToken, info);
                // ⑤セットパス確認
                await DraftSetPassCheckPreprocessingTask(cancellationToken);
            }
            catch (Exception e)
            {
                DraftExitFunc();
                throw;
            }
            
            void CopyProxyToUdon(Scene scene)
            {
                var udonSharpComponents = scene.GetRootGameObjects()
                    .SelectMany(root => root.GetComponentsInChildren<UdonSharpBehaviour>(true));
                foreach (var udonSharpComponent in udonSharpComponents)
                {
                    UdonSharpEditorUtility.CopyProxyToUdon(udonSharpComponent);
                    EditorUtility.SetDirty(UdonSharpEditorUtility.GetBackingUdonBehaviour(udonSharpComponent));
                }
            }
        }
        
        /// <summary>
        /// プレイモード突入時に実行するタスク
        /// セットパス確認とスクリーンショット撮影
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="isSetPassOnly">入稿処理中の場合はfalse, 「セットパス確認」ボタンからの場合はtrue</param>
        private static async UniTask EditorPlay(CancellationToken cancellationToken, bool isSetPassOnly)
        {
            try
            {
                LoginInfo info = AssetUtility.LoginInfoData;
                // ⑤セットパスチェック前半
                await SetPassCheckPreprocessingTask(cancellationToken, info);
                if (!isSetPassOnly)
                {
                    // ⑥スクリーンショット撮影開始
                    await DraftScreenShotPreprocessingTask(cancellationToken);
                }
                // ⑤セットパスチェック後半
                await SetPassCheckPostprocessingTask(cancellationToken, info);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                FailedEditorPlayTask();
                throw;
            }
            
            void FailedEditorPlayTask()
            {
                Debug.Log("EditorPlayClose");
                
                AssetUtility.EditorPlayInfoData.isVketEditorPlay = false;
                AssetUtility.EditorPlayInfoData.isSetPassCheckOnly = false;
                AssetUtility.EditorPlayInfoData.setPassFailedFlag = true;
                AssetUtility.SaveAsset(AssetUtility.EditorPlayInfoData);
                
                // Playモード中止
                EditorApplication.isPlaying = false;
                
                // 入稿処理中、Editorモードで失敗した場合の処理
                if(!isSetPassOnly)
                {
                    Debug.Log(VketSequenceDrawer.GetResultLog());
                    DraftExitFunc();
                }
            }
        }

        /// <summary>
        /// 入稿後処理
        /// 入稿処理中かつスクリーンショットの撮影に成功している場合に呼ばれる
        /// </summary>
        /// <param name="cancellationToken"></param>
        private static async UniTask ExportAndUpload(CancellationToken cancellationToken)
        {
            // ウィンドウが開いていない場合は開きなおす。
            OpenWindowIfClose();

            LoginInfo info = AssetUtility.LoginInfoData;
            if (CircleNullOrEmptyCheck())
            {
                ExitExportAndUpload();
                throw new OperationCanceledException("Failed Circle Null Or Empty Check");
            }

            EditorPlayInfo editorPlayInfo = AssetUtility.EditorPlayInfoData;
            try
            {
                // ⑤セットパス確認後処理、⑥スクリーンショット撮影後処理
                await DraftSetPassCheckPostprocessingTask(cancellationToken, editorPlayInfo);
                // ⑦アップロード開始処理
                var result = await DraftUploadPreprocessingTask(cancellationToken, info, editorPlayInfo);
                
                if (editorPlayInfo.buildSizeSuccessFlag && editorPlayInfo.setPassSuccessFlag &&
                    editorPlayInfo.ssSuccessFlag)
                {
                    // ⑦アップロード処理
                    var task = await DraftUploadTask(info, result);
                    if (task.IsCompleted)
                    {
                        // ⑧入稿が正常に完了したか問い合わせる
                        await DraftUploadPostprocessingTask(cancellationToken, info, result, editorPlayInfo);
                    }
                    else
                    {
                        Messenger.Messenger.ErrorMessage("Upload Failed.\r\n" + task.Status, "#00-0005");
                        throw new OperationCanceledException(task.Status.ToString());
                    }
                    
                    await UniTask.Delay(TimeSpan.FromSeconds(1f), DelayType.Realtime, cancellationToken: cancellationToken);
                }
                else
                {
                    // ビルドサイズチェックエラー、セットパスチェックエラー、スクリーンショットエラーが出る場合は通ることになるが無いはず
                    Debug.LogWarning("ビルドサイズチェック、セットパスチェック、スクリーンショットのいずれかに失敗しています。");
                }
            }
            finally
            {
                ExitExportAndUpload();
            }
            
            void ExitExportAndUpload()
            {
                RestoreClientSimEnabled();
                
                AssetUtility.EditorPlayInfoData.buildSizeSuccessFlag = false;
                AssetUtility.EditorPlayInfoData.setPassSuccessFlag = false;
                AssetUtility.EditorPlayInfoData.ssSuccessFlag = false;
                AssetUtility.SaveAsset(AssetUtility.EditorPlayInfoData);
                
                DraftExitFunc();
            }
        }
        
        #endregion
        
        #region 入稿逐次処理

        /// <summary>
        /// 入稿処理①
        /// ベイクチェック
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="info"></param>
        static async UniTask DraftBakeCheckFunc(CancellationToken cancellationToken, LoginInfo info)
        {
            _currentSeq = DraftSequence.BakeCheck;
            VketSequenceDrawer.SetState(_currentSeq, Status.Running);
            
            // 1秒待つ
            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime, cancellationToken: cancellationToken);
            // ベイク実行
            await BakeCheckAndRun(true, cancellationToken);
            
            VketSequenceDrawer.SetState(_currentSeq, Status.Complete);
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
        }

        /// <summary>
        /// 入稿処理②
        /// VRChat確認
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="info"></param>
        static async UniTask DraftVrcCheckFunc(CancellationToken cancellationToken, LoginInfo info)
        {
            _currentSeq = DraftSequence.VRChatCheck;
            VketSequenceDrawer.SetState(_currentSeq, Status.Running);
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);

            if (info.vrcLaunchFlag)
            {
                // VRChat.exe実行
                if (!await BuildAndTestWorld())
                {
                    throw new OperationCanceledException("LocalTest Failed or Cancel");
                }
            }

            VketSequenceDrawer.SetState(_currentSeq, Status.Complete);
            await UniTask.Yield(cancellationToken);
        }

        /// <summary>
        /// 入稿処理③
        /// ルールチェック
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="info"></param>
        static async UniTask DraftRuleCheckFunc(CancellationToken cancellationToken, LoginInfo info)
        {
            _currentSeq = DraftSequence.RuleCheck;
            VketSequenceDrawer.SetState(_currentSeq, Status.Running);

            // InfoData設定
            AssetUtility.EditorPlayInfoData.buildSizeSuccessFlag = false;
            AssetUtility.EditorPlayInfoData.setPassSuccessFlag = false;
            AssetUtility.EditorPlayInfoData.ssSuccessFlag = false;
            AssetUtility.SaveAsset(AssetUtility.EditorPlayInfoData);

            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime, cancellationToken: cancellationToken);

            /*// またベイク確認？
            bakeFlg = await BakeCheckAndRun(false, cancellationToken);

            if (!bakeFlg &&
                EditorUtility.DisplayDialog("Error", /* 入稿を中断しました。 #1#LocalizedMessage.Get("Vket_ControlPanel.DraftFunc.Interrupted"), "OK"))
            {
                Debug.Log(Vket_SequenceWindow.GetResultLog());
                DraftExitFunc();
                return;
            }*/

            //await UniTask.Yield( PlayerLoopTiming.Update, cancellationToken );

            UserSettings userSettings = UserSettingUtility.GetUserSettings();
            userSettings.validatorRuleSetType = GetValidatorRule();
            UserSettingUtility.SaveUserSettings(userSettings);
            DefaultAsset baseFolder = AssetDatabase.LoadAssetAtPath<DefaultAsset>(userSettings.validatorFolderPath);
            if (baseFolder == null && EditorUtility.DisplayDialog("Error", /* Please Try Booth Check Manually */
                    "Validator could not initialize validation rules.\nPlease Try Booth Check Manually.", "OK"))
            {
                throw new OperationCanceledException("Validator could not initialize validation rules.");
            }

            string baseFolderPath = AssetDatabase.GetAssetPath(baseFolder);

            // ルールチェック実行
            var results = Validator.Validate(Validator.GetRuleSet(userSettings.validatorRuleSetType), baseFolderPath);
            AssetDatabase.Refresh();

            if (results == null)
            {
                Debug.LogError( /* ルールチェックが正常に終了しませんでした。 */
                    LocalizedMessage.Get("Vket_ControlPanel.DraftFunc.RuleCheckFailed"));
                Debug.Log(VketSequenceDrawer.GetResultLog());
                throw new OperationCanceledException("The Rule Check failed to run properly.");
            }

            // ルールチェックのエラーフラグの保持
            info.errorFlag = results.Any(w => w.Issues.Any(w2 => w2.level == IssueLevel.Error));

            VketSequenceDrawer.SetState(_currentSeq, Status.Complete);
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            // ③ルールチェック終了
        }

        /// <summary>
        /// 入稿処理④
        /// ビルドサイズチェック
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="info"></param>
        static async UniTask DraftBuildSizeCheckFunc(CancellationToken cancellationToken, LoginInfo info)
        {
            _currentSeq = DraftSequence.BuildSizeCheck;
            VketSequenceDrawer.SetState(_currentSeq, Status.Running);
            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime, cancellationToken: cancellationToken);
            
            // アイテムの場合はPickup用コライダーの更新
            if (info.IsItem())
            {
                // Pickup更新
                UpdateItemType(info, true);
            }
            
            // シーン保存
            Scene scene = SceneManager.GetActiveScene();
            EditorSceneManager.SaveScene(scene);

            BuildSizeCheckPreprocessingFunc();
            
            // ビルドサイズ確認
            float cmpSize;

            try
            {
                cmpSize = await CalculationCompileSize();
            }
            finally
            {
                // シーンを再度開き元の状態に戻す。
                EditorSceneManager.OpenScene(SceneManager.GetActiveScene().path);
            }

            if (BuildSizeCheck(cmpSize))
            {
                // ビルドサイズ容量オーバーのためエラーフラグを立てておく
                info.errorFlag = true;
                Debug.LogError($"[<color=green>VketTools</color>] AssetBundle Size <color=#ff0000ff><size=30>{cmpSize}</size></color>MB is over Regulation: {GetBuildMaxSize()}MB");
            }
            else if (cmpSize == -1)
            {
                EditorUtility.DisplayDialog("Error", /* 容量チェックに失敗しました。 */LocalizedMessage.Get("Vket_ControlPanel.DraftFunc.SizeCheckFailed"), "OK");
                Debug.Log(VketSequenceDrawer.GetResultLog());
                
                // 例外をスロー
                throw new OperationCanceledException("Build Size Check failed");
            }
            
            VketSequenceDrawer.SetState(_currentSeq, Status.Complete, $"{cmpSize}MB");
            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime, cancellationToken:cancellationToken);
        }
        
        /// <summary>
        /// 入稿処理⑤-1
        /// セットパスチェック前処理
        /// </summary>
        /// <param name="cancellationToken"></param>
        static async UniTask DraftSetPassCheckPreprocessingTask(CancellationToken cancellationToken)
        {
            _currentSeq = DraftSequence.SetPassCheck;
            VketSequenceDrawer.SetState(_currentSeq, Status.Running);
            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime, cancellationToken: cancellationToken);
            
            // ブラインド解除(Layoutに保存させない)
            Vket_BlinderToolbar.IsBlind = false;
            
            // WindowLayoutを保存
            SaveEditorLayout();

            var assembly = typeof(EditorWindow).Assembly;
            var type = assembly.GetType("UnityEditor.GameView");
            var game = EditorWindow.GetWindow(type);
            game.Close();
            var setpass = EditorWindow.GetWindow(type, true);
            setpass.ShowUtility();
            SetClientSimEnabled(false);

            AssetUtility.EditorPlayInfoData.buildSizeSuccessFlag = true;
            AssetUtility.EditorPlayInfoData.isVketEditorPlay = true;
            AssetUtility.EditorPlayInfoData.isSetPassCheckOnly = false;
            AssetUtility.EditorPlayInfoData.ssPath = null;
            AssetUtility.EditorPlayInfoData.clientSimEnabled = GetClientSimEnabled();
            AssetUtility.SaveAsset(AssetUtility.EditorPlayInfoData);

            // UnityEditor起動
            EditorApplication.isPlaying = true;
        }

        const string SetPassCheckProgressTitle = "SetPassCalls and Batches Check.";

        /// <summary>
        /// 入稿処理⑤-2 & セットパスチェック前処理
        /// </summary>
        static async UniTask SetPassCheckPreprocessingTask(CancellationToken cancellationToken, LoginInfo info)
        {
            EditorUtility.DisplayProgressBar(SetPassCheckProgressTitle, "Initializing...", 0);

            if (EditorApplication.isPaused)
            {
                EditorApplication.isPaused = false;
            }

            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1.84f), DelayType.Realtime,
                    cancellationToken: cancellationToken);

                if (EditorApplication.isPaused)
                {
                    EditorApplication.isPaused = false;
                }

                if (!EditorApplication.isPlaying)
                {
                    EditorUtility.DisplayDialog("Error", /* Editorが再生中になっていません。\r\nもう一度お試しください。 */
                        LocalizedMessage.Get("Vket_ControlPanel.EditorPlay.NotPlayEditor"), "OK");
                    throw new OperationCanceledException("Editor is not in play mode, \nPlease try again");
                }

                var circles = info.GetCirclesOrCompanyCircles();

                // サークルIDが無い場合
                if (circles == null)
                {
                    switch (_packageType)
                    {
                        case PackageType.Stable:
                        case PackageType.Dev:
                        {
                            if (EditorUtility.DisplayDialog(
                                    "Warning", /* サークルIDを取得できませんでした。\r\n正常にチェックできない可能性がありますが実行しますか？ */
                                    LocalizedMessage.Get("Vket_ControlPanel.EditorPlay.NotFoundCircleID"), /* はい */
                                    LocalizedMessage.Get("Yes"), /* いいえ */
                                    LocalizedMessage.Get("No")))
                            {
                            }
                            else
                            {
                                throw new OperationCanceledException("Failed to obtain CircleID. Cancel Check.");
                            }
                        }
                            break;
                    }
                }

                // ルートオブジェクト名
                var rootObjectName = GetRootObjectName();
                // ルートオブジェクトが存在するか
                bool isExistRootObject = false;
                // 親オブジェクトが無いシーン上のオブジェクトを走査
                foreach (var obj in Array.FindAll(Resources.FindObjectsOfTypeAll<GameObject>(),
                             (item) => item.transform.parent == null))
                {
                    // ルートオブジェクト以外は削除
                    if (obj.name != rootObjectName && AssetDatabase.GetAssetOrScenePath(obj).Contains(".unity"))
                    {
                        DestroyImmediate(obj);
                    }
                    else
                    {
                        isExistRootObject = true;
                    }
                }

                if (!isExistRootObject)
                {
                    EditorUtility.DisplayDialog("Error", /* ブースオブジェクトが見つかりませんでした。\r\nブースチェックを行ってください。 */
                        LocalizedMessage.Get("Vket_ControlPanel.EditorPlay.NotFoundBoothObject"), "OK");
                    throw new OperationCanceledException("Booth object not found.");
                }

                string GetRootObjectName()
                {
                    Scene scene = SceneManager.GetActiveScene();
                    switch (_packageType)
                    {
                        case PackageType.Stable:
                            return CircleNullOrEmptyCheck()
                                ? scene.name
                                : circles[info.selectedCircleNum].id.ToString();
                        default:
                            return scene.name;
                    }
                }
            }
            catch
            {
                EditorUtility.ClearProgressBar();
                throw;
            }
        }

        /// <summary>
        /// 入稿処理⑤-3 & セットパスチェック後処理
        /// カメラを動かしてセットパスを計算する
        /// </summary>
        static async UniTask SetPassCheckPostprocessingTask(CancellationToken cancellationToken, LoginInfo info)
        {
            if (EditorApplication.isPaused)
            {
                EditorApplication.isPaused = false;
            }

            foreach (Camera cam in Camera.allCameras.Union(SceneView.sceneViews.ToArray()
                         .Select(s => ((SceneView)s).camera)))
            {
                cam.enabled = false;
            }

            GameObject checkParentObj = new GameObject("SetPassCheck");
            checkParentObj.transform.position = new Vector3(0, 0, 0);
            GameObject cameraObj = new GameObject("CheckCamera");
            Camera checkCam = cameraObj.AddComponent<Camera>();
            cameraObj.AddComponent<AudioListener>();
            checkCam.depth = 100;
            cameraObj.transform.SetParent(checkParentObj.transform);
            var pos = new Vector3(0.0f, 2.5f * GetBuildMaxSize() / 10.0f, GetBuildMaxSize() * -1.0f);
            cameraObj.transform.position = pos;
            SceneManager.GetActiveScene().GetRootGameObjects()[0]?.transform.Find("Dynamic")?.gameObject
                .SetActive(true);

            List<int> setPassCallsList = new List<int>();
            List<int> batchesList = new List<int>();
            int setPassCalls;
            int batches;

            try
            {
                // 360度計測
                for (int rotation = 0; rotation < 360; rotation++)
                {
                    if (EditorApplication.isPaused)
                    {
                        EditorApplication.isPaused = false;
                    }

                    float progress = (float)rotation / 360;
                    if (EditorUtility.DisplayCancelableProgressBar(SetPassCheckProgressTitle,
                            (progress * 100).ToString("F2") + "%",
                            progress))
                    {
                        throw new OperationCanceledException("Cancel Set Pass Check");
                    }

                    checkParentObj.transform.rotation = Quaternion.Euler(0, rotation, 0);
                    setPassCallsList.Add(UnityStats.setPassCalls);
                    batchesList.Add(UnityStats.batches);
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
                }

                DestroyImmediate(cameraObj);
                DestroyImmediate(checkParentObj);

                setPassCalls = AssetUtility.EditorPlayInfoData.setPassCalls =
                    (int)setPassCallsList.Average() - GetSetPassCallsOffset();
                batches = AssetUtility.EditorPlayInfoData.batches = (int)batchesList.Average() - GetBatchesOffset();
                AssetUtility.SaveAsset(AssetUtility.EditorPlayInfoData);

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }

            // 終了処理
            if (AssetUtility.EditorPlayInfoData.isSetPassCheckOnly)
            {
                var maxSetPass = GetMaxSize(MaxSizeType.SetPass);
                var maxBatches = GetMaxSize(MaxSizeType.Batches);
                var overSetPass = setPassCalls > maxSetPass;
                var overBatches = batches > maxBatches;
                if (overSetPass)
                    Debug.LogError(
                        $"[<color=green>VketTools</color>] SetPass Calls <color=#ff0000ff><size=30>{setPassCalls}</size></color> exceed the limit: {maxSetPass}");
                if (overBatches)
                    Debug.LogError(
                        $"[<color=green>VketTools</color>] Batches <color=#ff0000ff><size=30>{batches}</size></color> exceed the limit: {maxBatches}");
                var message = $"SetPass Calls: {setPassCalls} / {maxSetPass}, "
                              + $"Batches: {batches} / {maxBatches}";
                if (overSetPass || overBatches)
                {
                    message += "\r\n\r\n" + (Application.systemLanguage == SystemLanguage.Japanese
                            ? "制限を超過しています"
                            : "exceed the regulation"
                        );
                }

                if (EditorUtility.DisplayDialog("Complete!", message, /*入稿ルール*/
                        LocalizedMessage.Get("Vket_ControlPanel.SubmissionRule"), "OK"))
                {
                    Application.OpenURL(GetRuleURL());
                }
            }
            else
            {
                // スクリーンショット撮影はssManager側に任せる
                var assembly = typeof(EditorWindow).Assembly;
                var type = assembly.GetType("UnityEditor.GameView");
                var game = EditorWindow.GetWindow(type, true);
                game.Show();
                GameObject SSCaptureObj = Instantiate(AssetUtility.SSCapturePrefab);
                GameObject SSCameraObj = Instantiate(AssetUtility.SSCameraPrefab);
                SSManager ssManager = SSCaptureObj.GetComponent<SSManager>();
                ssManager.capCamera = SSCameraObj.GetComponent<Camera>();

                if (info.IsItem())
                {
                    SSCameraObj.transform.position = new Vector3(0f, 1f,
                        3.0f);
                }
                else
                {
                    SSCameraObj.transform.position = new Vector3(0, 2.5f * (float)_worldDefinition.BuildMaxSize / 10.0f,
                        (float)_worldDefinition.BuildMaxSize - 2.0f);
                }
                
                var dcThumbnailUpdateFlag = string.IsNullOrEmpty(info.dcInfoData.image.url);
                ssManager.dcThumbnailChangeToggle.isOn = dcThumbnailUpdateFlag;
                ssManager.dcThumbnailChangeToggle.interactable = !dcThumbnailUpdateFlag;
                ssManager.previewPlaceHopeToggle.transform.Find("Label").GetComponent<Text>()
                    .text = /* 下見時に配置されていた場所を希望する */LocalizedMessage.Get("Vket_ControlPanel.EditorPlay.HopePreview");
                ssManager.mainSubmissionToggle.transform.Find("Label").GetComponent<Text>().text = /* この入稿を本入稿とする */
                    LocalizedMessage.Get("Vket_ControlPanel.EditorPlay.FormalSubmission");
                ssManager.screenshotMessageText
                        .text = /* ※ <size=38>※</size> こちらのスクリーンショットは、Vketからの告知などに使用する可能性があります。\n　 あらかじめご了承下さい。また、ブース全体が見えるように撮影して下さい */
                    LocalizedMessage.Get("Vket_ControlPanel.EditorPlay.ScreenshotAttention");
                ssManager.dcThumbnailChangeToggle.gameObject.SetActive(true);
                ssManager.dcThumbnailChangeToggle.transform.Find("Label").GetComponent<Text>().text = /* ブース画像を変更する */
                    LocalizedMessage.Get("Vket_ControlPanel.EditorPlay.ChangeThumbnail");

                try
                {
                    if (NetworkUtility.TermCheck(AssetUtility.VersionInfoData.event_version,
                            AssetUtility.VersionInfoData.package_type))
                    {
                        ssManager.mainSubmissionToggle.isOn = true;
                        ssManager.mainSubmissionToggle.interactable = false;
                    }
                }
                // TermCheckのNull例外は握りつぶす
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        /// <summary>
        /// 入稿処理⑤-4
        /// セットパスチェック後処理
        /// スクリーンショットのキャプチャ後に呼ばれる
        /// 入稿チェック⑥-2(スクリーンショット完了処理)も同時に進行する
        /// </summary>
        /// <param name="cancellationToken"></param>
        static async UniTask DraftSetPassCheckPostprocessingTask(CancellationToken cancellationToken, EditorPlayInfo editorPlayInfo)
        {
            var info = AssetUtility.LoginInfoData;

            int setPassCalls = editorPlayInfo.setPassCalls;
            int batches = editorPlayInfo.batches;
            int maxSetPass = GetMaxSize(MaxSizeType.SetPass);
            int maxBatches = GetMaxSize(MaxSizeType.Batches);

            var message = $"SetPass Calls {setPassCalls} / {maxSetPass}, Batches {batches} / {maxBatches}";
            if (setPassCalls > maxSetPass || batches > maxBatches)
            {
                info.errorFlag = true;
                if (setPassCalls > maxSetPass)
                    Debug.LogError(
                        $"[<color=green>VketTools</color>] SetPass Calls <color=#ff0000ff><size=30>{setPassCalls}</size></color> is over Regulation: {maxSetPass}");
                if (batches > maxBatches)
                    Debug.LogError(
                        $"[<color=green>VketTools</color>] Batches <color=#ff0000ff><size=30>{batches}</size></color> is over Regulation: {maxBatches}");
                //if (EditorUtility.DisplayDialog("Error", $"{/* SetPassCalls/Batchesが規定をオーバーしています。 */AssetUtility.GetLabelString(41)}\r\nSetPass Calls {setPassCalls}, Batches {batches}\r\nRegulation: SetPass Calls {maxSetPass}, Batches {maxBatches}", /*入稿ルール*/LocalizedMessage.Get("Vket_ControlPanel.SubmissionRule"), "OK"))
                //{
                //    Application.OpenURL(GetRuleURL());
                //}
                //Debug.LogError($"SetPass Calls {setPassCalls}, Batches {batches}\r\nRegulation: SetPass Calls {maxSetPass}, Batches {maxBatches}");
                //Debug.Log(Vket_SequenceWindow.GetResultLog());
                //Vket_SequenceWindow.Window.Close();
                //enumerators[EnumeratorType.Export] = null;
                //yield break;

                // こちらは画面領域が狭いので記号で対応
                message = "⚠ " + message;
            }

            _currentSeq = DraftSequence.SetPassCheck;
            VketSequenceDrawer.SetState(_currentSeq, Status.Complete, message);
            editorPlayInfo.setPassSuccessFlag = true;
            AssetUtility.SaveAsset(editorPlayInfo);
            
            await DraftScreenShotPostprocessingTask(cancellationToken);
        }
        
        /// <summary>
        /// 入稿処理⑥-1
        /// スクリーンショット撮影前処理
        /// シーケンスウィンドウのフラグを立てるだけ
        /// </summary>
        /// <param name="cancellationToken"></param>
        static async UniTask DraftScreenShotPreprocessingTask(CancellationToken cancellationToken)
        {
            _currentSeq = DraftSequence.ScreenShotCheck;
            VketSequenceDrawer.SetState(_currentSeq, Status.Running);
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
        }
        
        /// <summary>
        /// 入稿処理⑥-2
        /// スクリーンショット撮影後処理
        /// </summary>
        /// <param name="cancellationToken"></param>
        static async UniTask DraftScreenShotPostprocessingTask(CancellationToken cancellationToken)
        {
            _currentSeq = DraftSequence.ScreenShotCheck;
            VketSequenceDrawer.SetState(_currentSeq, Status.Complete);
            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// ⑦-1アップロード前処理開始
        /// </summary>
        /// <param name="cancellationToken"></param>
        static async UniTask<ValidatedExportResult> DraftUploadPreprocessingTask(CancellationToken cancellationToken, LoginInfo info, EditorPlayInfo editorPlayInfo)
        {
            _currentSeq = DraftSequence.UploadCheck;
            VketSequenceDrawer.SetState(_currentSeq, Status.Running);
            await UniTask.Delay(TimeSpan.FromSeconds(1.84f), DelayType.Realtime, cancellationToken: cancellationToken);

            var result = ExportBooth();

            if (result?.exportResult.exportFilePath == null || string.IsNullOrEmpty(editorPlayInfo.ssPath))
            {
                EditorUtility.DisplayDialog("Error", /* 問題が発生した為、入稿を中断しました。 */LocalizedMessage.Get("Vket_ControlPanel.ExportAndUpload.InterruptedSubmission"), "OK");
                throw new OperationCanceledException("Submission interrupted due to an error");
            }

            if (!result.IsExportSuccess)
            {
                info.errorFlag = true;
                //EditorUtility.DisplayDialog("Error", /* 問題が発生した為、入稿を中断しました。 */AssetUtility.GetLabelString(42), "OK");
                //Debug.Log(Vket_SequenceWindow.GetResultLog());
                //Vket_SequenceWindow.Window.Close();
                //enumerators[EnumeratorType.Export] = null;
                //yield break;
            }

            var isCompany = info.data.company != null && info.data.company.Length > 0;
            if (info.data.user.id != info.data.circle?[info.selectedCircleNum].owner_id && !isCompany)
            {
                EditorUtility.DisplayDialog("Error", /* 入稿は代表者のみが可能です。{n}入稿を中断します。 */LocalizedMessage.Get("Vket_ControlPanel.DraftButton_Click.CancelSubmission"),
                    "OK");
                throw new OperationCanceledException("Only the Circle Leader can submit.");
            }

            return result;
        }

        /// <summary>
        /// ⑦-2アップロード処理
        /// </summary>
        /// <param name="info"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        static async UniTask<Task<Google.Apis.Storage.v1.Data.Object>> DraftUploadTask(LoginInfo info, ValidatedExportResult result)
        {
            string path = result.exportResult.exportFilePath;
            string boothCategory = "";
            var isCompany = info.data.company != null && info.data.company.Length > 0;
            if (isCompany)
            {
                boothCategory = "Company";
            }
            else if (info.IsQuest(info.selectedWorldId))
            {
                boothCategory = "Quest";
            }
            else
            {
                boothCategory = "PC";
            }

            var circles = AssetUtility.LoginInfoData.GetCirclesOrCompanyCircles();
            var context = SynchronizationContext.Current;
            var task = Vket_GoogleCloudStorage.GcsClient.UnityUploadAsync(path,
                AssetUtility.VersionInfoData.package_type + "_" + boothCategory,
                $"{circles[info.selectedCircleNum].id}", context);
            await task;
            if (task.IsCompleted)
            {
                // アップロード終了処理
                VketSequenceDrawer.SetState(_currentSeq, Status.Complete);
            }
            return task;
        }

        /// <summary>
        /// ⑧後処理
        /// 入稿が正常に完了したか問い合わせる
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="info"></param>
        /// <param name="result"></param>
        /// <param name="editorPlayInfo"></param>
        /// <returns></returns>
        static async UniTask DraftUploadPostprocessingTask(CancellationToken cancellationToken, LoginInfo info, ValidatedExportResult result, EditorPlayInfo editorPlayInfo)
        {
            _currentSeq = DraftSequence.PostUploadCheck;
            VketSequenceDrawer.SetState(_currentSeq, Status.Running);
            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime,
                cancellationToken: cancellationToken);

            NetworkUtility.UploadResult uploadResult = new NetworkUtility.UploadResult();
            uploadResult.package = Path.GetFileNameWithoutExtension(result.exportResult.exportFilePath);
            uploadResult.setPassCalls = editorPlayInfo.setPassCalls;
            uploadResult.batches = editorPlayInfo.batches;

            NetworkUtility.VketPostUploadResponse vketResultResponse = null;
            NetworkUtility.MallPostUploadResponse mallResultResponse = null;

            var boothOuterType = (int?)BoothOuterRule2023SummerPlaza.GetBoothOuterType() ?? 0;
            string ssPath = editorPlayInfo.ssPath;
            switch (_packageType)
            {
                case PackageType.Stable:
                    vketResultResponse = NetworkUtility.VketPostUploadResult(
                        info.vketId, info.GetCirclesOrCompanyCircles()[info.selectedCircleNum].id,
                        info.selectedWorldId, uploadResult.package, boothOuterType, uploadResult, ssPath,
                        !info.errorFlag);
                    break;
                case PackageType.Company:
                case PackageType.Community:
                    vketResultResponse = NetworkUtility.VketPostUploadResult(
                        info.vketId, info.data.company[info.selectedCircleNum].id,
                        info.selectedWorldId, uploadResult.package, boothOuterType, uploadResult, ssPath,
                        !info.errorFlag);
                    break;
                default:
                    vketResultResponse = NetworkUtility.VketPostUploadResult(
                        info.vketId, info.data.circle[info.selectedCircleNum].id,
                        info.selectedWorldId, uploadResult.package, boothOuterType, uploadResult, ssPath,
                        !info.errorFlag);
                    break;
            }

            Debug.Log(vketResultResponse);

            if (vketResultResponse == null && mallResultResponse == null)
            {
                EditorUtility.DisplayDialog( /*入稿*/
                    LocalizedMessage.Get(
                        "Vket_ControlPanel.SubmissionButton"), /* 入稿に失敗しました。\r\n入稿データを確認してからもう一度お試しください。 */
                    LocalizedMessage.Get("Vket_ControlPanel.ExportAndUpload.FailedSubmission"), "OK");
                throw new OperationCanceledException("Submission failed.");
            }

            // ⑨アップロード処理終了
            VketSequenceDrawer.SetState(_currentSeq, Status.Complete);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), DelayType.Realtime,
                cancellationToken: cancellationToken);

            if (info.errorFlag)
            {
                if (EditorUtility.DisplayDialog( /* 入稿 */
                        LocalizedMessage.Get("Vket_ControlPanel.SubmissionButton"),
                        LocalizedMessage.Get(
                            "Vket_ControlPanel.ExportAndUpload.ErrorMessage") /*"エラーのある状態でアップロードしました。\nエラー修正後再度入稿してください。"*/,
                        "OK"))
                {
                }
            }
            else
            {
                if (EditorUtility.DisplayDialog(
                        LocalizedMessage.Get("Vket_ControlPanel.SubmissionButton"), /*入稿*/
                        LocalizedMessage.Get(
                            "Vket_ControlPanel.ExportAndUpload.CompleateSubmission"), /* 入稿完了しました。\r\n公式サイトのマイページから「入稿管理ページ」をご確認ください。 */
                        "OK"))
                {
                }
            }
        }
        
        #endregion // 入稿逐次処理

        /// <summary>
        /// ビルドサイズを取得するタスク
        /// </summary>
        /// <returns>ビルドサイズ</returns>
        private static async UniTask<float> CalculationCompileSize()
        {
            float cmpSize = AssetUtility.ForceRebuild();
            cmpSize = BuildSizeZeroAdjust(cmpSize);

            /*
             * NOTE: このタイミングで、Yieldを使用しないとタスクが止まる
             * await new YieldAwaitable(PlayerLoopTiming.Update)と同等の処理
             * このタイミングでのYield(CancellationToken cancellationToken)は処理が止まるので使用しないこと
             */
            await UniTask.Yield();
            
            return cmpSize;
        }

        /// <summary>
        /// VRCローカルテスト
        /// </summary>
        private static async UniTask<bool> BuildAndTestWorld(bool showDialog = true)
        {
            // VRC_SceneDescriptorが存在するか確認
            if (!FindObjectsOfType<VRC_SceneDescriptor>().Any())
            {
                EditorUtility.DisplayDialog(
                    "Error", /* VRC_SceneDescriptorが見つかりませんでした。\nVRCWorldをシーンに追加してからもう一度実行してください。 */
                    LocalizedMessage.Get("Vket_ControlPanel.VRC_LocalTestLaunch.NotFoundVRCWorld"), "OK");
                return false;
            }
            
            // Builderが存在するか確認
            if (!VRCSdkControlPanel.TryGetBuilder<IVRCSdkWorldBuilderApi>(out var builder))
            {
                EditorUtility.DisplayDialog(
                    "Error", "Not Found IVRCSdkWorldBuilderApi", "OK");
                return false;
            }
            
            LoginInfo info = AssetUtility.LoginInfoData;
            Scene scene = SceneManager.GetActiveScene();
            var circles = info.GetCirclesOrCompanyCircles();
            
            string rootObjectName;
            switch (_packageType)
            {
                case PackageType.Company:
                case PackageType.Community:
                case PackageType.Dev:
                    rootObjectName = scene.name;
                    break;
                default:
                    rootObjectName = CircleNullOrEmptyCheck()
                        ? scene.name
                        : circles[info.selectedCircleNum].id.ToString();
                    break;
            }
            
            // シーンに配置しているparentを持たないオブジェクトで不要なもののアクティブ状態のリスト
            // Key:gameObject名,Value:activeSelf
            var notSubmitObjList = new Dictionary<string, bool>();
            if (circles != null)
            {
                // シーンに配置しているparentを持たないオブジェクトで不要なものを非アクティブにしてからローカルテストする
                foreach (GameObject obj in Array.FindAll(FindObjectsOfType<GameObject>(),
                             (item) => item.transform.parent == null))
                {
                    if (obj.name != rootObjectName && obj.name != "ReferenceObjects" &&
                        obj.name != "ExhibitorItemManager" && obj.name != "ExhibitorBoothManager")
                    {
                        notSubmitObjList.Add(obj.name, obj.activeSelf);
                        obj.SetActive(false);
                    }
                }
            }
            
            // ビルド実行
            try
            {
                _isBuildWorld = true;
                await builder.BuildAndTest();
                _isBuildWorld = false;
                // ウィンドウに反映
                await UniTask.DelayFrame(3);
            }
            catch (Exception e) when (!(e is OperationCanceledException))
            {
                // エラーメッセージ表示
                Debug.LogError(e.Message);
                if (showDialog)
                {
                    EditorUtility.DisplayDialog(
                        "Error", /* "ビルドに失敗しました。入稿処理を中断します。" */
                        LocalizedMessage.Get("Vket_ControlPanel.BuildAndTestWorld.BuildFaild.Message"), "OK");
                }

                return false;
            }
            finally
            {
                _isBuildWorld = false;
                
                // シーンに配置しているparentを持たないオブジェクトで不要なもののアクティブ状態を元に戻す
                foreach (var obj in Array.FindAll(Resources.FindObjectsOfTypeAll<GameObject>(),
                             (item) => item.transform.parent == null))
                {
                    if (obj.name != rootObjectName && obj.name != "ReferenceObjects" &&
                        AssetDatabase.GetAssetOrScenePath(obj).Contains(".unity"))
                    {
                        foreach (var pair in notSubmitObjList)
                        {
                            if (obj.name == pair.Key)
                            {
                                obj.SetActive(pair.Value);
                                break;
                            }
                        }
                    }
                }
            }
            
            // ビルド成功している場合、このタイミングでVRChat.exeが自動で起動する
            if (showDialog && !EditorUtility.DisplayDialog("VRChat Client Check.", /* VRChat内での確認を待っています。 */
                    LocalizedMessage.Get("Vket_ControlPanel.DraftButton_Click.WaitVRCCheck"), /* 確認しました */
                    LocalizedMessage.Get("Confirmed"), /* キャンセル */
                    LocalizedMessage.Get("Cancel")))
            {
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// シーンのベイク
        /// </summary>
        /// <param name="isVRChatCheck"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async UniTask BakeCheckAndRun(bool isVRChatCheck, CancellationToken cancellationToken)
        {
            var objList = new Dictionary<GameObject, bool>();

            Scene scene = SceneManager.GetActiveScene();
            Scene untitledScene = SceneManager.GetSceneByPath("");
            var rootObjectName = scene.name;
            if (scene == untitledScene)
            {
                EditorUtility.DisplayDialog("Error", /* "シーンファイルが保存されていません。" */ LocalizedMessage.Get("Vket_ControlPanel.BakeCheckAndRun.NotSaveScene.Message"), "OK");
                throw new Exception("Active Scene is Untitled Scene");
            }
            
            if (!isVRChatCheck)
            {
                // 親が無いオブジェクトのアクティブ状態を保持
                foreach (GameObject obj in Array.FindAll(FindObjectsOfType<GameObject>(),
                             (item) => item.transform.parent == null))
                {
                    if (obj.name != rootObjectName)
                    {
                        objList.Add(obj, obj.activeSelf);
                    }
                }
            }

            EditorUtility.DisplayProgressBar("Bake", "Baking...", 0);
            bool bakeFlag = Lightmapping.BakeAsync();
            if (!bakeFlag)
            {
                if(EditorUtility.DisplayDialog("Error", /* Light Bakeに失敗しました。 */LocalizedMessage.Get("Vket_ControlPanel.BakeCheckAndRun.LightBakeFailed"), "OK"))
                {
                    BakeCheckAndRunExit();
                    // 例外を投げる
                    throw new OperationCanceledException("Light Bake Failed");
                }
            }

            try
            {
                while (Lightmapping.isRunning)
                {
                    EditorUtility.DisplayProgressBar("Bake", "Baking...", Lightmapping.buildProgress);
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
                }
            }
            catch
            {
                Lightmapping.Cancel();
                throw;
            }
            finally
            {
                EditorUtility.ClearProgressBar();
                BakeCheckAndRunExit();
            }

            void BakeCheckAndRunExit()
            {
                if (!isVRChatCheck)
                {
                    // 親が無いオブジェクトのアクティブ状態を戻す
                    foreach (var pair in objList)
                    {
                        pair.Key.SetActive(pair.Value);
                    }
                }
            }
        }

        private static ValidatedExportResult ExportBooth()
        {
            var type = typeof(ValidatedExporterWindow);
            var instance = CreateInstance<ValidatedExporterWindow>();
            ReflectionUtility.InvokeMethod(type, "Init", instance, new object[] { });
            var baseFolder = (DefaultAsset)ReflectionUtility.GetField(type, "baseFolder", instance);
            var userSettings = VitDeck.Utilities.UserSettingUtility.GetUserSettings();
            if (string.IsNullOrEmpty(userSettings.exporterSettingFileName))
            {
                userSettings.exporterSettingFileName = GetExportSetting();
            }

            var selectedSetting = VitDeck.Exporter.Exporter.GetExportSettings()
                .First(w => w.name == userSettings.exporterSettingFileName);
            var baseFolderPath = AssetDatabase.GetAssetPath(baseFolder);
            Debug.Log("BaseFolder：" + baseFolderPath);
            Debug.Log("ExportRule：" + selectedSetting);
            var result = ValidatedExporter.ValidatedExport(baseFolderPath, selectedSetting, true);
            SetExportValidateLog(type, instance, result, baseFolderPath, selectedSetting.ruleSetName);
            DestroyImmediate(instance);
            return result;
        }

        #endregion // タスク処理

        private static string GetValidatorRule()
        {
            if (CircleNullOrEmptyCheck())
            {
                return "ConceptWorldRuleSet";
            }

            return _worldDefinition.ValidatorRuleSet;
        }

        private static string GetExportSetting()
        {
            if (CircleNullOrEmptyCheck())
            {
                return "VketExportSetting";
            }

            return _worldDefinition.ExportSetting;
        }

        /// <summary>
        /// Vketアカウントにログインする
        /// </summary>
        /// <param name="authentication">認証情報</param>
        private static async void VketAccountLogin(NetworkUtility.AccessTokenProvider.Result authentication)
        {
            // Play 中なら何もしない
            if (EditorPlayCheck() || EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

            if (authentication == null)
            {
                if (IsLogin && AssetUtility.LoginInfoData != null && AssetUtility.LoginInfoData.data != null &&
                    !string.IsNullOrEmpty(AssetUtility.LoginInfoData.accessToken))
                    _accessToken = AssetUtility.LoginInfoData.accessToken;
                else
                {
                    //EditorUtility.DisplayDialog("Error", /* Vketアカウントでログインしてください。 */AssetUtility.GetLabelString(35), "OK");
                    return;
                }
            }
            else
            {
                _authentication = authentication;
                _accessToken = authentication.AccessToken;
            }

            LoginInfo info = AssetUtility.LoginInfoData;

            try
            {
                var profile = await Utilities.Networking.NetworkUtility.GetProfile(_accessToken);

                if (profile == null)
                {
                    Logout();
                    return;
                }

                info.profile = profile;
                _vketId = info.profile.VketId;
                info.authentication = authentication;
                info.accessToken = _accessToken;
                info.vketId = _vketId;
                AssetUtility.SaveAsset(info);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                Debug.LogError( /* ログインに失敗しました。お問い合わせにて表示されたエラーコードをお伝えください。 */LocalizedMessage.Get("Vket_ControlPanel.LoginErrorMessage"));
                Logout();
                return;
            }

            AssetDatabase.SaveAssets();
        }

        private static async void VketAccountKeepLogin()
        {
            // Play 中なら何もしない
            if (EditorPlayCheck() || EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

            LoginInfo info = AssetUtility.LoginInfoData;

            if (info.authentication == null)
            {
                Debug.LogError( /* ログインに失敗しました。お問い合わせにて表示されたエラーコードをお伝えください。 */LocalizedMessage.Get("Vket_ControlPanel.LoginErrorMessage"));
                Logout();
                return;
            }

            // AccessToken が期限切れの場合、 RefreshToken で新しい AccessToken を得る
            if (info.authentication.CreatedAt + info.authentication.ExpiresIn < DateTimeOffset.Now.ToUnixTimeSeconds())
            {
                var refreshResult = await NetworkUtility.GetRefreshToken(info.authentication.RefreshToken);
                if (refreshResult == null)
                {
                    Debug.LogError( /* ログインに失敗しました。お問い合わせにて表示されたエラーコードをお伝えください。 */LocalizedMessage.Get("Vket_ControlPanel.LoginErrorMessage"));
                    Logout();
                    return;
                }

                var oldAuth = info.authentication;
                info.accessToken = refreshResult.AccessToken;
                info.authentication = JsonUtility.FromJson<NetworkUtility.AccessTokenProvider.Result>(
                    "{\"access_token\":\"" + refreshResult.AccessToken +
                    "\",\"expires_in\":" + refreshResult.ExpiresIn + ",\"id_token\":\"" + oldAuth.IdToken +
                    "\",\"refresh_token\":\"" + refreshResult.RefreshToken + "\",\"scope\":\"" + refreshResult.Scope +
                    "\",\"token_type\":\"" + refreshResult.TokenType + "\",\"created_at\":" + refreshResult.CreatedAt +
                    "}");
                _authentication = info.authentication;
                info.vrcLaunchFlag = true;
                AssetUtility.SaveAsset(info);
            }

            try
            {
                var profile = await Utilities.Networking.NetworkUtility.GetProfile(info.accessToken);

                if (profile == null)
                {
                    Logout();
                    return;
                }

                info.profile = profile;
                _vketId = info.profile.VketId;
                info.vketId = _vketId;
                info.vrcLaunchFlag = true;
                AssetUtility.SaveAsset(info);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                Debug.LogError( /* ログインに失敗しました。お問い合わせにて表示されたエラーコードをお伝えください。 */LocalizedMessage.Get("Vket_ControlPanel.LoginErrorMessage"));
                Logout();
                return;
            }

            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// AssetUtility.LoginInfoData内の情報を更新
        /// </summary>
        private static void UpdateLoginInfoData()
        {
            if (EditorPlayCheck())
            {
                return;
            }

            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

            LoginInfo info = AssetUtility.LoginInfoData;

            if (string.IsNullOrEmpty(info.profile.VketId))
            {
                //EditorUtility.DisplayDialog("Error", /* Vketアカウントでログインしてください。 */AssetUtility.GetLabelString(35), "OK");
                return;
            }

            VitDeck.Utilities.UserSettings userSettings = VitDeck.Utilities.UserSettingUtility.GetUserSettings();
            IsLogin = true;

            switch (_packageType)
            {
                case PackageType.Stable:
                    try
                    {
                        _vketId = string.IsNullOrEmpty(_vketId) ? info.profile.VketId : _vketId;
                        _vketId = string.IsNullOrEmpty(_vketId) ? info.vketId : _vketId;
                        var data = NetworkUtility.GetVketData(_vketId);
                        if (data == null)
                        {
                            Logout();
                            Debug.LogError(data);
                            return;
                        }

                        info.data = data;
                        if (info.IsCircleAvailable())
                        {
                            info.world = NetworkUtility.GetWorldData().data.worlds;
                            if (info.selectedCircleNum != 9999)
                            {
                                _worldDefinition =
                                    AssetUtility.AssetLoad<VketWorldDefinition>(GetWorldDefinitionPath(info));
                                userSettings.validatorFolderPath = "Assets/" + info.GetCircle().id;
                                userSettings.validatorRuleSetType = GetValidatorRule();
                                userSettings.exporterSettingFileName = GetExportSetting();
                            }

                            info.submissionTerm = NetworkUtility.GetTerm(AssetUtility.VersionInfoData.event_version,
                                AssetUtility.VersionInfoData.package_type);
                        }

                        AssetUtility.SaveAsset(info);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                        Debug.LogError( /* ログインに失敗しました。お問い合わせにて表示されたエラーコードをお伝えください。 */LocalizedMessage.Get("Vket_ControlPanel.LoginErrorMessage"));
                        Logout();
                        return;
                    }

                    break;
                case PackageType.Company:
                case PackageType.Community:
                    break;
                case PackageType.Dev:
                    try
                    {
                        _vketId = string.IsNullOrEmpty(_vketId) ? info.profile.VketId : _vketId;
                        _vketId = string.IsNullOrEmpty(_vketId) ? info.vketId : _vketId;


                        info.world = NetworkUtility.GetWorldData().data.worlds;

                        AssetUtility.SaveAsset(info);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                        Debug.LogError( /* ログインに失敗しました。お問い合わせにて表示されたエラーコードをお伝えください。 */LocalizedMessage.Get("Vket_ControlPanel.LoginErrorMessage"));
                        Logout();
                        return;
                    }

                    break;
                default:
                    _worldDefinition =
                        AssetUtility.AssetLoad<VketWorldDefinition>(
                            $"{AssetUtility.ConfigFolderPath}/WorldDefinitions/0.asset");
                    break;
            }

            if (CircleNullOrEmptyCheck())
            {
                userSettings.validatorRuleSetType = "ConceptWorldRuleSet";
            }

            VitDeck.Utilities.UserSettingUtility.SaveUserSettings(userSettings);

            EditorCoroutineUtility.StartCoroutine(GetTexture(), new object());

            AssetDatabase.SaveAssets();
        }

        private static IEnumerator GetTexture()
        {
            LoginInfo info = AssetUtility.LoginInfoData;
            info.worldIcon = new Texture2D[info.world.Length];
            
            if (!Directory.Exists(WorldThumbnailFolderPath))
            {
                Directory.CreateDirectory(WorldThumbnailFolderPath);
            }

            switch (_packageType)
            {
                case PackageType.Company:
                case PackageType.Community:
                    break;
                case PackageType.Dev:
                {
                    for (int i = 0; i < info.world.Length; i++)
                    {
                        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(info.world[i].thumbnail.url))
                        {
                            yield return uwr.SendWebRequest();

                            if (uwr.isNetworkError || uwr.isHttpError)
                            {
                                Debug.Log(uwr.error);
                            }
                            else
                            {
                                info.worldIcon[i] = new Texture2D(150, 150);
                                Graphics.ConvertTexture(DownloadHandlerTexture.GetContent(uwr), info.worldIcon[i]);
                            }
                        }
                    }
                    break;
                }
                case PackageType.Stable:
                {
                    // サークルサムネイルの更新
                    var circles = info.GetCirclesOrCompanyCircles();
                    if (circles[0].thumbnail == null || circles[0].thumbnail.url == "")
                    {
                        Debug.Log("no thumbnail");
                    }
                    else
                    {
                        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(circles[0].thumbnail.url))
                        {
                            yield return uwr.SendWebRequest();

                            if (uwr.isNetworkError || uwr.isHttpError)
                            {
                                Debug.Log($"{uwr.error} {uwr.url}");
                            }
                            else
                            {
                                info.circleIcon = DownloadHandlerTexture.GetContent(uwr);
                            }
                        }
                    }

                    // ワールドサムネイルの更新
                    foreach (var world in info.world)
                    {
                        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture($"https://uc-vket-event.vketcdn.com/2023winter/world/small/{world.image}.jpg"))
                        {
                            uwr.disposeUploadHandlerOnDispose = false;
                            // ファイルを保存
                            uwr.downloadHandler = new DownloadHandlerFile(Path.Combine(WorldThumbnailFolderPath, world.id.ToString()));
                            yield return uwr.SendWebRequest();

                            if (uwr.isNetworkError || uwr.isHttpError)
                            {
                                Debug.Log(uwr.error);
                            }
                        }
                    }

                    AssetDatabase.ImportAsset(WorldThumbnailFolderPath, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ImportRecursive);
                    break;
                }
            }
            
            LoadWorldThumbnail(info, true);
            
            AssetDatabase.SaveAssets();
        }
        
        private static string GetWorldDefinitionPath(LoginInfo info)
        {
            switch (_packageType)
            {
                case PackageType.Stable:
                case PackageType.Company:
                case PackageType.Community:
                    return GetWorldDefinitionPath(info.selectedWorldId);
                case PackageType.Dev:
                default:
                    return $"{AssetUtility.ConfigFolderPath}/WorldDefinitions/0.asset";
            }
        }

        private static string GetWorldDefinitionPath(int worldId)
        {
            return $"{AssetUtility.ConfigFolderPath}/WorldDefinitions/{(EventFolder)ushort.Parse(AssetUtility.VersionInfoData.event_version)}/{worldId.ToString()}.asset";
        }

        private static void Logout()
        {
            if (EditorPlayCheck())
            {
                return;
            }

            if (string.IsNullOrEmpty(_accessToken))
            {
                _accessToken = AssetUtility.LoginInfoData.accessToken;
            }

            if (string.IsNullOrEmpty(_vketId))
            {
                _vketId = AssetUtility.LoginInfoData.vketId;
            }

            Utilities.Networking.NetworkUtility.VketData data = new Utilities.Networking.NetworkUtility.VketData();
            Utilities.Networking.NetworkUtility.MallData mall_data = new Utilities.Networking.NetworkUtility.MallData();
            Utilities.Networking.NetworkUtility.Profile.Result profile =
                new Utilities.Networking.NetworkUtility.Profile.Result();
            Utilities.Networking.NetworkUtility.AccessTokenProvider.Result initialAuthentication =
                new Utilities.Networking.NetworkUtility.AccessTokenProvider.Result();
            LoginInfo info = AssetUtility.LoginInfoData;
            info.data = data.data;
            info.profile = profile;
            info.mall_data = mall_data;
            info.world = null;
            info.shop = null;
            info.genre = null;
            info.authentication = initialAuthentication;
            _authentication = null;
            info.accessToken = "";
            info.vketId = "";
            info.selectedItemNum = 9999;
            info.selectedCircleNum = 9999;
            info.selectedWorldId = 9999;
            IsSelected = false;
            info.worldIcon = null;
            _worldDefinition = null;
            VitDeck.Utilities.UserSettings userSettings = VitDeck.Utilities.UserSettingUtility.GetUserSettings();
            userSettings.validatorFolderPath = "";
            userSettings.validatorRuleSetType = "";
            userSettings.exporterSettingFileName = "";
            VitDeck.Utilities.UserSettingUtility.SaveUserSettings(userSettings);
            AssetUtility.SaveAsset(info);
            AssetDatabase.SaveAssets();
            IsLogin = false;
            IsSelected = false;
            IsWaitAuth = false;
            _authorizationCode = "";
            _authorizationResult = new Utilities.Networking.NetworkUtility.AuthorizationCodeProvider.Result();
            _vketId = "";
            info.circleIcon = null;
        }

        private static bool UpdateCheck()
        {

            if (!Utilities.Hiding.HidingUtil.DebugMode && !UpdateUtility.UpdateCheck())
            {
                EditorUtility.DisplayDialog("Error", /* アップデートを行ってから実行してください。 */LocalizedMessage.Get("Vket_ControlPanel.UpdateCheck.RequestUpdate"), "OK");
                return false;
            }

            return true;
        }

        private static bool EditorPlayCheck()
        {
            if (EditorApplication.isPlaying)
            {
                if (EditorUtility.DisplayDialog("Error", /* Editorを再生中は実行できません。 */LocalizedMessage.Get("Vket_ControlPanel.EditorPlayCheck.CannotExecute"),
                        "OK"))
                    EditorApplication.isPlaying = false;
                return true;
            }

            return false;
        }

        private static bool BuildSizeCheck(float cmpSize)
        {
            return cmpSize > GetBuildMaxSize();
        }

        /// <summary>
        /// サークル情報があるかチェック
        /// falseでの運用を想定
        /// </summary>
        /// <returns>サークルが存在しない場合trueを返す</returns>
        private static bool CircleNullOrEmptyCheck()
        {
            LoginInfo info = AssetUtility.LoginInfoData;
            switch (_packageType)
            {
                case PackageType.Stable:
                    return info.data == null || (info.data.circle == null && info.data.company == null);
                default:
                    return false;
            }
        }

        private static void SetExportValidateLog(Type type, object instance, ValidatedExportResult result,
            string baseFolderPath, string ruleSetName)
        {
            string header = string.Format("- version:{0}", VitDeck.Utilities.ProductInfoUtility.GetVersion()) +
                            Environment.NewLine;
            header += string.Format("- Rule set:{0}", ruleSetName) + Environment.NewLine;
            header += string.Format("- Base folder:{0}", baseFolderPath) + Environment.NewLine;
            string log = header + result.GetValidationLog() + result.GetExportLog() + result.log;
            ReflectionUtility.InvokeMethod(type, "SetMessages", instance, new object[] { header, result });
            ReflectionUtility.InvokeMethod(type, "OutLog", instance, new object[] { log });
            ReflectionUtility.InvokeMethod(type, "OutLog", instance, new object[] { "Export completed." });
        }

        private static void SetValidateLog(Type type, object instance, VitDeck.Validator.ValidationResult[] results,
            string baseFolderPath, string ruleSetName)
        {
            string header = string.Format("- version:{0}", VitDeck.Utilities.ProductInfoUtility.GetVersion()) +
                            Environment.NewLine;
            header += string.Format("- Rule set:{0}", ruleSetName) + Environment.NewLine;
            header += string.Format("- Base folder:{0}", baseFolderPath) + Environment.NewLine;
            bool isHideInfoMessage = (bool)ReflectionUtility.GetField(type, "isHideInfoMessage", instance);
            string log = header + (string)ReflectionUtility.InvokeMethod(type, "GetResultLog", instance,
                new object[]
                {
                    results,
                    isHideInfoMessage ? VitDeck.Validator.IssueLevel.Warning : VitDeck.Validator.IssueLevel.Info
                });
            ReflectionUtility.InvokeMethod(type, "SetMessages", instance, new object[] { header, results });
            ReflectionUtility.InvokeMethod(type, "OutLog", instance, new object[] { log });
            ReflectionUtility.InvokeMethod(type, "OutLog", instance, new object[] { "Export completed." });
        }

        private static int GetMaxSize(MaxSizeType type)
        {
            if (_worldDefinition == null) return -1;
            switch (type)
            {
                case MaxSizeType.SetPass:
                    return _worldDefinition.SetPassMaxSize;
                case MaxSizeType.Batches:
                    return _worldDefinition.BatchesMaxSize;
            }

            return -1;
        }

        private static float GetBuildMaxSize()
        {
            var info = AssetUtility.LoginInfoData;
            _worldDefinition =
                AssetUtility.AssetLoad<VketWorldDefinition>(GetWorldDefinitionPath(info));
            if (_worldDefinition == null) return -1;
            return _worldDefinition.BuildMaxSize;
        }

        // どのテンプレートを読み込んでも固定だったので
        private static float GetBuildSizeOffset() => 0.07074451f;

        // 背景か何かで、空シーンでも固定で2取っていくので軽減
        private static int GetSetPassCallsOffset() => 2;
        private static int GetBatchesOffset() => 2;

        private static float BuildSizeZeroAdjust(float cmpSize)
        {
            if (cmpSize == -1) return cmpSize;
            float original = cmpSize;
            // 浮動小数点数の誤差や、環境の違いによる数値のずれを丸めで吸収する
            float adjusted = Mathf.Max(0, (float)Math.Round(cmpSize - GetBuildSizeOffset(), 5));
            Debug.Log($"vketscene.vrcw fileSize: {original}MiB (zero-adjusted: {adjusted}MiB)");
            return adjusted;
        }

        private static string GetRuleURL()
        {
            if (_worldDefinition == null) return "";
            return (Application.systemLanguage == SystemLanguage.Japanese)
                ? _worldDefinition.RuleUrlJa
                : _worldDefinition.RuleUrlEn;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.ExitingEditMode:
                {
                    // Unityの仕様でEditorモードに入る前にウィンドウが閉じるのでこのタイミングで参照などを切る
                    _vrcSdkWindow = null;
                    _controlPanel = null;
                    
                    // SDKウィンドウが閉じたときのコールバックを外す
                    VRCSdkControlPanel.OnSdkPanelDisable -= OnVrcSdkPanelDisable;
                }
                    break;
                
                case PlayModeStateChange.EnteredPlayMode:
                {
                    // 入稿処理orセットパス確認中の場合
                    if (AssetUtility.EditorPlayInfoData.isVketEditorPlay)
                    {
                        if (AssetUtility.EditorPlayInfoData.isSetPassCheckOnly)
                        {
                            SetPassCheckButtonIntermediateProcessingTask().Forget();
                        }
                        else
                        {
                            // プレイモード中の入稿処理
                            DraftSecondTask().Forget();
                        }
                    }
                    else
                    {
                        // 表示フラグが立っている場合はダイアログを表示する。
                        if (AssetUtility.EditorPlayInfoData.showPlayModeNotification && 
                            EditorUtility.DisplayDialog("Warning", /* "Vketでは専用のプログラムで動作させるため、UnityEditor上での確認では入稿物の動作が保証されません。" */ LocalizedMessage.Get("Vket_ControlPanel.OnPlayModeStateChanged.PlayModeNotification.Message"), /* "次回以降表示しない" */ LocalizedMessage.Get("Vket_ControlPanel.OnPlayModeStateChanged.PlayModeNotification.NotDisplayNext"), "OK"))
                        {
                            AssetUtility.EditorPlayInfoData.showPlayModeNotification = false;
                            AssetUtility.SaveAsset(AssetUtility.EditorPlayInfoData);
                        }
                    }
                } 
                    break;
                case PlayModeStateChange.EnteredEditMode:
                {
                    // SDKウィンドウが閉じたときのコールバックを再登録
                    VRCSdkControlPanel.OnSdkPanelDisable += OnVrcSdkPanelDisable;

                    // セットパス確認失敗フラグが立っている場合
                    if (AssetUtility.EditorPlayInfoData.setPassFailedFlag)
                    {
                        ReloadLayout();
                        AssetUtility.EditorPlayInfoData.setPassFailedFlag = false;
                        AssetUtility.SaveAsset(AssetUtility.EditorPlayInfoData);
                    }
                    
                    if (AssetUtility.EditorPlayInfoData.isVketEditorPlay)
                    {
                        AssetUtility.EditorPlayInfoData.isVketEditorPlay = false;
                        AssetUtility.SaveAsset(AssetUtility.EditorPlayInfoData);

                        if (!AssetUtility.EditorPlayInfoData.isSetPassCheckOnly &&
                            !AssetUtility.EditorPlayInfoData.ssSuccessFlag)
                        {
                            // 入稿処理中断
                            Debug.Log(VketSequenceDrawer.GetResultLog());
                            DraftExitFunc();
                        }
                        // 入稿処理中かつスクリーンショットの撮影に成功している場合
                        else if (!AssetUtility.EditorPlayInfoData.isSetPassCheckOnly &&
                            AssetUtility.EditorPlayInfoData.ssSuccessFlag)
                        {
                            ReloadLayout();
                            // 続きの入稿処理へ
                            DraftThirdTask().Forget();
                        }
                        else
                        {
                            // セットパスコールチェック終了
                            ReloadLayout();
                            SetPassCheckButtonPostprocessingFunc();
                        }
                    }
                }
                    break;
            }
            
            // WorldDefinition更新
            SetupPlayModeWorldDefinition();
            
            void ReloadLayout()
            {
                // WindowLayoutを復元
                LoadEditorLayout();
                DeleteEditorLayout();
            
                // Builderタブを開く
                OpenSDKBuilderTabTask().Forget();
            }
        }

        private static bool GetClientSimEnabled()
        {
            try
            {
                var settings = Assembly.Load("VRC.ClientSim").GetType("VRC.SDK3.ClientSim.ClientSimSettings");
                var instance = settings.GetProperty("Instance").GetMethod.Invoke(null, new object[] { });
                var enabled = (bool)settings.GetField("enableClientSim").GetValue(instance);
                Debug.Log($"ClientSimEnabled: {enabled}");
                return enabled;
            }
            catch
            {
            }

            return true;
        }

        private static void SetClientSimEnabled(bool enabled)
        {
            try
            {
                var settings = Assembly.Load("VRC.ClientSim").GetType("VRC.SDK3.ClientSim.ClientSimSettings");
                var instance = settings.GetProperty("Instance").GetMethod.Invoke(null, new object[] { });
                settings.GetField("enableClientSim").SetValue(instance, enabled);
                settings.GetMethod("SaveSettings").Invoke(null, new[] { instance });
            }
            catch
            {
            }
        }

        private static void RestoreClientSimEnabled()
        {
            SetClientSimEnabled(AssetUtility.EditorPlayInfoData.clientSimEnabled);
            Debug.Log($"ClientSimEnabled: restored to {AssetUtility.EditorPlayInfoData.clientSimEnabled}");
        }

        /// <summary>
        /// VRChatのパスが間違えていないか判定
        /// </summary>
        /// <returns>
        /// 間違えていない場合true
        /// VRChatをインストールしていない場合はfalseを返す
        /// </returns>
        /// <exception cref="Exception">VRCSDKにログインできていない場合は例外を投げる</exception>
        private static bool ExistVRCClient()
        {
            if (!APIUser.IsLoggedIn)
                throw new OperationCanceledException("VRCHAT SDK is not Login!");

            // EditorPrefsからVRChat.exeのパスを取得
            var clientInstallPath = SDKClientUtilities.GetSavedVRCInstallPath();

            // 空文字の場合はデフォルトパスを取得
            if (string.IsNullOrEmpty(clientInstallPath))
                clientInstallPath = SDKClientUtilities.LoadRegistryVRCInstallPath();

            // 判定
            return File.Exists(clientInstallPath);
        }

        /// <summary>
        /// 一時的に保存するWindowレイアウトのファイル名
        /// </summary>
        private static readonly string TempLayoutFileName = "TempLayout.wlt";

        /// <summary>
        /// Windowレイアウトの保存
        /// SetPassCallチェック時のPlayモード切替時に呼ぶ想定
        /// </summary>
        private static void SaveEditorLayout()
        {
            Debug.Log("SaveEditorLayout Start");

            var assetPath = Path.Combine(Path.Combine(InternalEditorUtility.unityPreferencesFolder, "Layouts"),
                TempLayoutFileName);
            var layout = Type.GetType("UnityEditor.WindowLayout,UnityEditor");
            var method = layout.GetMethod("SaveWindowLayout",
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) },
                null);
            method.Invoke(null, new object[] { assetPath });

            Debug.Log("SaveEditorLayout End");
        }

        /// <summary>
        /// Windowレイアウトの読み込み
        /// SetPassCallチェック時のEditorモード切替時に呼ぶ想定
        /// </summary>
        private static void LoadEditorLayout()
        {
            Debug.Log("LoadEditorLayout Start");
            var assetPath = Path.Combine(Path.Combine(InternalEditorUtility.unityPreferencesFolder, "Layouts"),
                TempLayoutFileName);
            var layout = Type.GetType("UnityEditor.WindowLayout,UnityEditor");
            var method = layout.GetMethod("LoadWindowLayout",
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, null,
                new Type[] { typeof(string), typeof(bool) }, null);
            method.Invoke(null, new object[] { assetPath, false });

            Debug.Log("LoadEditorLayout End");
        }

        /// <summary>
        /// Windowレイアウトの削除
        /// </summary>
        private static void DeleteEditorLayout()
        {
            var assetPath = Path.Combine(Path.Combine(InternalEditorUtility.unityPreferencesFolder, "Layouts"),
                TempLayoutFileName);
            File.Delete(assetPath);
            InternalEditorUtility.ReloadWindowLayoutMenu();
        }
    }
    
    /// <summary>
    /// 入稿方法ポップアップに表示するコンテンツ
    /// </summary>
    public class MessagePopup : PopupWindowContent
    {
        private readonly string _message;
        public MessagePopup(string message)
        {
            _message = message;
        }
        
        /// <summary>
        /// サイズを取得する
        /// </summary>
        public override Vector2 GetWindowSize()
        {
            return new Vector2(Screen.width, Vket_ControlPanel.GetHelpBoxHeight(_message));
        }

        /// <summary>
        /// GUI描画
        /// </summary>
        public override void OnGUI(Rect rect)
        {
            var style = new GUIStyle(EditorStyles.miniLabel)
            {
                wordWrap = true
            };
            EditorGUILayout.LabelField(_message, style );
        }
    }
}