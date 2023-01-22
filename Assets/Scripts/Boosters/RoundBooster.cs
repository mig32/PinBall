using UnityEngine;

namespace LazyBalls.Boosters
{
    public class RoundBooster : MonoBehaviour
    {
        [SerializeField] private float boosterForce;

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag(GameTags.Ball))
            {
                return;
            }

            var ballRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody == null)
            {
                return;
            }

            var dir = other.contacts[0].point - transform.position;
            dir = dir.normalized;
            ballRigidbody.AddForce(dir * boosterForce);
        }
    }
}
