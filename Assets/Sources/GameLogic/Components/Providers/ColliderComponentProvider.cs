using BeresnevTest.MonoConverters;
using UnityEngine;

namespace BeresnevTest.GameLogic.Components.Providers
{
    [RequireComponent(typeof(Collider))]
    public class ColliderComponentProvider : MonoProvider<ColliderComponent>
    {
        private void OnValidate()
        {
            _value.Collider = GetComponent<Collider>();
        }
    }
}