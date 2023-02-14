using BeresnevTest.Data;
using BeresnevTest.GameLogic.Events;
using Leopotam.EcsLite;
using UnityEngine;

namespace BeresnevTest.Services
{
    public class GamePauseService : IGamePauseService
    {
        private readonly EcsWorld _world;

        private readonly EcsPool<StartGameEvent> _startGameEventPool;
        
        private readonly ICurrentSessionData _currentSessionData;
        
        public GamePauseService(EcsWorld world, ICurrentSessionData currentSessionData)
        {
            _world = world;
            _currentSessionData = currentSessionData;
            
            _startGameEventPool = _world.GetPool<StartGameEvent>();
        }

        public void RestartGame()
        {
            var gameStartEvent = _world.NewEntity();
            _startGameEventPool.Add(gameStartEvent);
            
            _currentSessionData.ResetScore();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }
        
        public void UnpauseGame()
        {
            Time.timeScale = 1;
        }
    }
}