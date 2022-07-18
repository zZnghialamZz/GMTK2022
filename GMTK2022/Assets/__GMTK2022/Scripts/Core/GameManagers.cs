using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GMTK2022.Utils;

namespace GMTK2022.Core
{
    public interface IManager
    {
        public void Initialize();
    }

    public class GameManagers : PersistentSingleton<GameManagers>, IManager
    {
        public GameplayManager gameplayManager;
        public InputManager inputManager;
        public LevelManager levelManager;

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        public void Initialize()
        {
            gameplayManager?.Initialize();
            inputManager.Initialize();
            levelManager.Initialize();
        }
    }

    public static class GM
    {
        public static GameplayManager Gameplay => GameManagers.Instance.gameplayManager;
        public static InputManager Input => GameManagers.Instance.inputManager;
        public static LevelManager Level => GameManagers.Instance.levelManager;
    }
}