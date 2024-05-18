using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LazyBalls.GameField.Boosters
{
    public class LetterBooster : BoosterBase
    {
        private static readonly List<LetterBooster> s_letters = new ();

        public static void ResetWord()
        {
            foreach (var letter in s_letters)
            {
                letter.SetWord(WordBooster.GetWord());
            }
        }

        [SerializeField] private TextMeshPro _text;
        [SerializeField] private int _letterIdx;

        private bool _isEnabled = true;
        
        protected override BoosterType _type => BoosterType.Letter;

        private void Awake()
        {
            s_letters.Add(this);
        }

        protected override void Start()
        {
            base.Start();
            SetWord(WordBooster.GetWord());
        }

        private void OnDestroy()
        {
            s_letters.Remove(this);
        }

        public void SetWord(string word)
        {
            if (_letterIdx < 0 || _letterIdx >= word.Length)
            {
                gameObject.SetActive(false);
                return;
            }
            
            gameObject.SetActive(true);
            SetEnabled(true);
            _text.text = $"{word[_letterIdx]}";
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isEnabled)
            {
                return;
            }
            
            if (!other.gameObject.CompareTag(GameTags.Ball))
            {
                return;
            }

            var ballRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody == null)
            {
                return;
            }

            SetEnabled(false);
            AddScore();
            WordBooster.OnLetterTaken(_letterIdx);
        }

        private void SetEnabled(bool isEnabled)
        {
            if (isEnabled == _isEnabled)
            {
                return;
            }

            _isEnabled = isEnabled;
            
            var color = _text.color;
            color.a = isEnabled ? 1.0f : 0.5f;
            _text.color = color;
        }
    }
}
