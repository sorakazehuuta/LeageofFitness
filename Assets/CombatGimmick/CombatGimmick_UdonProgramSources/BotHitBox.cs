
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
//using UnityEngine.UI;

namespace CombatGimmick
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class BotHitBox : HitBox
    {
        //---------------------------------------
        [Tooltip("この部位のダメージ倍率")] [SerializeField] float DamageMultiplier = 1;
        //---------------------------------------
        [Header("----任意項目.無しでも動作可能----")]
        [Header("このオブジェクトには最低1つColliderをつけること")]
        [Tooltip("被弾時に発動するパーティクル")] [SerializeField] ParticleSystem DamageParticle;
        [Tooltip("被弾時に発動する効果音")] [SerializeField] AudioClip DamageAudioClip;
        //---------------------------------------
        Bot bot;
        AudioSource audioSource;
        [HideInInspector] public BotHitPointType botHitPointType;
        [HideInInspector] public float AvatarParticleDamage;
        //---------------------------------------
        private void Start()
        {
            Initialize();
        }
        //---------------------------------------
        public void TakeDamage(float dmg, bool isAvatarParticle)
        {
            if (bot && bot.Invincible) { return; }
            if (bot) { bot.TakeDamage(dmg * DamageMultiplier, isAvatarParticle); }

            if (DamageParticle) { DamageParticle.Play(); }
            if (DamageAudioClip) { audioSource.PlayOneShot(DamageAudioClip, 1.0f); }
        }
        //---------------------------------------
        public void OnParticleCollision(GameObject other)
        {
            if (!Utilities.IsValid(other) && botHitPointType == BotHitPointType.Sync_EnableAvatarParticle)
            {
                //アバターのパーティクルを被弾した場合(アクセス制限によりUtilities.IsValid(other) == false)になる
                TakeDamage(AvatarParticleDamage, true);
            }
        }
        //---------------------------------------
        public void EnableColliders()
        {
            Collider[] col = this.GetComponents<Collider>();
            foreach(Collider c in col)
            {
                c.enabled = true;
            }
        }
        //---------------------------------------
        public void DisableColliders()
        {
            Collider[] col = this.GetComponents<Collider>();
            foreach (Collider c in col)
            {
                c.enabled = false;
            }
        }
        //---------------------------------------
        public override void Initialize()
        {
            audioSource = this.GetComponent<AudioSource>();
        }
        //---------------------------------------
        public void SetBot(Bot b)
        {
            bot = b;
        }
        //---------------------------------------
        public Bot GetBot()
        {
            return bot;
        }
        //---------------------------------------
        public bool AutoBuild(Bot b, CombatGimmickBuilder builder, int HitBoxLayerNum)
        {
            this.gameObject.layer = HitBoxLayerNum;

            bot = b;
            if (!this.GetComponent<Collider>())
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " にColliderコンポーネントがありません.", this.gameObject);
                return false;
            }

            if(this.GetComponent<Collider>().isTrigger == true)
            {
                this.GetComponent<Collider>().isTrigger = false;
            }

            return true;
        }
        //---------------------------------------
    }
}

