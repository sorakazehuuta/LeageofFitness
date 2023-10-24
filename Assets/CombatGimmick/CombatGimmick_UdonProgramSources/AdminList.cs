
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    public enum ExecuteType
    {
        NoSync_Automatic, NoSync_Manual, Sync_Manual
    }

    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class AdminList : UdonSharpBehaviour
    {
        //---------------------------------------
        [Header("任意項目.無しでも動作可能")]
        [Tooltip("Admin権限を持つプレイヤーのVRCID")] [SerializeField] string[] AdminName;
        [Tooltip("Admin権限プレイヤーのみアクティブになるオブジェクト")] [SerializeField] GameObject[] AdminActiveObjects;
        [Tooltip("Admin権限プレイヤーのみ非アクティブになるオブジェクト")] [SerializeField] GameObject[] AdminDeactiveObjects;
        [Tooltip("オブジェクトを表示・非表示させるタイミング")] [SerializeField] ExecuteType executeType;
        [UdonSynced] bool SyncedAdminInteracted;
        //---------------------------------------
        void Start()
        {
            if (executeType != ExecuteType.NoSync_Automatic) { return; }

            if (Utilities.IsValid(Networking.LocalPlayer) && CheckLocalPlayerIsAdmin(Networking.LocalPlayer.displayName))
            {
                EnableAdminActiveObjects();
                DisableAdminDeactiveObjects();
            }
        }
        //---------------------------------------
        public void EnableAdminActiveObjects()
        {
            for (int i = 0; i < AdminActiveObjects.Length; ++i)
            {
                if (AdminActiveObjects[i]) { AdminActiveObjects[i].SetActive(true); }
            }
        }
        //---------------------------------------
        public void DisableAdminDeactiveObjects()
        {
            for (int i = 0; i < AdminDeactiveObjects.Length; ++i)
            {
                if (AdminDeactiveObjects[i]) { AdminDeactiveObjects[i].SetActive(false); }
            }
        }
        //---------------------------------------
        public override void Interact()
        {
            if (executeType == ExecuteType.NoSync_Automatic) { return; }

            if (executeType == ExecuteType.NoSync_Manual)
            {
                if (Utilities.IsValid(Networking.LocalPlayer) && CheckLocalPlayerIsAdmin(Networking.LocalPlayer.displayName))
                {
                    EnableAdminActiveObjects();
                    DisableAdminDeactiveObjects();
                }
                return;
            }

            else if(executeType == ExecuteType.Sync_Manual)
            {
                if (Utilities.IsValid(Networking.LocalPlayer) && CheckLocalPlayerIsAdmin(Networking.LocalPlayer.displayName))
                {
                    TrySetOwner(this.gameObject);
                    SyncedAdminInteracted = true;
                    Sync();
                }
                return;
            }
        }
        //---------------------------------------
        public bool CheckLocalPlayerIsAdmin(string localPlayerName)
        {
            for (int i = 0; i < AdminName.Length; ++i)
            {
                if (localPlayerName == AdminName[i])
                {
                    return true;
                }
            }

            return false;
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
        public void Sync()
        {
            RequestSerialization();
            if (CheckLocalPlayerIsOwner(this.gameObject))
            {
                OnDeserialization();
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
            if (executeType != ExecuteType.Sync_Manual) { return; }

            if (SyncedAdminInteracted)
            {
                EnableAdminActiveObjects();
                DisableAdminDeactiveObjects();
            }
        }
        //---------------------------------------
    }
}

