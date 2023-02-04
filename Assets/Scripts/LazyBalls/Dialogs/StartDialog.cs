using LazyBalls.Ads;
using LazyBalls.Singletons;
using UnityEngine;
using UnityEngine.UI;

namespace LazyBalls.Dialogs
{
    public class StartDialog : DialogBase
    {
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Button startButton;
        [SerializeField] private Button adButton;

        protected override void Start()
        {
            base.Start();
            
            startButton.onClick.AddListener(StartGame);
            
#if UNITY_IOS || UNITY_ANDROID
            adButton.onClick.AddListener(ShowAd);
#else
            adButton.gameObject.SetActive(false);
#endif

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

        private void ShowAd()
        {
            AdsController.Instance().ShowAd(AdType.Basic);
        }

        public override DialogType GetDialogType() => DialogType.Start;
    }
}