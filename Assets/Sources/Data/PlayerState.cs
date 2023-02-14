using System;
using BeresnevTest.BallCustomization;
using BeresnevTest.Services;
using UnityEngine;

namespace BeresnevTest.Data
{
    [CreateAssetMenu]
    public class PlayerState : ScriptableObject, IPlayerState
    {
        public event Action<int> BestScoreChangedEvent;
        public int BestScore => _playerData.BestScore;

        public Skin BallSkin => _ballCustomizationConfig.Skins[_playerData.BallSkinIndex];
        public int BallSkinIndex => _playerData.BallSkinIndex;

        [SerializeField]
        private PlayerData _playerData;

        [SerializeField] 
        private BallCustomizationConfig _ballCustomizationConfig;

        private readonly ISaveService _saveService = new FileSaveService();

        private void OnEnable()
        {
            Restore();
        }
        
        private void OnDisable()
        {
            Save();
        }

        public void Save()
        {
            #if !UNITY_EDITOR
            if (_playerData is null)
                throw new InvalidOperationException("Can't save uninitialized object");

            _saveService.Save(_playerData);
            #endif
        }

        public void Restore()
        {
            #if !UNITY_EDITOR
            _playerData = _saveService.Restore();
            
            if (_playerData == null || !IsDataValid(_playerData))
                Initialize();
            #endif
        }
        
        public void UpdateBestScore(int score)
        {
            _playerData.BestScore = score;
            BestScoreChangedEvent?.Invoke(score);
        }

        public void ChangeBallSkin(int newBallSkinIndex)
        {
            _playerData.BallSkinIndex = newBallSkinIndex;
        }
        
        private void Initialize()
        {
            _playerData = new PlayerData
            {
                BestScore = 0,
                BallSkinIndex = 0
            };

            _saveService.Save(_playerData);
        }

        private bool IsDataValid(PlayerData data)
        {
            if (data.BestScore < 0)
                return false;

            if (data.BallSkinIndex < 0 || data.BallSkinIndex >= _ballCustomizationConfig.Skins.Length)
                return false;

            return true;
        }
    }
}