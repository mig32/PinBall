using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.GameField
{
    public class BallController : MonoBehaviour
    {
        public static int s_count = 0;
        
        private void Start()
        {
            PlayerInfo.Instance().ClearBalls += DestroyBall;
            ++s_count;
        }

        private void DestroyBall()
        {
            Destroy(gameObject);
        }
        
        private void OnDestroy()
        {
            --s_count;
            if (PlayerInfo.Instance() != null)
            {
                PlayerInfo.Instance().ClearBalls -= DestroyBall;
                PlayerInfo.Instance().BallDestroyed();
            }
        }
        
        private void OnCollisionEnter(Collision other)
        {
            SoundController.Instance().PlaySound(SoundController.SoundType.BallHit);
        }
    }
}