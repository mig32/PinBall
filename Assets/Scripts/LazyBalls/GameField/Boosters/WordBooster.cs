using TMPro;
using UnityEngine;

namespace LazyBalls.GameField.Boosters
{
    public class WordBooster : BoosterBase
    {
        public static void OnLetterTaken(int letterIdx)
        {
            if (s_instance == null)
            {
                return;
            }

            s_instance.OnLetterTakenInternal(letterIdx);
        }

        public static string GetWord()
        {
            if (s_instance == null)
            {
                return "";
            }

            return s_instance._word;
        }

        private static WordBooster s_instance;
        
        [SerializeField] private string _word;
        [SerializeField] private TextMeshPro _text;
        [SerializeField] private Color _colorTaken;
        [SerializeField] private Color _colorNotTaken;
        [SerializeField] private bool _resetAfterComplete;

        private int _takenLetters;
            
        protected override BoosterType _type => BoosterType.Word;

        protected override void Start()
        {
            base.Start();
            s_instance = this;
            UpdateWordText();
        }

        private void OnDestroy()
        {
            s_instance = null;
        }

        private void OnLetterTakenInternal(int letterIdx)
        {
            var flag = 1 << letterIdx;
            _takenLetters |= flag;
            UpdateWordText();

            if (_takenLetters + 1 == 1 << _word.Length)
            {
                AddScore();
                if (_resetAfterComplete)
                {
                    Reset();
                }
            }
        }

        private void Reset()
        {
            _takenLetters = 0;
            UpdateWordText();
            LetterBooster.ResetWord();
        }

        private void UpdateWordText()
        {
            var text = "";
            var wasTaken = false;
            for (var i = 0; i < _word.Length; ++i)
            {
                var flag = 1 << i;
                var isTaken = (_takenLetters & flag) > 0;
                if (i == 0 || isTaken != wasTaken)
                {
                    var color = isTaken ? _colorTaken : _colorNotTaken;
                    text += $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>";
                    wasTaken = isTaken;
                }

                text += _word[i];
            }
            
            _text.text = text;
        }
    }
}