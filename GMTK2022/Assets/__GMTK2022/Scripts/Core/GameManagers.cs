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
        public GridManager gridManager;
        public InputManager inputManager;

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        public void Initialize()
        {
            gridManager?.Initialize();
            inputManager.Initialize();
        }
    }

    public static class GM
    {
        public static GridManager Grid => GameManagers.Instance.gridManager;
        public static InputManager Input => GameManagers.Instance.inputManager;
    }
}