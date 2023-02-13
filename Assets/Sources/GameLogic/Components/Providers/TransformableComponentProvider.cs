using BeresnevTest.MonoConverters;

namespace BeresnevTest.GameLogic.Components.Providers
{
    public class TransformableComponentProvider : MonoProvider<TransformableComponent>
    {
        private void OnValidate()
        {
            _value.Transform = transform;
        }
    }
}