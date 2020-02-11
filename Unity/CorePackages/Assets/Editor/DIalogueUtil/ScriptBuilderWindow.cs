using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[ExecuteInEditMode]
public class ScriptBuilderWindow : EditorWindow
{
    [MenuItem("Window/ScriptBuilder")]
    public static void Init()
    {
        EditorWindow.GetWindow(typeof(ScriptBuilderWindow)).Show();
    }
}

#endif