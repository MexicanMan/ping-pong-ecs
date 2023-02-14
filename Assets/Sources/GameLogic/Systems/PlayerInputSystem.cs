using BeresnevTest.GameLogic.Events;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BeresnevTest.GameLogic.Systems
{
    public class PlayerInputSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsPool<PlayerInputEvent> _inputPool;

        private readonly InputAction _playerSlideInput;

        private readonly CoordinatesConverter _coordinatesConverter;

        public PlayerInputSystem(Input.Input input, CoordinatesConverter coordinatesConverter)
        {
            _playerSlideInput = input.Player.Slide;
            _playerSlideInput.performed += OnSlideInput;

            _coordinatesConverter = coordinatesConverter;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _inputPool = _world.GetPool<PlayerInputEvent>();
        }
        
        public void Destroy(IEcsSystems systems)
        {
            _playerSlideInput.performed -= OnSlideInput;
        }
        
        private void OnSlideInput(InputAction.CallbackContext context)
        {
            var touchScreenPosition = context.ReadValue<Vector2>();
            Vector3 worldCoordinates = _coordinatesConverter.ConvertFromScreenToWorldPlane(touchScreenPosition);

            var ent = _world.NewEntity();
            ref var inputEvent = ref _inputPool.Add(ent);
            inputEvent.Input = new Vector2(worldCoordinates.x, worldCoordinates.z);
        }
    }
}