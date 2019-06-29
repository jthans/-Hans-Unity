using Assets.Scripts.Core.Raycasting.Models;
using Hans.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Core.Raycasting
{
    /// <summary>
    ///  This Raycaster object is responsible for sending a raycast from a particular location, pointed in a certain direction as configured, and calling any subscribers
    ///     to its activities, allowing actions to occur based on what's being seen by the raycast.  Rather than doing 20 raycasts from a single point, this allows a
    ///     central location to manage ONE raycast, as a character will only see a single object at a time anywa.
    ///     
    ///     NOTE: The goal in game design is always to be fast - So try not to overload any one raycaster with too many subscribers - It has to process a raycast each frame, remember!
    /// </summary>
    public class Raycaster : MonoBehaviour
    {
        #region Events

        /// <summary>
        ///  Event thrown when the raycaster detects this is the last observation of this new object.
        /// </summary>
        public delegate void RaycastEnded();
        public event RaycastEnded OnRaycastEnded;

        #endregion

        #region Fields

        [Tooltip("Whether or not to show a debugging line to represent the raycast in 3D space.")]
        public bool DebuggerLineEnabled = true;

        [Tooltip("The entity consuming these raycast events.")]
        public Entity Entity;

        [Tooltip("How far to send the raycast, how far the vision should be for detecting things, basically.")]
        public float RaycastLength = 5;

        [Tooltip("The list of subscribers that this raycaster will be responsible for managing.")]
        public List<RaycastSubscriber> Subscribers;

        #endregion

        #region Internal Properties

        /// <summary>
        ///  Saves the last game object that was seen by this raycaster.
        /// </summary>
        private int lastGameObjectDetected = -1;

        /// <summary>
        ///  Logger used to import useful information about the class.
        /// </summary>
        private Hans.Logging.Interfaces.ILogger log;


        #endregion

        #region Unity Methods

        /// <summary>
        ///  Called on an equal time tick always, this is recommended to be used as Update is based
        ///     entirely in the user's computer speed.
        /// </summary>
        protected void FixedUpdate()
        {
            // TODO: Make a more complex/configurable layer mask.
            int layerMask = 1 << 8;
            layerMask = ~layerMask;

            // Perform the raycast, and react to it.
            int detectedGO = -1;
            bool firstHit = false;
            float rayDistance = this.RaycastLength;
            RaycastHit rayHit;
            if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out rayHit, this.RaycastLength, layerMask))
            {
                detectedGO = rayHit.collider.gameObject.GetInstanceID();
                firstHit = this.lastGameObjectDetected != detectedGO;

                // See if we've changed gameObjects - if we have, we have some events to throw.
                if (firstHit)
                {
                    this.UpdateDetectedObject(detectedGO);
                }

                this.ProcessRaycastHit(rayHit, firstHit);

                // Update the debugger line's distance.
                rayDistance = rayHit.distance;
            }
            else
            {
                // See if we've changed gameObjects - if we have, we have some events to throw.
                if (this.lastGameObjectDetected != detectedGO)
                {
                    this.UpdateDetectedObject(detectedGO);
                }
            }

#if UNITY_EDITOR
            // If debugging is enabled, draw a line that's visible to the player.
            if (this.DebuggerLineEnabled)
            {
                Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * rayDistance, Color.red);
            }

            // GameObject.Find("DEBUG_FOCUSED_OBJ").GetComponent<Text>().text = rayHit.collider?.gameObject?.name;
#endif
        }

        void Start()
        {
             this.log = LoggerManager.CreateLogger(typeof(Raycaster));
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///  Builds a payload to send to useful subscribers with information pertaining to the raycast.
        /// </summary>
        /// <param name="rayHit">The raycast object, to pull info from.</param>
        /// <returns>A payload containing important information about the raycast.</returns>
        private RaycastBeginPayload BuildPayload(RaycastHit rayHit)
        {
            return new RaycastBeginPayload()
            {
                CallingObject = this,
                FocusedObject = rayHit.collider.gameObject
            };
        }

        /// <summary>
        ///  Processes a raycast hit by determining any important factors, and calling subscribers if the raycast pertains to them.
        /// </summary>
        /// <param name="rayHit"></param>
        private void ProcessRaycastHit(RaycastHit rayHit, bool isFirstHit)
        {
            try
            {
                // Get the generic payload.
                var enterPayload = this.BuildPayload(rayHit);
                if (isFirstHit)
                {
                    // Handle Tag Searches First, as these will be more common.
                    var tagSubscribers = this.Subscribers.Where(x => x.Type == Enums.RaycastSubscriptionType.Tag &&
                                                                     x.Value == rayHit.collider.gameObject.tag);
                    foreach (var sub in tagSubscribers)
                    {
                        sub.OnRaycastEnter(enterPayload);
                    }

                    // Handle Name Subscribers, next, these are very specific cases.
                    var nameSubscribers = this.Subscribers.Where(x => x.Type == Enums.RaycastSubscriptionType.Name &&
                                                                      x.Value == rayHit.collider.gameObject.name);

                    foreach (var sub in nameSubscribers)
                    {
                        sub.OnRaycastEnter(enterPayload);
                    }
                }
            }
            catch (Exception ex)
            {
                this.log.LogMessage($"Exception Processing Raycast: { ex.ToString() }");
            }
        }

        /// <summary>
        ///  Called when a new object is seen, compared to the last frame.  Update our variables.
        /// </summary>
        /// <param name="detectedGO">The newly detected GO, or -1 if nothing detected.</param>
        private void UpdateDetectedObject(int detectedGO)
        {
            // We're leaving an object - Throw an event if so.
            if (this.lastGameObjectDetected != -1 &&
                this.OnRaycastEnded != null) // Only throw if we have subscribers to this event.
            {
                this.OnRaycastEnded();
            }

            this.lastGameObjectDetected = detectedGO;
        }

        #endregion
    }
}
