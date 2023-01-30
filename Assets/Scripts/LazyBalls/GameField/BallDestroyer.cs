using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.GameField
{
    public class BallDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GameTags.Ball))
            {
                Destroy(other.gameObject);
                PlayerInfo.Instance().BallDestroyed();
                SoundController.Instance().PlaySound(SoundController.SoundType.BallDestroyed);
            }
        }
    }
}
