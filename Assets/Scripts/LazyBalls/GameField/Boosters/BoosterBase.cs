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
        }
        
        private static BoosterHistory s_boosterHistory = new() 
        {
            LastBoosterType = BoosterType.None,
            ComboAmount = 0
        };
        
        public static void ResetCombo()
        {
            s_boosterHistory.LastBoosterType = BoosterType.None;
            s_boosterHistory.ComboAmount = 0;
        }
        
        protected abstract BoosterType _type { get; }
        private BoosterScore _boosterScore;

        protected virtual void Start()
        {
            _boosterScore = BoostersLib.Instance().GetBoosterScoreForType(_type);
        }

        protected void AddScore()
        {
            var score = _boosterScore.score;
            if (s_boosterHistory.LastBoosterType == _type)
            {
                ++s_boosterHistory.ComboAmount;
                score += Mathf.Min(s_boosterHistory.ComboAmount * _boosterScore.combo, _boosterScore.max);
            }
            else
            {
                s_boosterHistory.LastBoosterType = _type;
                s_boosterHistory.ComboAmount = 0;
            }

            PlayerInfo.Instance().AddScore(score);
        }
    }
}