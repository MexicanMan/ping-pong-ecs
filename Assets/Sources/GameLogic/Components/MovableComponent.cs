using System;
using UnityEngine;

namespace BeresnevTest.GameLogic.Components
{
    [Serializable]
    public struct MovableComponent
    {
        public Vector2 MovementDirection;
        public float MovementSpeed;
    }
}