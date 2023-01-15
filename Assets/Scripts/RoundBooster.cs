using UnityEngine;

namespace LazyBalls.Boosters
{
    public class RoundBooster : MonoBehaviour
    {
        [SerializeField] private float boosterForce;

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag(GameTags.Ball))
            {
                return;
            }

            var ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody == null)
            {
                return;
            }

            var dir = collision.contacts[0].point - transform.position;
            dir = dir.normalized;
            ballRigidbody.AddForce(dir * boosterForce);
        }
    }
}
