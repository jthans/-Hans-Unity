using Assets.Scripts.Core;
using Hans.Logging;
using Hans.Logging.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
    /// <summary>
    ///  Entities that are cached once loaded, so we don't search the scene every time.
    /// </summary>
    private Dictionary<string, GameObject> _cachedEntities = new Dictionary<string, GameObject>();

    /// <summary>
    ///  Logging class to output data about what's happening.
    /// </summary>
    private Hans.Logging.Interfaces.ILogger _log;

    /// <summary>
    ///  Initialization
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        this._log = LoggerManager.CreateLogger(typeof(EntityManager));
    }

    /// <summary>
    ///  Gets the entity with the ID requested, and adds it to the cache.
    /// </summary>
    /// <param name="entityId">Entity ID to be searched.</param>
    /// <returns>The entity game object found.</returns>
    public GameObject FindEntityWithId(string entityId)
    {
        if (string.IsNullOrEmpty(entityId)) { return null; }

        this._log.LogMessage($"Locating Entity { entityId }.");
        if (this._cachedEntities.ContainsKey(entityId))
        {
            this._log.LogMessage($"Returning Cached Entity.");
            return this._cachedEntities[entityId];
        }

        var foundEntities = GameObject.FindObjectsOfType(typeof(Entity));
        var entityToLocate = foundEntities.Select(x => x as Entity).FirstOrDefault(x => x.Id == entityId);

        if (entityToLocate != null)
        {
            this._cachedEntities.Add(entityId, entityToLocate.gameObject);

            this._log.LogMessage($"Returning Newly Loaded Entity.");
            return this._cachedEntities[entityId];
        }

        this._log.LogMessage($"Entity Not Found.");
        return null;
    }
}
