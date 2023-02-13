using BeresnevTest.GameLogic.Events;
using BeresnevTest.MonoConverters;
using UnityEngine;

namespace BeresnevTest.GameLogic
{
    public class BallView : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        private ConvertToEntity _converter;

        private void OnValidate()
        {
            _converter = GetComponent<ConvertToEntity>();
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            var world = _converter.GetSpawnWorld();
            var collisionPool = world.GetPool<OnCollisionEnterEvent>();
            var ent = _converter.TryGetEntity();

            if (ent.HasValue)
            {
                ref var collisionComponent = ref collisionPool.Add(ent.Value);
                collisionComponent.FirstContactPoint = collision.GetContact(0);
            }
        }
    }
}