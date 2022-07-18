using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GMTK2022.Core;
using GMTK2022.UI;

namespace GMTK2022.Entities
{
    public class EntityMovement : MonoBehaviour
    {
        public GridManager grid;
        public Animator headAnimator;
        public Animator bodyAnimator;
        public SpriteRenderer headsprite;
        public SpriteRenderer bodysprite;

        public float moveTime = 0.2f;

        public EntityElement head;
        public EntityElement body;

        private bool _isMove = false;
        private Entity _entity;

        public void Start()
        {
            _entity = GetComponent<Entity>();
        }

        public void Update()
        {
            if (GM.Gameplay.currentEntity != _entity) 
                return;
            if (GM.Input.PrimaryAxis != Vector2.zero)
            {
                Move(GM.Input.PrimaryAxis);
                GM.Input.PrimaryAxis = Vector2.zero;
            }
        }

        private void Move(Vector2 direction)
        {
            if (CanMove(direction) && !_isMove) 
            {
                StartCoroutine(SmoothMove(direction));
                if (direction.x == 1)
                {
                    headsprite.flipX = false;
                    bodysprite.flipX = false;
                    head.SetEmotion(head.currentEmotion.right);
                    body.SetEmotion(body.currentEmotion.right);
                }
                else if (direction.x == -1)
                {
                    headsprite.flipX = true;
                    bodysprite.flipX = true;
                    head.SetEmotion(head.currentEmotion.left);
                    body.SetEmotion(body.currentEmotion.left);
                }
                else if (direction.y != 0)
                {
                    head.SetEmotion(head.currentEmotion.head);
                    body.SetEmotion(body.currentEmotion.head);
                }
            }
        }

        private bool CanMove(Vector2 direction)
        {
            Vector3Int gridPosition = grid.GetGridPosition(transform.position + (Vector3)direction);
            if (grid.HasCollision(gridPosition) || GM.Gameplay.HasCollisions(gridPosition))
                return false;
            return true;
        }

        private IEnumerator SmoothMove(Vector2 direction)
        {
            if (!head.isLock)
                headAnimator.SetBool("IsMovingSide", true);
            if (!body.isLock)
                bodyAnimator.SetBool("IsMovingSide", true);
            _isMove = true;
            Vector3 startingPos  = transform.position;
            Vector3 finalPos = transform.position + (Vector3)direction;
            float elapsedTime = 0;
            
            while (elapsedTime < moveTime)
            {
                transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / moveTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = finalPos;
            _isMove = false;
            if (!head.isLock)
                headAnimator.SetBool("IsMovingSide", false);
            if (!body.isLock)
                bodyAnimator.SetBool("IsMovingSide", false);
        }
    }
}