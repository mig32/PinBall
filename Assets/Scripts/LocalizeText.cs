using LazyBalls.CSV;
using TMPro;
using UnityEngine;

namespace LazyBalls
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizeText : MonoBehaviour
    {
        [SerializeReference] protected string localeKey;

        protected TMP_Text Text;

        private void Start()
        {
            Text = GetComponent<TMP_Text>();
            LocalizationLib.Instance().OnLanguageChanged += UpdateText;
            OnStart();
        }

        protected virtual void OnStart()
        {
            UpdateText();
        }

        private void OnDestroy()
        {
            LocalizationLib.Instance().OnLanguageChanged -= UpdateText;
        }
        
        protected virtual void UpdateText()
        {
            Text.text = LocalizationLib.Instance().GetTranslation(localeKey);
        }
    }
}