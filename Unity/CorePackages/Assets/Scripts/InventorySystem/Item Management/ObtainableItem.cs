using Assets.Scripts.EntityManagement;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
{
    /// <summary>
    ///  A more specific type of inventory item that indicates that it's able to be obtained in the world - This
    ///     will have a model associated, as well as a collider required that will allow a player to pick it up.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(IDComponent))]
    public class ObtainableItem : MonoBehaviour
    {
        #region Fields

        [Tooltip("The collder that allows the user's vision to interact with items as they look across them.")]
        public Collider Collider;

        [Tooltip("The model that's present/visible to the player in the game world.")]
        public Mesh Model;

        #endregion

        #region Unity Methods

        /// <summary>
        ///  Called before the scene begins rendering this object.
        /// </summary>
        protected void Awake()
        {
            // Ensure we're always setting these as triggers - Necessary for vision collision.
            this.Collider.isTrigger = true;
        }

        #endregion
    }
}
