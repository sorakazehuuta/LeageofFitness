using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CombatGimmick
{
    [CustomEditor(typeof(CombatGimmickBuilder))]
    public class CombatGimmickBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();            //元のInspectorを表示
            
            CombatGimmickBuilder combatGimmickBuilder = target as CombatGimmickBuilder;

            if (GUILayout.Button("AutoBuild"))
            {
                combatGimmickBuilder.AutoBuild();
            }
        }
    }
}
