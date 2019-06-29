using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Main Firearm Class - Ranged weapons that can deal damage, manage ammo, etc. 
/// </summary>
public class Firearm : Weapon
{
    #region Properties

    /// <summary>
    ///  FOV used when a player is aiming.
    /// </summary>
    public float ADS_FOV = 30;

    /// <summary>
    ///  ADS Animation (Aiming)
    /// </summary>
    public AnimationClip Anim_ADS;

    /// <summary>
    ///  Reload Animation
    /// </summary>
    public AnimationClip Anim_Reload;

    /// <summary>
    ///  Clip Size of the Weapon
    /// </summary>
    public int ClipSize;

    /// <summary>
    ///  If this weapon is fully auto (can hold down trigger to fire.)
    /// </summary>
    public bool IsAuto;

    #endregion

    #region Instance Methods

    /// <summary>
    ///  Firearm's attack method, will use raycasting/distance settings to determine a successful hit.
    /// </summary>
    /// <returns></returns>
    public override bool Attack()
    {
        return base.Attack();
    }

    #endregion
}
