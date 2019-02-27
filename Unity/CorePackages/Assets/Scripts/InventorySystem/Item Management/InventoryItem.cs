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
        ///  Initializes a new instance of the inventory item, by generating a GUID to identify this GUID by.  Later, this ID needs to be
        ///     changed to a string, or find a better way to generate the GUID.
        /// </summary>
        /// <param name="itemId"></param>
        public InventoryItem(string itemId)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(itemId.GetHashCode()).CopyTo(bytes, 0);

            this.Id = new Guid(bytes);
        }
    }
}
