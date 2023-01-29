using LazyBalls.CSV;
using TMPro;
using UnityEngine;

namespace LazyBalls
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
            Text.text = string.Format(LocalizationLib.Instance().GetTranslation(localeKey), _pram); 
        }
    }
}