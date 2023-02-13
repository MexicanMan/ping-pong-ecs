using Leopotam.EcsLite;

namespace BeresnevTest.MonoConverters
{
    public interface IConvertToEntity
    {
        void Convert(int entity, EcsWorld world);
    }
}
