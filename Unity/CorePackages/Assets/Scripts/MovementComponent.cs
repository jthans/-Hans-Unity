namespace Hans.Unity
{
    using Hans.Extensions;
    using Hans.Unity.Enums;
	using UnityEngine;

    /// <summary>
    ///  MovementComponent class, used to allow player input into the character's avatar to move/interact with the space/etc.
    /// </summary>
	[RequireComponent(typeof(Rigidbody))]
    public class MovementComponent : MonoBehaviour 
	{
		#region Fields

		private object _collisionLock;

		/// <summary>
		///  If the object is located on the ground.
		/// </summary>
		private bool _isGrounded = false;

		/// <summary>
		///  If the object is jumping currently, meaning not on the ground, or triggering a jump this frame.
		/// </summary>
		private bool _isJumping = false;

		/// <summary>
		///  Indicates the number of jumps that have been taken by this object. This is tracked to support multiple jumps in one.
		/// </summary>
		private int _numJumps = 0;

		/// <summary>
		///  Enum of 4 directions (Forward/Backward/Left/Right) to track which direction is moving at any given time.  Pulling this out allows easy debugging/reporting,
		/// 	as this will always give the enabled directions, as well as ways to intercept this data if necessary.  This enum stores combinations of the flags as necessary.
		/// </summary>
		private Direction _objectDirection;

        #endregion

        #region Properties/Configuration

		/// <summary>
		///  How many times the character may jump at once.
		/// </summary>
		public int AllowedJumps = 1;

		/// <summary>
		///  Whether or not we'll allow the character to jump from the air, or if it needs ground.
		/// </summary>
		public bool AllowJumpWhileFloating = false;

		/// <summary>
		///  How high the character jumps.
		/// </summary>
		public float JumpHeight = 1.0f;

        /// <summary>
        ///  How fast the character walks.
        /// </summary>
        [Tooltip("Float Value. How fast the character walks, in relation to time.  Represents unit/s.")]
        public float WalkSpeed = 3.0f;

		#endregion

		#region Unity Methods
		
		/// <summary>
		///  Called before the scene starts processing, but when we can initialize components of our object.true  Used for initialization that doesn't
		/// 	require access to other objects in the scene.
		/// </summary>
		void Awake() {
			this.InitializeLocal();
		}

		/// <summary>
		///  Called the same number of times per second - This is where the bulk of our calculations will be done, to avoid more performant computers
		///   	from processing things faster.
		/// </summary>
		void FixedUpdate() {
			this.ProcessMovement();
		}

		/// <summary>
		///  Called when a collion is considered "exited", or over - Meaning they fell off the land.
		/// </summary>
		void OnCollisionExit() {
			if (!this._isJumping)
			{
				this._isGrounded = false;
				this._numJumps = 1; // We start this at 1 jump, so we can't save ourselves if we're careless.
			}
		}

		/// <summary>
		///  Called when a collion is considered "stayed" or consistent. This tells us that our object is on the ground, and isn't considered floating.
		/// </summary>
		void OnCollisionStay() {
			if (!this._isJumping)
			{
				this._isGrounded = true;
				this._numJumps = 0;
			}
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
			if (this._isGrounded)
			{
				// Update Directional Buttons, based on whether the input is selected on the input or not.
				this._objectDirection = Input.GetButton(ButtonNames.Forward) ? this._objectDirection |= Direction.Forwards : this._objectDirection &= ~Direction.Forwards;
				this._objectDirection = Input.GetButton(ButtonNames.Backward) ? this._objectDirection |= Direction.Backwards : this._objectDirection &= ~Direction.Backwards;
				this._objectDirection = Input.GetButton(ButtonNames.Left) ? this._objectDirection |= Direction.Left : this._objectDirection &= ~Direction.Left;
				this._objectDirection = Input.GetButton(ButtonNames.Right) ? this._objectDirection |= Direction.Right : this._objectDirection &= ~Direction.Right;
			}

			// Update Jump Status
			this._isJumping = this._isJumping || Input.GetButtonDown(ButtonNames.Jump);
        }

		/// <summary>
		///	 Processes movement based on the directional information stored in the current player instance.  Moves based on the configured values.
		/// </summary>
		private void ProcessMovement()
		{
            // Get the directional vectors for this object.
            var forwardVector = this.gameObject.transform.forward;
            var rightVector = this.gameObject.transform.right;

			#region Movement

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

			#endregion

#region Jumping

			if (this._isJumping && this._numJumps < this.AllowedJumps)
			{
				var charComponent = this.GetComponent<Rigidbody>();

				// Add an upward force with the magnitude configured.
				charComponent.AddForce(Vector3.up * this.JumpHeight, ForceMode.Impulse);
				this._numJumps++;

				this._isJumping = false;
			}

#endregion
		}

		/// <summary>
		///	 Initializes values necessary to this object only, without reference to other objects in the scene.
		/// </summary>
		private void InitializeLocal()
		{
			// This line exists to prevent an object from jumping as soon as the scene starts.
			this._numJumps = this.AllowedJumps;
		}

		#endregion
	}
}