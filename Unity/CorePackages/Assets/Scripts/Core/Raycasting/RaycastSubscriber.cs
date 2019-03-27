using Assets.Scripts.Enums;
using UnityEngine;
using Assets.Scripts.Core.Raycasting.Models;
using Hans.Logging;

namespace Assets.Scripts.Core.Raycasting
{
    /// <summary>
    ///  Base Raycast Subscriber class - Needed for basic event assignment/raycast reactions that will remain the same
    ///     every time.
    /// </summary>
    public class RaycastSubscriber : MonoBehaviour
    {
        #region Fields

        /// <summary>
        ///  Type of subscription this object will subscribe to on raycasters.
        /// </summary>
        public RaycastSubscriptionType Type;

        /// <summary>
        ///  What value we'll be matching up to when the raycaster detects a hit.
        /// </summary>
        public string Value;

        #endregion

        #region Internal Fields

        /// <summary>
        ///  The currently focused gameObject, tracked if something is being viewed.
        /// </summary>
        protected GameObject focusedGameObject;

        /// <summary>
        ///  Indicates if this subscriber currently holds the gaze passed by the raycaster, determined on a gameObject being held.
        /// </summary>
        protected bool hasGaze { get { return this.focusedGameObject != null; } }

        /// <summary>
        ///  The last calling raycaster, so we can unsubscribe from the exit event once it's complete.
        /// </summary>
        private Raycaster lastCallingRaycaster;

        /// <summary>
        ///  Logger class used to export information about this subscriber to the Unity Engine.
        /// </summary>
        protected Hans.Logging.Interfaces.ILogger log;

        #endregion

        #region Unity Methods

        /// <summary>
        ///  Initializes a new instance of the <see cref="RaycastSubscriber" /> class.
        /// </summary>
        void Start()
        {
            this.log = LoggerManager.CreateLogger(this.GetType());
        }

        #endregion

        #region Instance Methods

        /// <summary>
        ///  When the Raycaster starts detecting an object this subscriber desires, we'll have to react to that change, and ready for the exit.
        /// </summary>
        public void OnRaycastEnter(RaycastBeginPayload enterPayload)
        {
            // Subscribe to the exit event on this subscriber.
            this.lastCallingRaycaster = enterPayload.CallingObject;
            this.lastCallingRaycaster.OnRaycastEnded += this.OnRaycastExit;

            this.focusedGameObject = enterPayload.FocusedObject;

            // Call the logic asynronously for this subscriber.
            this.RaycastStarted();
        }

        /// <summary>
        ///  When the Raycaster stops detecting an object this subscriber desires, we'll have to react to that change.
        /// </summary>
        public void OnRaycastExit()
        {
            // Call the logic syncronously for this subscriber.
            this.RaycastEnded();

            // Unsubscribe from further exit events.
            this.lastCallingRaycaster.OnRaycastEnded -= this.OnRaycastExit;
            this.lastCallingRaycaster = null;

            this.focusedGameObject = null;
        }

        #endregion

        /// <summary>
        ///  When a raycast ends, we'll perform these actions in the background.
        /// </summary>
        protected virtual void RaycastEnded()
        {
            
        }

        /// <summary>
        ///  When a raycast starts on our object, we'll perform these actions in the background.
        /// </summary>
        protected virtual void RaycastStarted()
        {
            
        }
    }
}
