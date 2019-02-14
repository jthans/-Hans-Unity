using Assets.Scripts.Core;
using Assets.Scripts.InventorySystem.Item_Management;
using Hans.Inventory.Core;
using Hans.Logging;
using Hans.Logging.Interfaces;

namespace Assets.Scripts.InventorySystem
{
    /// <summary>
    ///  Inventory Manager that will amange inventory for all entities in a scene, returning any critical information
    ///     to the entity when requested.  Interacts with the Inventory backend to store a quick/simple inventory.
    /// </summary>
    public class InventoryManager : Singleton<InventoryManager>
    {
        #region Fields

        /// <summary>
        ///  The lookup table associated with THIS manager that will allow us to access item information.
        /// </summary>
        public InventoryLookupTable InventoryLookup;

        #endregion

        #region Internal Properties

        /// <summary>
        ///  Inventory System (Backend)
        /// </summary>
        private Inventory _inventorySystem;

        /// <summary>
        ///  Logging class to output data about what's happening.
        /// </summary>
        private ILogger _log = LoggerManager.CreateLogger(typeof(InventoryManager));

        #endregion 

        #region Protected Methods

        /// <summary>
        ///  Initializes this manager as an inventory interaction object.  Creates the inventory, and sets up
        ///     any additional configuration needed.
        /// </summary>
        protected override  void Initialize()
        {
            this._inventorySystem = new Inventory(1); // TODO: Dynamic Sizing.
        }

        #endregion

        #region Manager Methods

        /// <summary>
        ///  Adds an item in a given amount to an entity's inventory.  Uses the inventory reference class loaded into 
        ///     this manager object.
        /// </summary>
        /// <param name="entityId">The entity to which we'll add the item.</param>
        /// <param name="itemId">The item's ID, should reference the class passed here.</param>
        /// <param name="quantity">How many of this item to add.</param>
        public void AddItemToInventory(string entityId, string itemId, int quantity = 1)
        {
            var foundItem = this.InventoryLookup[itemId];
            if (foundItem == null)
            {
                this._log.LogMessage($"Item { itemId } couldn't be added to entity { entityId }, due to it not existing in the item database.");
                return;
            }

            this._inventorySystem.AddItem(foundItem, quantity);   
        }

        #endregion
    }
}
