using BeresnevTest.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BeresnevTest.Ui.MainMenu
{
    public class PlayMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _playButton;

        [SerializeField] 
        private SceneReference _gameScene;
        
        private void Awake()
        {
            _playButton.onClick.AddListener(PlayClicked);
        }
        

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(PlayClicked);
        }
        
        private void PlayClicked()
        {
            SceneManager.LoadScene(_gameScene.Path);
        }
    }
}