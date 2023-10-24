
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [DefaultExecutionOrder(15)]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Bullet : UdonSharpBehaviour
    {
        //---------------------------------------
        [Header("範囲攻撃武器にする場合に設定")]
        [Tooltip("SubBullet")] public SubBullet subBullet;
        //---------------------------------------
        [Header("----ダメージ設定----")]
        [Tooltip("ダメージ")] [SerializeField] float Damage;
        //---------------------------------------
        RangedWeapon_MainModule mainModule;
        Turret turret;
        AudioSource audioSource;
        TeamName Team;
        //---------------------------------------
        void Start()
        {
            Initialize();
        }
        //---------------------------------------
        public void OnParticleCollision(GameObject other)
        {
            if (!Utilities.IsValid(other)) { return; }
            
            if (mainModule)
            {
                mainModule.DealDamage(Damage, other);
                return;
            }
            if (turret)
            {
                turret.DealDamage(Damage, other);
                return;
            }
        }
        //---------------------------------------
        public void SetMainModule(RangedWeapon_MainModule mm)
        {
            mainModule = mm;

            if (subBullet) { subBullet.SetMainModule(mm); }
        }        
        //---------------------------------------
        public void SetTurret(Turret t)
        {
            turret = t;
            if (subBullet) { subBullet.onTurret = true; }
        }
        //---------------------------------------
        public void SetTeam(TeamName t)
        {
            Team = t;

            if (!subBullet) { return; }
            subBullet.SetSyncedCarrierPlayerHitBoxIndex(); 
        }
        //---------------------------------------
        public void Initialize()
        {
            if (!this.GetComponent<ParticleSystem>())
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " にParticleSystemコンポーネントがありません.");
            }
            if (Damage < 0)
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " ダメージは0より大きくしてください.");
                Damage = 1;
            }

            audioSource = this.GetComponent<AudioSource>();
        }
        //---------------------------------------
        public void PlayFireSound(AudioClip clip)
        {
            audioSource.PlayOneShot(clip, 1.0f);
        }
        //---------------------------------------
    }
}


