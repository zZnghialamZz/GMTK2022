using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GMTK2022.UI
{
    public class SceneLoadAfterTime : MonoBehaviour
    {
        public float timeToLoadNextScene = 10f;
        public float fadeTime = 2f;

        public Image sceneImage;

        public string sceneToLoad;

        private float timeElapsed;

        public void Awake()
        {
            StartCoroutine(Fade(0f, 1f));
        }

        public void Update()
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > timeToLoadNextScene)
            {
                StartCoroutine(Fade(1f, 0f, LoadScene));
            }
        }

        private IEnumerator Fade(float source, float des, Action action = null)
        {
            float elapsedTime = 0;
            
            while (elapsedTime < fadeTime)
            {
                sceneImage.color = new Color(1, 1, 1, Mathf.Lerp(source, des, (elapsedTime / fadeTime)));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            timeElapsed = 0;

            action?.Invoke();
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
