using System;
using BeresnevTest.MonoConverters.World.Components;
using UnityEngine;
using Leopotam.EcsLite;

namespace BeresnevTest.MonoConverters
{
    public enum ConvertMode
    {
        ConvertAndInject,
        ConvertAndDestroy,
        ConvertAndSave
    }

    public class ConvertToEntity : MonoBehaviour
    {
        [SerializeField] 
        private ConvertMode _convertMode;
        
        [SerializeField] 
        private string _customWorld;
        
        private EcsPackedEntity _packedEntity;
        private EcsWorld _spawnWorld;
        private bool _isProccessed = false;
        
        private void Start()
        {
            var world = WorldHandler.GetMainWorld(); 
            
            if (world != null && !_isProccessed)
            {
                var entity = world.NewEntity();
                var instantiatePool = world.GetPool<InstantiateComponent>();
                ref var instantiateComponent = ref instantiatePool.Add(entity);
                instantiateComponent.GameObject = gameObject;
                instantiateComponent.WorldName = _customWorld;
            }
        }

        public string GetWorldName()
        {
            return _customWorld;
        }
        
        public EcsWorld GetSpawnWorld()
        {
            return _spawnWorld;
        }

        public ConvertMode GetConvertMode()
        {
            return _convertMode;
        }
        
        public void SetProccessed()
        {
            _isProccessed = true;
        }
        
        public int? TryGetEntity()
        {
            if (_spawnWorld != null)
            {
                int entity;
                if (_packedEntity.Unpack(_spawnWorld, out entity))
                {
                    return entity;
                }
            }
            return null;
        }

        public void Set(int entity, EcsWorld world)
        {
            _spawnWorld = world;
            _packedEntity = EcsEntityExtensions.PackEntity(world, entity);
        }
    }
}