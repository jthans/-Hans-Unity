using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.EntityManagement
{
    /// <summary>
    ///  The Hans framework is going to rely heavily on IDs being passed all around, as identifiers for particular
    ///     entities and objects.  This component will allow us to enforce a similar rule across all entities, as well as  
    ///     an easy way to access the identifier.
    /// </summary>
    public class IDComponent : MonoBehaviour
    {
        /// <summary>
        ///  The actual ID property, containing the identifier.
        /// </summary>
        public string ID;

        /// <summary>
        ///  THe type of this item, that tells us what to do with items when they're interacted with.
        /// </summary>
        public ItemType Type;
    }
}
