using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GMTK2022.Core;
using GMTK2022.Utils;

namespace GMTK2022.Entities
{
    [RequireComponent(typeof(EntityMovement))]
    public class Entity : MonoBehaviour 
    {
        public Vector2 startPos;
        public Nodes[] startEmotions = new Nodes[2];

        public float rollTime = .2f;

        public Animator headAnimator;
        public Animator bodyAnimator;

        public EntityElement head;
        public EntityElement body;

        public int rollCount = 0;

        public string winText;

        private bool _isRoll = false;

        public void Start()
        {
            EntityMovement movement = GetComponent<EntityMovement>();
            startPos = transform.position;
            startEmotions[0] = movement.head.currentEmotion;
            startEmotions[1] = movement.body.currentEmotion;
        }

        public void Update()
        {
            if (GM.Input.Roll.CurrentState == InputButton.States.BUTTON_DOWN 
                && !_isRoll 
                && transform.position == (Vector3)startPos
                && this == GM.Gameplay.currentEntity)
                StartCoroutine(SmoothRoll());
        }
        
        private IEnumerator SmoothRoll()
        {
            if (!head.isLock)
                headAnimator.SetBool("IsMovingSide", true);
            if (!body.isLock)
                bodyAnimator.SetBool("IsMovingSide", true);
            _isRoll = true;

            float elapsedTime = 0;
            while (elapsedTime < rollTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _isRoll = false;

            // Actual roll
            int random = Random.Range(0, 7);
            if (rollCount == 5)
            {
                head.SetEmotion(startEmotions[0]);
                body.SetEmotion(startEmotions[1]);
                rollCount = 0;
            }
            else
            {
                head.SetEmotion(GM.Gameplay.emotions[random]);
                body.SetEmotion(GM.Gameplay.emotions[random]);
            }
            rollCount++;

            if (!head.isLock)
                headAnimator.SetBool("IsMovingSide", false);
            if (!body.isLock)
                bodyAnimator.SetBool("IsMovingSide", false);
        }
    }
}