using UnityEngine;
using Leopotam.EcsLite;

namespace BeresnevTest.MonoConverters
{
    public abstract class MonoProvider<T> : BaseMonoProvider, IConvertToEntity where T : struct
    {
        [SerializeField] 
        protected T _value;

        public void Convert(int entity, EcsWorld world)
        {
            var pool = world.GetPool<T>();
            if (pool.Has(entity))
            {
                pool.Del(entity);
            }

            pool.Add(entity) = _value;
        }
    }
}
