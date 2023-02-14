using BeresnevTest.BallCustomization;
using BeresnevTest.Data;
using UnityEngine;

namespace BeresnevTest.Ui.MainMenu
{
    public class SkinsMenu : MonoBehaviour
    {
        [SerializeField] 
        private PlayerState _playerState;
        
        [SerializeField] 
        private BallCustomizationConfig _ballCustomizationConfig;

        [SerializeField] 
        private SkinButton _skinButtonPrefab;

        private SkinButton[] _skinButtons;
        
        private void Start()
        {
            _skinButtons = new SkinButton[_ballCustomizationConfig.Skins.Length];
            
            for (int i = 0; i < _ballCustomizationConfig.Skins.Length; i++)
            {
                _skinButtons[i] = Instantiate(_skinButtonPrefab, transform);
                _skinButtons[i].Init(i, _ballCustomizationConfig.Skins[i].Image, SkinButtonClicked);
            }
            
            _skinButtons[_playerState.BallSkinIndex].ChooseSkin();
        }

        private void SkinButtonClicked(int skinIndex)
        {
            foreach (var skinButton in _skinButtons)
                skinButton.UnchooseSkin();
            
            _playerState.ChangeBallSkin(skinIndex);
        }
    }
}
