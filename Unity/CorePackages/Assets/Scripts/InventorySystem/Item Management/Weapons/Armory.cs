using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.InventorySystem.Item_Management.Weapons
{
    /// <summary>
    ///  Armory - Contains all weapons that can be used by characters, with all their info.  Prefab storage. :)
    /// </summary>
    public static class Armory
    {
        /// <summary>
        ///  Private collection of weapons, indexed by their names.
        /// </summary>
        public static Dictionary<string, Weapon> Weapons = new Dictionary<string, Weapon>();

        #region Class Methods

        /// <summary>
        ///  Saves the weapons to the Armory class, for access later.
        /// </summary>
        /// <param name="weaponsList">List of weapon prefabs that we're storing.</param>
        public static void LoadWeapons(Weapon[] weaponsList)
        {
            Weapons = weaponsList.ToDictionary(x => x.Name, x => x);
        }

        #endregion
    }
}
