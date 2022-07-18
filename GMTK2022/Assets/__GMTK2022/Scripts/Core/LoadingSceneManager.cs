using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using GMTK2022.Utils;

namespace GMTK2022.Core
{
    /// <summary>
    /// A class to load scenes using a loading screen instead of just the default API
    /// </summary>
    public class LoadingSceneManager : MonoBehaviour
    {
        public const string loadingSceneName = "LoadingScene";

        [Header("Default")]
        [Tooltip("The default scene to be loaded.")]
        public string defaultSceneName;
        
        [Header("GameObjects")]
        [Tooltip("The text object where you want the loading message to be displayed")]
        public Text loadingText;

        [Tooltip("The canvas group containing the progress bar")]
        public CanvasGroup loadingProgressBar;

        [Header("Time")]
        [Tooltip("The duration (in seconds) of the initial fade in")]
        public float startFadeDuration = 0.2f;

        [Tooltip("The speed of the progress bar")]
        public float progressBarSpeed = 2f;

        [Tooltip("The duration (in seconds) of the load complete fade out")]
        public float exitFadeDuration = 0.2f;

        [Tooltip("The delay (in seconds) before leaving the scene when complete")]
        public float loadCompleteDelay = 0.5f;

        // Variables Setup
        // ------------------------------------------------------
        private static string _sceneToLoad = "";
        private AsyncOperation _asyncOperation;
        private string _loadingTextValue;
        private Image _progressBarImage;
        private float _fillTarget = 0f;

        // Loading Methods
        // ------------------------------------------------------

        /// <summary>
        /// Call this static method to load the default scene from anywhere
        /// </summary>
        public static void LoadScene()
        {
            _sceneToLoad = null;
            Application.backgroundLoadingPriority = ThreadPriority.High;
            SceneManager.LoadScene(loadingSceneName);
        }
        
        /// <summary>
        ///     Call this static method to load a scene from anywhere
        /// </summary>
        /// <param name="sceneToLoad">Level name.</param>
        public static void LoadScene(string sceneToLoad)
        {
            _sceneToLoad = sceneToLoad;
            Application.backgroundLoadingPriority = ThreadPriority.High;
            SceneManager.LoadScene(loadingSceneName);
        }

        // General Methods
        // ------------------------------------------------------

        /// <summary>
        ///     On Start(), we start loading the new level asynchronously
        /// </summary>
        private void Start()
        {
            _progressBarImage = loadingProgressBar.GetComponent<Image>();
            _loadingTextValue = loadingText.text;

            if (string.IsNullOrEmpty(_sceneToLoad))
                _sceneToLoad = defaultSceneName;
            
            StartCoroutine(LoadAsynchronously());
        }

        /// <summary>
        ///     Every frame, we fill the bar smoothly according to loading progress
        /// </summary>
        private void Update()
        {
            Time.timeScale = 1f;
            _progressBarImage.fillAmount = MathLib.Approach(_progressBarImage.fillAmount, _fillTarget, Time.deltaTime * progressBarSpeed);
        }

        private IEnumerator LoadAsynchronously()
        {
            LoadingSetup();

            // TODO(Nghia Lam): We fade from black here

            yield return new WaitForSeconds(startFadeDuration);

            // We start loading the scene
            _asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Single);
            _asyncOperation.allowSceneActivation = false;

            // While the scene loads, we assign its progress to a target that we'll use to fill the progress bar smoothly
            while (_asyncOperation.progress < 0.9f)
            {
                _fillTarget = _asyncOperation.progress;
                yield return null;
            }

            // When the load is close to the end (it'll never reach it), we set it to 100%
            _fillTarget = 1f;

            // We wait for the bar to be visually filled to continue
            while (_progressBarImage.fillAmount != _fillTarget)
            {
                yield return null;
            }

            // TODO(Nghia Lam): The load is now complete, we replace the bar with the complete animation
            yield return new WaitForSeconds(loadCompleteDelay);

            // TODO(Nghia Lam): We fade to black here
            yield return new WaitForSeconds(exitFadeDuration);

            // We switch to the new scene
            _asyncOperation.allowSceneActivation = true;
            LoadingComplete();
        }

        /// <summary>
        ///     Sets up all visual elements, fades from black at the start
        /// </summary>
        private void LoadingSetup()
        {
            _progressBarImage.fillAmount = 0f;
            loadingText.text = _loadingTextValue;
        }

        private void LoadingComplete()
        {
            Debug.Log($"LoadingSceneManager : Load {_sceneToLoad} scene completed !");
        }
    }
}