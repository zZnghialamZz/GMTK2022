using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GMTK2022.Utils 
{
    [CreateAssetMenu(fileName = "Node", menuName = "GMTK2022/Node", order = 1)]
    public class Nodes : ScriptableObject
    {
        public string DataName => this.name;
        public int id;
        public Color color;
        public Nodes right;
        public Nodes left;
        public Nodes head;
        public float uiRotate;
        public Sprite sprite;
    }
}
