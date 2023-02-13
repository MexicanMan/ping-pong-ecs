using BeresnevTest.GameLogic.Components;
using BeresnevTest.GameLogic.Events;
using BeresnevTest.Movement.Tags;
using Leopotam.EcsLite;
using UnityEngine;

namespace BeresnevTest.GameLogic.Systems
{
    public class CollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<MovableComponent> _movablePool;
        private EcsPool<OnCollisionEnterEvent> _collisionPool;

        private EcsFilter _movableCollidedBallFilter;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _movablePool = world.GetPool<MovableComponent>();
            _collisionPool = world.GetPool<OnCollisionEnterEvent>();
            
            _movableCollidedBallFilter = world.Filter<MovableComponent>()
                .Inc<OnCollisionEnterEvent>()
                .Inc<BallTag>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var ent in _movableCollidedBallFilter)
            {
                ref var movableComponent = ref _movablePool.Get(ent);
                var collisionComponent = _collisionPool.Get(ent);

                var normalizedContactPoint = collisionComponent.FirstContactPoint.normal;
                movableComponent.MovementDirection = Vector2.Reflect(movableComponent.MovementDirection,
                    new Vector2(normalizedContactPoint.x, normalizedContactPoint.z));
                
                _collisionPool.Del(ent);
            }
        }
    }
}