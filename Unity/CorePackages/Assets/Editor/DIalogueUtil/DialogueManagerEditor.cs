using Assets.Scripts.DialogueSystem;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

/// <summary>
///  Custom editor for <see cref="DialogueManager" /> objects, so that we can more directly configure the 
///     dialogue and it's child scripts.
/// </summary>
[ExecuteInEditMode]
[CustomEditor(typeof(DialogueManager))]
public class DialogueManagerEditor : Editor
{
    #region Unity Methods

    /// <summary>
    ///  When the inspector is rendered, render our custom editor controls.
    /// </summary>
    public override void OnInspectorGUI()
    {
        // If the user selects the "ScriptBuilder" option, open the script management utility.
        if (GUILayout.Button("ScriptBuilder", EditorStyles.miniButton))
        {
            var scriptBuilder = new ScriptBuilderWindow();
            scriptBuilder.Init();
        }
    }

    #endregion
}

#endif