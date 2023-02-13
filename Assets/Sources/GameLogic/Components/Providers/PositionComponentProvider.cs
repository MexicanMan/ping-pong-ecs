using BeresnevTest.MonoConverters;
using UnityEngine;

namespace BeresnevTest.GameLogic.Components.Providers
{
    public class PositionComponentProvider : MonoProvider<PositionComponent>
    {
        private void OnValidate()
        {
            _value.Position = new Vector2(transform.position.x, transform.position.z);
        }
    }
}