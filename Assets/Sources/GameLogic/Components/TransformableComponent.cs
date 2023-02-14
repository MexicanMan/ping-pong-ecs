using System;
using UnityEngine;

namespace BeresnevTest.GameLogic.Components
{
    /// <summary>
    /// Компонент-обертка над трансформом
    /// </summary>
    [Serializable]
    public struct TransformableComponent
    {
        public Transform Transform;
    }
}