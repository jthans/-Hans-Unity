using System;
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
    ///  The angle that forms the shooting cone when ADS.
    /// </summary>
    public float ADS_ShotAngle = 5;

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
    ///  If we should render debugger lines or not.
    /// </summary>
    public bool DebuggerLineEnabled;

    /// <summary>
    ///  If this weapon is fully auto (can hold down trigger to fire.)
    /// </summary>
    public bool IsAuto;

    /// <summary>
    ///  The point of bullet tracing on the weapon.
    /// </summary>
    public GameObject MuzzleObj;

    /// <summary>
    ///  The angle that forms the shooting cone when hip-firing.
    /// </summary>
    public float ShotAngle = 20;

    /// <summary>
    ///  How far the bullets shoot once they leave the weapon.
    /// </summary>
    public float ShotDistance = 20;

    #endregion

    #region Instance Methods

    /// <summary>
    ///  Firearm's attack method, will use raycasting/distance settings to determine a successful hit.
    /// </summary>
    /// <returns>If the attack impacted anything.</returns>
    public override bool Attack(bool isADS = false)
    {
        // Set up the initial point, and the direction in which we're shooting.
        Vector3 pointOfShot = this.MuzzleObj.transform.position;
        Vector3 resultantRay = UnityEngine.Random.insideUnitCircle * (float)(Math.Tan(Mathf.Deg2Rad * (isADS ? this.ADS_ShotAngle : this.ShotAngle)) * this.ShotDistance);
        // Vector3 resultantRay = Random.insideUnitCircle * (isADS ? this.ADS_ShotAngle : this.ShotAngle);
        resultantRay.z = this.ShotDistance;

        // Transform the direction to our current weapon position.
        resultantRay = this.MuzzleObj.transform.TransformDirection(resultantRay);

        // Perform the raycast, to see if we impact anything.
        RaycastHit outHit;
        if(Physics.Raycast(pointOfShot, resultantRay, out outHit, this.ShotDistance))
        {

        }

        #if UNITY_EDITOR
            // If debugging is enabled, draw a line that's visible to the player.
            if (this.DebuggerLineEnabled)
            {
                Debug.DrawLine(this.MuzzleObj.transform.position, this.transform.TransformDirection(resultantRay) * this.ShotDistance, Color.blue);
            }
        #endif

        return base.Attack(isADS);
    }

#endregion
}
