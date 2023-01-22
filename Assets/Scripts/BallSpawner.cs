using UnityEngine;

namespace LazyBalls
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
