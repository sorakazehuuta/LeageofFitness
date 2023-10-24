
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace CombatGimmick
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class FlagGoal : UdonSharpBehaviour
    {
        //---------------------------------------
        [Tooltip("対応するチーム")] public TeamName teamName;
        //---------------------------------------
    }
}