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
    void Awake()
    {
        this._log = LoggerManager.CreateLogger(typeof(Weapon));
    }

    #endregion

    #region Instance Methods

    /// <summary>
    ///  Base Attack Action, in case some types have specialized versions.
    /// </summary>
    /// <returns></returns>
    public virtual bool Attack()
    {
        this._log?.LogMessage($"Attack w/ Weapon { this.Name }.");
        return true;
    }

    #endregion
}
