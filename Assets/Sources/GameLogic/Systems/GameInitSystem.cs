using BeresnevTest.Data;
using BeresnevTest.GameLogic.Components;
using BeresnevTest.Movement.Tags;
using Leopotam.EcsLite;

namespace BeresnevTest.GameLogic.Systems
{
    public class GameInitSystem : IEcsInitSystem
    {
        private readonly IPlayerState _playerState;

        public GameInitSystem(IPlayerState playerState)
        {
            _playerState = playerState;
        }
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var meshPool = world.GetPool<MeshComponent>();
            
            var meshBallFilter = world.Filter<MeshComponent>()
                .Inc<BallTag>()
                .End();

            foreach (var ballEnt in meshBallFilter)
            {
                ref var meshComponent = ref meshPool.Get(ballEnt);
                meshComponent.Mesh.sharedMaterial = _playerState.BallSkin.Material;
            }
        }
    }
}