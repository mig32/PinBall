using LazyBalls.Dialogs.Common;
using LazyBalls.Singletons;
using UnityEngine;
using UnityEngine.UI;

namespace LazyBalls.Dialogs
{
    public class PauseDialog : DialogBase
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private ToggleButtonImage _musicToggle;
        [SerializeField] private ToggleButtonImage _soundToggle;

        protected override void Start()
        {
            base.Start();
            
            _continueButton.onClick.AddListener(Continue);
            _exitButton.onClick.AddListener(ExitToMenu);

            _musicToggle.OnClick.AddListener(ToggleMusic);
            _musicToggle.SetEnabled(MusicController.Instance().IsEnabled);
            
            _soundToggle.OnClick.AddListener(ToggleSound);
            _soundToggle.SetEnabled(SoundController.Instance().IsEnabled);
        }

        private void Continue()
        {
            Destroy(gameObject);
        }

        private void ExitToMenu()
        {
            PlayerInfo.Instance().ResetGame();
            GUIController.Instance().ShowDialog(DialogType.Start);
            Destroy(gameObject);
        }
        
        private void ToggleMusic()
        {            
            MusicController.Instance().IsEnabled = !MusicController.Instance().IsEnabled;
        }

        private void ToggleSound()
        {
            SoundController.Instance().IsEnabled = !SoundController.Instance().IsEnabled;
        }

        public override DialogType GetDialogType() => DialogType.Pause;
    }
}