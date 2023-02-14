using BeresnevTest.Common;

namespace BeresnevTest.Data
{
    public interface ICurrentSessionData
    {
        IReadable<int> Score { get; }
        IChangeable<int> ScoreChangedEvent { get; }

        void IncrementScore();

        void ResetScore();
    }
}