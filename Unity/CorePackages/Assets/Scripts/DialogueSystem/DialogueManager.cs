using Assets.Scripts.Core;
using Hans.Logging;
using Hans.Logging.Interfaces;

namespace Assets.Scripts.DialogueSystem
{
    /// <summary>
    ///  Manager class to maintain script reading/reacting in any scene.
    /// </summary>
    public class DialogueManager : Singleton<DialogueManager>
    {
        /// <summary>
        ///  Logger object.
        /// </summary>
        private ILogger _log;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            this._log = LoggerManager.CreateLogger(typeof(DialogueManager));
        }

        #endregion
    }
}
