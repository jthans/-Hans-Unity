namespace Hans.Unity.PlayerControl
{
    using System;
    using UnityEngine;

    /// <summary>
    ///  Component used for mouse look rotations on an object.  Rotates the object to follow the mouse focus.  This is used when an object is in 
    ///     first-person mode, meaning they can look around.
    /// </summary>
    public class MouseLookComponent : MonoBehaviour
    {
        #region Fields

        /// <summary>
        ///  Location of the mouse pointer, in X/Y space.
        /// </summary>
        private float mouseRotationX = 0.0f;
        private float mouseRotationY = 0.0f;

        #endregion

        #region Properties/Settings

        /// <summary>
        ///  Rotations impacted by this script on an associated camera, passed as <see cref="FPCamera" />
        /// </summary>
        [Tooltip("If the associated FPCamera moves with the mouse focus in the X axis.")]
        public bool CameraXRotation = false;
        [Tooltip("If the associated FPCamera moves with the mouse focus in the Y axis.")]
        public bool CameraYRotation = true;

        /// <summary>
        ///  The camera associated with this mouse look object - Included as typically a camera is involved for a FP view.
        /// </summary>
        [Tooltip("Camera to view the FP view. with.")]
        public Camera FPCamera;

        /// <summary>
        ///  The FOV that will be maintained for each axis.  This is how far the object can rotate.
        /// </summary>
        [Tooltip("How far an object can rotate in the X axis.")]
        public float XFieldOfView = 360.0f;
        [Tooltip("How far an object can rotate in the Y axis.")]
        public float YFieldOfView = 90.0f;

        /// <summary>
        ///  How sensitive the mouse is/how fast it moves.
        /// </summary>
        [Tooltip("How quickly the mouse moves the view - The higher the number, the faster the move.")]
        public float MouseSensitivity = 100.0f;

        /// <summary>
        ///  Rotations impacted by this script on the object the script resides on.
        /// </summary>
        [Tooltip("If this object moves with the mouse focus in the X axis.")]
        public bool ObjectXRotation = true;
        [Tooltip("If this object moves with the mouse focus in the Y axis.")]
        public bool ObjectYRotation = false;

        #endregion

        #region Unity Methods

        /// <summary>
        ///  Called when the object is instantiated.  In this case, we're checking configured inputs, and we we'll throw errors if configured improperly.
        /// </summary>
        void Start()
        {
            // Validate.
            this.ValidateConfiguration();

            // Initialize.
            Vector3 initVector = this.transform.localRotation.eulerAngles;

            this.mouseRotationX = initVector.x;
            this.mouseRotationY = initVector.y;
        }

        /// <summary>
        ///  Called the same number of times per second - This is where the bulk of our calculations will be done, to avoid more performant computers
        ///   	from processing things faster.
        /// </summary>
        void FixedUpdate()
        {
            // Grab the mouse input, and apply it to the rotation.
            float mouseX = Input.GetAxis(ButtonNames.MouseX);
            float mouseY = -Input.GetAxis(ButtonNames.MouseY);

            this.mouseRotationY += mouseX * this.MouseSensitivity * Time.deltaTime;
            this.mouseRotationX += mouseY * this.MouseSensitivity * Time.deltaTime;

            //Make sure the rotation doesn't exceed the given values.
            var xAngle = this.YFieldOfView / 2.0f;
            var yAngle = this.XFieldOfView / 2.0f;

            if (this.YFieldOfView > 0 && this.YFieldOfView < 360) { this.mouseRotationX = Mathf.Clamp(this.mouseRotationX, -xAngle, xAngle); }
            if (this.XFieldOfView > 0 && this.XFieldOfView < 360) { this.mouseRotationY = Mathf.Clamp(this.mouseRotationY, -yAngle, yAngle); }

            // If the camera is set to rotate, update the camera's rotation.
            if (this.FPCamera != null &&
                (this.CameraXRotation || this.CameraYRotation))
            {
                Quaternion cameraRotation = Quaternion.Euler(this.CameraXRotation ? this.mouseRotationX : 0.0f,
                                                                this.CameraYRotation ? this.mouseRotationY : 0.0f,
                                                                0.0f);

                this.FPCamera.transform.localRotation = cameraRotation;
            }

            // If the object is set to rotate, rotate the actual object.
            if (this.ObjectXRotation || this.ObjectYRotation)
            {
                Quaternion objectRotation = Quaternion.Euler(this.ObjectXRotation ? this.mouseRotationX : 0.0f,
                                                                this.ObjectYRotation ? this.mouseRotationY : 0.0f,
                                                                0.0f);

                this.transform.localRotation = objectRotation;
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        ///  Validates the configuration for the object, warrning the developer if anything is incorrect.
        /// </summary>
        private void ValidateConfiguration()
        {
            // Throw an exception on the object if we have more than one X or Y rotation tracked at a time.
            if ((this.CameraXRotation && this.ObjectXRotation) ||
                (this.CameraYRotation && this.ObjectYRotation))
            {
                throw new ArgumentException("Only 1 object/camera can be tracked for either the X or Y axis at a time.  Ensure 1 X and 1 Y is checked.");
            }

            // Throw an exception on the object if we have no camera, but axes configured on the camera.
            if ((this.CameraXRotation || this.CameraYRotation) &&
                this.FPCamera == null)
            {
                throw new ArgumentException($"{ this.gameObject.name } is configured to track rotation for a camera, but no camera has been passed to the object.");
            }
        }

        #endregion
    }
}