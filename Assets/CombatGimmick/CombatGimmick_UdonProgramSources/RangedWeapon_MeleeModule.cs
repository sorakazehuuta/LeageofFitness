
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class RangedWeapon_MeleeModule : Weapon
    {        
        //---------------------------------------
        [Header("必須項目.無いとエラー")]
        [Tooltip("攻撃力")] public float Damage;
        [Tooltip("攻撃スピード(秒)")] public float AttackInterval;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        [HideInInspector] [Tooltip("RangedWeapon_MainModule")] [SerializeField] RangedWeapon_MainModule mainModule;
        //---------------------------------------
        float LastHitTime;  //最後にヒットした時間
        //---------------------------------------
        public void OnTriggerEnter(Collider other)
        {
            //ゲームから脱落していればダメージを与えない
            if (playerManager.playerHitBox && playerManager.playerHitBox.SyncedReviveTicket <= 0) { return; }

            //ダウン状態・または戦闘中でない場合、ダメージを与えない
            if (!playerManager.isAlive || !gameManager.SyncedInBattle || !gameManager.SyncedInGame) { return; }

            //最後のヒットから一定時間経過していない場合、ダメージを与えない
            if (Time.time - LastHitTime < AttackInterval) { return; }

            //自分が持っていない武器はダメージを与えない
            if (!IsPickedupByMe()) { return; }

            //自分以外のヒットボックスに命中した場合にダメージ処理を実行
            if (other.GetComponent<HitBox>() && other.GetComponent<HitBox>() != playerManager.playerHitBox)
            {
                if (other.GetComponent<BotHitBox>())
                {
                    other.GetComponent<BotHitBox>().TakeDamage(Damage, false);
                    LastHitTime = Time.time;
                }
                else if (other.GetComponent<PlayerHitBox>())
                {
                    other.GetComponent<PlayerHitBox>().TakeDamage(Damage, false);
                    LastHitTime = Time.time;
                }
            }
        }
        //---------------------------------------
        public bool IsPickedupByMe()
        {
            if (!Utilities.IsValid(Networking.LocalPlayer)) { return false; }
            if (mainModule.Pickup.currentPlayer != Networking.LocalPlayer) { return false; }
            return true;
        }
        //---------------------------------------
        public void SetMainModule(RangedWeapon_MainModule mm)
        {
            mainModule = mm;
        }
        //---------------------------------------
        public bool AutoBuild(GameManager gm, PlayerManager pm)
        {
            playerManager = pm;
            gameManager = gm;
                        
            return true;
        }
        //---------------------------------------
    }
}

