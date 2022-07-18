using System.Collections;

using UnityEngine;

using GMTK2022.Core;

namespace GMTK2022.Entities
{
    public class Trap : MonoBehaviour
    {
        public float moveTime = 3.0f;
        public float moveOffset = .2f;

        private SpriteRenderer _sprite;

        private void Awake()
        {
            if (_sprite == null)
                _sprite = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Entity")
            {
                StartCoroutine(ShakeMe(col));
            }
        }

        private IEnumerator ShakeMe(Collider2D col)
        {
            float elapsedTime = 0;
            Vector3 oldPos = transform.position;

            while (elapsedTime < moveTime)
            {
                transform.position = new Vector3(
                    Random.Range(-moveOffset, moveOffset) + oldPos.x,
                    Random.Range(-moveOffset, moveOffset) + oldPos.y,
                    Random.Range(-moveOffset, moveOffset) + oldPos.z
                    );
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = oldPos;
            _sprite.color = new Color(0, 0, 0, 0);

            // Restart that entity
            Entity entity = col.gameObject.GetComponent<Entity>();
            col.gameObject.transform.position = entity.startPos;

            EntityMovement movement  = col.gameObject.GetComponent<EntityMovement>();
            movement.head.SetEmotion(entity.startEmotions[0]); 
            movement.body.SetEmotion(entity.startEmotions[1]);
        }
    }
}
