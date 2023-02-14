using UnityEngine;

namespace BeresnevTest.BallCustomization
{
    [CreateAssetMenu(menuName = "Configuration/" + nameof(BallCustomizationConfig))]
    public class BallCustomizationConfig : ScriptableObject
    {
        [field: SerializeField, NonReorderable]
        public Skin[] Skins { get; private set; }
    }
}