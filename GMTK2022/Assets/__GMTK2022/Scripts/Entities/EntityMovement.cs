using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GMTK2022.Core;

namespace GMTK2022.Entities
{
    public class EntityMovement : MonoBehaviour
    {
        public void Update()
        {
            if (GM.Input.PrimaryAxis != Vector2.zero)
            {
                Move(GM.Input.PrimaryAxis);
                GM.Input.PrimaryAxis = Vector2.zero;
            }
        }

        private void Move(Vector2 direction)
        {
            if (CanMove(direction))
                this.transform.position += (Vector3)direction;
        }

        private bool CanMove(Vector2 direction)
        {
            Vector3Int gridPosition = GM.Grid.GetGridPosition(transform.position + (Vector3)direction);
            if (GM.Grid.HasCollision(gridPosition))
                return false;
            return true;
        }
    }
}