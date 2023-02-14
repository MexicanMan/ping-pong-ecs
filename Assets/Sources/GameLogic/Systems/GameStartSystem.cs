using BeresnevTest.GameLogic.Components;
using BeresnevTest.GameLogic.Events;
using BeresnevTest.Movement.Tags;
using Leopotam.EcsLite;
using UnityEngine;

namespace BeresnevTest.GameLogic.Systems
{
    public class GameStartSystem : IEcsInitSystem, IEcsRunSystem
    {
        private static readonly Vector2[] InitialDirections = { new Vector2(1, 1), new Vector2(-1, 1), 
            new Vector2(1, -1), new Vector2(-1, -1)};
        
        private EcsPool<MovableComponent> _movablePool;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<StartGameEvent> _startGameEventPool;

        private EcsFilter _movablePositionBallFilter;
        private EcsFilter _startGameFilter;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _movablePool = world.GetPool<MovableComponent>();
            _positionPool = world.GetPool<PositionComponent>();
            _startGameEventPool = world.GetPool<StartGameEvent>();
            
            _movablePositionBallFilter = world.Filter<PositionComponent>()
                .Inc<MovableComponent>()
                .Inc<BallTag>()
                .End();
            _startGameFilter = world.Filter<StartGameEvent>().End();

            RespawnBall();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var startGameEvent in _startGameFilter)
            {
                RespawnBall();
                
                _startGameEventPool.Del(startGameEvent);
            }
        }

        private void RespawnBall()
        {
            foreach (var ent in _movablePositionBallFilter)
            {
                ref var movableComponent = ref _movablePool.Get(ent);
                ref var positionComponent = ref _positionPool.Get(ent);
                
                positionComponent.Position = Vector2.zero;

                int randomIndex = Random.Range(0, InitialDirections.Length);
                movableComponent.MovementDirection = InitialDirections[randomIndex];
            }
        }
    }
}