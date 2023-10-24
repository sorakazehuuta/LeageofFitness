
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class SubBullet : UdonSharpBehaviour
    {        
        //---------------------------------------
        [Header("----ダメージ設定----")]
        [Tooltip("ダメージ")] [SerializeField] float Damage = 10;
        [Tooltip("自爆ありならtrue")] [SerializeField] bool EnableLocalPlayerHit;
        //---------------------------------------
        //[Header("手動で変更しないこと")]
        [HideInInspector] public bool onTurret;
        [HideInInspector] public TeamName CarrierTeam;
        [HideInInspector] [UdonSynced] public int SyncedCarrierPlayerHitBoxIndex = -1;
        [HideInInspector] public int CarrierPlayerHitBoxIndex = -1;
        [HideInInspector] public SubBullet ClonedSubBullet;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        [HideInInspector] [Tooltip("PlayerHitBox[]")] public PlayerHitBox[] playerHitBoxes;
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        //---------------------------------------
        RangedWeapon_MainModule mainModule;
        //---------------------------------------
        void Start()
        {
            SyncedCarrierPlayerHitBoxIndex = -1;
        }
        //---------------------------------------
        public void OnParticleCollision(GameObject other)
        {
            if (!Utilities.IsValid(other)) { return; }
            if (!playerManager.playerHitBox) { return; }
            
            if (other.GetComponent<PlayerHitBox>()) { HitPlayerHitBox(other.GetComponent<PlayerHitBox>()); return; }
            if (other.GetComponent<BotHitBox>() && CheckLocalPlayerIsOwner(this.gameObject)) { other.GetComponent<BotHitBox>().TakeDamage(Damage, false); }
        }
        //---------------------------------------
        public void HitPlayerHitBox(PlayerHitBox _playerHitBox)
        {

            if (!playerManager.playerHitBox) { return; }   //ローカルプレイヤーが未登録
            if (playerManager.playerHitBox != _playerHitBox) { return; }   //他プレイヤーにヒット(被弾側で処理)
            if (!onTurret) { if (CarrierPlayerHitBoxIndex < 0 || playerHitBoxes.Length <= CarrierPlayerHitBoxIndex) { return; } }   //インデックスが不正

            //タレットの攻撃が自分に命中した場合
            if (onTurret && playerManager.playerHitBox == _playerHitBox)
            {
                playerManager.playerHitBox.TakeExplosiveDamage(Damage, true, -1);
                return;
            }

            //他プレイヤーの攻撃が自分に命中した場合
            if (playerManager.playerHitBox != playerHitBoxes[CarrierPlayerHitBoxIndex])
            {
                //FriendlyFireはしない
                if (gameManager.teamMode == TeamMode.TeamBattle && playerManager.playerHitBox.teamName == CarrierTeam) { return; }
                
                //敵の範囲攻撃を被弾した場合
                playerManager.playerHitBox.TakeExplosiveDamage(Damage, true, CarrierPlayerHitBoxIndex);
                return;
            }

            //自分の攻撃で自爆した場合
            if (playerManager.playerHitBox == playerHitBoxes[CarrierPlayerHitBoxIndex] && EnableLocalPlayerHit)
            {
                playerManager.playerHitBox.TakeExplosiveDamage(Damage, true, -1);
                return;
            }
        }
        //---------------------------------------
        public void SetMainModule(RangedWeapon_MainModule mm)
        {
            mainModule = mm;
        }
        //---------------------------------------
        public void UpdateCarrierPlayerHitBoxIndex(int newIndex)
        {
            //Debug.Log("SetSyncedCarrierPlayerHitBoxIndex() on SubModule, newIndex=" + newIndex);
            TrySetOwner(this.gameObject);
            SyncedCarrierPlayerHitBoxIndex = newIndex;
            Sync();
        }
        //---------------------------------------        
        public void SetSyncedCarrierPlayerHitBoxIndex()
        {
            int index = -1; 
            if (playerManager.playerHitBox)
            {
                for(int i = 0; i < playerHitBoxes.Length; ++i)
                {
                    if(playerHitBoxes[i] == playerManager.playerHitBox) { index = i; }
                }
            }

            TrySetOwner(this.gameObject);
            SyncedCarrierPlayerHitBoxIndex = index;
            Sync();
        }        
        //---------------------------------------
        public void TrySetOwner(GameObject obj)
        {
            if (Utilities.IsValid(Networking.LocalPlayer) && Networking.GetOwner(obj) != Networking.LocalPlayer)
            {
                Networking.SetOwner(Networking.LocalPlayer, obj);
            }
        }
        //---------------------------------------
        public bool CheckLocalPlayerIsOwner(GameObject obj)
        {
            if (!Utilities.IsValid(Networking.LocalPlayer))
            {
                return true;    //ローカルプレイヤーが存在しない場合(CliantSimを使用しない場合)
            }
            else if (Networking.GetOwner(obj) == Networking.LocalPlayer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //---------------------------------------
        public void Sync()
        {
            RequestSerialization();
            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                OnDeserialization();
            }
        }
        //---------------------------------------
        public override void OnDeserialization()
        {
            //Debug.Log("SyncedCarrierPlayerHitBoxIndex = " + SyncedCarrierPlayerHitBoxIndex);
            ClonedSubBullet.CarrierPlayerHitBoxIndex = SyncedCarrierPlayerHitBoxIndex;
            CarrierPlayerHitBoxIndex = SyncedCarrierPlayerHitBoxIndex;

            if (SyncedCarrierPlayerHitBoxIndex < 0 || playerHitBoxes.Length <= SyncedCarrierPlayerHitBoxIndex)
            {
                CarrierTeam = TeamName.None;
                ClonedSubBullet.CarrierTeam = TeamName.None;
            }
            else
            {
                CarrierTeam = playerHitBoxes[SyncedCarrierPlayerHitBoxIndex].teamName;
                ClonedSubBullet.CarrierTeam = playerHitBoxes[SyncedCarrierPlayerHitBoxIndex].teamName;
            }
        }
        //---------------------------------------
        public bool AutoBuild(PlayerManager pm, GameManager gm, Assigner[] _assigners)
        {
            gameManager = gm;
            playerManager = pm;

            playerHitBoxes = new PlayerHitBox[_assigners.Length];
            for (int i = 0; i < playerHitBoxes.Length; ++i) { playerHitBoxes[i] = _assigners[i].playerHitBox; }

            return true;
        }
        //---------------------------------------
    }
}

