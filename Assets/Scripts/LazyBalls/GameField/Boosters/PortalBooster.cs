using System.Collections;
using LazyBalls.Singletons;
using UnityEngine;

namespace LazyBalls.GameField.Boosters
{
    public class PortalBooster : BoosterBase
    {
        [SerializeField] private float teleportDelay;
        [SerializeField] private float boosterForce;
        [SerializeField] private Transform portalExitTransform;
        
        private void OnTriggerEnter(Collider other)
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

            AddScore();
            StartCoroutine(TeleportDelayCoroutine(ballRigidbody));
        }

        private IEnumerator TeleportDelayCoroutine(Rigidbody ballRigidbody)
        {
            SoundController.Instance().PlaySound(SoundController.SoundType.PortalEnter);
            
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.isKinematic = true;
            ballRigidbody.gameObject.SetActive(false);
            yield return new WaitForSeconds(teleportDelay);

            var ballTransform = ballRigidbody.transform;
            ballTransform.position = portalExitTransform.position;
            ballTransform.rotation = portalExitTransform.rotation;

            ballRigidbody.gameObject.SetActive(true);
            ballRigidbody.isKinematic = false;
            ballRigidbody.AddForce(portalExitTransform.forward * boosterForce);
            
            SoundController.Instance().PlaySound(SoundController.SoundType.PortalExit);
        }    
        
        public void OnDrawGizmos()
        {
            var gizmosColor = Color.magenta;
            var enterPosition = GetComponentInChildren<Collider>().bounds.center;
            var exitPosition = portalExitTransform.position;
            Gizmos.color = gizmosColor;
            Gizmos.DrawLine(enterPosition, exitPosition);
            Gizmos.color = gizmosColor;
            Gizmos.DrawSphere(enterPosition, 0.05f);
            Gizmos.DrawSphere(exitPosition, 0.05f);
            Gizmos.color = gizmosColor;
            Gizmos.DrawRay(new Ray(exitPosition, portalExitTransform.forward));
        }
    }
}
