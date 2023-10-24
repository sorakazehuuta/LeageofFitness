using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#if UNITY_EDITOR // These using statements must be wrapped in this check to prevent issues on builds
using UnityEditor.SceneManagement;
using UdonSharpEditor;
using UdonSharp;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif

#if UNITY_EDITOR
/// <summary>
/// PreSaveMarkerがアタッチされたコンポーネントからMarkerTagが一致したものを検索し、
/// Variableの型と同じコンポーネントを取得してアタッチします。
/// 
/// ※※使い方※※
/// ①アタッチ先のコンポーネントと同じ場所に"PreSaveMarker"コンポーネントをアタッチし、
/// 　"MarkerTag"に検索対象となる文字列を指定する。
/// ②UdonBehaviour（U#)と同じ場所にこのScriptをアタッチし、"SearchMarkerTag"に"MarkerTag"と同じ文字列を指定する。
/// ③"Variable"にU#で宣言したPublic変数名を指定する。
/// 
/// 配列型の変数を指定したら、見つけたコンポーネントを全て取得するように変更しました。
/// また、UdonSharpBehaviour継承クラスの型ならその型のUdonBehaviourのみを取得するように変更しました。(hatsuca 20211217)
/// </summary>
[IgnoreBuild]
public class PreSaveExecuteFindAttachComponent : MonoBehaviour, IProcessSceneWithReport
{
    [SerializeField]
    string SearchMarkerTag;
    [SerializeField]
    string Variable;

    public int callbackOrder => -1000;

    public void OnProcessScene(Scene scene, BuildReport report)
    {
        EditorUtility.DisplayProgressBar("PreSaveExecuteFindAttach", "Processing..",0);
        foreach (var item in scene.GetRootGameObjects())
        {
            foreach (var comp in item.GetComponentsInChildren<PreSaveExecuteFindAttachComponent>(true))
            {
                try
                {
                    comp?.PreSaveExecute();
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message,comp);
                }
            }
        }
        EditorUtility.ClearProgressBar();
    }
    [MenuItem("Tools/Hikky/Manual Find Attach Component")]
    public static void ManualExecuteProcess()
    {
        EditorUtility.DisplayProgressBar("PreSaveExecuteFindAttach", "Processing..", 0);
        foreach (var item in EditorSceneManager.GetActiveScene().GetRootGameObjects())
        {
            foreach (var comp in item.GetComponentsInChildren<PreSaveExecuteFindAttachComponent>(true))
            {
                try
                {
                    comp?.PreSaveExecute();
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message, comp);
                }
            }
        }
        EditorUtility.ClearProgressBar();
    }
    public void PreSaveExecute()
    {
        PreSaveMarker[] markerComponents = FindPreSaveMarkerComponents(SearchMarkerTag);

        UdonSharpBehaviour[] attachedUdonBehabiours = GetComponents<UdonSharpBehaviour>();
        foreach (UdonSharpBehaviour attachedScript in attachedUdonBehabiours)
        {
#if UDONSHARP
            // proxyからUdonBehaviourに値を反映
            UdonSharpEditorUtility.CopyProxyToUdon(attachedScript);
#endif

            // Reflectionでスクリプトから型取得
            FieldInfo fieldInfo = attachedScript.GetType().GetField(Variable, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo == null)
            {
                Debug.LogWarning($"{gameObject.name}: \"{Variable}\" field not found", gameObject);
                continue;
            }
            Type insertVariableType = fieldInfo.FieldType;

            if (insertVariableType.IsArray)
            {
                StoreVariables(attachedScript, ConstructVariableArray(markerComponents, insertVariableType), insertVariableType);
            }
            else
            {
                var value = ConstructVariable(markerComponents, insertVariableType);
                if (value != null)
                    StoreVariables(attachedScript, value, insertVariableType);
                else
                    Debug.LogWarning($"{gameObject.name}: \"{SearchMarkerTag}\" Component not found");
            }

#if UDONSHARP
            // Proxy=>Udonに値を反映
            UdonSharpEditorUtility.CopyProxyToUdon(attachedScript);
            EditorUtility.SetDirty(attachedScript);
#endif
            #region ~20211217
            /*
            PreSaveMarker markerComponent = FindPreSaveMarkerComponent(SearchMarkerTag);
            if (markerComponent == null)
            {
                Debug.LogWarning("MarkerPreSaveComponent not found");
                return;
            }

            UdonBehaviour attachedBehaviour = UdonSharpEditorUtility.GetBackingUdonBehaviour(attachedScript);
            Type InsertVariableType = GetVariableType(attachedBehaviour, Variable);
            if (InsertVariableType == null) continue;



            if (insertVariableType == typeof(GameObject))
            {
                attachedScript.UpdateProxy();
                attachedScript.SetProgramVariable(Variable, markerComponent.gameObject);
                attachedScript.ApplyProxyModifications();
                EditorUtility.SetDirty(UdonSharpEditorUtility.GetBackingUdonBehaviour(attachedScript));
            }
            else
            {
                Component searchedComponent = markerComponent.gameObject.GetComponent(InsertVariableType);
                if (searchedComponent == null)
                    continue;

                SetVariableToScript(attachedScript, searchedComponent, InsertVariableType);
            }
            */
            #endregion
        }
    }
    // 配列型の値を取得、構成
    private object ConstructVariableArray(PreSaveMarker[] markerComponents, Type variableType)
    {
        Type baseVariableType = variableType.GetElementType();

        List<UnityEngine.Object> objects = new List<UnityEngine.Object>();
        foreach (var markerComponent in markerComponents)
        {
            // UdonSharpBehaviour継承クラス
            if (baseVariableType.IsSubclassOf(typeof(UdonSharpBehaviour)))
            {
                var component = markerComponent.GetComponent(baseVariableType);
                if (component != null)
                    objects.Add((UdonSharpBehaviour)component);
            }
            // GameObject型
            else if (baseVariableType.Equals(typeof(GameObject)))
            {
                objects.Add(markerComponent.gameObject);
            }
            // その他Component型
            else
            {
                var component = markerComponent.GetComponent(baseVariableType);
                if (component != null)
                    objects.Add(component);
            }
        }

        // 指定型の配列に詰め直してobject型に変換
        var value = objects.ToArray();
        Array destinationArray = Array.CreateInstance(baseVariableType, value.Length);
        Array.Copy(value, destinationArray, value.Length);
        object variableValue = destinationArray;

        return variableValue;
    }
    // 配列型以外の値を取得
    private UnityEngine.Object ConstructVariable(PreSaveMarker[] markerComponents, Type variableType)
    {
        foreach (var markerComponent in markerComponents)
        {
            // UdonSharpBehaviour継承クラス
            if (variableType.IsSubclassOf(typeof(UdonSharpBehaviour)))
            {
                var component = markerComponent.GetComponent(variableType);
                if (component != null)
                    return (UdonSharpBehaviour)component;
            }
            else if (variableType.Equals(typeof(GameObject)))
            {
                return markerComponent.gameObject;
            }
            else
            {
                var component = markerComponent.GetComponent(variableType);
                if (component != null)
                    return component;
            }
        }

        return null;
    }
    private void StoreVariables(UdonSharpBehaviour attachedScript, object value, Type variableType)
    {
        attachedScript.GetType().GetField(Variable, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(attachedScript, value);
        EditorUtility.SetDirty(attachedScript);
    }
    private static IUdonVariable CreateUdonVariable(string symbolName, object value, Type declaredType)
    {
        Type udonVariableType = typeof(VRC.Udon.Common.UdonVariable<>).MakeGenericType(declaredType);
        return (IUdonVariable)Activator.CreateInstance(udonVariableType, symbolName, value);
    }
    private PreSaveMarker[] FindPreSaveMarkerComponents(string tag)
    {
        List<PreSaveMarker> markers = new List<PreSaveMarker>();
        var preSaveComponents = Resources.FindObjectsOfTypeAll<PreSaveMarker>();
        foreach (var component in preSaveComponents)
        {
            if (component.MarkerTag == SearchMarkerTag && component.gameObject.scene == gameObject.scene)
                markers.Add(component);
        }
        return markers.ToArray();
    }

    #region ~20211217
    /*
    private Type GetVariableType(UdonBehaviour behaviour,string symbolName)
    {
        IUdonSymbolTable symbolTable = behaviour?.programSource?.SerializedProgramAsset?.RetrieveProgram()?.SymbolTable;
        if (symbolTable == null)
            return null;

        if (!symbolTable.HasExportedSymbol(symbolName))
            return null;

        return symbolTable.GetSymbolType(symbolName);
    }
    private void SetVariableToScript(UdonSharpBehaviour attachedScript,Component searchedComponent,Type InsertVariableType)
    {
        try
        {
            attachedScript.UpdateProxy();
            attachedScript.SetProgramVariable(Variable, searchedComponent);
            attachedScript.ApplyProxyModifications();
            EditorUtility.SetDirty(UdonSharpEditorUtility.GetBackingUdonBehaviour(attachedScript));
        }
        catch (ArgumentException)
        {
            if (Type.Equals(InsertVariableType, typeof(UdonBehaviour)))
            {
                SetUdonSharpToScript(attachedScript, searchedComponent, InsertVariableType);
            }
            else
            {
                Debug.LogWarning($"{Variable} is defferent type.");
            }
            return;
        }
        if (attachedScript.GetProgramVariable(Variable) == null)
            Debug.LogWarning($"Failed to set variable. using the wrong variable name? \"{Variable}\"");

    }
    private void SetUdonSharpToScript(UdonSharpBehaviour attachedScript, Component searchedComponent, Type InsertVariableType)
    {
        try
        {
            attachedScript.UpdateProxy();
            attachedScript.SetProgramVariable(Variable, ConvertUdonSharpComponentType(searchedComponent, InsertVariableType));
            attachedScript.ApplyProxyModifications();
            EditorUtility.SetDirty(UdonSharpEditorUtility.GetBackingUdonBehaviour(attachedScript));
        }
        catch (ArgumentException)
        {
            Debug.LogWarning($"{Variable} is defferent type.");
        }
    }
    private Component ConvertUdonSharpComponentType(Component component, Type InsertVariableType)
    {
        if (!Type.Equals(InsertVariableType, typeof(UdonBehaviour))) return component;

        if (Type.Equals(component.GetType(), typeof(UdonBehaviour)))
        {
            UdonSharpBehaviour proxy = UdonSharpEditorUtility.GetProxyBehaviour((UdonBehaviour)component);
            if (proxy != null)
                component = proxy;
        }
        return component;
    }
    private PreSaveMarker FindPreSaveMarkerComponent(string tag)
    {
        var preSaveComponents = Resources.FindObjectsOfTypeAll<PreSaveMarker>();
        foreach (var component in preSaveComponents)
        {
            if (component.MarkerTag == SearchMarkerTag && component.gameObject.scene == gameObject.scene)
            {
                return component;
            }
        }
        return null;
    }
    */
    #endregion
}
#endif

