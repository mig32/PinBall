using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.GameField
{
    public class BallController : MonoBehaviour
    {
        private void Start()
        {
            PlayerInfo.Instance().ClearBalls += DestroyBall;
        }

        private void DestroyBall()
        {
            Destroy(gameObject);
        }
        
        private void OnDestroy()
        {
            if (PlayerInfo.Instance() != null)
            {
                PlayerInfo.Instance().ClearBalls -= DestroyBall;
            }
        }
        
        private void OnCollisionEnter(Collision other)
        {
            SoundController.Instance().PlaySound(SoundController.SoundType.BallHit);
        }
    }
}