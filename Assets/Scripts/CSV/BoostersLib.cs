using System;
using System.Linq;
using LazyBalls.Boosters;
using UnityEngine;

namespace LazyBalls.CSV
{
    [Serializable]
    public struct BoosterScore
    {
        public int id;
        public int score;
        public int combo;
        public int max;
    }

    public class BoostersLib : MonoBehaviour
    {
        [SerializeField] private TextAsset boostersCSV;
        
        private BoosterScore[] _boostersList;
        
        private static BoostersLib _instance;
        public static BoostersLib Instance() => _instance;
        private void Awake()
        {
            if (_instance != null)
            {
                UnityEngine.Debug.Assert(false, "BoostersLib must be single");
                return;
            }

            _instance = this;
            _boostersList = CSVSerializer.Deserialize<BoosterScore>(boostersCSV.text);
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        public BoosterScore GetBoosterScoreForType(BoosterType type) =>
            _boostersList.FirstOrDefault(it => it.id == (int) type);
    }
}