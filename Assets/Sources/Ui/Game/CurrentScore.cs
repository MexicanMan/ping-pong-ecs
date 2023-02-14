namespace BeresnevTest.Ui.Game.Game
{
    public class CurrentScore : Score
    {
        protected override string TextTemplate => "Score: {0}";
        
        private void Start()
        {
            startup.CurrentSessionData.ScoreChangedEvent.ValueChanged += UpdateScore;
            UpdateScore(0);
        }

        private void OnDestroy()
        {
            startup.CurrentSessionData.ScoreChangedEvent.ValueChanged -= UpdateScore;
        }
    }
}
