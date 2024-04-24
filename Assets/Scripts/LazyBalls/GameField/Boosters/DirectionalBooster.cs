using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.GameField.Boosters
{
    public class DirectionalBooster : BoosterBase
    {
        [SerializeField] private float boosterForce;
        [SerializeField] private float angleThreshold = 60;

        protected override BoosterType _type => BoosterType.Directional;
        
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
            
            SoundController.Instance().PlaySound(SoundController.SoundType.DirectionalBooster);
        }
        
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(new Ray(transform.position, transform.forward));
        }
    }
}
