using System.Collections.Generic;
using System.Linq;
using LazyBalls.Dialogs;
using UnityEngine;

namespace LazyBalls.Singletons
{
    public class GUIController : MonoBehaviour
    {
        [SerializeField] private LocalizeTextWithIntParam scoreText;
        [SerializeField] private LocalizeTextWithIntParam ballsText;
        [SerializeField] private RectTransform dialogsContainer;
        [SerializeField] private List<DialogBase> dialogList;
        [SerializeField] private string pauseButtonKey = "Pause";

        private static GUIController _instance;
        public static GUIController Instance() => _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                UnityEngine.Debug.Assert(false, "GUIController must be single");
                return;
            }

            _instance = this;
        }

        private void Start()
        {
            PlayerInfo.Instance().ScoreChanged += UpdateScore;
            PlayerInfo.Instance().BallsChanged += UpdateBalls;
            UpdateScore();
            UpdateBalls();
            ShowDialog(DialogType.Start);
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }

            if (PlayerInfo.Instance() != null)
            {
                PlayerInfo.Instance().ScoreChanged -= UpdateScore;
                PlayerInfo.Instance().BallsChanged -= UpdateBalls;
            }
        }

        public DialogBase ShowDialog(DialogType type)
        {
            var prefab = dialogList.FirstOrDefault(it => it.GetDialogType() == type);
            return Instantiate(prefab, dialogsContainer);
        }

        private void UpdateScore()
        {
            scoreText.SetParam(PlayerInfo.Instance().Score);
        }

        private void UpdateBalls()
        {
            ballsText.SetParam(PlayerInfo.Instance().Balls);
        }

        private void Update()
        {
            var windowShown = dialogsContainer.childCount > 0;
            var isPaused = PlayerInfo.Instance().IsGamePaused();
            if (windowShown)
            {
                if (!isPaused)
                {
                    PlayerInfo.Instance().PauseGame();
                }
            }
            else
            {
                if (Input.GetButtonDown(pauseButtonKey))
                {
                    ShowDialog(DialogType.Pause);
                }
                else if (isPaused)
                {
                    PlayerInfo.Instance().UnpauseGame();
                }
            }
        }
    }
}