namespace BeresnevTest.Services
{
    public interface IGamePauseService
    {
        void RestartGame();
        void PauseGame();
        void UnpauseGame();
    }
}