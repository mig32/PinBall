using System;
using LazyBalls.Ads;
using LazyBalls.Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LazyBalls.Dialogs
{
    public class StartDialog : DialogBase
    {
        [Serializable]
        private struct LanguageItem
        {
            public SystemLanguage language;
            public string languageName;
        }
        
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Button startButton;
        [SerializeField] private Button adButton;
        [SerializeField] private TMP_Dropdown languageDropdown;
        [SerializeField] private LanguageItem[] languageList;

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
            
            var selectedLanguage = LocalizationLib.Instance().GetSelectedLanguage();
            languageDropdown.onValueChanged.AddListener(ChangeLanguage);
            languageDropdown.options.Clear();
            for (var i = 0; i < languageList.Length; ++i)
            {
                languageDropdown.options.Add(new TMP_Dropdown.OptionData(languageList[i].languageName));
                if (selectedLanguage == languageList[i].language)
                {
                    languageDropdown.value = i;
                }
            }

            
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

        private void ChangeLanguage(int languageIdx)
        {
            var selectedLanguage = languageList[languageIdx].language;
            LocalizationLib.Instance().SetLanguage(selectedLanguage);
        }
        
        public override DialogType GetDialogType() => DialogType.Start;
    }
}