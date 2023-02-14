namespace BeresnevTest.Ui.Game
{
    public class BestScore : Score
    {
        protected override string TextTemplate => "Best: {0}";
        
        private void Start()
        {
            startup.PlayerState.BestScoreChangedEvent += UpdateScore;
            UpdateScore(startup.PlayerState.BestScore);
        }

        private void OnDestroy()
        {
            startup.PlayerState.BestScoreChangedEvent -= UpdateScore;
        }
    }
}
