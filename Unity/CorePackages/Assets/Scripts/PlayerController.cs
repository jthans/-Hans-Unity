namespace Hans.Unity
{
    using Hans.Extensions;
    using Hans.Unity.Enums;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	/// <summary>
	///  PlayerController class, used to allow player input into the character's avatar to move/interact with the space/etc.  This is one of the
	///   most critical parts of any Unity FPS game, this may also eventually be expanded to other POVs in the future.
	/// </summary>
	public class PlayerController : MonoBehaviour 
	{
		#region Fields

		/// <summary>
		///  Enum of 4 directions (Forward/Backward/Left/Right) to track which direction is moving at any given time.  Pulling this out allows easy debugging/reporting,
		/// 	as this will always give the enabled directions, as well as ways to intercept this data if necessary.  This enum stores combinations of the flags as necessary.
		/// </summary>
		private Direction _playerDirection;

		#endregion

		#region Properties/Configuration

		public float WalkSpeed = 3.0f;

		#endregion

		#region Unity Methods

		// Start
		void Start () {
			
		}

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
            Debug.Log(this._playerDirection);

            // Update Directional Buttons, based on whether the input is selected on the input or not.
            this._playerDirection = Input.GetButton(ButtonNames.Forward) ? this._playerDirection |= Direction.Forwards : this._playerDirection &= ~Direction.Forwards;
            this._playerDirection = Input.GetButton(ButtonNames.Backward) ? this._playerDirection |= Direction.Backwards : this._playerDirection &= ~Direction.Backwards;
            this._playerDirection = Input.GetButton(ButtonNames.Left) ? this._playerDirection |= Direction.Left : this._playerDirection &= ~Direction.Left;
            this._playerDirection = Input.GetButton(ButtonNames.Right) ? this._playerDirection |= Direction.Right : this._playerDirection &= ~Direction.Right;
        }

		/// <summary>
		///	 Processes movement based on the directional information stored in the current player instance.  Moves based on the configured values.
		/// </summary>
		private void ProcessMovement()
		{
			var playerVector = this.gameObject.transform.position;

			// Forwards
			if (this._playerDirection.IsFlagSet(Direction.Forwards))
			{
				playerVector.z += this.WalkSpeed * Time.fixedDeltaTime;
			}

			// Backwards
			if (this._playerDirection.IsFlagSet(Direction.Backwards))
			{
				playerVector.z -= this.WalkSpeed * Time.fixedDeltaTime;
			}

			// Left
			if (this._playerDirection.IsFlagSet(Direction.Left))
			{
				playerVector.x -= this.WalkSpeed * Time.fixedDeltaTime;
			}

			// Right
			if (this._playerDirection.IsFlagSet(Direction.Right))
			{
				playerVector.x += this.WalkSpeed * Time.fixedDeltaTime;
			}

			this.gameObject.transform.position = playerVector;
		}

		#endregion
	}
}