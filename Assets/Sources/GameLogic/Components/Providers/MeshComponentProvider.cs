using BeresnevTest.MonoConverters;
using UnityEngine;

namespace BeresnevTest.GameLogic.Components.Providers
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MeshComponentProvider : MonoProvider<MeshComponent>
    {
        private void OnValidate()
        {
            _value.Mesh = GetComponent<MeshRenderer>();
        }
    }
}