
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class DamageArea : UdonSharpBehaviour
    {
        //---------------------------------------
        [Tooltip("ダメージ・回復が発動する間隔")] [SerializeField] float DamageInterval = 1;
        [Tooltip("プラスならダメージ、マイナスなら回復")] [SerializeField] float DamageValue = 20;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        //---------------------------------------
        AudioSource audioSource;
        float lastDamageTime;
        [HideInInspector] public bool inTrigger;
        //---------------------------------------
        private void Start()
        {
            audioSource = this.GetComponent<AudioSource>();
        }
        //---------------------------------------
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            //ダウン状態・または戦闘中でない場合、ダメージを与えない
            if (!playerManager.isAlive || !gameManager.SyncedInBattle || !gameManager.SyncedInGame) { return; }
            
            //ゲームに登録済みで、最後の判定から一定時間経過していれば、回復またはダメージ付与を行う
            if (Utilities.IsValid(Networking.LocalPlayer) && player == Networking.LocalPlayer && playerManager.isAlive)
            {
                inTrigger = true;

                if (Time.time - lastDamageTime > DamageInterval && playerManager.playerHitBox)
                {
                    ChangeHitPoint();
                    SendCustomEventDelayedSeconds("DelayedSecond_TryChangeHitPoint", DamageInterval); //一定時間後に再度判定を行う
                }
            }
        }
        //---------------------------------------
        public void DelayedSecond_TryChangeHitPoint()
        {
            if (inTrigger && playerManager.playerHitBox && playerManager.isAlive)
            {
                ChangeHitPoint();

                SendCustomEventDelayedSeconds("DelayedSecond_TryChangeHitPoint", DamageInterval); //一定時間後に再度判定を行う
            }
        }
        //---------------------------------------
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (Utilities.IsValid(Networking.LocalPlayer) && player == Networking.LocalPlayer)
            {
                inTrigger = false;
            }
        }
        //---------------------------------------
        public override void OnPlayerRespawn(VRCPlayerApi player)
        {
            if (Utilities.IsValid(Networking.LocalPlayer) && player == Networking.LocalPlayer)
            {
                inTrigger = false;
            }
        }
        //---------------------------------------
        public void OnDisable()
        {
            inTrigger = false;
        }
        //---------------------------------------
        public void ChangeHitPoint()
        {
            if (!playerManager.isAlive) { return; }

            //回復
            if (DamageValue < 0 && playerManager.playerHitBox.MaxPlayerHitPoint > playerManager.playerHitBox.SyncedHitPoint)
            {
                playerManager.playerHitBox.TakeDamage(DamageValue, true);
            }

            //ダメージ
            else if(DamageValue > 0 && 0 < playerManager.playerHitBox.SyncedHitPoint)
            {
                playerManager.playerHitBox.TakeDamage(DamageValue, true);
            }

            lastDamageTime = Time.time;
        }
        //---------------------------------------
        public bool AutoBuild(PlayerManager pm, GameManager _gameManager)
        {
            playerManager = pm;
            gameManager = _gameManager;

            return true;
        }
        //---------------------------------------
    }
}


