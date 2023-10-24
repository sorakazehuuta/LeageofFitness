#if !COMPILER_UDONSHARP && UNITY_EDITOR
using UnityEngine;
using UnityEditor.Build;

public class IgnoreComponentOnBuild : IProcessSceneWithReport
{
    public void OnProcessScene(UnityEngine.SceneManagement.Scene scene, UnityEditor.Build.Reporting.BuildReport report)
    {
        if (report == null)
            return;

        int removeCount = 0;
        foreach(var root in scene.GetRootGameObjects())
        {
            foreach(MonoBehaviour component in root.GetComponentsInChildren<MonoBehaviour>(true))
            {
                if (HasIgnoreBuildAttribute(component))
                {
                    GameObject.DestroyImmediate(component);
                    removeCount++;
                }
            }
        }

        Debug.Log($"Ignore components on build ({removeCount})");
    }

    private bool HasIgnoreBuildAttribute(MonoBehaviour component)
    {
        if (component == null)
            return false;

        if (component.GetType().GetCustomAttributes(typeof(IgnoreBuildAttribute), true).Length != 0)
            return true;

        return false;
    }

    public int callbackOrder { get { return 0; } }
}
#endif
