using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

using GMTK2022.Utils;

namespace GMTK2022.Core
{
    public class GridManager : MonoBehaviour, IManager
    {
        public Grid grid;
        public Tilemap ground;
        public Tilemap collisions;

        public void Initialize()
        {
            if (grid == null && collisions == null)
                gameObject.SetActive(false);
        }

        public Vector3Int GetGridPosition(Vector3 position)
        {
            return ground.WorldToCell(position);
        }

        public bool HasCollision(Vector3Int gridPosition)
        {
            return collisions.HasTile(gridPosition);
        }
    }
}