using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using GMTK2022.Entities;
using GMTK2022.Utils;

namespace GMTK2022.Core
{
    public class GameplayManager : MonoBehaviour, IManager
    {
        public Entity currentEntity;
        public Nodes[] emotions;

        private List<Entity> _allEntities = new();
        private GridManager _grid;

        private EntityDestination[] _destinations;

        public void Initialize() 
        {
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        public void OnSceneLoad(Scene scene, LoadSceneMode mode)
        {
            _allEntities.Clear();
            _grid = FindObjectOfType<GridManager>();
            _destinations = FindObjectsOfType<EntityDestination>();

            GameObject[] entities = GameObject.FindGameObjectsWithTag("Entity");
            for (int i = 0; i < entities.Length; i++)
                _allEntities.Add(entities[i].GetComponent<Entity>());
            currentEntity = _allEntities[0];
        }

        public void Update()
        {
            if (GM.Input.Swap.CurrentState == InputButton.States.BUTTON_DOWN)
            {
                for (int i = 0; i < _allEntities.Count; i++)
                {
                    if (_allEntities[i] == currentEntity)
                    {
                        if (i == _allEntities.Count - 1)
                            currentEntity = _allEntities[0];
                        else
                            currentEntity = _allEntities[i + 1];
                        break;
                    }
                }
            }
        }

        public void CheckForWin()
        {
            bool result = true;
            int count = 0;
            for (int i = 0; i < _destinations.Length; i++)
            {
                if (!_destinations[i].isSatisfy)
                    result = false;
                else
                    count++;
            }

            if (result || count == 8)
            {
                GM.Level.LoadNextScene();
            }
        }

        public bool HasCollisions(Vector3Int position)
        {
            for (int i = 0; i < _allEntities.Count; i++)
            {
                Vector3Int entityGridPosition = _grid.GetGridPosition(_allEntities[i].transform.position);
                if (entityGridPosition == position)
                    return true;
            }

            return false;
        }
    }
}