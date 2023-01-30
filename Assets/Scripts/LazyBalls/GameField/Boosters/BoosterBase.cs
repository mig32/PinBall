using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.GameField.Boosters
{
    public abstract class BoosterBase : MonoBehaviour
    {
        private struct BoosterHistory
        {
            public BoosterType LastBoosterType;
            public int ComboAmount;

            public static BoosterHistory Create() => new BoosterHistory
            {
                LastBoosterType = BoosterType.None,
                ComboAmount = 0
            };
        }
        
        private static BoosterHistory _boosterHistory = BoosterHistory.Create();
        public static void ResetCombo()
        {
            _boosterHistory.LastBoosterType = BoosterType.None;
            _boosterHistory.ComboAmount = 0;
        }
        
        [SerializeField] private BoosterType type;
        
        private BoosterScore _boosterScore;

        protected virtual void Start()
        {
            _boosterScore = BoostersLib.Instance().GetBoosterScoreForType(type);
        }

        protected void AddScore()
        {
            var score = _boosterScore.score;
            if (_boosterHistory.LastBoosterType == type)
            {
                ++_boosterHistory.ComboAmount;
                score += Mathf.Min(_boosterHistory.ComboAmount * _boosterScore.combo, _boosterScore.max);
            }
            else
            {
                _boosterHistory.LastBoosterType = type;
                _boosterHistory.ComboAmount = 0;
            }

            PlayerInfo.Instance().AddScore(score);
        }
    }
}