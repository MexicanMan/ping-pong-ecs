using BeresnevTest.GameLogic.Components;
using BeresnevTest.GameLogic.Events;
using BeresnevTest.Movement.Tags;
using Leopotam.EcsLite;

namespace BeresnevTest.GameLogic.Systems
{
    public class RacketDirectionUpdateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<MovableComponent> _movablePool;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<PlayerInputEvent> _inputPool;

        private EcsFilter _movableRacketPositionFilter;
        private EcsFilter _inputFilter;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _movablePool = world.GetPool<MovableComponent>();
            _positionPool = world.GetPool<PositionComponent>();
            _inputPool = world.GetPool<PlayerInputEvent>();
            
            _movableRacketPositionFilter = world.Filter<MovableComponent>()
                .Inc<PositionComponent>()
                .Inc<RacketTag>()
                .End();

            _inputFilter = world.Filter<PlayerInputEvent>().End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var input in _inputFilter)
            {
                var inputEvent = _inputPool.Get(input);

                foreach (var racket in _movableRacketPositionFilter)
                {
                    ref var movableComponent = ref _movablePool.Get(racket);
                    var positionComponent = _positionPool.Get(racket);
                    movableComponent.MovementDirection.x = positionComponent.Position.x - inputEvent.Input.x;
                }
                
                _inputPool.Del(input);
            }
        }
    }
}