using LazyBalls.Singletons;
using UnityEngine;
using UnityEngine.UI;

namespace LazyBalls.Dialogs
{
    public class PauseDialog : DialogBase
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private LocalizeTextWithIntParam maxScoreText;
        [SerializeField] private LocalizeTextWithIntParam prevScoreText;

        protected override void Start()
        {
            base.Start();
            
            continueButton.onClick.AddListener(Continue);
            exitButton.onClick.AddListener(ExitToMenu);

            musicToggle.isOn = MusicController.Instance().IsEnabled;
            musicToggle.onValueChanged.AddListener(SetMusicEnabled);
            
            soundToggle.isOn = SoundController.Instance().IsEnabled;
            soundToggle.onValueChanged.AddListener(SetSoundEnabled);
            
            maxScoreText.SetParam(PlayerInfo.Instance().MaxScore);
            prevScoreText.SetParam(PlayerInfo.Instance().PrevScore);
        }

        private void Continue()
        {
            SoundController.Instance().PlaySound(SoundController.SoundType.ButtonClick);
            Destroy(gameObject);
        }

        private void ExitToMenu()
        {
            PlayerInfo.Instance().ResetGame();
            GUIController.Instance().ShowDialog(DialogType.Start);
            SoundController.Instance().PlaySound(SoundController.SoundType.ButtonClick);
            Destroy(gameObject);
        }

        private void SetMusicEnabled(bool isEnabled)
        {
            MusicController.Instance().IsEnabled = isEnabled;
            SoundController.Instance().PlaySound(SoundController.SoundType.ButtonClick);
        }

        private void SetSoundEnabled(bool isEnabled)
        {
            SoundController.Instance().IsEnabled = isEnabled;
            if (isEnabled)
            {
                SoundController.Instance().PlaySound(SoundController.SoundType.ButtonClick);
            }
        }

        public override DialogType GetDialogType() => DialogType.Pause;
    }
}