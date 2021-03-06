﻿using Assets.Scripts.DamageManagement;
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
    ///  Indicates whether or not this entity is damageable or not.
    /// </summary>
    public bool Damageable = true;

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
        if (this.Damageable)
        {
            // Register the entity for damage tracking, if it hasn't been already.
            DamageSystemManager.Instance.RegisterEntity(this);
        }
    }

    #endregion

    #region Events

    /// <summary>
    ///  Placeholder for what happens when an entity dies.
    /// </summary>
    /// <param name="deathArgs"></param>
    public void OnEntityDeath()
    {
        Debug.Log($"{ this.Id } DEAD.");
    }

    /// <summary>
    ///  Event that occurs when an impact is recieved by this entity.  We'll damage/show effects, etc.
    /// </summary>
    /// <param name="dmgAmount">How much damage is associated with this impact.</param>
    /// <param name="raycastHit">The hit that occured, to indicate where we impacted.</param>
    /// <param name="inflictingEntity">The entity that dealt this damage.</param>
    public void OnImpactReceived(decimal dmgAmount, RaycastHit raycastHit, Entity inflictingEntity = null)
    {
        if (this.Damageable)
        {
            // TODO: Dynamic Damage Type!
            DamageSystemManager.Instance.ApplyDamage(this, new Hans.DamageSystem.Models.DamageUnit() { BaseHealth = dmgAmount });
        }
    }

    #endregion
}
