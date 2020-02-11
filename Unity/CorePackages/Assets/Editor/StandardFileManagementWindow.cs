using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{ 

    #if UNITY_EDITOR

    /// <summary>
    ///  Standard file management window that contains basic flows for New, Open, Save, etc. and the alerts 
    ///     that come with this functionality.  If we keep it generic, we can reuse this logic later on.
    /// </summary>
    [ExecuteInEditMode]
    public class StandardFileManagementWindow : EditorWindow
    {
        /// <summary>
        ///  File extension this window will be filtered by.
        /// </summary>
        protected string fileExtension;

        /// <summary>
        ///  The currently opened file location, so we can save directly back to it.  If NULL, hasn't been saved.
        /// </summary>
        protected string fileLocation;

        /// <summary>
        ///  Indicates if the document state is dirty or not, to track whether a save should be recommended.
        /// </summary>
        protected bool isDirty = false;

        public StandardFileManagementWindow(string fileExtension = null)
        {
            this.fileExtension = fileExtension;
        }

        /// <summary>
        ///  When the window initializes, we do any initial loading that needs to be accomplished for the window to function properly.
        /// </summary>
        public virtual void Init()
        {
            EditorWindow.GetWindow(this.GetType()).Show();
        }

        /// <summary>
        ///  Every refresh of this GUI, perform these actions.
        /// </summary>
        public virtual void OnGUI()
        {
            this.InitializeContextMenus();
        }

        #region Internal Methods

        /// <summary>
        ///  Checks for unsaved changes on the current document.  If unsaved changes exist, we need to ensure that the user is okay losing those changes.
        /// </summary>
        /// <returns>If it's safe to proceed with clearing a document. (Unsaved changes present, and user approves to ignore.)</returns>
        private bool CheckForUnsavedChanges()
        {
            return !this.isDirty ||
                    EditorUtility.DisplayDialog($"Unsaved Changes", $"Are you sure you want to proceed without saving changes?", "Ignore Changes and Continue", "Cancel");
        }

        /// <summary>
        ///  Generate/Build the context menus for this application.
        /// </summary>
        private void InitializeContextMenus()
        {
            GUILayout.BeginHorizontal();

            // File Menu
            if (GUILayout.Button("File", EditorStyles.miniButtonLeft))
            {
                GenericMenu contextMenu = new GenericMenu();

                contextMenu.AddMenuItem("New", NewTemplate);
                contextMenu.AddMenuItem("Open", OpenTemplate);
                contextMenu.AddMenuItem("Save", SaveTemplate);

                contextMenu.ShowAsContext();
            }

            this.AddCustomContextMenus();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        ///  Starts a new template of whatever the utility should control.  Checks if there's any unsaved states in the 
        ///     current document, and warns the user if so.
        /// </summary>
        private void NewTemplate()
        {
            if (this.CheckForUnsavedChanges())
            {
                this.NewTemplateSelected();
                this.isDirty = false;
            }
        }

        /// <summary>
        ///  Opens a template of whatever the utility should control.  Checks if there's any unsaved states in the current
        ///     document, and warns the user if so.
        /// </summary>
        private void OpenTemplate()
        {
            if (this.CheckForUnsavedChanges())
            {
                string filePath = EditorUtility.OpenFilePanel("Open File", "", this.fileExtension);

                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    try
                    {
                        this.isDirty = false;

                        string fileContents = File.ReadAllText(filePath);
                        this.OpenTemplateSelected(fileContents);
                    }
                    catch (Exception ex)
                    {
                        this.NewTemplate();
                        EditorUtility.DisplayDialog("ERROR", "Exception encountered opening file.  Ensure it's the proper file type, and that the file is not corrupted.", "OK", null);
                    }
                }
            }
        }

        /// <summary>
        ///  Saves a template to where it was previously saved, or alerting the user to select a location if there isn't one stored.
        /// </summary>
        private void SaveTemplate()
        {
            string fileContents = this.SaveTemplateSelected();
            if (!string.IsNullOrWhiteSpace(this.fileLocation))
            {
                File.WriteAllText(this.fileLocation, fileContents);
                this.isDirty = false;
            }
            else
            {
                this.fileLocation = EditorUtility.SaveFilePanel("Save File", "", "Template_001", this.fileExtension);
                if (!string.IsNullOrWhiteSpace(this.fileLocation) &&
                    !string.IsNullOrWhiteSpace(fileContents))
                {
                    File.WriteAllText(this.fileLocation, fileContents);
                    this.isDirty = false;
                }
                else
                {
                    EditorUtility.DisplayDialog($"ERROR", "An exception or issue occurred while saving the file.", "OK", null);
                }
            }
        }

        #endregion

        #region Virtual Methods

        /// <summary>
        ///  Overriden to allow the user to add further context menus to the application.
        /// </summary>
        protected virtual void AddCustomContextMenus()
        {

        }

        /// <summary>
        ///  Overriden to clear the utility when the NEW functionality has been requested.
        /// </summary>
        protected virtual void NewTemplateSelected()
        {

        }

        /// <summary>
        ///  Overriden to do something with the file that's been opened up via the open dialog.
        /// </summary>
        /// <param name="fileContents">Contents read when parsing the file.</param>
        protected virtual void OpenTemplateSelected(string fileContents)
        {

        }

        /// <summary>
        ///  Overriden to pull all active template controls, and format into a string that can be saved to the computer as a file.
        /// </summary>
        /// <returns>The file contents that will be saved to the location selected by the user.</returns>
        protected virtual string SaveTemplateSelected()
        {
            return null;
        }

        #endregion
    }

    #endif 
}