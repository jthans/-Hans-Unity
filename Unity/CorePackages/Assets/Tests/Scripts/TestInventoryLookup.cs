using Assets.Scripts.InventorySystem;
using Assets.Scripts.InventorySystem.Item_Management;
using Assets.Scripts.InventorySystem.Item_Management.Weapons.Firearms;
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
            #region Debugging
            { "TestItem-Cube", new InventoryItem("TestItem-Cube") },
            #endregion

            #region Ammo
            { "Ammo_Pistol_Reserves", new InventoryItem("Ammo_Pistol_Reserves") },
            { "Weapon_Pistol_M9_Chambered", new InventoryItem("Weapon_Pistol_M9_Chambered") },
            #endregion

            #region Weapons
            { "Weapon_Pistol_M9", new InventoryFirearm("Weapon_Pistol_M9", 36, 24, 12, AmmoType.Pistol) }
            #endregion
        };
    }
}
