using System;
using UnityEngine;

namespace BeresnevTest.BallCustomization
{
    [Serializable]
    public class Skin
    {
        [field: SerializeField]
        public Material Material { get; set; }
        
        [field: SerializeField]
        public Sprite Image { get; set; }
    }
}