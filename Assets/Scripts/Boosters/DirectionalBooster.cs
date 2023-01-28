using UnityEngine;

namespace LazyBalls.Boosters
{
    public class DirectionalBooster : BoosterBase
    {
        [SerializeField] private float boosterForce;
        [SerializeField] private float angleThreshold = 60;

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

            var collisionVec = other.transform.position - transform.position;
            var collisionAngle = Vector3.Angle(collisionVec, transform.forward);
            if (collisionAngle > angleThreshold)
            {
                return;
            }
            
            ballRigidbody.AddForce(transform.forward * boosterForce);
            AddScore();
        }
        
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(new Ray(transform.position, transform.forward));
        }
    }
}
