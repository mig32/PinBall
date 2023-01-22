using UnityEngine;

namespace LazyBalls
{
    public class LaunchPadController : MonoBehaviour
    {
        [SerializeField] private string buttonKey;
        [SerializeField] private float minForce;
        [SerializeField] private float maxForce;
        [SerializeField] private float chargeTime;

        private float _deltaForce;
        private float _currentForce;
        private Rigidbody _ballRigidbody;

        private void Start()
        {
            _deltaForce = chargeTime > 0 ? (maxForce - minForce) / chargeTime : 999999;
        }

        private void Update()
        {
            if (Input.GetButtonDown(buttonKey))
            {
                _currentForce = minForce;
            }

            if (Input.GetButton(buttonKey) && _currentForce < maxForce)
            {
                _currentForce += _deltaForce * Time.deltaTime;
            }

            if (Input.GetButtonUp(buttonKey) && _ballRigidbody != null)
            {
                _ballRigidbody.AddForce(transform.forward * Mathf.Clamp(_currentForce, minForce, maxForce));
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GameTags.Ball))
            {
                _ballRigidbody = other.gameObject.GetComponent<Rigidbody>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(GameTags.Ball))
            {
                _ballRigidbody = null;
            }
        }
    }
}
