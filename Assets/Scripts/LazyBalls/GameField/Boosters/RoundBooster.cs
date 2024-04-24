using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.GameField.Boosters
{
    public class RoundBooster : BoosterBase
    {
        [SerializeField] private float boosterForce;

        protected override BoosterType _type => BoosterType.Round;
        
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
            AddScore();
            
            SoundController.Instance().PlaySound(SoundController.SoundType.RoundBooster);
        }
    }
}
