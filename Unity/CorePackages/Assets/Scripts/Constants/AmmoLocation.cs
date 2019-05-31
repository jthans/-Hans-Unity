namespace Assets.Scripts.Constants
{
    /// <summary>
    ///  Class containing location information for ammo (chambered, reserves, etc.)
    /// </summary>
    public class AmmoLocation
    {
        /// <summary>
        ///  Ammo is chambered in a weapon.
        /// </summary>
        public const string Chambered = "Chambered";

        /// <summary>
        ///  Ammo is reserved in the user's inventory.
        /// </summary>
        public const string Reserves = "Reserves";

        /// <summary>
        ///  Gets the inventory ID for the ammo based on where it's located, and what weapon is associated with it.
        /// </summary>
        /// <param name="ammoType">The type of ammo that's being configured.</param>
        /// <param name="isChambered">If the ammo is being chambered or stored.</param>
        /// <param name="weaponType">The type of weapon that's being loaded.</param>
        /// <returns>The inventory storage ID for the ammo.</returns>
        public static string GetInventoryIndicator(string ammoType, bool isChambered = false, string weaponType = null)
        {
            return isChambered ?
                    $"{ weaponType }_{ Chambered }" :
                    $"{ ammoType }_{ Reserves }";
        }
    }
}
