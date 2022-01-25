using Assets.Editor;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[ExecuteInEditMode]
public class ScriptBuilderWindow : StandardFileManagementWindow
{
    private bool addNewNode = false;

    /// <summary>
    ///  Drawn every frame, this is how we create the interactibles in the window.  Overriden, so we can 
    ///     leverage the standard file management systems as well.
    /// </summary>
    public override void OnGUI()
    {
        // Perform all base actions first, such as menu construction, etc.
        base.OnGUI();

        if (this.addNewNode)
        {
            // TODO: Node Creation Logic
            GUILayout.BeginArea(new Rect(0, 0, 100, 100));

            GUILayout.TextField("This is a test field.");
            GUILayout.Button("Test Button");

            GUILayout.EndArea();

            this.addNewNode = false;
        }
    }

    #region Internal Methods

    /// <summary>
    ///  Adds context menus specific to this utility.
    /// </summary>
    protected override void AddCustomContextMenus()
    {
        // Edit Menu
        if (GUILayout.Button("Edit", EditorStyles.miniButtonMid))
        {
            GenericMenu editMenu = new GenericMenu();
            editMenu.AddMenuItem("Add Node", AddNode);

            editMenu.ShowAsContext();
        }
    }

    /// <summary>
    ///  Adds a conversation node, with tools to build a full conversation piece if needed.
    /// </summary>
    private void AddNode()
    {
        this.addNewNode = true;
    }
    
    #endregion
}

#endif