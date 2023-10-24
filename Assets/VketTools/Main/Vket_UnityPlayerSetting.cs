using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace VketTools.Main
{
    public static class Vket_UnityPlayerSetting
    {
        [InitializeOnLoadMethod]
        private static void DisableAssemblyVersionValidation()
        {
            var manager = AssetDatabase.LoadAllAssetsAtPath( "ProjectSettings/ProjectSettings.asset" ).FirstOrDefault();
            var obj     = new SerializedObject( manager );
            var prop    = obj.FindProperty( "assemblyVersionValidation" );
            if (prop.boolValue != false)
            {
                prop.boolValue = false;
                obj.ApplyModifiedProperties();
            }
        }
    }
}