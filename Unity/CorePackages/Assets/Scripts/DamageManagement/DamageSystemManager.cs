using Assets.Scripts.Core;
using Hans.DamageSystem;
using Hans.DamageSystem.Events;
using Hans.DamageSystem.Models;
using Hans.Logging;
using Hans.Logging.Interfaces;

namespace Assets.Scripts.DamageManagement
{
	/// <summary>
	///  Singleton Manager for the Damage System, that allows damage to be tracked across entities/objects/anything that needs it,
	/// 	in a single repeatable place.  This damage system will interact with whatever the Hans system is running as its backend.
	/// </summary>
	public class DamageSystemManager : Singleton<DamageSystemManager> 
	{
        /// <summary>
        ///  The damage controller for the entire scene, that manages entity/damage relationships.
        /// </summary>
        private DamageController<DamageUnit> _damageController;

        /// <summary>
        ///  Logger object.
        /// </summary>
        private ILogger _log;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            this._log = LoggerManager.CreateLogger(typeof(DamageSystemManager));

            // TODO: Make this a dynamic type, instead of just the base.
            this._damageController = new DamageController<DamageUnit>();
        }

        protected void Start()
        {
            
        }

        #endregion

        #region Manager Methods

        /// <summary>
        ///  Applies damage to an entity based on the damage unit created.
        /// </summary>
        /// <param name="targetEntity">Which entity should take this damage.</param>
        /// <param name="damageAmt">The amount of damage to apply.</param>
        public void ApplyDamage(Entity targetEntity, DamageUnit damageAmt)
        {
            var remainingHealth = this._damageController.ApplyDamage(targetEntity.Id, damageAmt);
            this._log.LogMessage($"Entity { targetEntity.Id } Health Remaining: { remainingHealth.BaseHealth }");
        }

        /// <summary>
        ///  Registers an entity with the damage manager, in order to track it's health throughout.
        /// </summary>
        /// <param name="entityId">The entity to register in the manager.</param>
        /// <param name="startHealth">How much health the entity should start with.</param>
        public void RegisterEntity(Entity entityRef)
        {
            this._damageController.DamageManager.BeginTrackingDamage(entityRef.Id, entityRef.StartHealth);
            this._damageController.OnEntityDeath += this.OnEntityDeath;
        }

        #endregion

        /// <summary>
        ///  When an entity dies, we need to call the appropriate death handler.
        /// </summary>
        /// <param name="sender">Sender that has declared "death".</param>
        /// <param name="deathArgs">The event arguments we're subscribing to.</param>
        private void OnEntityDeath(object sender, System.EventArgs deathArgs)
        {
            var affectedEntity = (deathArgs as EntityDeathEventArgs)?.EntityId;
            if (affectedEntity != null)
            {
                EntityManager.Instance.FindEntityWithId(affectedEntity)?.GetComponent<Entity>()?.OnEntityDeath();
            }
        }
    }
}