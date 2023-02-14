using System;
using UnityEngine;

namespace BeresnevTest.GameLogic.Components
{
    /// <summary>
    /// Компонент, хранящий игровую позицию сущности
    /// </summary>
    [Serializable]
    public struct PositionComponent
    {
        public Vector2 Position;
    }
}