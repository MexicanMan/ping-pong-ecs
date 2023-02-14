using BeresnevTest.GameLogic.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace BeresnevTest.GameLogic.Systems
{
    /// <summary>
    /// Система, двигающая сущности вдоль их направления движения с заданной для них скоростью
    /// </summary>
    public class MovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<MovableComponent> _movablePool;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<ColliderComponent> _colliderPool;

        private EcsFilter _movablePositionColliderFilter;

        private readonly MovementClamper _movementClamper;

        public MovementSystem(MovementClamper clamper)
        {
            _movementClamper = clamper;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _movablePool = world.GetPool<MovableComponent>();
            _positionPool = world.GetPool<PositionComponent>();
            _colliderPool = world.GetPool<ColliderComponent>();
            
            _movablePositionColliderFilter = world.Filter<MovableComponent>()
                .Inc<PositionComponent>()
                .Inc<ColliderComponent>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var ent in _movablePositionColliderFilter)
            {
                var movableComponent = _movablePool.Get(ent);
                var colliderComponent = _colliderPool.Get(ent);
                ref var positionComponent = ref _positionPool.Get(ent);

                Vector2 newPosition = positionComponent.Position + movableComponent.MovementDirection * 
                    movableComponent.MovementSpeed * Time.deltaTime;

                positionComponent.Position = _movementClamper.Clamp(newPosition, colliderComponent.Collider.bounds.extents);
            }
        }
    }
}