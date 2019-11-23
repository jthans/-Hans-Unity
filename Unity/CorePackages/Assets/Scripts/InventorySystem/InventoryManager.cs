using Assets.Scripts.Core;
using Assets.Scripts.InventorySystem.Item_Management;
using Assets.Scripts.InventorySystem.Item_Management.Weapons;
using Hans.Inventory.Core;
using Hans.Inventory.Core.Interfaces;
using Hans.Logging;
using Hans.Logging.Interfaces;
using System.Linq;

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

        /// <summary>
        ///  Packs of weapons to be loaded into the inventory management system.
        /// </summary>
        public WeaponPack[] WeaponPacks;

        #endregion

        #region Internal Properties

        /// <summary>
        ///  Inventory System (Backend)
        /// </summary>
        private Inventory _inventorySystem;

        /// <summary>
        ///  Logging class to output data about what's happening.
        /// </summary>
        private ILogger _log;

        #endregion

        #region Unity Methods

        /// <summary>
        ///  Initialization
        /// </summary>
        void Start()
        {
            this._log = LoggerManager.CreateLogger(typeof(InventoryManager));
            Armory.LoadWeapons(this.WeaponPacks.SelectMany(x => x.Weapons).ToArray());
        }

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
        public bool AddItemToInventory(string entityId, string itemId, int quantity = 1, bool isPickup = false)
        {
            var foundItem = this.GetInventoryItemById(itemId);
            if (foundItem == null)
            {
                this._log.LogMessage($"Item { itemId } couldn't be added to entity { entityId }, due to it not existing in the item database.");
                return false;
            }

            // Trigger any extra logic when pickup occurs.
            if (isPickup)
                (foundItem as InventoryItem).OnPickup(entityId);

            this._inventorySystem.AddItem(foundItem, quantity);
            this._log.LogMessage($"Item ID { itemId }, New Qty: { this._inventorySystem.GetItemProfile(foundItem).TotalQuantity }");

            return true;
        }

        /// <summary>
        ///  Adds a weapon to the inventory, by managing what stays equipped, where it goes, etc.
        /// </summary>
        /// <param name="entityId">The entity picking up the given weapon.</param>
        /// <param name="itemId">The weapon ID that we'll be adding.</param>
        /// <param name="isEquip">If this is an equip action, meaning the actor equips it immediately.</param>
        public void AddWeaponToInventory(string entityId, string itemId, bool isEquip)
        {
            // Attempt to add the weapon to the entity's inventory.
            if (AddItemToInventory(entityId, itemId, isPickup: true) &&
                isEquip)
            {
                var weaponComp = EntityManager.Instance.FindEntityWithId(entityId)?.GetComponent<WeaponHandler>();
                if (weaponComp != null &&
                    Armory.Weapons.ContainsKey(itemId))
                {
                    this._log.LogMessage($"Equipping { itemId }.");
                    weaponComp.EquipWeapon(Armory.Weapons[itemId]);
                }
            }
        }

        /// <summary>
        ///  Indicates whether or not an item is being held by an entity.
        /// </summary>
        /// <param name="entityId">The entity that we're checking.</param>
        /// <param name="itemId">The ID of the item we're checking.</param>
        /// <returns>If the entity has the item in it's inventory.</returns>
        public bool IsEntityHoldingItem(string entityId, string itemId)
        {
            var foundItem = this.GetInventoryItemById(itemId);
            var itemSearch = this._inventorySystem.GetItemProfile(foundItem);

            // TODO: Inventory should support multiple entities.
            return itemSearch.TotalQuantity > 0;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        ///  Gets an inventory item stored in the lookup tables via the ID.
        /// </summary>
        /// <param name="itemId">The ID of the item to retrieve.</param>
        /// <returns>The item, if one is found.  Null if not.</returns>
        private IIInventoryItem GetInventoryItemById(string itemId)
        {
            return this.InventoryLookup[itemId];
        }

        #endregion
    }
}
