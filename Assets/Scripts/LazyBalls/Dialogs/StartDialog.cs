using LazyBalls.Singletons;
using UnityEngine;
using UnityEngine.UI;

namespace LazyBalls.Dialogs
{
    public class StartDialog : DialogBase
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle soundToggle;

        protected override void Start()
        {
            base.Start();
            
            startButton.onClick.AddListener(StartGame);

            musicToggle.isOn = MusicController.Instance().IsEnabled;
            musicToggle.onValueChanged.AddListener(SetMusicEnabled);
            
            soundToggle.isOn = SoundController.Instance().IsEnabled;
            soundToggle.onValueChanged.AddListener(SetSoundEnabled);
            
            MusicController.Instance().PlayMusic(MusicController.MusicType.Menu);
        }

        private void StartGame()
        {
            PlayerInfo.Instance().StartGame();
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

        public override DialogType GetDialogType() => DialogType.Start;
    }
}