using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GMTK2022.Core
{
    public class LevelManager : MonoBehaviour, IManager
    {
        public string[] gameScenes;
        public Image faderImage;

        public float loadTime = 0.5f;

        private int _currentScene = 0;
        private int _allCount = 0;

        public void Initialize()
        {
            SceneManager.sceneLoaded += OnSceneLoad;
            faderImage.fillAmount = 0;
            _allCount = gameScenes.Length;
        }

        public void LoadNextScene()
        {
            if (_currentScene == _allCount - 1)
            {
                SceneManager.LoadScene("WinScene");
                return;
            }

            _currentScene++;
            StartCoroutine(LoadScene(_currentScene));
        }

        public void ReloadScene()
        {
            StartCoroutine(LoadScene(_currentScene));
        }

        private void OnSceneLoad(Scene scene, LoadSceneMode mode)
        {
            faderImage.fillAmount = 0;
        }

        private IEnumerator LoadScene(int index)
        {
            float elapsedTime = 0;
            
            while (elapsedTime < loadTime)
            {
                faderImage.fillAmount = Mathf.Lerp(0f, 1f, (elapsedTime / loadTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            LoadingSceneManager.LoadScene(gameScenes[index]);
        }
    }
}
