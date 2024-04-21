using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LazyBalls.Singletons
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
            var langInt = LocalStorage.SelectedLanguage;
            if (langInt < 0)
            {
                var systemLanguage = Application.systemLanguage;
                if (!_knownLanguages.Contains(systemLanguage))
                {
                    systemLanguage = SystemLanguage.English;
                }

                _selectedLanguage = systemLanguage;
            }
            else
            {
                _selectedLanguage = (SystemLanguage)langInt;
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
                LocalStorage.SelectedLanguage = (int)language;
                _selectedLanguage = language;
                OnLanguageChanged?.Invoke();
            }
        }

        public SystemLanguage GetSelectedLanguage() => _selectedLanguage;
        
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