using Assets.Scripts.Core;
using Hans.Inventory.Core;

namespace Assets.Scripts.InventorySystem
{
    /// <summary>
    ///  Inventory Manager that will amange inventory for all entities in a scene, returning any critical information
    ///     to the entity when requested.  Interacts with the Inventory backend to store a quick/simple inventory.
    /// </summary>
    public class InventoryManager : Singleton<InventoryManager>
    {
        /// <summary>
        ///  Inventory System (Backend)
        /// </summary>
        private Inventory _inventorySystem;

        #region Protected Methods

        /// <summary>
        ///  Initializes this manager as an inventory interaction object.  Creates the inventory, and sets up
        ///     any additional configuration needed.
        /// </summary>
        protected override  void Initialize()
        {
            this._inventorySystem = new Inventory();
        }

        #endregion
    }
}
