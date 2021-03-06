﻿using Hans.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Weapon Handler for player interactions - Tracks input keys, state managements, etc.
/// </summary>
public class WeaponHandler : MonoBehaviour
{
    #region Internal Properties

    /// <summary>
    ///  The Equipped Weapon and all its info.
    /// </summary>
    private Weapon _equippedWeapon;

    /// <summary>
    ///  Whether the character is currently aiming or not.
    /// </summary>
    private bool _isAiming;

    /// <summary>
    ///  Whether the character is actively firing or not.
    /// </summary>
    private bool _isFiring;

    /// <summary>
    ///  Indicates whether or not the stored weapon is a firearm. (Handled differently.)
    /// </summary>
    private bool _isFirearm;

    /// <summary>
    ///  Logger used to import useful information about the class.
    /// </summary>
    private Hans.Logging.Interfaces.ILogger log;

    #endregion

    #region Properties

    /// <summary>
    ///  Animator we'll use to play animations with the weapon.
    /// </summary>
    public Animation CharacterAnimator;

    /// <summary>
    ///  The POV of the entity holding this weapon, for aiming calculations.
    /// </summary>
    public GameObject EntityPOV;

    /// <summary>
    ///  Where to anchor the weapon once spawned.
    /// </summary>
    public GameObject WeaponAnchor;

    #endregion

    #region Unity Methods

    void Awake()
    {
        this.log = LoggerManager.CreateLogger(typeof(WeaponHandler));
    }

    /// <summary>
    ///  Called Every Frame.
    /// </summary>
    void Update()
    {
        if (this._equippedWeapon != null)
        {
            if (this._isFirearm &&
                Input.GetButtonDown(ButtonNames.Reload))
            {
                this.log.LogMessage($"Reloading { this._equippedWeapon.Name }");
                return;
            }

            // Track whether or not the weapon is being aimed.
            this.SetAiming(Input.GetButton(ButtonNames.Aim));

            // Set the firing mechanism, and trigger the co-routine if necessary.
            if (!this._isFiring)
            {
                this._isFiring = (this._isFirearm && (this._equippedWeapon as Firearm).IsAuto && Input.GetButton(ButtonNames.Fire)) ||
                                    Input.GetButtonDown(ButtonNames.Fire);

                // If this is a fire action, we should trigger the fire coroutine.
                if (this._isFiring)
                {
                    StartCoroutine("FireAction");
                }
            }
        }
    }

    #endregion

    #region Instance Properties

    /// <summary>
    ///  Equips a weapon within this handler, so we can track interactions with it.
    /// </summary>
    /// <param name="weaponToEquip">The weapon we're going to equip and start tracking.</param>
    public void EquipWeapon(Weapon weaponToEquip)
    {
        if (weaponToEquip == null)
        {
            return;
        }

        // If they're already holding a weapon, get rid of it.
        if (this._equippedWeapon != null)
        {
            Destroy(this._equippedWeapon.gameObject);
        }

        // TODO: Rotation Needs to be Dynamic.
        var spawnedWeapon = Instantiate(weaponToEquip.gameObject, this.WeaponAnchor.transform);
        spawnedWeapon.transform.localRotation = Quaternion.Euler(180, 90, 90);

        // Equip the weapon, and assign the POV.
        this._equippedWeapon = spawnedWeapon.GetComponent<Weapon>();
        this._isFirearm = this._equippedWeapon.GetType() == typeof(Firearm);

        // Initialize the weapon state.
        this._equippedWeapon.Initialize(this.CharacterAnimator, this._isFirearm ? this.EntityPOV : null);
    }

    #endregion

    #region Internal Methods

    /// <summary>
    ///  Couroutine that handles the firing mechanisms of the given weapon. Assumes firing is true, for at least the first run.
    /// </summary>
    /// <returns>Runs until firing action is complete.</returns>
    private IEnumerator FireAction()
    {
        while (this._isFiring)
        {
            this._equippedWeapon.Attack(this._isAiming);

            if (!this._isFirearm ||
                !(this._equippedWeapon as Firearm).IsAuto)
            {
                this._isFiring = false;
            }
            else
            {
                this._isFiring = Input.GetButton(ButtonNames.Fire);
            }

            yield return new WaitForSeconds(this._equippedWeapon.AttackRate);
        }

        // When the firing action completes, return out from the co-routine.
        yield return null;
    }

    /// <summary>
    ///  Sets whether or not the character is aiming or not.
    /// </summary>
    /// <param name="isAiming">If they're currently aiming.</param>
    private void SetAiming(bool isAiming)
    {
        if (this._isAiming != isAiming)
        {
            this._isAiming = isAiming;
            this._equippedWeapon.Focus(this._isAiming, this.CharacterAnimator);
        }
    }

    #endregion
}
