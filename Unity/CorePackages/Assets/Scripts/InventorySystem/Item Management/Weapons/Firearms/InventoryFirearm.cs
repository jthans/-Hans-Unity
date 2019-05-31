using Assets.Scripts.Constants;

namespace Assets.Scripts.InventorySystem.Item_Management.Weapons.Firearms
{
    /// <summary>
    ///  InventoryFirearm - Inventory tracked item that represents a firearm, as most of these will be 
    ///     handled similarly.
    /// </summary>
    public class InventoryFirearm : InventoryWeapon
    {
        /// <summary>
        ///  The type of ammo this firearm uses/provides.
        /// </summary>
        protected string AmmoType { get; set; }

        /// <summary>
        ///  The amount of ammo that resides in a clip at any given time.
        /// </summary>
        protected int ClipSize { get; set; }

        /// <summary>
        ///  The amount of ammo that should also be added to the reserves when a gun is picked up fresh.
        /// </summary>
        protected int NumAmmoFresh { get; set; }

        /// <summary>
        ///  The amount of ammo that should also be added to the reserves when a gun is already held.
        /// </summary>
        protected int NumAmmoHolding { get; set; }

        /// <summary>
        ///  Initializes a new instance of <see cref="InventoryFirearm" /> class.
        /// </summary>
        /// <param name="itemId">The item ID that's been added.</param>
        /// <param name="ammoFresh">The amount of ammo added when the gun isn't held.</param>
        /// <param name="ammoHolding">The amount of ammo added when the gun is held.</param>
        public InventoryFirearm(string itemId, int ammoFresh, int ammoHolding, int clipSize, string ammoType)
            : base(itemId)
        {
            this.AmmoType = ammoType;
            this.ClipSize = clipSize;
            this.NumAmmoFresh = ammoFresh;
            this.NumAmmoHolding = ammoHolding;
        }

        /// <summary>
        ///  When a firearm is picked up, we have some extra logic to maintain to react accordingly.
        /// </summary>
        /// <param name="entityId">The ID of the entity that picked this item up.</param>
        public override void OnPickup(string entityId)
        {
            base.OnPickup(entityId);

            #region Ammo Addition
            
            if (InventoryManager.Instance.IsEntityHoldingItem(entityId, this.TextId))
            {
                InventoryManager.Instance.AddItemToInventory(entityId, 
                                                                AmmoLocation.GetInventoryIndicator(this.AmmoType), 
                                                                this.NumAmmoHolding);
            }
            else
            {
                InventoryManager.Instance.AddItemToInventory(entityId,
                                                                AmmoLocation.GetInventoryIndicator(this.AmmoType),
                                                                this.NumAmmoFresh - this.ClipSize);

                InventoryManager.Instance.AddItemToInventory(entityId,
                                                                AmmoLocation.GetInventoryIndicator(this.AmmoType, true, this.TextId),
                                                                this.ClipSize);
            }

            #endregion
        }
    }
}
