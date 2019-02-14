using UnityEngine;

namespace Assets.Scripts.Core
{
    /// <summary>
    ///  Core Singleton class that allows us to develop objects that utilize the singleton pattern - This means that no matter how many copies of the class
    ///     exist in a scene, there will always only be 1 instance that all objects can read from.
    /// </summary>
    /// <typeparam name="T">What class this singleton represents.  Will share this object type for all objects that access it.</typeparam>
    public class Singleton<T> : MonoBehaviour
        where T : Singleton<T>
    {
        #region Properties

        private static T _instance;
        public static T Instance { get { return _instance; } }

        #endregion
        
        #region Unity Methods

        /// <summary>
        ///  Used for initialization - Called before the scene loads/starts rendering.
        /// </summary>
        protected virtual void Awake()
        {
            this.SetSingleton();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        ///  Most Singlestons will need initialization - Give it the opportunity to do so.
        /// </summary>
        protected virtual void Initialize()
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        ///  Maintains setting the singleton instance the first time it needs to be created. 
        /// </summary>
        private void SetSingleton()
        {
            // If an instance already exists, we won't override it - Once it's there, it stays there.
            if (_instance == null)
            {
                _instance = (T) this;
                this.Initialize();
            }
        }

        #endregion
    }
}
