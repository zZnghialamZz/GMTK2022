using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GMTK2022.Core;

namespace GMTK2022.UI
{
    public class SelectionArrow : MonoBehaviour
    {
        public float offset = 0.3f;

        public void Update()
        {
            if (GM.Gameplay.currentEntity != null)
                transform.position = GM.Gameplay.currentEntity.transform.position + new Vector3(0, offset, 0);
        }
    }
}
