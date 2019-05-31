namespace Assets.Scripts.InventorySystem.Item_Management.Weapons
{
    /// <summary>
    ///  InventoryWeapon - An item that belongs in a player's inventory that is a <i>weapon</i> type.
    /// </summary>
    public class InventoryWeapon : InventoryItem
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="InventoryWeapon" /> class.
        /// </summary>
        /// <param name="itemId">The ID of the item we're tracking.</param>
        public InventoryWeapon(string itemId)
         : base(itemId) { }

        /// <summary>
        ///  What we should do when a weapon is picked up.
        /// </summary>
        /// <param name="entityId">The entity that picked this item up.</param>
        public override void OnPickup(string entityId)
        {
            base.OnPickup(entityId);
        }
    }
}
