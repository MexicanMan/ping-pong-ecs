using System;
using BeresnevTest.BallCustomization;
using UnityEngine;

namespace BeresnevTest.Data
{
    [Serializable]
    public class PlayerData
    {
        public int BestScore { get; set; }
        
        public int BallSkinIndex { get; set; }
    }
}