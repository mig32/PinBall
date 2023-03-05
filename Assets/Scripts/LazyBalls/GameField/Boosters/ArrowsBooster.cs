using System.Collections;
using UnityEngine;

namespace LazyBalls.GameField.Boosters
{
    public class ArrowsBooster : BoosterBase
    {
        [SerializeField] private float boosterForce;
        [SerializeField] private float rechargeTime;
        [SerializeField] private SpriteRenderer icon;

        private bool _isEnabled = true;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_isEnabled)
            {
                return;
            }
            
            if (!other.gameObject.CompareTag(GameTags.Ball))
            {
                return;
            }

            var ballRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody == null)
            {
                return;
            }

            SetEnabled(false);
            AddScore();
            ballRigidbody.AddForce(transform.forward * boosterForce);
            StartCoroutine(RechargeCoroutine());
        }

        private IEnumerator RechargeCoroutine()
        {
            yield return new WaitForSeconds(rechargeTime);
            SetEnabled(true);
        }

        private void SetEnabled(bool isEnabled)
        {
            if (isEnabled == _isEnabled)
            {
                return;
            }

            _isEnabled = isEnabled;
            
            var color = icon.color;
            color.a = isEnabled ? 1.0f : 0.5f;
            icon.color = color;
        }
        
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(new Ray(transform.position, transform.forward));
        }
    }
}
