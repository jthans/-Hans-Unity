using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Main Firearm Class - Ranged weapons that can deal damage, manage ammo, etc. 
/// </summary>
public class Firearm : Weapon
{
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
}
