using UnityEngine;

namespace LazyBalls
{
    public class BallDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GameTags.Ball))
            {
                Destroy(other.gameObject);
                PlayerInfo.Instance().BallDestroyed();
            }
        }
    }
}
