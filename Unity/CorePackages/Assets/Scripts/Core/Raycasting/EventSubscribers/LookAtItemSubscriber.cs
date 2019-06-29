using Assets.Scripts.Core.Raycasting;
using Assets.Scripts.EntityManagement;
using Assets.Scripts.InventorySystem;
using UnityEngine;

namespace Hans.Unity.Core.Raycasting 
{
    /// <summary>
    ///  Subscriber that maintains state/UI changes when a player looks at any item. Also includes the input additions that manage the item being interacted
    ///     with within the world.
    /// </summary>
    public class LookAtItemSubscriber : RaycastSubscriber
    {
        #region Unity Methods

        /// <summary>
        ///  Called on a consistent time interval, we'll check for important changes here.
        /// </summary>
        protected void FixedUpdate()
        {
            // If an item is interacted with while being looked at, add the item to the inventory.
            if (this.hasGaze &&
                UnityEngine.Input.GetButtonDown(ButtonNames.Interact))
            {
                this.AddToPlayerInventory();
            }
        }

        #endregion

        #region RaycastSubscriber Methods

        /// <summary>
        ///  When a Raycast is no longer being tracked, we'll perform these actions.
        /// </summary>
        protected override void RaycastEnded()
        {
            this.log.LogMessage($"EndRaycast for Item.");
        }

        /// <summary>
        ///  When a Raycast is being tracked, we'll perform these actions.
        /// </summary>
        protected override void RaycastStarted()
        {
            this.log.LogMessage($"StartRaycast for Item { this.focusedGameObject.name }.");
        }

        #endregion

        #region Internal Methods

        /// <summary>
        ///  Adds an item to a player's inventory by calling the <see cref="InventoryManager" /> class.
        /// </summary>
        private void AddToPlayerInventory()
        {
            var itemID = this.focusedGameObject.GetComponent<IDComponent>();

            // Get rid of the object, it's been "picked up".
            Destroy(this.focusedGameObject);

            // Add the item to the player's inventory.
            if (itemID.Type == Assets.Scripts.Enums.ItemType.Weapon)
            {
                InventoryManager.Instance.AddWeaponToInventory(this.lastCallingRaycaster.Entity?.Id, itemID.ID, true);
            }
            else
            {
                InventoryManager.Instance.AddItemToInventory(this.lastCallingRaycaster.Entity?.Id, itemID.ID, isPickup: true);
            }
        }

        #endregion
    }
}