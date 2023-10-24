
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class PlayerTeleporter : UdonSharpBehaviour
    {
        //---------------------------------------
        [Header("必須項目.無いとエラー")]
        [Tooltip("テレポート先の座標")] [SerializeField] Transform Destination;
        //---------------------------------------
        [Header("任意項目.無しでも動作可能")]
        [Tooltip("テレポート時の効果音")] [SerializeField] AudioClip TeleportClip;
        [Tooltip("テレポート元のエフェクト")] [SerializeField] ParticleSystem TeleportFromParticle;
        [Tooltip("テレポート先のエフェクト")] [SerializeField] ParticleSystem TeleportToParticle;
        //---------------------------------------
        [Header("その他")]
        [Tooltip("trueならエフェクトを同期")] [SerializeField] bool SyncEffect;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] [Tooltip("PlayerManager")] public PlayerManager playerManager;
        //---------------------------------------
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (player.isLocal)
            {
                Networking.LocalPlayer.SetVelocity(Vector3.zero);
                Networking.LocalPlayer.TeleportTo(Destination.position, Destination.rotation);

                if (SyncEffect)
                {
                    Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Networking_PlayTeleportEffect");
                }
                else
                {
                    PlayLocalTeleportEffect();
                }
            }
        }
        //---------------------------------------
        public void Networking_PlayTeleportEffect()
        {
            if(CheckLocalPlayerIsOwner(this.gameObject))
            {
                PlayLocalTeleportEffect();
            }
            else
            {
                PlayRemoteTeleportEffect();
            }
        }
        //---------------------------------------
        public void PlayRemoteTeleportEffect()
        {
            if (TeleportClip) { this.GetComponent<AudioSource>().PlayOneShot(TeleportClip, 1.0f); }
            if (TeleportFromParticle) { TeleportFromParticle.Play(); }
            if (TeleportToParticle) { TeleportToParticle.Play(); }
        }     
        //---------------------------------------
        public void PlayLocalTeleportEffect()
        {
            if (TeleportClip) { playerManager.GetComponent<AudioSource>().PlayOneShot(TeleportClip, 1.0f); }
            if (TeleportFromParticle) { TeleportFromParticle.Play(); }
            if (TeleportToParticle) { TeleportToParticle.Play(); }
        }
        //---------------------------------------
        public Transform GetDestination()
        {
            return Destination;
        }
        //---------------------------------------
        public bool AutoBuild(PlayerManager pm)
        {
            playerManager = pm;

            return true;
        }
        //---------------------------------------
        public override void OnOwnershipTransferred(VRCPlayerApi player)
        {
            base.OnOwnershipTransferred(player);
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


