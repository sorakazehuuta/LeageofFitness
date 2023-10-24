
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public enum ConquestZoneType
{
    Constant, Linear
}

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class ConquestZone : UdonSharpBehaviour
    {
        //---------------------------------------
        [Tooltip("点数の加算方式")] public ConquestZoneType conquestZoneType;
        [Tooltip("Linearモードで1点になる最小距離")] [SerializeField] float minDistamce;
        [Tooltip("Linearモードで0点になる最大距離")] [SerializeField] float maxDistamce;
        //---------------------------------------
        [Header("その他(任意項目.無しでも動作可能)")]
        [Tooltip("ゾーン内にいるときに表示されるオブジェクト")] [SerializeField] GameObject InZoneMarker;
        [Tooltip("ゾーン侵入時に発動する効果音")] [SerializeField] AudioClip InZoneClip;
        //---------------------------------------
        //[Header("手動で変更しないこと")]
        [HideInInspector] public bool InZone;
        //---------------------------------------
        //[Header("AutoBuild時に自動で設定")]
        [HideInInspector] public PlayerManager playerManager;
        //---------------------------------------
        Collider collider;
        AudioSource audioSource;
        //---------------------------------------
        void Start()
        {
            audioSource = this.GetComponent<AudioSource>();
            collider = this.GetComponent<Collider>();
            if (InZoneMarker) { InZoneMarker.SetActive(false); }
        }
        //---------------------------------------
        public void ResetOnStartGame()
        {
            InZone = false;
        }
        //---------------------------------------
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (!Utilities.IsValid(Networking.LocalPlayer)) { return; }
            if (!player.isLocal) { return; }

            InZone = true; 
            ShowInZoneMarker();

            if (InZoneClip) { audioSource.PlayOneShot(InZoneClip); }
        }
        //---------------------------------------
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (Utilities.IsValid(Networking.LocalPlayer) && Networking.LocalPlayer == player) { InZone = false; }
            HideInZoneMarker();
        }
        //---------------------------------------
        public override void OnPlayerRespawn(VRCPlayerApi player)
        {
            if (Utilities.IsValid(Networking.LocalPlayer) && Networking.LocalPlayer == player) { InZone = false; }
            HideInZoneMarker();
        }
        //---------------------------------------
        public void OnDisable()
        {
            InZone = false;
        }
        //---------------------------------------
        public void ShowInZoneMarker()
        {
            if (InZoneMarker) { InZoneMarker.SetActive(true); }
        }
        //---------------------------------------
        public void HideInZoneMarker()
        {
            if (InZoneMarker) { InZoneMarker.SetActive(false); }
        }
        //---------------------------------------
        public float CalculateAddScore()
        {
            //playerManager.playerHitBox != nullの状態で呼び出すこと

            if (!InZone) return 0;

            if (conquestZoneType == ConquestZoneType.Constant) { return 1; }

            if (conquestZoneType == ConquestZoneType.Linear && Utilities.IsValid(Networking.LocalPlayer))
            {
                float distance = (Networking.LocalPlayer.GetPosition() - this.transform.position).magnitude;
                if (distance < minDistamce) { return 1; }
                else if (maxDistamce < distance) { return 0; }
                else { return (maxDistamce - distance) / (maxDistamce - minDistamce); }
            }

            else return 0;  //エラー回避
        }
        //---------------------------------------
        public bool AutoBuild(PlayerManager _playerManager)
        {
            playerManager = _playerManager;
            return true;
        }
        //---------------------------------------
    }
}

