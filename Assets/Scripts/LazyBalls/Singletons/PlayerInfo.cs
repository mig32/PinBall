using System;
using LazyBalls.Dialogs;
using LazyBalls.GameField;
using LazyBalls.GameField.Boosters;
using UnityEngine;

namespace LazyBalls.Singletons
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private int _initialBallsAmount;
        
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
        
        private int _maxScore;
        public int MaxScore 
        { 
            get => _maxScore;
            private set
            {
                _maxScore = value;
                LocalStorage.MaxScore = value;
            }
        }        
        
        private int _prevScore;
        public int PrevScore 
        { 
            get => _prevScore;
            private set
            {
                _prevScore = value;
                LocalStorage.PrevScore = value;
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

        private bool _isGameStarted;

        public event Action CreateNewBall;
        public event Action ClearBalls;
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
            _maxScore = LocalStorage.MaxScore;
            _prevScore = LocalStorage.PrevScore;
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        private void Start()
        {
            ResetGame();
        }
        
        public void RestartGame()
        {
            ResetGame();
            StartGame();
        }
        
        public void StartGame()
        {
            _isGameStarted = true;
            CreateNewBall?.Invoke();
            MusicController.Instance().PlayMusic(MusicController.MusicType.Game);
        }

        public void ResetGame()
        {
            if (_score > 0)
            {
                SaveScore();
            }
            
            _isGameStarted = false;
            BoosterBase.ResetCombo();
            ClearBalls?.Invoke();
            Score = 0;
            Balls = _initialBallsAmount;
        }

        public void AddScore(int amount)
        {
            Score += amount;
        }
        
        public void BallDestroyed()
        {
            if (!_isGameStarted || BallController.s_count > 0)
            {
                return;
            }
            
            if (Balls > 0)
            {
                Balls -= 1;
                CreateNewBall?.Invoke();
                return;
            }

            SaveScore();
            
            GUIController.Instance().ShowDialog(DialogType.GameOver);
        }

        private void SaveScore()
        {
            PrevScore = _score;
            if (_score > _maxScore)
            {
                MaxScore = _score;
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1;
        }

        public bool IsGamePaused()
        {
            return Time.timeScale <= float.Epsilon;
        }

        public void DoCreateNewBall() => CreateNewBall?.Invoke();
    }
}