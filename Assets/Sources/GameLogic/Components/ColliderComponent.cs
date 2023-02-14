using System;
using UnityEngine;

namespace BeresnevTest.GameLogic.Components
{
    /// <summary>
    /// Компонент-оберкта над коллайдером
    /// </summary>
    [Serializable]
    public struct ColliderComponent
    {
        public Collider Collider;
    }
}