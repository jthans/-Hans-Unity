namespace Hans.Unity.Enums
{
	using System;

	/// <summary>
	///  Directional Enum, that represents a direction an avatar can, or will be moving.  Various combinations
	/// 	of these values will give more precise/varied directions.
	/// </summary>
	[Flags]
	public enum Direction
	{
		Forwards = 1,
		
		Backwards = 2,

		Left = 4,

		Right = 8
	}
}