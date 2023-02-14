using BeresnevTest.GameLogic.Components;
using BeresnevTest.GameLogic.Events;
using BeresnevTest.Movement.Tags;
using BeresnevTest.Data;
using Leopotam.EcsLite;

namespace BeresnevTest.GameLogic.Systems
{
    public class GoalSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<StartGameEvent> _startGameEventPool;

        private EcsFilter _positionBallFilter;

        private readonly float _topGateCoordinate;
        private readonly float _bottomGateCoordinate;

        private readonly ICurrentSessionData _currentSessionData;
        private readonly IPlayerState _playerState;
        
        public GoalSystem(float topGateCoordinate, float bottomGateCoordinate, ICurrentSessionData currentSessionData,
            IPlayerState playerState)
        {
            _topGateCoordinate = topGateCoordinate;
            _bottomGateCoordinate = bottomGateCoordinate;

            _currentSessionData = currentSessionData;
            _playerState = playerState;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _positionPool = _world.GetPool<PositionComponent>();
            _startGameEventPool = _world.GetPool<StartGameEvent>();
            
            _positionBallFilter = _world.Filter<PositionComponent>()
                .Inc<BallTag>()
                .End();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var ent in _positionBallFilter)
            {
                var positionComponent = _positionPool.Get(ent);
                if (positionComponent.Position.y > _topGateCoordinate ||
                    positionComponent.Position.y < _bottomGateCoordinate)
                {
                    var gameStartEvent = _world.NewEntity();
                    _startGameEventPool.Add(gameStartEvent);

                    UpdateScores();
                }
            }
        }

        private void UpdateScores()
        {
            _currentSessionData.IncrementScore();
                    
            if (_currentSessionData.Score.Value > _playerState.BestScore)
                _playerState.UpdateBestScore(_currentSessionData.Score.Value);
        }
    }
}