using Assets.Scripts.InventorySystem;
using Assets.Scripts.InventorySystem.Item_Management;
using Hans.Inventory.Core.Interfaces;
using System.Collections.Generic;

public class TestInventoryLookup : InventoryLookupTable
{
    /// <summary>
    ///  Initializes this test class, so we have the information available while testing.
    /// </summary>
    public TestInventoryLookup()
    {
        this._inventoryLookup = new Dictionary<string, IIInventoryItem>()
        {
            { "TestItem-Cube", new InventoryItem("TestItem-Cube") }
        };
    }
}
