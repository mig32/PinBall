using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LazyBalls.Boosters
{
    public class RandomPortalBooster : BoosterBase
    {
        private static List<RandomPortalBooster> entries = new();

        private static RandomPortalBooster PickRandomPortal(RandomPortalBooster entry)
        {
            if (entries.Count < 1)
            {
                return null;
            }

            if (entries.Count == 1)
            {
                return entries[0];
            }

            if (!entries.Contains(entry))
            {
                return entries[Random.Range(0, entries.Count)];
            }

            var entryIdx = entries.IndexOf(entry);
            var rnd = Random.Range(0, entries.Count - 1);
            if (rnd >= entryIdx)
            {
                rnd++;
            }

            return entries[rnd];
        }
        
        [SerializeField] private float teleportDelay;
        [SerializeField] private float boosterForce;
        [SerializeField] private Transform exitTransform;

        private bool _isEntryDisabled;
        
        protected override void Start()
        {
            entries.Add(this);
            base.Start();
        }

        private void OnDestroy()
        {
            entries.Remove(this);
        }

        private void DisableEntryUntilBallExit()
        {
            _isEntryDisabled = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(GameTags.Ball))
            {
                return;
            }
            
            if (_isEntryDisabled)
            {
                _isEntryDisabled = false;
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

        private void OnTriggerExit(Collider other)
        {
            if (!_isEntryDisabled)
            {
                return;
            }
            
            if (!other.gameObject.CompareTag(GameTags.Ball))
            {
                return;
            }
            
            _isEntryDisabled = false;
        }

        private IEnumerator TeleportDelayCoroutine(Rigidbody ballRigidbody)
        {
            var portalExit = PickRandomPortal(this);
            if (portalExit == null)
            {
                yield break;
            }
            
            portalExit.DisableEntryUntilBallExit();
            var portalExitTransform = portalExit.exitTransform;
            
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
        }    
        
        public void OnDrawGizmos()
        {
            var gizmosColor = Color.magenta;
            var entryPosition = GetComponentInChildren<Collider>().bounds.center;
            var exitPosition = exitTransform.position;
            Gizmos.color = gizmosColor;
            Gizmos.DrawCube(entryPosition, new Vector3(0.1f, 0.1f, 0.1f));
            Gizmos.DrawSphere(exitPosition, 0.025f);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(new Ray(exitPosition, exitTransform.forward));
            foreach (var randomPortalBooster in FindObjectsOfType<RandomPortalBooster>())
            {
                if (randomPortalBooster == this)
                {
                    continue;
                }
                
                var otherExit = randomPortalBooster.exitTransform.position;
                Gizmos.color = gizmosColor;
                Gizmos.DrawLine(entryPosition, otherExit);
            }
        }
    }
}
