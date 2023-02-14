using BeresnevTest.GameLogic;
using TMPro;
using UnityEngine;

namespace BeresnevTest.Ui.Game
{
    public abstract class Score : MonoBehaviour
    {
        protected abstract string TextTemplate { get; }
        
        [SerializeField] 
        protected Startup startup;

        [SerializeField, HideInInspector] 
        private TMP_Text _text;

        private void OnValidate()
        {
            _text = GetComponent<TMP_Text>();
        }

        protected void UpdateScore(int score)
        {
            _text.text = string.Format(TextTemplate, score);
        }
    }
}
