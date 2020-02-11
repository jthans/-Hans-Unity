using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{

#if UNITY_EDITOR

    /// <summary>
    ///  Tools useful for any custom editor management.  These will be generic, to be utilized across all custom editor tools.
    /// </summary>
    public static class EditorTools
    {
        #region Window Tools

        /// <summary>
        ///  Adds a menu item to a given <see cref="GenericMenu" /> object, with a hover cover selected if desired.
        /// </summary>
        /// <param name="menu">The menu to add the control to.</param>
        /// <param name="menuPath">The tree definition of the menu item.</param>
        /// <param name="funcToCall">Logic that will run when the item is selected.</param>
        public static void AddMenuItem(this GenericMenu menu, string menuPath, GenericMenu.MenuFunction funcToCall = null)
        {
            menu.AddItem(new GUIContent(menuPath), false, funcToCall);
        }

        #endregion
    }

#endif
}
