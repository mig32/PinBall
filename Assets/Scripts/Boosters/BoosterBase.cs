using UnityEngine;

namespace LazyBalls.Boosters
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
        [SerializeField] private int boosterScore;
        [SerializeField] private int boosterComboScore;
        [SerializeField] private int boosterMaxScore;

        protected void AddScore()
        {
            var score = boosterScore;
            if (_boosterHistory.LastBoosterType == type)
            {
                ++_boosterHistory.ComboAmount;
                score += Mathf.Min(_boosterHistory.ComboAmount * boosterComboScore, boosterMaxScore);
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