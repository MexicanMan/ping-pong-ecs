using System;
using BeresnevTest.MonoConverters.World.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace BeresnevTest.MonoConverters.World.Systems
{
    class WorldInitSystem : IEcsPreInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsPool<InstantiateComponent> _instantiatePool;
        private EcsFilter _filter;
        private EcsWorld _baseWorld;

        public void PreInit(IEcsSystems systems)
        {
            var convertableGameObjects =
                GameObject.FindObjectsOfType<ConvertToEntity>();

            foreach (var convertable in convertableGameObjects)
            {
                AddEntity(convertable.gameObject, systems, convertable.GetWorldName());
            }

            _baseWorld = systems.GetWorld();
            _filter = _baseWorld.Filter<InstantiateComponent>().End();
            _instantiatePool = _baseWorld.GetPool<InstantiateComponent>();
            
            WorldHandler.Init(_baseWorld);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var ent in _filter)
            {
                ref InstantiateComponent instantiate = ref _instantiatePool.Get(ent);
                if (instantiate.GameObject)
                {
                    AddEntity(instantiate.GameObject, systems, instantiate.WorldName);
                }

                _baseWorld.DelEntity(ent);
            }
        }

        public void Destroy(IEcsSystems systems)
        {
            WorldHandler.Destroy();
        }
        
        private void AddEntity(GameObject gameObject, IEcsSystems systems, String worldName)
        {
            var nameValue = worldName == "" ? null : worldName;
            var spawnWorld = systems.GetWorld(nameValue);
            int entity = spawnWorld.NewEntity();
            
            ConvertToEntity convertComponent = gameObject.GetComponent<ConvertToEntity>();
            if (convertComponent)
            {
                foreach (var component in gameObject.GetComponents<Component>())
                {
                    if (component is IConvertToEntity entityComponent)
                    {
                        entityComponent.Convert(entity, spawnWorld);
                        GameObject.Destroy(component);
                    }
                }
		
	            convertComponent.SetProccessed();
                switch (convertComponent.GetConvertMode())
                {
                    case ConvertMode.ConvertAndDestroy:
                        GameObject.Destroy(gameObject);
                        break;
                    case ConvertMode.ConvertAndInject:
                        GameObject.Destroy(convertComponent);
                        break;
                    case ConvertMode.ConvertAndSave:
                        convertComponent.Set(entity, spawnWorld);
                        break;
                }
            }
        }
    }
}