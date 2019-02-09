using System;
using Assets.Scripts.Enums;
using UnityEngine;
using Assets.Scripts.Core.Raycasting.Models;
using System.Threading;

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
        ///  The last calling raycaster, so we can unsubscribe from the exit event once it's complete.
        /// </summary>
        private Raycaster lastCallingRaycaster;

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

            // Call the logic asynronously for this subscriber.
            new Thread(this.RaycastStarted).Start();
        }

        /// <summary>
        ///  When the Raycaster stops detecting an object this subscriber desires, we'll have to react to that change.
        /// </summary>
        public void OnRaycastExit()
        {
            // Unsubscribe from further exit events.
            this.lastCallingRaycaster.OnRaycastEnded -= this.OnRaycastExit;
            this.lastCallingRaycaster = null;

            // Call the logic syncronously for this subscriber.
            new Thread(this.RaycastEnded).Start();
        }

        #endregion

        /// <summary>
        ///  When a raycast starts, we'll perform these actions in the background.
        /// </summary>
        protected virtual void RaycastEnded()
        {
            Debug.Log("End");
        }

        /// <summary>
        ///  WHen a raycast ends on our object, we'll perform these actions in the background.
        /// </summary>
        protected virtual void RaycastStarted()
        {
            Debug.Log("Start");
        }
    }
}
