
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class PlayerCatapult : UdonSharpBehaviour
    {
        //---------------------------------------
        [Header("必須項目.無いとエラー")]
        [Tooltip("プレイヤーを飛ばす方向(Z方向)")] [SerializeField] Transform Direction;
        [Tooltip("プレイヤーを飛ばす勢い")] [SerializeField] float Impulse;
        //---------------------------------------
        [Header("任意項目.無しでも動作可能")]
        [Tooltip("射出時の効果音")] [SerializeField] AudioClip CatapultClip;
        [Tooltip("射出時のエフェクト")] [SerializeField] ParticleSystem CatapultParticle;
        //---------------------------------------
        [Header("その他")]
        [Tooltip("falseなら射出前のスピードを無視")] [SerializeField] bool InheritVelocity;
        [Tooltip("trueならエフェクトを同期")] [SerializeField] bool SyncEffect;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        //---------------------------------------
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (player.isLocal)
            {
                Vector3 currentSpeed;
                if (InheritVelocity)
                {
                    currentSpeed = Networking.LocalPlayer.GetVelocity();
                }
                else
                {
                    currentSpeed = Vector3.zero;
                }
                Networking.LocalPlayer.SetVelocity(currentSpeed + Impulse * Direction.forward);
                
                if (SyncEffect)
                {
                    Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Networking_PlayCatapultEffect");
                }
                else
                {
                    PlayLocalCatapultEffect();
                }
            }
        }
        //---------------------------------------
        public void Networking_PlayCatapultEffect()
        {
            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                PlayLocalCatapultEffect();
            }
            else
            {
                PlayRemoteCatapultEffect();
            }
        }
        //---------------------------------------
        public void PlayRemoteCatapultEffect()
        {
            if (CatapultClip) { this.GetComponent<AudioSource>().PlayOneShot(CatapultClip, 1.0f); }
            if (CatapultParticle) { CatapultParticle.Play(); }
        }
        //---------------------------------------
        public void PlayLocalCatapultEffect()
        {
            if (CatapultClip) { playerManager.GetComponent<AudioSource>().PlayOneShot(CatapultClip, 1.0f); }
            if (CatapultParticle) { CatapultParticle.Play(); }
        }
        //---------------------------------------
        public bool AutoBuild(PlayerManager pm)
        {
            playerManager = pm;

            return true;
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
    }
}


