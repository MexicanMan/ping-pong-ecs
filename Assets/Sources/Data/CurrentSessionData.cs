using BeresnevTest.Common;

namespace BeresnevTest.Data
{
    public class CurrentSessionData : ICurrentSessionData
    {
        public IReadable<int> Score => _score;
        public IChangeable<int> ScoreChangedEvent => _score;

        private ObservableVariable<int> _score;

        public CurrentSessionData()
        {
            _score = new ObservableVariable<int>(0);
        }

        public void IncrementScore()
        {
            _score.Value++;
        }

        public void ResetScore()
        {
            _score.Value = 0;
        }
    }
}