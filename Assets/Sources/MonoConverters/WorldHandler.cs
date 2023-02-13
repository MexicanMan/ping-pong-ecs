using Leopotam.EcsLite;

namespace BeresnevTest.MonoConverters
{
    public static class WorldHandler
    {
        private static EcsWorld _world;

        public static void Init(EcsWorld ecsWorld)
        {
            _world = ecsWorld;
        }

        public static EcsWorld GetMainWorld()
        {
            return _world;
        }

        public static void Destroy()
        {
            _world = null;
        }
    }
}
