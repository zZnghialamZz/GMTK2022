using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GMTK2022.Core;

namespace GMTK2022.UI
{
    public class MainMenuController : MonoBehaviour
    {
        public void PlayGame()
        {
            LoadingSceneManager.LoadScene("Stage1");
        }
    }
}
