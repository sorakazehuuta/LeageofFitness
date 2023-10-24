using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UdonSharpEditor;
//using VRC.Udon;
//using UdonSharp;
using VRC.SDKBase;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace CombatGimmick
{
    [ExecuteInEditMode]
    public class CombatGimmickBuilder : MonoBehaviour
    {
        //---------------------------------------
        [Header("必要な場合のみ変更すること")]
        [Tooltip("HitBox用のレイヤー")] [SerializeField] int HitBoxLayerNum = 29;
        //---------------------------------------
        PlayerManager playerManager;
        GameManager gameManager;
        MusicManager musicManager;
        ScoreManager scoreManager;
        Assigner[] assigners;
        DropItem[] dropItems;
        RangedWeapon_MainModule[] rangedWeapon_MainModules;
        MeleeWeapon[] meleeWeapons;
        ExWeapon[] exWeapons;
        Flag[] flags;
        ConquestZone[] conquestZones;
        Bot[] bots;
        Turret[] turrets;
        HUD[] hud;
        BotHitBox[] botHitBoxes;
        GameManagerModifier[] gameManagerModifiers;
        MovingPlatformManager movingPlatformManager;
        SpectatorCamera[] spectatorCameras;
        DamageArea[] damageAreas;

        int TeamA_Num;
        int TeamB_Num;
        int TeamC_Num;
        int TeamD_Num;
        //---------------------------------------
        public void AutoBuild()
        {
            Debug.Log("-----<color=#228b22>オートビルド開始</color>-----");
            
            AddComponents();

            assigners = FindObjectsOfType<Assigner>();
            rangedWeapon_MainModules = FindObjectsOfType<RangedWeapon_MainModule>();
            meleeWeapons = FindObjectsOfType<MeleeWeapon>();
            dropItems = FindObjectsOfType<DropItem>();
            flags = FindObjectsOfType<Flag>();
            conquestZones = FindObjectsOfType<ConquestZone>();
            bots = FindObjectsOfType<Bot>();
            turrets = FindObjectsOfType<Turret>();
            hud = FindObjectsOfType<HUD>();
            botHitBoxes = FindObjectsOfType<BotHitBox>();
            gameManagerModifiers = FindObjectsOfType<GameManagerModifier>();
            exWeapons = FindObjectsOfType<ExWeapon>();
            spectatorCameras = FindObjectsOfType<SpectatorCamera>();

            if (!CheckPlayerManager())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckGameManager())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckAssigner())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckScoreManager())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckMusicManager())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckRangedWeapon_MainModule())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckRangedWeapon_SubModule())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckRangedWeapon_MeleeModule())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckMeleeWeapon())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckExWeapon())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckBot())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckPlayerTeleporter())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckPlayerCatapult())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckDamageArea())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckDropItem())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckFlag())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckConquestZone())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckTurret())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckMissile())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckPointerModule())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckGameManagerModifier())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckSpectatorCamera())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckMovingPlatformManager())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckMovingPlatformAnchorTrigger())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckSubBullet())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckPlayerHitBoxSetting(assigners.Length))
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }

            if (!CheckTransformRandomizer())
            {
                Debug.Log("-----<color=#ff0000>オートビルド中断</color>-----");
                return;
            }            

#if UNITY_EDITOR
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
#endif

            Debug.Log("-----<color=#228b22>オートビルド完了!</color>-----");
        }
        //---------------------------------------
        public bool CheckPlayerManager()
        {
            PlayerManager[] playerManagers = FindObjectsOfType<PlayerManager>();
            if(playerManagers.Length == 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> PlayerManagerがシーンにありません.");
                return false;
            }
            else if (playerManagers.Length > 1)
            {
                Debug.Log("<color=#ff0000>Warning:</color> PlayerManagerが2つ以上あります.", playerManagers[0].gameObject);
                return false;
            }

            playerManager = playerManagers[0];
            return playerManager.AutoBuild(damageAreas, conquestZones);
        }
        //---------------------------------------
        public bool CheckGameManager()
        {
            GameManager[] gameManagers = FindObjectsOfType<GameManager>();
            if (gameManagers.Length == 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> GameManagerがシーンにありません.");
                return false;
            }
            if (gameManagers.Length > 1)
            {
                Debug.Log("<color=#ff0000>Warning:</color> GameManagerが2つ以上あります.", gameManagers[0].gameObject);
                return false;
            }
            if (hud.Length == 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> HUDがシーンにありません.");
                return false;
            }
            if (hud.Length > 1)
            {
                Debug.Log("<color=#ff0000>Warning:</color> HUDが2つ以上あります.", hud[0].gameObject);
                return false;
            }

            HUD _hud = hud[0];
            gameManager = gameManagers[0];
            return gameManager.AutoBuild(assigners, rangedWeapon_MainModules, meleeWeapons, exWeapons, playerManager, dropItems, flags, conquestZones, bots, turrets, _hud);
        }
        //---------------------------------------
        public bool CheckAssigner()
        {
            Assigner[] Assigners = FindObjectsOfType<Assigner>();
            
            if (Assigners.Length > 0)
            {
                for (int i = 0; i < Assigners.Length; ++i)
                {
                    if (!Assigners[i].AutoBuild(gameManager, playerManager, assigners, HitBoxLayerNum))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        //---------------------------------------
        public bool CheckPlayerHitBoxSetting(int assignerNum)
        {
            PlayerHitBox[] _playerHitBoxes = FindObjectsOfType<PlayerHitBox>();
            int playerHitBoxNum = _playerHitBoxes.Length;

            if (assignerNum != playerHitBoxNum)
            {
                Debug.Log("<color=#ff0000>Warning:</color> AssignerとPlayerHitBoxの数が違います");
                return false;
            }

            bool[] isOccupied = new bool[assignerNum];
            for(int i= 0; i < assigners.Length; ++i)
            {
                int playerHitBoxIndex = GetPlayerHitBoxIndex(assigners[i], _playerHitBoxes);
                if (playerHitBoxIndex < 0 || _playerHitBoxes.Length <= playerHitBoxIndex)
                {
                    Debug.Log("<color=#ff0000>Warning:</color> PlayerHitBoxの設定が正しくありません");
                    return false;
                }

                if (isOccupied[playerHitBoxIndex])
                {
                    Debug.Log("<color=#ff0000>Warning:</color> 複数のAssignerに同じPlayerHitBoxが設定されています", assigners[i].gameObject);
                    return false;
                }

                isOccupied[playerHitBoxIndex] = true;
            }
            
            return true;
        }
        //---------------------------------------
        public int GetPlayerHitBoxIndex(Assigner _assigner, PlayerHitBox[] _playerHitBoxes)
        {
            for (int i = 0; i < _playerHitBoxes.Length; ++i)
            {
                if(_assigner.playerHitBox == _playerHitBoxes[i]) { return i; }
            }

            return -1;
        }
        //---------------------------------------
        public bool CheckScoreManager()
        {
            ScoreManager[] scoreManagers = FindObjectsOfType<ScoreManager>();
            if (scoreManagers.Length == 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> ScoreManagerがシーンにありません.");
                return false;
            }
            else if (scoreManagers.Length > 1)
            {
                Debug.Log("<color=#ff0000>Warning:</color> ScoreManagerが2つ以上あります.", scoreManagers[0].gameObject);
                return false;
            }

            scoreManager = scoreManagers[0];
            //scoreManager.musicManager = musicManager;
            return scoreManager.AutoBuild(assigners, gameManager);
        }
        //---------------------------------------
        public bool CheckMusicManager()
        {
            MusicManager[] musicManagers = FindObjectsOfType<MusicManager>();
            if (musicManagers.Length == 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> MusicManagerがシーンにありません.");
                return false;
            }
            else if (musicManagers.Length > 1)
            {
                Debug.Log("<color=#ff0000>Warning:</color> MusicManagerが2つ以上あります.", musicManagers[0].gameObject);
                return false;
            }

            musicManager = musicManagers[0];
            gameManager.musicManager = musicManager;
            return musicManager.AutoBuild();
        }
        //---------------------------------------
        public bool CheckRangedWeapon_MainModule()
        {
            RangedWeapon_MainModule[] mm = FindObjectsOfType<RangedWeapon_MainModule>();
            if (mm.Length > 0)
            {
                for(int i =0; i < mm.Length; ++i)
                {
                    if (!mm[i].AutoBuild(playerManager, gameManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckRangedWeapon_SubModule()
        {
            RangedWeapon_SubModule[] sm = FindObjectsOfType<RangedWeapon_SubModule>();
            if (sm.Length > 0)
            {
                for (int i = 0; i < sm.Length; ++i)
                {
                    if (!sm[i].AutoBuild(gameManager, playerManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckRangedWeapon_MeleeModule()
        {
            RangedWeapon_MeleeModule[] mem = FindObjectsOfType<RangedWeapon_MeleeModule>();
            if (mem.Length > 0)
            {
                for (int i = 0; i < mem.Length; ++i)
                {
                    if (!mem[i].AutoBuild(gameManager, playerManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckMeleeWeapon()
        {
            MeleeWeapon[] _meleeWeapons = FindObjectsOfType<MeleeWeapon>();
            if (_meleeWeapons.Length > 0)
            {
                for (int i = 0; i < _meleeWeapons.Length; ++i)
                {
                    if (!_meleeWeapons[i].AutoBuild(playerManager, gameManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckExWeapon()
        {
            ExWeapon[] _subWeapons = FindObjectsOfType<ExWeapon>();
            if (_subWeapons.Length > 0)
            {
                for (int i = 0; i < _subWeapons.Length; ++i)
                {
                    if (!_subWeapons[i].AutoBuild(playerManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckBot()
        {
            Bot[] b = FindObjectsOfType<Bot>();
            if (b.Length > 0)
            {
                for (int i = 0; i < b.Length; ++i)
                {
                    if (!b[i].AutoBuild(this.GetComponent<CombatGimmickBuilder>(), playerManager, gameManager, HitBoxLayerNum))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckPlayerTeleporter()
        {
            PlayerTeleporter[] tp = FindObjectsOfType<PlayerTeleporter>();
            if (tp.Length > 0)
            {
                for (int i = 0; i < tp.Length; ++i)
                {
                    if (!tp[i].AutoBuild(playerManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckPlayerCatapult()
        {
            PlayerCatapult[] pc = FindObjectsOfType<PlayerCatapult>();
            if (pc.Length > 0)
            {
                for (int i = 0; i < pc.Length; ++i)
                {
                    if (!pc[i].AutoBuild(playerManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckDamageArea()
        {
            DamageArea[] _damageArea = FindObjectsOfType<DamageArea>();
            if (_damageArea.Length > 0)
            {
                for (int i = 0; i < _damageArea.Length; ++i)
                {
                    if (!_damageArea[i].AutoBuild(playerManager, gameManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckDropItem()
        {
            DropItem[] _dropItems = FindObjectsOfType<DropItem>();
            if (_dropItems.Length > 0)
            {
                for (int i = 0; i < _dropItems.Length; ++i)
                {
                    if (!_dropItems[i].AutoBuild(playerManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckFlag()
        {
            Flag[] _flags = FindObjectsOfType<Flag>();
            if (_flags.Length > 0)
            {
                for (int i = 0; i < _flags.Length; ++i)
                {
                    if (!_flags[i].AutoBuild(gameManager, playerManager, assigners, scoreManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckConquestZone()
        {
            ConquestZone[] _conquestZones = FindObjectsOfType<ConquestZone>();
            if (_conquestZones.Length > 0)
            {
                for (int i = 0; i < _conquestZones.Length; ++i)
                {
                    if (!_conquestZones[i].AutoBuild(playerManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckTurret()
        {
            Turret[] _turrets = FindObjectsOfType<Turret>();
            if (_turrets.Length > 0)
            {
                for (int i = 0; i < _turrets.Length; ++i)
                {
                    if (!_turrets[i].AutoBuild(assigners, gameManager, playerManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckMissile()
        {
            Missile[] _missiles = FindObjectsOfType<Missile>();
            if (_missiles.Length > 0)
            {
                for (int i = 0; i < _missiles.Length; ++i)
                {
                    if (!_missiles[i].AutoBuild(gameManager, playerManager, assigners, botHitBoxes, HitBoxLayerNum))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckPointerModule()
        {
            PointerModule[] _pointerModules = FindObjectsOfType<PointerModule>();
            if (_pointerModules.Length > 0)
            {
                for (int i = 0; i < _pointerModules.Length; ++i)
                {
                    if (!_pointerModules[i].AutoBuild(playerManager, gameManager, HitBoxLayerNum, assigners, botHitBoxes))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckGameManagerModifier()
        {
            GameManagerModifier[] _gameManagerModifiers = FindObjectsOfType<GameManagerModifier>();
            if (_gameManagerModifiers.Length > 0)
            {
                for (int i = 0; i < _gameManagerModifiers.Length; ++i)
                {
                    if (!_gameManagerModifiers[i].AutoBuild(gameManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckSpectatorCamera()
        {
            SpectatorCamera[] _spectatorCameras = FindObjectsOfType<SpectatorCamera>();
            if (_spectatorCameras.Length > 0)
            {
                for (int i = 0; i < _spectatorCameras.Length; ++i)
                {
                    if (!_spectatorCameras[i].AutoBuild(gameManager, assigners))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckMovingPlatformManager()
        {
            MovingPlatformManager[] _movingPlatformManagers = FindObjectsOfType<MovingPlatformManager>();

            if (_movingPlatformManagers.Length == 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> MovingPlatformManagerがシーンにありません.");
                return false;
            }
            if (_movingPlatformManagers.Length > 1)
            {
                Debug.Log("<color=#ff0000>Warning:</color> MovingPlatformManagerが2つ以上あります.", _movingPlatformManagers[0].gameObject);
                return false;
            }

            movingPlatformManager = _movingPlatformManagers[0];

            return true;
        }
        //---------------------------------------
        public bool CheckMovingPlatformAnchorTrigger()
        {
            MovingPlatformAnchorTrigger[] _movingPlatformAnchorTriggers = FindObjectsOfType<MovingPlatformAnchorTrigger>();
            if (_movingPlatformAnchorTriggers.Length > 0)
            {
                for (int i = 0; i < _movingPlatformAnchorTriggers.Length; ++i)
                {
                    if (!_movingPlatformAnchorTriggers[i].AutoBuild(movingPlatformManager))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckSubBullet()
        {
            SubBullet[] _subBullets = FindObjectsOfType<SubBullet>();
            if (_subBullets.Length > 0)
            {
                for (int i = 0; i < _subBullets.Length; ++i)
                {
                    if (!_subBullets[i].AutoBuild(playerManager, gameManager, assigners))
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public bool CheckTransformRandomizer()
        {
            TransformRandomizer[] _TransformRandomizers = FindObjectsOfType<TransformRandomizer>();
            if (_TransformRandomizers.Length > 0)
            {
                for (int i = 0; i < _TransformRandomizers.Length; ++i)
                {
                    if (!_TransformRandomizers[i].AutoBuild())
                    {
                        return false;
                    }
                }
            }

            return true;    //チェック成功
        }
        //---------------------------------------
        public void AddComponents()
        {
#if UNITY_EDITOR

            PlayerManager[] playerManagers = FindObjectsOfType<PlayerManager>();
            if (playerManagers.Length > 0)
            {
                for (int i = 0; i < playerManagers.Length; ++i)
                {
                    if (!playerManagers[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = playerManagers[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }
                }
            }

            GameManager[] gameManagers = FindObjectsOfType<GameManager>();
            if (gameManagers.Length > 0)
            {
                for (int i = 0; i < gameManagers.Length; ++i)
                {
                    if (!gameManagers[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = gameManagers[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }
                }
            }

            MusicManager[] musicManagers = FindObjectsOfType<MusicManager>();
            if (musicManagers.Length > 0)
            {
                for (int i = 0; i < musicManagers.Length; ++i)
                {
                    if (!musicManagers[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = musicManagers[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = true;
                        a.loop = true;
                    }
                }
            }

            Assigner[] assigners = FindObjectsOfType<Assigner>();
            if (assigners.Length > 0)
            {
                for (int i = 0; i < assigners.Length; ++i)
                {
                    if (!assigners[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = assigners[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }
                }
            }

            Bullet[] bullet = FindObjectsOfType<Bullet>();
            if (bullet.Length > 0)
            {
                for (int i = 0; i < bullet.Length; ++i)
                {
                    if (!bullet[i].GetComponent<ParticleSystem>())
                    {
                        ParticleSystem p = bullet[i].gameObject.AddComponent<ParticleSystem>();
                        SetUpBulletParticleSystem(p);
                    }

                    if (!bullet[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = bullet[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }

                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(bullet[i].GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    bullet[i].GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;
                }
            }

            Bot[] bot = FindObjectsOfType<Bot>();
            if (bot.Length > 0)
            {
                for (int i = 0; i < bot.Length; ++i)
                {
                    if (!bot[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = bot[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }

                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(bot[i].GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    bot[i].GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;
                }
            }

            BotHitBox[] botHitBox = FindObjectsOfType<BotHitBox>();
            if (botHitBox.Length > 0)
            {
                for (int i = 0; i < botHitBox.Length; ++i)
                {
                    if (!botHitBox[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = botHitBox[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }

                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(botHitBox[i].GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    botHitBox[i].GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;
                }
            }

            PlayerHitBox[] playerHitBox = FindObjectsOfType<PlayerHitBox>();
            if (playerHitBox.Length > 0)
            {
                for (int i = 0; i < playerHitBox.Length; ++i)
                {
                    if (!playerHitBox[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = playerHitBox[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }

                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(playerHitBox[i].GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    playerHitBox[i].GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;
                }
            }

            RangedWeapon_MainModule[] mainModule = FindObjectsOfType<RangedWeapon_MainModule>();
            if (mainModule.Length > 0)
            {
                for (int i = 0; i < mainModule.Length; ++i)
                {
                    if (!mainModule[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = mainModule[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }

                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(mainModule[i].GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    mainModule[i].GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;
                }
            }

            RangedWeapon_SubModule[] subModule = FindObjectsOfType<RangedWeapon_SubModule>();
            if (subModule.Length > 0)
            {
                for (int i = 0; i < subModule.Length; ++i)
                {
                    if (!subModule[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = subModule[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }

                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(subModule[i].GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    subModule[i].GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;
                }
            }

            PlayerTeleporter[] teleporter = FindObjectsOfType<PlayerTeleporter>();
            if (teleporter.Length > 0)
            {
                for (int i = 0; i < teleporter.Length; ++i)
                {
                    if (!teleporter[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = teleporter[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }
                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(teleporter[i].GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    teleporter[i].GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;

                    if (!teleporter[i].GetComponent<Collider>())
                    {
                        BoxCollider c = teleporter[i].gameObject.AddComponent<BoxCollider>();
                        c.isTrigger = true;
                        c.center = Vector3.zero;
                        c.size = Vector3.one;
                    }
                    if (!teleporter[i].GetDestination().GetComponent<AudioSource>())
                    {
                        AudioSource a = teleporter[i].GetDestination().gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }
                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(teleporter[i].GetDestination().GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    teleporter[i].GetDestination().GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;
                }
            }

            PlayerCatapult[] catapult = FindObjectsOfType<PlayerCatapult>();
            if (catapult.Length > 0)
            {
                for (int i = 0; i < catapult.Length; ++i)
                {
                    if (!catapult[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = catapult[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }
                    if (!catapult[i].GetComponent<Collider>())
                    {
                        BoxCollider c = catapult[i].gameObject.AddComponent<BoxCollider>();
                        c.isTrigger = true;
                        c.center = Vector3.zero;
                        c.size = Vector3.one;
                    }

                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(catapult[i].GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    catapult[i].GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;
                }
            }

            SimpleButton[] simpleButtons = FindObjectsOfType<SimpleButton>();
            if (simpleButtons.Length > 0)
            {
                for (int i = 0; i < simpleButtons.Length; ++i)
                {
                    if (!simpleButtons[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = simpleButtons[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }
                }
            }

            DamageArea[] damageAreas = FindObjectsOfType<DamageArea>();
            if (damageAreas.Length > 0)
            {
                for (int i = 0; i < damageAreas.Length; ++i)
                {
                    if (!damageAreas[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = damageAreas[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }
                }
            }

            MeleeWeapon[] meleeWeapons = FindObjectsOfType<MeleeWeapon>();
            if (meleeWeapons.Length > 0)
            {
                for (int i = 0; i < meleeWeapons.Length; ++i)
                {
                    if (!meleeWeapons[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = meleeWeapons[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }

                    if (!meleeWeapons[i].GetComponent<Collider>())
                    {
                        BoxCollider c = meleeWeapons[i].gameObject.AddComponent<BoxCollider>();
                        c.isTrigger = true;
                        c.center = Vector3.zero;
                        c.size = Vector3.one;
                    }

                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(meleeWeapons[i].GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    meleeWeapons[i].GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;
                }
            }

            Turret[] turrets = FindObjectsOfType<Turret>();
            if (turrets.Length > 0)
            {
                for (int i = 0; i < turrets.Length; ++i)
                {
                    if (!turrets[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = turrets[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }

                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(turrets[i].GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    turrets[i].GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;
                }
            }

            Missile[] missiles = FindObjectsOfType<Missile>();
            if (missiles.Length > 0)
            {
                for (int i = 0; i < missiles.Length; ++i)
                {
                    if (!missiles[i].GetComponent<Collider>())
                    {
                        BoxCollider c = missiles[i].gameObject.AddComponent<BoxCollider>();
                        c.enabled = false;
                        c.isTrigger = false;
                        c.size = new Vector3(0.1f, 0.1f, 0.1f);
                        c.center = Vector3.zero;
                    }

                    if (!missiles[i].GetComponent<Rigidbody>())
                    {
                        Rigidbody r = missiles[i].gameObject.AddComponent<Rigidbody>();
                        r.useGravity = false;
                        r.isKinematic = false;
                    }

                    if (!missiles[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = missiles[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }

                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(missiles[i].GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    missiles[i].GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;
                }
            }

            PointerModule[] pointerModules = FindObjectsOfType<PointerModule>();
            if (pointerModules.Length > 0)
            {
                for (int i = 0; i < pointerModules.Length; ++i)
                {
                    if (!pointerModules[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = pointerModules[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }

                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(pointerModules[i].GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    pointerModules[i].GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;
                }
            }

            DropItem[] dropItems = FindObjectsOfType<DropItem>();
            if (dropItems.Length > 0)
            {
                for (int i = 0; i < dropItems.Length; ++i)
                {
                    if (!dropItems[i].GetComponent<Collider>())
                    {
                        BoxCollider c = dropItems[i].gameObject.AddComponent<BoxCollider>();
                        c.isTrigger = true;
                        c.center = Vector3.zero;
                        c.size = Vector3.one;
                    }

                    if (!dropItems[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = dropItems[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }
                }
            }

            Flag[] flags = FindObjectsOfType<Flag>();
            if (flags.Length > 0)
            {
                for (int i = 0; i < flags.Length; ++i)
                {
                    if (!flags[i].GetComponent<Collider>())
                    {
                        BoxCollider c = dropItems[i].gameObject.AddComponent<BoxCollider>();
                        c.isTrigger = true;
                        c.center = Vector3.zero;
                        c.size = Vector3.one;
                    }

                    if (!flags[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = flags[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }
                }
            }

            ConquestZone[] conquestZones = FindObjectsOfType<ConquestZone>();
            if (conquestZones.Length > 0)
            {
                for (int i = 0; i < conquestZones.Length; ++i)
                {
                    if (!conquestZones[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = conquestZones[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }
                }
            }

            ExWeapon[] _exWeapons = FindObjectsOfType<ExWeapon>();
            if (_exWeapons.Length > 0)
            {
                for (int i = 0; i < _exWeapons.Length; ++i)
                {
                    if (!_exWeapons[i].GetComponent<AudioSource>())
                    {
                        AudioSource a = _exWeapons[i].gameObject.AddComponent<AudioSource>();
                        a.playOnAwake = false;
                        a.loop = false;
                    }

                    if (!_exWeapons[i].GetComponent<Collider>())
                    {
                        BoxCollider c = _exWeapons[i].gameObject.AddComponent<BoxCollider>();
                        c.isTrigger = true;
                        c.center = Vector3.zero;
                        c.size = Vector3.one;
                    }

                    AutoAddSpatialAudioComponents.AddVRCSpatialToBareAudioSource(_exWeapons[i].GetComponent<AudioSource>());    //VRC_SpatialAudioSourceを追加
                    _exWeapons[i].GetComponent<VRC_SpatialAudioSource>().EnableSpatialization = true;
                }
            }
#endif
        }
        //---------------------------------------
        public void SetUpBulletParticleSystem(ParticleSystem p)
        {
            LayerMask bulletHitLayerMask = 1 << 0 | 1 << HitBoxLayerNum;

            var main = p.main;
            main.loop = false;
            main.playOnAwake = false;
            main.simulationSpace = ParticleSystemSimulationSpace.World;

            var colModule = p.collision;
            colModule.enabled = true;
            colModule.type = ParticleSystemCollisionType.World;
            colModule.collidesWith = bulletHitLayerMask;
            colModule.sendCollisionMessages = true;
            colModule.lifetimeLoss = 1;
            colModule.bounce = 0;

            var emissionModule = p.emission;
            emissionModule.rateOverDistance = 0;
            emissionModule.rateOverTime = 0;
            emissionModule.burstCount = 1;
        }
        //---------------------------------------
    }
}
