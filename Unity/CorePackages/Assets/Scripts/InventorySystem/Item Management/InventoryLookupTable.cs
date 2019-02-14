using Hans.Inventory.Core.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.InventorySystem.Item_Management
{
    /// <summary>
    ///  Object that represents a lookup table for the inventory - Loaded on scene load, and will be accessed for all inventory processes.
    /// </summary>
    public class InventoryLookupTable
    {
        /// <summary>
        ///  Lookup dictionary, created in the constructor and used as a way to easily access item information in the inventory's system.  Can
        ///     easily use a script to auto-generate these classes, and their data from another source.
        /// </summary>
        private Dictionary<string, IIInventoryItem> _inventoryLookup;

        /// <summary>
        ///  Indexer that allows easy access to this lookup table.
        /// </summary>
        /// <param name="itemKey">The item key that we're searching for in our table.</param>
        /// <returns>The item that is referred to as the key, or if it doesn't exist. NULL.</returns>
        public IIInventoryItem this[string itemKey]
        {
            get
            {
                if (this._inventoryLookup != null &&
                    this._inventoryLookup.ContainsKey(itemKey))
                {
                    return this._inventoryLookup[itemKey];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
