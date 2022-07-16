using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GMTK2022.Utils
{
    /// <summary>
    /// Singleton Pattern: where there's only one instance of the gameObject who
    /// inherit from the Singleton Pattern.
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        // Variable
        // ------------------------------------------------------------------------
        protected static T _instance;

        private static bool _isQuitting = false;

        // Property
        // ------------------------------------------------------------------------

        /// <summary>
        /// Singleton design pattern.
        /// </summary>
        /// <value>The Instance</value>
        public static T Instance
        {
            get
            {
                if (_isQuitting)
                    return null;

                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        _instance = obj.AddComponent<T>();

                        // Setting name for better management
                        string[] typeName = obj.GetComponent<T>().ToString().Split('.', ')');
                        _instance.name = typeName[typeName.Length - 2];
                    }
                }

                return _instance;
            }
        }

        // Methods
        // ------------------------------------------------------------------------

        protected virtual void Awake()
        {
            if (!Application.isPlaying) return;
            _instance = this as T;
        }

        /// <summary>
        /// When Unity quits, it destroys objects in a random order. In principle,
        /// a Singleton is only destroyed when application quits. If any script calls
        /// Instance after it have been destroyed, it will create a buggy ghost object
        /// that will stay on the Editor scene even after stopping playing the Application.
        /// Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        protected virtual void OnDestroy()
        {
            _isQuitting = true;
        }
    }

    /// <summary>
    /// A persistence object that dont be destroyed when loading another
    /// scene, which can be inherited and make sure there's only one
    /// instance in the scene.
    /// </summary>
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        // Variables
        // ------------------------------------------------------------------------
        protected bool _enabled;
        protected static T _instance;

        // Property
        // ------------------------------------------------------------------------

        /// <summary>
        /// The instance of this PersistentObject. Using Singleton Design Pattern.
        /// </summary>
        /// <value>The Instance</value>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        _instance = obj.AddComponent<T>();

                        // Setting name for better management
                        string[] typeName = obj.GetComponent<T>().ToString().Split('.', ')');
                        _instance.name = typeName[typeName.Length - 2];
                    }
                }

                return _instance;
            }
        }

        // Methods
        // -----------------------------------------------------------------------
        protected virtual void Awake()
        {
            if (!Application.isPlaying) return;
            if (_instance != null) return;

            // If this is the first instance, make it singleton.
            _instance = this as T;
            _enabled = true;
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void Start()
        {
            // If another Singleton already exists, destroy this one.
            if (_instance != null && _instance != this)
                Destroy(this);
        }
    }
}