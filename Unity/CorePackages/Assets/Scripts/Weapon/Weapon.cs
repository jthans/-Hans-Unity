using Assets.Scripts.Core;
using Hans.Logging;
using Hans.Logging.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Main Weapon Class - Contains any information about weapons that are needed to maintain inventory, inflict basic damage, store weapon information, etc.
/// </summary>
public class Weapon : MonoBehaviour
{
    #region Fields

    /// <summary>
    ///  Logging class to output data about what's happening.
    /// </summary>
    private Hans.Logging.Interfaces.ILogger _log;

    #endregion

    #region Properties

    /// <summary>
    ///  Animation to play when the character attacks with this weapon.
    /// </summary>
    public AnimationClip Anim_Attack;

    /// <summary>
    ///  Animation to play while a character is idle.  (Unmoving.)
    /// </summary>
    public AnimationClip Anim_Idle;

    /// <summary>
    ///  Attack rate (in seconds) of the weapon, meaning how quickly you can attack with the weapon.
    /// </summary>
    public float AttackRate = 1.0f;

    /// <summary>
    ///  Name of the Weapon.
    /// </summary>
    public string Name;

    #endregion

    #region Unity Methods

    /// <summary>
    ///  Initialization.
    /// </summary>
    void Start()
    {
        this._log = LoggerManager.CreateLogger(typeof(Weapon));
    }

    #endregion

    #region Instance Methods

    /// <summary>
    ///  Base Attack Action, in case some types have specialized versions.
    /// </summary>
    /// <param name="isADS">If the weapon is being aimed or not.</param>
    /// <param name="animObj">The object to animate with the fire action, if any.</param>
    /// <returns>If the attack made impact.</returns>
    public virtual bool Attack(bool isADS = false, Animation animObj = null)
    {
        this._log?.LogMessage($"Attack w/ Weapon { this.Name }.");
        return true;
    }

    /// <summary>
    ///  What to do when the weapon is aimed ADS, or not.  Handles animations/interactions per weapon type.
    /// </summary>
    /// <param name="isADS">If the weapon is currently being focused.</param>
    /// <param name="animObj">The object to animate with the fire action, if any.</param>
    public virtual void Focus(bool isADS = false, Animation animObj = null)
    {
        return;
    }

    public virtual void Initialize(Animation animObj = null, GameObject weaponPov = null)
    {
        animObj?.AddClip(this.Anim_Idle, "Idle");
        animObj?.Play("Idle", PlayMode.StopAll);
    }

    #endregion
}
