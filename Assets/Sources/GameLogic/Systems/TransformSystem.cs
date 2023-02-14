using BeresnevTest.GameLogic.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace BeresnevTest.GameLogic.Systems
{
    /// <summary>
    /// View-система, обновляющая трансформ сущностей в соответствии с их позицией
    /// </summary>
    public class TransformSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<TransformableComponent> _transformablePool;
        private EcsPool<PositionComponent> _positionPool;

        private EcsFilter _transformablePositionFilter;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _transformablePool = world.GetPool<TransformableComponent>();
            _positionPool = world.GetPool<PositionComponent>();
            
            _transformablePositionFilter = world.Filter<TransformableComponent>().Inc<PositionComponent>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var ent in _transformablePositionFilter)
            {
                ref var transformableComponent = ref _transformablePool.Get(ent);
                var positionComponent = _positionPool.Get(ent);

                transformableComponent.Transform.position = new Vector3(positionComponent.Position.x, 
                    transformableComponent.Transform.position.y, 
                    positionComponent.Position.y);
            }
        }
    }
}