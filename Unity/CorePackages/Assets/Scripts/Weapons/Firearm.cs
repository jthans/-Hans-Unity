using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Main Firearm Class - Ranged weapons that can deal damage, manage ammo, etc. 
/// </summary>
public class Firearm : Weapon
{
    #region Fields

    /// <summary>
    ///  If ADS is currently active.
    /// </summary>
    private bool _isADS;

    /// <summary>
    ///  The camera's original FOV.
    /// </summary>
    private float _originalFOV;

    #endregion

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
    ///  How quickly the FOV transitions from the player FOV to the firearm, or vice versa.
    /// </summary>
    public float FOVSpeed = 1.0f;

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
    /// <param name="isADS">If the weapon is being aimed or not.</param>
    /// <param name="animObj">The object to animate with the fire action, if any.</param>
    /// <returns>If the attack impacted anything.</returns>
    public override bool Attack(bool isADS = false, Animation animObj = null)
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

    /// <summary>
    ///  Aims with the given weapon, by playing the appropriate animations and focusing the weapon.
    /// </summary>
    /// <param name="isADS">If the weapon is currently being aimed.</param>
    /// <param name="animObj">The object to animate with the weapon, if one exists.</param>
    public override void Focus(bool isADS = false, Animation animObj = null)
    {
        this._isADS = isADS;

        var fovCoroutine = ManageFOV(isADS ? this.ADS_FOV : this._originalFOV, !isADS);
        StartCoroutine(fovCoroutine);

        if (isADS)
        {
            animObj?.Play("ADS", PlayMode.StopAll);
            return;
        }
        
        animObj?.Play("Idle", PlayMode.StopAll);
    }

    public override void Initialize(Animation animObj = null, GameObject weaponPov = null)
    {
        base.Initialize(animObj);
        
        animObj?.AddClip(this.Anim_ADS, "ADS");
        this.MuzzleObj = weaponPov;
        this._originalFOV = Camera.main.fieldOfView;
    }

    /// <summary>
    ///  Manages the speed at which the FOV changes for this weapon, and updates the camera.
    /// </summary>
    /// <param name="targetFOV">FOV we're moving to.</param>
    /// <param name="isADSInitial">What the ADS setting was when this coroutine started.</param>
    /// <returns>Continues until the FOV is reached, or a new </returns>
    private IEnumerator ManageFOV(float targetFOV, bool isADSInitial)
    {
        float fovChange = (targetFOV - Camera.main.fieldOfView) / (this.FOVSpeed / Time.fixedDeltaTime);
        
        // If the ADS has changed or we've reached our target, stop the routine.
        while (isADSInitial != this._isADS &&
              ((isADSInitial && Camera.main.fieldOfView < this._originalFOV) ||
              (!isADSInitial && Camera.main.fieldOfView > this.ADS_FOV)))
        {
            Camera.main.fieldOfView += fovChange;
            yield return null;
        }
    }

#endregion
}
