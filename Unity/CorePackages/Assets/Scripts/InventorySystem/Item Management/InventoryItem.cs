using System;
using Hans.Inventory.Core.Interfaces;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
{
    /// <summary>
    ///  Class representing an inventory item
    /// </summary>
    public class InventoryItem : MonoBehaviour
    {
        /// <summary>
        ///  ID of the item, used to reference this item from many different places.
        /// </summary>
        public Guid Id { get { return this.Information.Id; } }

        /// <summary>
        ///  Class containing all information about the item, to track it within the inventory.
        /// </summary>
        public IIInventoryItem Information { get; set; }
    }
}
