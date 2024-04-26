using System.Collections;
using UnityEngine;

namespace LazyBalls.GameField.Boosters
{
    public class BallSplitBooster : BoosterBase
    {
        [SerializeField] private float _rechargeTime = 5.0f;
        [SerializeField] private float _force = 5.0f;
        [SerializeField] private SpriteRenderer _sprite;

        private bool _isEnabled = true;
        
        protected override BoosterType _type => BoosterType.BallSplitter;
        
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

            SetEnabled(false);
            AddScore();
            CloneBall(other.gameObject);
            StartCoroutine(RechargeCoroutine());
        }

        private void CloneBall(GameObject ball)
        {
            var ballCopy = Instantiate(ball, ball.transform.parent);

            var shift = ball.transform.localScale;
            var pos = ball.transform.localPosition;
            pos.x -= shift.x * 0.5f;
            ball.transform.localPosition = pos;
            pos.x += shift.x;
            ballCopy.transform.localPosition = pos;

            var ballRigidbody = ball.GetComponent<Rigidbody>();
            ballRigidbody.AddForce(-_force, 0, 0);
            var ballCopyRigidbody = ballCopy.GetComponent<Rigidbody>();
            ballCopyRigidbody.velocity = ballRigidbody.velocity;
            ballCopyRigidbody.AddForce(_force, 0, 0);
        }
        
        private IEnumerator RechargeCoroutine()
        {
            yield return new WaitForSeconds(_rechargeTime);
            SetEnabled(true);
        }

        private void SetEnabled(bool isEnabled)
        {
            if (isEnabled == _isEnabled)
            {
                return;
            }

            _isEnabled = isEnabled;
            
            var color = _sprite.color;
            color.a = isEnabled ? 1.0f : 0.5f;
            _sprite.color = color;
        }
    }
}