using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeresnevTest.Ui.MainMenu
{
    public class SkinButton : MonoBehaviour
    {
        [SerializeField] 
        private Button _button;

        [SerializeField] 
        private Image _buttonImage;
        
        [SerializeField] 
        private Image _chosenImage;

        private Action<int> _clickedAction;
        private int _skinIndex;

        private void OnValidate()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(ButtonClicked);
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonClicked);
        }

        public void Init(int skinIndex,Sprite skinSprite, Action<int> clickedAction)
        {
            _skinIndex = skinIndex;
            _buttonImage.sprite = skinSprite;
            
            _clickedAction = clickedAction;
        }

        public void UnchooseSkin()
        {
            _chosenImage.enabled = false;
        }
        
        public void ChooseSkin()
        {
            _chosenImage.enabled = true;
        }

        private void ButtonClicked()
        {
            _clickedAction(_skinIndex);
            ChooseSkin();
        }
    }
}