using System;
using LazyBalls.Boosters;
using LazyBalls.Dialogs;
using UnityEngine;

namespace LazyBalls
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private int initialBallsAmount;
        
        private int _score;
        public int Score 
        { 
            get => _score;
            private set
            {
                _score = value;
                ScoreChanged?.Invoke();
            }
        }
        
        private int _balls;
        public int Balls 
        { 
            get => _balls;
            private set
            {
                _balls = value;
                BallsChanged?.Invoke();
            }
        }

        public event Action CreateNewBall;
        public event Action ScoreChanged;
        public event Action BallsChanged;

        
        private static PlayerInfo _instance;
        public static PlayerInfo Instance() => _instance;
        private void Awake()
        {
            if (_instance != null)
            {
                UnityEngine.Debug.Assert(false, "PlayerInfo must be single");
                return;
            }

            _instance = this;
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        public void RestartGame()
        {
            Score = 0;
            Balls = initialBallsAmount;
            BoosterBase.ResetCombo();
            CreateNewBall?.Invoke();
        }

        public void AddScore(int amount)
        {
            Score += amount;
        }
        
        public void BallDestroyed()
        {
            if (Balls > 0)
            {
                Balls -= 1;
                CreateNewBall?.Invoke();
                return;
            }

            GUIController.Instance().ShowDialog(DialogType.GameOver);
        }
    }
}