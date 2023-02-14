using BeresnevTest.Data;

namespace BeresnevTest.Services
{
    public interface ISaveService
    {
        void Save(PlayerData data);
        PlayerData Restore();
    }
}