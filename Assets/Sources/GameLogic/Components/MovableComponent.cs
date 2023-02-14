using System;
using UnityEngine;

namespace BeresnevTest.GameLogic.Components
{
    /// <summary>
    /// Компонент, предоставляющий необходимый для двигаемых сущностей параметры
    /// </summary>
    [Serializable]
    public struct MovableComponent
    {
        public Vector2 MovementDirection;
        public float MovementSpeed;
    }
}