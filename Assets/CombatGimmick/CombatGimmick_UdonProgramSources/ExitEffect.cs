
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class ExitEffect : UdonSharpBehaviour
    {
        //---------------------------------------
        [Tooltip("エフェクト消滅までの時間(秒)")] [SerializeField] float ExitEffectLength = 5;
        [Tooltip("プレイヤー離脱時に発動するパーティクル")] [SerializeField] ParticleSystem ExitParticle;
        [Tooltip("プレイヤー離脱時に発動する効果音")] [SerializeField] AudioClip ExitAudioClip;
        //---------------------------------------
        //[Header("手動で変更しないこと")]
        [HideInInspector] public bool isMaster = true;
        //---------------------------------------
        Vector3 LeftPlayerPos;
        //---------------------------------------
        public override void OnPlayerLeft(VRCPlayerApi leftPlayer)
        {
            if(isMaster == false) { return; }

            if (Utilities.IsValid(leftPlayer) && ExitEffectLength > 0)
            {
                LeftPlayerPos = leftPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;

                const float delay = 0.1f;
                SendCustomEventDelayedSeconds("DelayedSecond_SpawnExitEffect", delay);  //ディレイを挟まないとInvalidOperationExceptionが発生する
            }
        }
        //---------------------------------------
        public void DelayedSecond_SpawnExitEffect()
        {
            SpawnExitEffect(LeftPlayerPos);
        }
        //---------------------------------------
        public void SpawnExitEffect(Vector3 SpawnPos)
        {
            GameObject obj = VRCInstantiate(this.gameObject);
            obj.transform.position = SpawnPos;
            obj.GetComponent<ExitEffect>().PlayExitEffect();
        }
        //---------------------------------------
        public void PlayExitEffect()
        {
            isMaster = false;
            if (this.GetComponent<AudioSource>() && ExitAudioClip) { this.GetComponent<AudioSource>().PlayOneShot(ExitAudioClip, 1.0f); }
            if (ExitParticle) { ExitParticle.Play(); }

            SendCustomEventDelayedSeconds("DelayedSecond_DestroySelf", ExitEffectLength);
        }
        //---------------------------------------
        public void DelayedSecond_DestroySelf()
        {
            Destroy(this.gameObject);
        }
        //---------------------------------------
    }
}

