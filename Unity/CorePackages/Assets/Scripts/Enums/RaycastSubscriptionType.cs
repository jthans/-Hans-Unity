namespace Assets.Scripts.Enums
{
    /// <summary>
    ///  This signifies what type of subscription a <see cref="Assets.Scripts.Core.Raycasting.IRaycastSubscriber" /> is pertaining to - Different subscribers
    ///     will subscribe to different types of events.
    /// </summary>
    public enum RaycastSubscriptionType
    {
        // For efficiency's sake, tag should be the most used, and items/objects tagged appropriately.
        Tag,

        // Sometimes a subscriber may be looking for a particular name.
        Name
    }
}
