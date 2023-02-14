using BeresnevTest.GameLogic;
using BeresnevTest.Scenes;
using BeresnevTest.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BeresnevTest.Ui.Game
{
    [RequireComponent(typeof(Canvas))]
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] 
        private Startup _startup;
        
        [SerializeField] 
        private Button _menuButton;
        
        [SerializeField]
        private Button _continueButton;

        [SerializeField]
        private Button _restartButton;
        
        [SerializeField]
        private Button _mainMenuButton;

        [SerializeField] 
        private SceneReference _mainMenuScene;
        
        [SerializeField, HideInInspector] 
        private Canvas _canvas;

        private IGamePauseService _gamePauseService;
        
        private void OnValidate()
        {
            _canvas = GetComponent<Canvas>();
        }

        private void Awake()
        {
            _menuButton.onClick.AddListener(ShowMenu);
            _continueButton.onClick.AddListener(HideMenu);
            _restartButton.onClick.AddListener(RestartGame);
            _mainMenuButton.onClick.AddListener(MainMenuClicked);
        }

        private void Start()
        {
            _gamePauseService = _startup.GamePauseService;
        }

        private void OnDestroy()
        {
            HideMenu();
            
            _menuButton.onClick.RemoveListener(ShowMenu);
            _continueButton.onClick.RemoveListener(HideMenu);
            _restartButton.onClick.RemoveListener(RestartGame);
            _mainMenuButton.onClick.RemoveListener(MainMenuClicked);
        }
        
        private void ShowMenu()
        {
            _gamePauseService.PauseGame();
            _canvas.enabled = true;
        }
        
        private void HideMenu()
        {
            _canvas.enabled = false;
            _gamePauseService.UnpauseGame();
        }

        private void RestartGame()
        {
            _gamePauseService.RestartGame();
            HideMenu();
        }

        private void MainMenuClicked()
        {
            SceneManager.LoadScene(_mainMenuScene.Path);
        }
    }
}
