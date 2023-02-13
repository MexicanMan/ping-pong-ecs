using BeresnevTest.MonoConverters.World.Systems;
using Leopotam.EcsLite;

namespace BeresnevTest.MonoConverters
{
    public static class ConverterSystemExtension
    {
        public static IEcsSystems ConvertScene(this IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new WorldInitSystem());
            return ecsSystems;
        }
    }
}
