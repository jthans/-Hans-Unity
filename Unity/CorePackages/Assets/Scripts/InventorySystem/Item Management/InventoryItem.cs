using System;
using Hans.Inventory.Core.Interfaces;

namespace Assets.Scripts.InventorySystem
{
    /// <summary>
    ///  Class representing an inventory item
    /// </summary>
    public class InventoryItem : IIInventoryItem
    {
        /// <summary>
        ///  ID of the Item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  ID in plaintext.
        /// </summary>
        public string TextId { get; set; }

        /// <summary>
        ///  Initializes a new instance of the inventory item, by generating a GUID to identify this GUID by.  Later, this ID needs to be
        ///     changed to a string, or find a better way to generate the GUID.
        /// </summary>
        /// <param name="itemId"></param>
        public InventoryItem(string itemId)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(itemId.GetHashCode()).CopyTo(bytes, 0);

            this.Id = new Guid(bytes);
            this.TextId = itemId;
        }

        /// <summary>
        ///  Method called when an item is picked up <b>in addition to adding it to the inventory,</b> running based on who picked it up.
        /// </summary>
        public virtual void OnPickup(string entityId)
        {
            // BY DEFAULT, DO NOTHING.
        }
    }
}
