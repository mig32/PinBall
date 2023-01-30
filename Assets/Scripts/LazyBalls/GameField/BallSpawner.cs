using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.GameField
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private Transform ballParent;

        private void Start()
        {
            PlayerInfo.Instance().CreateNewBall += SpawnBall;
        }

        private void SpawnBall()
        {
            Instantiate(ballPrefab, transform.position, transform.rotation, ballParent);
            SoundController.Instance().PlaySound(SoundController.SoundType.BallAppear);
        }

        private void OnDestroy()
        {
            if (PlayerInfo.Instance() != null)
            {
                PlayerInfo.Instance().CreateNewBall -= SpawnBall;
            }
        }
    }
}
