using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.GameField
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
        private int chargeSound;

        private void Start()
        {
            _deltaForce = chargeTime > 0 ? (maxForce - minForce) / chargeTime : 999999;
        }

        private void Update()
        {
            if (Input.GetButtonDown(buttonKey))
            {
                chargeSound = SoundController.Instance().PlaySound(SoundController.SoundType.LauncherCharge);
                _currentForce = minForce;
            }

            if (Input.GetButton(buttonKey) && _currentForce < maxForce)
            {
                _currentForce += _deltaForce * Time.deltaTime;
            }

            if (Input.GetButtonUp(buttonKey) && _ballRigidbody != null)
            {
                SoundController.Instance().StopSound(chargeSound);
                SoundController.Instance().PlaySound(SoundController.SoundType.LauncherRelease);
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

        private void OnDestroy()
        {
            if (chargeSound > 0 && SoundController.Instance() != null)
            {
                SoundController.Instance().StopSound(chargeSound);
            }
        }
    }
}
