
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    [ExecuteInEditMode]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class TransformRandomizer : UdonSharpBehaviour
    {
        //---------------------------------------
        [SerializeField] float RandomRadius;
        [SerializeField] float RandomHeight;
        [UdonSynced] Vector3 SyncedRandomValue;
        //---------------------------------------
        public void Randomize()
        {
            if (!CheckLocalPlayerIsOwner(this.gameObject)) { return; }

            float rad = Random.Range(0.0f, RandomRadius);
            float arg = Random.Range(0.0f, 360.0f) * Mathf.PI / 180.0f;
            float randX = rad * Mathf.Cos(arg);
            float randZ = rad * Mathf.Sin(arg);

            SyncedRandomValue = new Vector3(randX, Random.Range(-RandomHeight, RandomHeight), randZ);
            Sync();
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
            this.transform.localPosition = SyncedRandomValue;
        }
        //---------------------------------------
        public bool AutoBuild()
        {
            return true;
        }
        //---------------------------------------
    }
}
