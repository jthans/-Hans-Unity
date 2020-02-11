using Assets.Editor;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[ExecuteInEditMode]
public class ScriptBuilderWindow : StandardFileManagementWindow
{
    public override void OnGUI()
    {
        // Perform all base actions first, such as menu construction, etc.
        base.OnGUI();
    }
}

#endif