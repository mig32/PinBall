using UnityEngine;

namespace LazyBalls.Debug
{
    public class BallDebug : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            UnityEngine.Debug.Log($"ball collided with => {collision.gameObject.name}");
        }
    }
}
