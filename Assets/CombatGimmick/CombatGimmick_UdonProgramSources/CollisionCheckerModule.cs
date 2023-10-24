
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System;

namespace CombatGimmick
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CollisionCheckerModule : UdonSharpBehaviour
    {
        //---------------------------------------
        [Header("必須項目.無いとエラー")]
        [Tooltip("2点の間に障害物があれば射撃できない")] public Transform[] CollisionCheckTransform = new Transform[2];
        //---------------------------------------
        [Header("壁抜き判定.0はtrue推奨")]
        [Tooltip("判定対象のレイヤー")] [SerializeField] bool[] hitLayer = new bool[32];
        //---------------------------------------
        RangedWeapon_MainModule mainModule;
        LayerMask rayMask;
        RaycastHit _hitInfo;
        float CollisionCheckDistance;
        //---------------------------------------
        void Start()
        {
            Initialize();
        }
        //---------------------------------------
        public bool CollisionCheck()
        {
            if (Physics.Raycast(CollisionCheckTransform[0].position, CollisionCheckTransform[0].forward, out _hitInfo, CollisionCheckDistance, rayMask, QueryTriggerInteraction.Ignore))
            {
                return true;
            }
            return false;
        }
        //---------------------------------------
        public void SetMainModule(RangedWeapon_MainModule mm)
        {
            mainModule = mm;
        }
        //---------------------------------------
        public void Initialize()
        {
            if (!CollisionCheckTransform[0])
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " CollisionCheckTransform[0]が設定されていません");
            }
            if (!CollisionCheckTransform[1])
            {
                Debug.Log("<color=#ff0000>Warning:</color> " + this.gameObject.name + " CollisionCheckTransform[1]が設定されていません");
            }

            CollisionCheckDistance = (CollisionCheckTransform[0].position - CollisionCheckTransform[1].position).magnitude;
            CollisionCheckTransform[0].LookAt(CollisionCheckTransform[1]);


            //ヒットするレイヤーの設定
            rayMask = Convert.ToInt32(hitLayer[0]) << 0
                | Convert.ToInt32(hitLayer[1]) << 1
                | Convert.ToInt32(hitLayer[2]) << 2
                | Convert.ToInt32(hitLayer[3]) << 3
                | Convert.ToInt32(hitLayer[4]) << 4
                | Convert.ToInt32(hitLayer[5]) << 5
                | Convert.ToInt32(hitLayer[6]) << 6
                | Convert.ToInt32(hitLayer[7]) << 7
                | Convert.ToInt32(hitLayer[8]) << 8
                | Convert.ToInt32(hitLayer[9]) << 9
                | Convert.ToInt32(hitLayer[10]) << 10
                | Convert.ToInt32(hitLayer[11]) << 11
                | Convert.ToInt32(hitLayer[12]) << 12
                | Convert.ToInt32(hitLayer[13]) << 13
                | Convert.ToInt32(hitLayer[14]) << 14
                | Convert.ToInt32(hitLayer[15]) << 15
                | Convert.ToInt32(hitLayer[16]) << 16
                | Convert.ToInt32(hitLayer[17]) << 17
                | Convert.ToInt32(hitLayer[18]) << 18
                | Convert.ToInt32(hitLayer[19]) << 19
                | Convert.ToInt32(hitLayer[20]) << 20
                | Convert.ToInt32(hitLayer[21]) << 21
                | Convert.ToInt32(hitLayer[22]) << 22
                | Convert.ToInt32(hitLayer[23]) << 23
                | Convert.ToInt32(hitLayer[24]) << 24
                | Convert.ToInt32(hitLayer[25]) << 25
                | Convert.ToInt32(hitLayer[26]) << 26
                | Convert.ToInt32(hitLayer[27]) << 27
                | Convert.ToInt32(hitLayer[28]) << 28
                | Convert.ToInt32(hitLayer[29]) << 29
                | Convert.ToInt32(hitLayer[30]) << 30
                | Convert.ToInt32(hitLayer[31]) << 31;
        }
        //---------------------------------------
    }
}
