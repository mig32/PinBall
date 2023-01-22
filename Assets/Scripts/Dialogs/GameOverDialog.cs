using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LazyBalls.Dialogs
{
    public class GameOverDialog : DialogBase
    {
        [SerializeField] private TMP_Text scoreAmountText;
        [SerializeField] private Button restartButton;

        private void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
            scoreAmountText.text = $"{PlayerInfo.Instance().Score}";
        }

        private void RestartGame()
        {
            PlayerInfo.Instance().RestartGame();
            Destroy(gameObject);
        }

        public override DialogType GetDialogType() => DialogType.GameOver;
    }
}