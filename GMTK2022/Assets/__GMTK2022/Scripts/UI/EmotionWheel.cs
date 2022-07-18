using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using GMTK2022.Core;
using GMTK2022.Entities;
using GMTK2022.Utils;

namespace GMTK2022.UI
{
    public class EmotionWheel : MonoBehaviour
    {
        public Image[] emotionImages;

        public CanvasGroup selectionImage;
        public RectTransform selection;

        public float moveTime = 0.2f;
        
        private bool _isMove = false;
        private float _destine = 0f;

        public void Start() 
        {
            for (int i = 0; i < emotionImages.Length; i++)
            {
                emotionImages[i].color = GM.Gameplay.emotions[i].color;
            }
        }

        public void Update()
        {
            if (_isMove) return;

            Entity currentChar = GM.Gameplay.currentEntity;
            if (currentChar == null)
            {
                selectionImage.alpha = 0;
                return;
            }

            EntityMovement entity = currentChar.gameObject.GetComponent<EntityMovement>();
            if (entity.head.isLock && entity.body.isLock)
                return;

            Nodes currentEmotion;
            if (!entity.head.isLock)
                currentEmotion = entity.head.currentEmotion;
            else
                currentEmotion = entity.body.currentEmotion;

            selectionImage.alpha = 1;
            if (currentEmotion.uiRotate == _destine) return;
            _destine = currentEmotion.uiRotate;
            StartCoroutine(SmoothMove());
        }
        
        private IEnumerator SmoothMove()
        {
            _isMove = true;
            Vector3 startingPos  = selection.eulerAngles;
            Vector3 finalPos = new(0, 0, _destine);
            float elapsedTime = 0;
            
            while (elapsedTime < moveTime)
            {
                selection.eulerAngles = Vector3.Lerp(startingPos, finalPos, (elapsedTime / moveTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            selection.eulerAngles = finalPos;
            _isMove = false;
        }
    }
}