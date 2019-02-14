using UnityEngine;

namespace Assets.Scripts.Core.Raycasting.Models
{
    /// <summary>
    ///  Payload representing information related to the raycast that triggered a <see cref="RaycastSubscriber" /> object.
    /// </summary>
    public class RaycastBeginPayload
    {
        /// <summary>
        ///  The Raycast that called this event.
        /// </summary>
        public Raycaster CallingObject { get; set; }

        /// <summary>
        ///  The object that is being viewed.
        /// </summary>
        public GameObject FocusedObject { get; set; }
    }
}
