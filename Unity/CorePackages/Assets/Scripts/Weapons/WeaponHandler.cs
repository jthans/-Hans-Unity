using Hans.Logging;
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

        this._equippedWeapon = weaponToEquip;
        this._isFirearm = weaponToEquip.GetType() == typeof(Firearm);

        #region Animations

        IList<AnimationClip> animationClips = new List<AnimationClip>();
        this.CharacterAnimator.AddClip(this._equippedWeapon.Anim_Idle, "Idle");

        if (this._isFirearm)
        {
            var firearmWeapon = this._equippedWeapon as Firearm;
            this.CharacterAnimator.AddClip(firearmWeapon.Anim_ADS, "ADS");
        }

        #endregion
    }

    #endregion

    #region Internal Methods

    /// <summary>
    ///  Sets whether or not the character is aiming or not.
    /// </summary>
    /// <param name="isAiming">If they're currently aiming.</param>
    private void SetAiming(bool isAiming)
    {
        if (this._isAiming != isAiming)
        {
            this._isAiming = isAiming;
            if (this._isAiming)
            {
                this.log.LogMessage($"ADS");
                this.CharacterAnimator.Play("ADS", PlayMode.StopAll);
            }
            else
            {
                this.CharacterAnimator.Play("Idle", PlayMode.StopAll);
            }
        }
    }

    #endregion
}
