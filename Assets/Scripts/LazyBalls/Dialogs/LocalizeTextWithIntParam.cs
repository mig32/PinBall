using LazyBalls.Singletons;
using TMPro;
using UnityEngine;

namespace LazyBalls.Dialogs
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizeTextWithIntParam : LocalizeText
    {
        private int _pram;

        protected override void OnStart()
        {
        }
        
        public void SetParam(int param)
        {
            _pram = param;
            UpdateText();
        }
        
        protected override void UpdateText()
        {
            text.text = string.Format(LocalizationLib.Instance().GetTranslation(localeKey), _pram); 
        }
    }
}