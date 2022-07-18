using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GMTK2022.Core;
using GMTK2022.Utils;

namespace GMTK2022.Entities
{
    public class EntityElement : MonoBehaviour
    {
        public bool isRandom = false;

        public Nodes currentEmotion;
        public Color currentColor;
        public SpriteRenderer sprite;

        public SpriteRenderer emotion;

        public bool isLock = false;
    
        public void Awake() 
        {
            if (isRandom)
            {
                int random = Random.Range(0, 7);
                currentEmotion = GM.Gameplay.emotions[random];
            }
            if (sprite == null)
                sprite = GetComponent<SpriteRenderer>();
            currentColor = currentEmotion.color;
            sprite.color = currentEmotion.color;
            emotion.sprite = currentEmotion.sprite;
        }

        public void SetEmotion(Nodes newEmotion)
        {
            if (!isLock) 
            {
                currentEmotion = newEmotion;
                emotion.sprite = currentEmotion.sprite;
                SetEmotionColor();
            }
        }

        public void SetEmotionColor() 
        {
            if (!isLock) 
            {
                currentColor = currentEmotion.color;
                sprite.color = currentEmotion.color;
            }
        }
    }
}