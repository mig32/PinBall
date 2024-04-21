using System;
using System.Linq;
using LazyBalls.Ads;
using LazyBalls.Dialogs.Common;
using LazyBalls.Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
        
        [SerializeField] private ToggleButtonImage _musicToggle;
        [SerializeField] private ToggleButtonImage _soundToggle;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _adButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _languageButton;
        [SerializeField] private TMP_Text _languageText;
        [SerializeField] private LanguageItem[] _languageList;
        [SerializeField] private LocalizeTextWithIntParam _maxScoreText;
        [SerializeField] private LocalizeTextWithIntParam _prevScoreText;

        private int _curLanguageIdx;
        
        protected override void Start()
        {
            base.Start();
            
            _startButton.onClick.AddListener(StartGame);
            _creditsButton.onClick.AddListener(ShowCredits);
            
#if UNITY_IOS || UNITY_ANDROID
            _adButton.onClick.AddListener(ShowAd);
#else
            _adButton.gameObject.SetActive(false);
#endif
            
            _musicToggle.OnClick.AddListener(ToggleMusic);
            _musicToggle.SetEnabled(MusicController.Instance().IsEnabled);
            
            _soundToggle.OnClick.AddListener(ToggleSound);
            _soundToggle.SetEnabled(SoundController.Instance().IsEnabled);
            
            _languageButton.onClick.AddListener(ChangeLanguage);
            var selectedLanguage = LocalizationLib.Instance().GetSelectedLanguage();
            for (var i = 0; i < _languageList.Length; ++i)
            {
                var lang = _languageList[i];
                if (selectedLanguage == lang.language)
                {
                    _curLanguageIdx = i;
                    _languageText.text = lang.languageName;
                    break;
                }
            }
            
            _maxScoreText.SetParam(PlayerInfo.Instance().MaxScore);
            _prevScoreText.SetParam(PlayerInfo.Instance().PrevScore);
            
            MusicController.Instance().PlayMusic(MusicController.MusicType.Menu);
        }

        private void StartGame()
        {
            PlayerInfo.Instance().StartGame();
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

        private void ShowAd()
        {
            AdsController.Instance().ShowAd(AdType.Basic);
        }
        
        private void ChangeLanguage()
        {
            ++_curLanguageIdx;
            if (_curLanguageIdx >= _languageList.Length)
            {
                _curLanguageIdx = 0;
            }
            
            var selectedLanguage = _languageList[_curLanguageIdx];
            _languageText.text = selectedLanguage.languageName;
            LocalizationLib.Instance().SetLanguage(selectedLanguage.language);
        }

        private void ShowCredits()
        {
            var creditsDialog = GUIController.Instance().ShowDialog(DialogType.Credits);
            creditsDialog.OnClose += ShowThis;
            gameObject.SetActive(false);

            void ShowThis()
            {
                gameObject.SetActive(true);
            }
        }
        
        public override DialogType GetDialogType() => DialogType.Start;
    }
}