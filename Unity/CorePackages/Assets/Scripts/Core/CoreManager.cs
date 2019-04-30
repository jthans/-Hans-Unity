using Assets.Scripts.Core;
using Hans.DependencyInjection;
using Hans.Logging;
using System;
using UnityEngine;

/// <summary>
///  The <see cref="CoreManager" /> class is used in every Unity scene that uses -Hans-Unity/CorePackages assets or objects.  This sets up the critical 
///     pieces of the application, so the scene is always ready to utilize the objects.  
///  NOTE: This is to be expanded with a configuration later.
/// </summary>
public class CoreManager : Singleton<CoreManager>
{
    #region Properties

    /// <summary>
    ///  A session ID will be generated whenever a scene is created - Eventually, we'll need to carry this over across scenes/loads.
    /// </summary>
    public Guid SessionID { get; set; }

    #endregion
    
    #region Internal Methods

    /// <summary>
    ///  Initializes a scene with -Hans-Unity specific instructions and benefits.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitializeScene()
    {
#if UNITY_EDITOR

        // Dependency Injection
        // MEFBootstrapper.RegisterPath(UnityEngine.Application.dataPath + "/../Library/ScriptAssemblies");

#endif

        MEFBootstrapper.Build();

        // Logging
        LoggerManager.StartLogging();
        LoggerManager.CreateLogger(typeof(CoreManager)).LogMessage($"Initialization Complete.");
    }

    #endregion
}
