using BeresnevTest.GameLogic.Systems;
using BeresnevTest.MonoConverters;
using BeresnevTest.Data;
using BeresnevTest.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace BeresnevTest.GameLogic
{
    public class Startup : MonoBehaviour
    {
        [Header("Field Boundaries")]
        
        [SerializeField] 
        private Collider _leftWall;
        
        [SerializeField] 
        private Collider _rightWall;

        [SerializeField] 
        private float _topGateCoordinate;
        
        [SerializeField] 
        private float _bottomGateCoordinate;

        [Header("Player State")] 
        
        [SerializeField]
        private PlayerState _playerState;

        public IGamePauseService GamePauseService { get; private set; }
        public ICurrentSessionData CurrentSessionData { get; private set; }
        public IPlayerState PlayerState => _playerState;

        private EcsWorld _world;
        private IEcsSystems _systems;

        private Input.Input _input;
        
        private void Awake()
        {
            _input = new Input.Input();
            _input.Enable();

            var coordinatesConverter = new CoordinatesConverter(Camera.main);
            var movementClamper = new MovementClamper(_leftWall.bounds.max.x, _rightWall.bounds.min.x);

            _world = new EcsWorld();
            
            CurrentSessionData = new CurrentSessionData();
            GamePauseService = new GamePauseService(_world, CurrentSessionData);
            
            _systems = new EcsSystems(_world);
            _systems
                .ConvertScene()
                .Add(new GameInitSystem(_playerState))
                .Add(new GameStartSystem())
                .Add(new PlayerInputSystem(_input, coordinatesConverter))
                .Add(new RacketDirectionUpdateSystem())
                .Add(new CollisionSystem())
                .Add(new MovementSystem(movementClamper))
                .Add(new GoalSystem(_topGateCoordinate, _bottomGateCoordinate, CurrentSessionData, PlayerState))
                .Add(new TransformSystem())
                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            _playerState.Save();
            
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
            
            _input?.Disable();
        }
    }
}
