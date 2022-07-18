using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace GMTK2022.UI
{
    public class UITempController : MonoBehaviour
    {
        public float fadeTime = 2f;

        public Image sceneImage;

        public float timeToLoadNextScene = 5f;
        private float timeElapsed;

        public void Update()
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > timeToLoadNextScene)
            {
                StartCoroutine(Fade(1f, 0f));
            }
        }

        private IEnumerator Fade(float source, float des)
        {
            float elapsedTime = 0;
            
            while (elapsedTime < fadeTime)
            {
                sceneImage.color = new Color(1, 1, 1, Mathf.Lerp(source, des, (elapsedTime / fadeTime)));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}
