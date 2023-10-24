using System;
using UnityEditor;
using UnityEngine;
using VketTools.Language;
using VketTools.Utilities;
using Status = VketTools.Utilities.SequenceStatus.RunStatus;

namespace VketTools.Main
{
    public enum DraftSequence
    {
        None,
        BakeCheck,
        VRChatCheck,
        RuleCheck,
        BuildSizeCheck,
        SetPassCheck,
        ScreenShotCheck,
        UploadCheck,
        PostUploadCheck,
    }

    public class VketSequenceDrawer
    {
        private static readonly string[] LocalizeKeys = 
        {
            /* None */
            "",
            /* LightBakeチェック */
            "Vket_SequenceWindow.LightBakeCheck",
            /* VRChat内での確認 */
            "Vket_SequenceWindow.EvaluationVRC",
            /* ルールチェック */
            "Vket_SequenceWindow.RuleCheck",
            /* 容量チェック */
            "Vket_SequenceWindow.BuildSizeCheck",
            /* SetPassチェック */
            "Vket_SequenceWindow.SetPassCheck",
            /* スクリーンショット撮影 */
            "Vket_SequenceWindow.TakeScreenshot",
            /* アップロード */
            "Vket_SequenceWindow.Upload",
            /* アップロード後処理 */
            "Vket_SequenceWindow.PostUploadProcess",
        };

        public static string GetSequenceText(DraftSequence sequence)
        {
            return LocalizedMessage.Get(LocalizeKeys[(int)sequence]);
        }

        public static void SetState(DraftSequence sequence, Status status, string desc = "")
        {
            AssetUtility.SequenceInfoData.sequenceStatus[(int)sequence].status = status;
            AssetUtility.SequenceInfoData.sequenceStatus[(int)DraftSequence.BuildSizeCheck].desc = desc;
        }
        
        public static string GetResultLog()
        {
            var result = "";
            var info = AssetUtility.SequenceInfoData;
            for (int i = 0; i < info.sequenceStatus.Length; ++i)
            {
                result += $"{Enum.ToObject(typeof(DraftSequence), i)}:{info.sequenceStatus[i].status},{info.sequenceStatus[i].desc}\r\n";
            }
            return result;
        }

        /// <summary>
        /// シーケンス情報の初期化
        /// ウィンドウは閉じたり開かれたりするため、SequenceInfoData内に情報を保持しておく
        /// </summary>
        public static void ResetSequence()
        {
            // シーケンスの初期化
            AssetUtility.SequenceInfoData.sequenceStatus = new SequenceStatus[Enum.GetValues(typeof(DraftSequence)).Length];
            for (int i = 0; i < AssetUtility.SequenceInfoData.sequenceStatus.Length; ++i)
            {
                AssetUtility.SequenceInfoData.sequenceStatus[i] = new SequenceStatus();
                AssetUtility.SequenceInfoData.sequenceStatus[i].status = Status.None;
                AssetUtility.SequenceInfoData.sequenceStatus[i].desc = "";
            }
        }

        private static GUIStyle _l1;
        private static GUIStyle _l2;

        private static void InitStyle()
        {
            if (_l1 == null)
            {
                _l1 = new GUIStyle(GUI.skin.label);
                _l2 = new GUIStyle(GUI.skin.label);
                _l1.fontSize = 13;
                _l1.fixedHeight = 30;
                _l2.fontSize = 10;
                _l2.fixedHeight = 25;
            }
        }
        
        public static void Draw(Rect position)
        {
            InitStyle();
            
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(position.width), GUILayout.Height(320f));
            {
                foreach (DraftSequence sequence in Enum.GetValues(typeof(DraftSequence)))
                {
                    if(sequence == DraftSequence.None)
                        continue;
                    
                    DrawSequenceStatus(sequence, position.width);
                }
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        private static void DrawSequenceStatus(DraftSequence sequence, float width)
        {
            EditorGUILayout.BeginHorizontal();
            {
                // アイコン
                GUILayout.Space(20);
                if (AssetUtility.SequenceInfoData.sequenceStatus[(int)sequence].status == Status.Running)
                {
                    UIUtility.EditorGUIWaitSpin();
                }
                else if (AssetUtility.SequenceInfoData.sequenceStatus[(int)sequence].status == Status.Complete)
                {
                    var icon = EditorGUIUtility.IconContent("toggle on");
                    GUILayout.Box(icon, GUIStyle.none, GUILayout.Width(16), GUILayout.Height(16));
                }
                else
                {
                    GUILayout.Space(16);
                }
                
                // テキスト
                EditorGUILayout.Space();
                var content = new GUIContent(GetSequenceText(sequence));
                EditorGUILayout.LabelField(content, UIUtility.GetContentSizeFitStyle(content, _l1, width));
                // テキスト左詰め
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
            
            // アップロード情報
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(36);
                EditorGUILayout.Space();
                var content = new GUIContent(AssetUtility.SequenceInfoData.sequenceStatus[(int)sequence].desc);
                EditorGUILayout.LabelField(content, UIUtility.GetContentSizeFitStyle(content, _l2, width));
            }
            EditorGUILayout.EndHorizontal();
            
            GUILayout.FlexibleSpace();
        }
    }
}