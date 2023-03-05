using System;
using LazyBalls.Singletons;
using TMPro;
using UnityEngine;

namespace LazyBalls.Dialogs
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizeText : MonoBehaviour
    {
        [SerializeField] protected string localeKey;
        [SerializeField] protected TMP_Text text;

        private void OnValidate()
        {
            if (text == null)
            {
                text = GetComponent<TMP_Text>();
            }
        }

        private void Start()
        {
            LocalizationLib.Instance().OnLanguageChanged += UpdateText;
            OnStart();
        }

        protected virtual void OnStart()
        {
            UpdateText();
        }

        private void OnDestroy()
        {
            if (LocalizationLib.Instance() != null)
            {
                LocalizationLib.Instance().OnLanguageChanged -= UpdateText;
            }
        }
        
        protected virtual void UpdateText()
        {
            text.text = LocalizationLib.Instance().GetTranslation(localeKey);
        }
    }
}