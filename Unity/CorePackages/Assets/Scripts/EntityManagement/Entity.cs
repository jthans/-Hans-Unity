using Assets.Scripts.DamageManagement;
using Assets.Scripts.EntityManagement;
using Hans.DamageSystem.Events;
using UnityEngine;

/// <summary>
///  Represents the base entity class, and manages all things an entity will need to care about, such as health, death events, etc.  Any more specific implementations
///     will be inherited from this class.
/// </summary>
[RequireComponent(typeof(IDComponent))]
public class Entity : MonoBehaviour
{
    #region Fields

    /// <summary>
    ///  How much health this entity will start with.
    /// </summary>
    public decimal StartHealth = 100;

    #endregion

    #region Properties

    /// <summary>
    ///  ID Property to read from the IDComponent object attached.  This caches the ID inside the entity, as it won't be changed after creation.
    /// </summary>
    private string _id;
    public string Id
    {
        get
        {
            if (string.IsNullOrEmpty(this._id))
            {
                this._id = this.GetComponent<IDComponent>().ID;
            }

            return this._id;
        }
    }

    #endregion

    #region Unity Methods

    /// <summary>
    ///  Used for initialization, when the entity is created in the scene.
    /// </summary>
    void Start()
    {
        // Register the entity for damage tracking, if it hasn't been already.
        // DamageSystemManager.Instance.RegisterEntity(this);
        
    }

    #endregion

    #region Events

    /// <summary>
    ///  Placeholder for what happens when an entity dies.
    /// </summary>
    /// <param name="deathArgs"></param>
    public void OnEntityDeath(object sender, EntityDeathEventArgs deathArgs)
    {
        
    }

    #endregion
}
