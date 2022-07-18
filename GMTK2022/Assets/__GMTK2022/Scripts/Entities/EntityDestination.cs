using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GMTK2022.Core;
using GMTK2022.Utils;
using GMTK2022.UI;

namespace GMTK2022.Entities
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EntityDestination : MonoBehaviour
    {
        public Nodes winEmotion;
        public SpriteRenderer sprite;

        public bool isSatisfy = false;

        private Entity _entity;
        private UIController _controller;

        private void Awake()
        {
            if (sprite == null)
                sprite = GetComponent<SpriteRenderer>();
            sprite.color = winEmotion.color;
            _controller = FindObjectOfType<UIController>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Entity")
            {
                // TODO(Nghia Lam): Move head and body to entity.
                _entity = col.gameObject.GetComponent<Entity>();
                if (_entity.head.currentEmotion == winEmotion && _entity.body.currentEmotion == winEmotion)
                    _controller.ShowDialogue(_entity.winText, CheckForSatisfy);
            }
        }

        public void CheckForSatisfy()
        {
            if (_entity.head.currentEmotion == winEmotion && _entity.body.currentEmotion == winEmotion)
                isSatisfy = true;
            else
                isSatisfy = false;
            GM.Gameplay.CheckForWin();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            isSatisfy = false;
        }
    }
}