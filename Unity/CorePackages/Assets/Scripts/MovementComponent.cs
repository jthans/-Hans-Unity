namespace Hans.Unity
{
    using Hans.Extensions;
    using Hans.Unity.Enums;
	using UnityEngine;

    /// <summary>
    ///  MovementComponent class, used to allow player input into the character's avatar to move/interact with the space/etc.
    /// </summary>
    public class MovementComponent : MonoBehaviour 
	{
		#region Fields

		/// <summary>
		///  Enum of 4 directions (Forward/Backward/Left/Right) to track which direction is moving at any given time.  Pulling this out allows easy debugging/reporting,
		/// 	as this will always give the enabled directions, as well as ways to intercept this data if necessary.  This enum stores combinations of the flags as necessary.
		/// </summary>
		private Direction _objectDirection;

        #endregion

        #region Properties/Configuration

        /// <summary>
        ///  How fast the character walks.
        /// </summary>
        [Tooltip("Float Value. How fast the character walks, in relation to time.  Represents unit/s.")]
        public float WalkSpeed = 3.0f;

		#endregion

		#region Unity Methods

		/// <summary>
		///  Called the same number of times per second - This is where the bulk of our calculations will be done, to avoid more performant computers
		///   	from processing things faster.
		/// </summary>
		void FixedUpdate() {
			this.ProcessMovement();
		}
		
		/// <summary>
		///  Called every frame update - We'll only use this for input recognition/updating, as these need to be close to real-time.
		/// </summary>
		void Update () {
            this.ProcessInput();
		}

		#endregion

		#region Internal Methods

        /// <summary>
        ///  Processes the input from the user's keyboard/controller at the time of Update.  Updates any internal properties, and performs any events that need to take place.
        /// </summary>
        private void ProcessInput()
        {
            // Update Directional Buttons, based on whether the input is selected on the input or not.
            this._objectDirection = Input.GetButton(ButtonNames.Forward) ? this._objectDirection |= Direction.Forwards : this._objectDirection &= ~Direction.Forwards;
            this._objectDirection = Input.GetButton(ButtonNames.Backward) ? this._objectDirection |= Direction.Backwards : this._objectDirection &= ~Direction.Backwards;
            this._objectDirection = Input.GetButton(ButtonNames.Left) ? this._objectDirection |= Direction.Left : this._objectDirection &= ~Direction.Left;
            this._objectDirection = Input.GetButton(ButtonNames.Right) ? this._objectDirection |= Direction.Right : this._objectDirection &= ~Direction.Right;
        }

		/// <summary>
		///	 Processes movement based on the directional information stored in the current player instance.  Moves based on the configured values.
		/// </summary>
		private void ProcessMovement()
		{
            // Get the directional vectors for this object.
            var forwardVector = this.gameObject.transform.forward;
            var rightVector = this.gameObject.transform.right;

			var objectVector = this.gameObject.transform.position;

			// Forwards
			if (this._objectDirection.IsFlagSet(Direction.Forwards))
			{
                objectVector += forwardVector * this.WalkSpeed * Time.fixedDeltaTime;
			}

			// Backwards
			if (this._objectDirection.IsFlagSet(Direction.Backwards))
			{
                objectVector -= forwardVector * this.WalkSpeed * Time.fixedDeltaTime;
			}

			// Left
			if (this._objectDirection.IsFlagSet(Direction.Left))
			{
				objectVector -= rightVector * this.WalkSpeed * Time.fixedDeltaTime;
			}

			// Right
			if (this._objectDirection.IsFlagSet(Direction.Right))
			{
				objectVector += rightVector * this.WalkSpeed * Time.fixedDeltaTime;
			}

			this.gameObject.transform.position = objectVector;
		}

		#endregion
	}
}