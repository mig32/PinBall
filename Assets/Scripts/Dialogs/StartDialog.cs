using UnityEngine;
using UnityEngine.UI;

namespace LazyBalls.Dialogs
{
    public class StartDialog : DialogBase
    {
        [SerializeField] private Button startButton;

        private void Start()
        {
            startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            PlayerInfo.Instance().RestartGame();
            Destroy(gameObject);
        }

        public override DialogType GetDialogType() => DialogType.Start;
    }
}