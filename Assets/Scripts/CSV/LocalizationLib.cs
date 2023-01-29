using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LazyBalls.CSV
{
    [Serializable]
    public struct LocaleInfo
    {
        public string Key;
        public string Ru;
        public string En;
    }
        
    public class LocalizationLib : MonoBehaviour
    {
        [SerializeField] private TextAsset localesCSV;
        
        private Dictionary<string, LocaleInfo> _locales = new();
        private SystemLanguage _selectedLanguage;
        private const string SelectedLanguageKey = "lang";
        private readonly SystemLanguage[] _knownLanguages = new []{ SystemLanguage.Russian, SystemLanguage.English};

        private static LocalizationLib _instance;
        public static LocalizationLib Instance() => _instance;

        public event Action OnLanguageChanged;
        
        private void Awake()
        {
            if (_instance != null)
            {
                UnityEngine.Debug.Assert(false, "LocalizationLib must be single");
                return;
            }

            _instance = this;
            var localesArr = CSVSerializer.Deserialize<LocaleInfo>(localesCSV.text);
            _locales = localesArr.ToDictionary(it => it.Key.ToLower());
            var langInt = PlayerPrefs.GetInt(SelectedLanguageKey, -1);
            if (langInt < 0)
            {
                var systemLanguage = Application.systemLanguage;
                if (!_knownLanguages.Contains(systemLanguage))
                {
                    systemLanguage = SystemLanguage.English;
                }

                _selectedLanguage = systemLanguage;
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        public void SetLanguage(SystemLanguage language)
        {
            if (_selectedLanguage != language)
            {
                PlayerPrefs.SetInt(SelectedLanguageKey, (int)language);
                _selectedLanguage = language;
                OnLanguageChanged?.Invoke();
            }
        }
        
        public string GetTranslation(string key)
        {
            var lowKey = key.ToLower();
            if (_locales.ContainsKey(lowKey))
            {
                return GetTranslation(_locales[lowKey]);
            }

            UnityEngine.Debug.LogError($"Locale not found for key {key}");
            return key;
        }

        private string GetTranslation(LocaleInfo info)
        {
            switch (_selectedLanguage)
            {
                case SystemLanguage.Russian:
                    return info.Ru;
                case SystemLanguage.English:
                default:
                    return info.En;
            }
        }
    }
}