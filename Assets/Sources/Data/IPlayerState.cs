using System;
using BeresnevTest.BallCustomization;
using UnityEngine;

namespace BeresnevTest.Data
{
    public interface IPlayerState
    {
        int BestScore { get; }
        event Action<int> BestScoreChangedEvent;
        
        Skin BallSkin { get; }

        void UpdateBestScore(int score);
        void ChangeBallSkin(int newBallSkinIndex);
        
        void Save();
        void Restore();
    }
}