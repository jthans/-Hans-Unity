using Assets.Scripts.Core;
using Hans.Logging;
using System;

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

    #region Unity Methods

    /// <summary>
    ///  Called on creation, we'll do any initialization here.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        this.InitializeScene();
	}

    #endregion

    #region Internal Methods

    /// <summary>
    ///  Initializes a scene with -Hans-Unity specific instructions and benefits.
    /// </summary>
    private void InitializeScene()
    {
        // Session
        this.SessionID = Guid.NewGuid();

        // Logging
        LoggerManager.StartLogging();
    }

    #endregion
}
