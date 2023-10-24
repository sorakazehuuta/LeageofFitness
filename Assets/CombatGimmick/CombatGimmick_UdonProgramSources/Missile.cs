
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

//---------------------------------------
//誘導ミサイル制御用のクラス
//Bulletクラスと併用する
//被弾判定は撃たれた側のプレイヤーで行う
//---------------------------------------

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Missile : UdonSharpBehaviour
    {
        //---------------------------------------
        const int BlueTeam = 1;
        const int GreenTeam = 2;
        const int NoneTeam = 0;
        const int NoneIndex = -1;
        //---------------------------------------
        [Header("ミサイルの性能")]
        [Tooltip("飛行速度")] [SerializeField, Range(0, 180)] float SeekerAngle = 180.0f;
        [Tooltip("飛行速度")] [SerializeField] float Velocity = 20f;
        [Tooltip("誘導性能")] [SerializeField, Range(0, 1)] float LerpValue = 0.05f;
        [Tooltip("設定時間経過で自爆")] [SerializeField] float LifeTime = 10;
        [Tooltip("同時発射数")] [SerializeField] int BurstNum = 1;
        [Tooltip("同時発射のディレイ")] [SerializeField] float BurstInterval = 0.15f;
        [Tooltip("ダメージ")] [SerializeField] float Damage = 30;
        [Tooltip("0以上なら設定した秒数経過後に誘導開始")] [SerializeField] float StartGuidanceDelay = 0.5f;
        //---------------------------------------
        [Header("任意項目.無しでも動作可能")]
        [Tooltip("ミサイル本体のモデル")] [SerializeField] GameObject ModelObject;
        [Tooltip("ミサイルの発射音")] public AudioClip FireAudioClip;
        [Tooltip("ミサイルの飛翔音")] [SerializeField] AudioClip FlightAudioClip;
        [Tooltip("ミサイルの爆発音")] [SerializeField] AudioClip ExplodeAudioClip;
        [Tooltip("爆発時のエフェクト")] [SerializeField] ParticleSystem ExplodeParticle;
        [Tooltip("発射から被弾判定を出すまでのディレイ")] [SerializeField] float ActivateDelay = 0.2f;
        //---------------------------------------        
        //[Header("手動で変更しないこと")]
        [HideInInspector] public bool isMaster = true;  //発射する前の原本オブジェクトであればtrue
        [HideInInspector] public int MasterTargetIndex = -1;
        [HideInInspector] public int MasterCarrierPlayerHitBoxIndex;  //射撃時に武器を持っていたプレイヤーのPlayerHitBoxのインデックス
        [HideInInspector] public int TargetIndex = -1;                //Instantiateしたオブジェクトはこちらを使用
        [HideInInspector] public int CarrierPlayerHitBoxIndex;        //Instantiateしたオブジェクトはこちらを使用
        //ターゲットの番号(0以上なら有効)
        //iならplayerHitBoxes[i]を狙う.
        //playerHitBoxes.Length = Pとして、P + iならbotHitBoxes[i]を狙う.
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        [HideInInspector] [Tooltip("GameManager")] public GameManager gameManager;
        [HideInInspector] [Tooltip("PlayerHitBox[]")] public PlayerHitBox[] playerHitBoxes;
        [HideInInspector] [Tooltip("BotHitBox[]")] public BotHitBox[] botHitBoxes;
        [HideInInspector] [Tooltip("RangedWeapon_MainModule")] public RangedWeapon_MainModule mainModule;
        [HideInInspector] [Tooltip("AudioSource")] public AudioSource audioSource;
        //---------------------------------------
        bool isAlive;
        float launchTime;
        Transform TargetTransform;
        //---------------------------------------      
        private void Start()
        {
            this.GetComponent<Collider>().enabled = false;
            if (ModelObject) { ModelObject.SetActive(false); } 
            if (this.GetComponent<TrailRenderer>()) { this.GetComponent<TrailRenderer>().enabled = false; }
        }
        //---------------------------------------
        public bool CheckTargetLocked()
        {
            if (isMaster)
            {
                if (MasterTargetIndex < 0 || playerHitBoxes.Length + botHitBoxes.Length <= MasterTargetIndex) { return false; }
                return true;
            }
            if (TargetIndex < 0 || playerHitBoxes.Length + botHitBoxes.Length <= TargetIndex) { return false; }
            return true;
        }
        //---------------------------------------
        public void Launch()
        {
            if (BurstNum <= 0) { return; }

            for (int i = 0; i < BurstNum; ++i)
            {
                SendCustomEventDelayedSeconds("DelayedSecond_BurstLaunch", BurstInterval * i);
            }
            return;
        }
        //---------------------------------------
        public void DelayedSecond_BurstLaunch()
        {
            if (!CheckTargetLocked()) { return; }

            GameObject newMissile = VRCInstantiate(this.gameObject);
            newMissile.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
            newMissile.GetComponent<Missile>().TargetIndex = MasterTargetIndex;
            newMissile.GetComponent<Missile>().CarrierPlayerHitBoxIndex = MasterCarrierPlayerHitBoxIndex;

            const float delay = 0.1f;
            newMissile.GetComponent<Missile>().SendCustomEventDelayedSeconds("DelayedSecond_InitializeOnLaunch", delay);    //Start実行後に発動
        }
        //---------------------------------------
        public void DelayedSecond_InitializeOnLaunch()
        {
            isMaster = false;

            if (!CheckTargetIsAlive())
            {
                Explode();
                return;
            }

            if (!CheckTargetLocked())
            {
                Explode();
                return;
            }

            isAlive = true;
            launchTime = Time.time;
            
            if(ActivateDelay <= 0) { this.GetComponent<Collider>().enabled = true; }
            else { SendCustomEventDelayedSeconds("DelayedSecond_EnableCollider", ActivateDelay); }

            if (this.GetComponent<TrailRenderer>()) { this.GetComponent<TrailRenderer>().enabled = true; }
            if (ModelObject) { ModelObject.SetActive(true); }            

            SetTargetTransform();

            if (FlightAudioClip)
            {
                audioSource.clip = FlightAudioClip;
                audioSource.loop = true;
                audioSource.Play();
                audioSource.enabled = true;
            }
        }
        //---------------------------------------
        public void DelayedSecond_EnableCollider()
        {
            if (!CheckTargetIsAlive())
            {
                Explode();
                return;
            }

            this.GetComponent<Collider>().enabled = true;
        }
        //---------------------------------------
        public void SetTargetTransform()
        {
            if (TargetIndex < playerHitBoxes.Length) { TargetTransform = playerHitBoxes[TargetIndex].transform; }
            else if (TargetIndex < playerHitBoxes.Length + botHitBoxes.Length) { TargetTransform = botHitBoxes[TargetIndex - playerHitBoxes.Length].transform; }
        }
        //---------------------------------------
        private void FixedUpdate()
        {
            if (isMaster) { return; }
            if (!isMaster && !isAlive) { return; }
            
            if (Time.time - launchTime > LifeTime && isAlive) { Explode(); }
            if (!CheckTargetIsAlive()) { Explode(); }

            Guidance();

            float deltaTime = Time.deltaTime;
            this.transform.position = this.transform.position + this.transform.forward * Velocity * deltaTime;
        }
        //---------------------------------------
        public void Guidance()
        {
            if (Vector3.Angle(this.transform.forward, TargetTransform.position - this.transform.position) > SeekerAngle) { return; }
            if (Time.time - launchTime > StartGuidanceDelay)
            {
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(TargetTransform.position - this.transform.position, Vector3.up), LerpValue);
            }
        }
        //---------------------------------------
        public bool CheckTargetIsAlive()
        {
            if (TargetIndex < playerHitBoxes.Length)
            {
                if (playerHitBoxes[TargetIndex].SyncedHitPoint <= 0) { return false; }
                else return true;
            }

            if (TargetIndex < playerHitBoxes.Length + botHitBoxes.Length)
            {
                if (botHitBoxes[TargetIndex - playerHitBoxes.Length].GetBot().isAlive) { return true; }
                else return false;
            }

            return false;
        }
        //---------------------------------------
        public void Explode()
        {
            isAlive = false;
            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<Rigidbody>().isKinematic = true;  //物理演算を止める
            if (ModelObject) { ModelObject.SetActive(false); }            
            if (this.GetComponent<TrailRenderer>()) { this.GetComponent<TrailRenderer>().enabled = false; }

            if (ExplodeAudioClip)
            {
                audioSource.Stop();
                audioSource.loop = false;
                audioSource.PlayOneShot(ExplodeAudioClip, 1.0f);
            }
            else
            {
                audioSource.Stop();
            }

            if (ExplodeParticle) { ExplodeParticle.Play(); }

            const float delay = 2.0f;
            SendCustomEventDelayedSeconds("DelayedSecond_Destroy", delay);
        }
        //---------------------------------------
        public void DelayedSecond_Destroy()
        {
            Destroy(this.gameObject);
        }
        //---------------------------------------
        public void OnCollisionEnter(Collision collision)
        {
            if (!playerManager.playerHitBox)
            {
                Explode();
                return;
            }

            if (collision.collider.GetComponent<PlayerHitBox>())
            {
                if (playerManager.playerHitBox == collision.collider.GetComponent<PlayerHitBox>())
                {
                    if (Utilities.IsValid(Networking.LocalPlayer) && mainModule.Pickup.currentPlayer == Networking.LocalPlayer) { return; }    //自爆した場合は処理スキップ

                    //ローカルプレイヤーへのダメージ判定
                    playerManager.playerHitBox.TakeExplosiveDamage(Damage, true, CarrierPlayerHitBoxIndex);
                    Explode();
                    return;
                }
            }

            if (collision.collider.GetComponent<BotHitBox>())
            {
                if (mainModule.Pickup.currentPlayer == Networking.LocalPlayer)
                {
                    BotHitBox botHitBox = collision.collider.GetComponent<BotHitBox>();
                    botHitBox.TakeDamage(Damage, false);
                    Explode();
                    return;
                }
            }

            Explode();
            return;
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
        public override void OnDeserialization()
        {

        }
        //---------------------------------------
        public bool AutoBuild(GameManager _gameManager, PlayerManager pm, Assigner[] _assigners, BotHitBox[] _botHitBoxes, int HitBoxLayerNum)
        {
            this.gameObject.layer = HitBoxLayerNum;

            audioSource = this.GetComponent<AudioSource>();
            audioSource.enabled = false;

            playerManager = pm;
            gameManager = _gameManager;
            botHitBoxes = _botHitBoxes;
            playerHitBoxes = new PlayerHitBox[_assigners.Length];
            for (int i = 0; i < playerHitBoxes.Length; ++i) { playerHitBoxes[i] = _assigners[i].playerHitBox; }

            return true;
        }
        //---------------------------------------
    }
}

