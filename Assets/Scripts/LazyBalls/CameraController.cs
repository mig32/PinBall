using System.Collections;
using UnityEngine;

namespace LazyBalls
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _thisCamera;
        [SerializeField] private BoxCollider _colliderToTrack;
        [SerializeField] private float _accuracity = 0.01f;
        [SerializeField] private float _zoomSpeed = 0.1f;
        
        private int _savedScreenWidth;
        private int _savedScreenHeight;
        private Coroutine _zoomCoroutine;
        
        private void Start()
        {
            if (_thisCamera == null)
            {
                _thisCamera = GetComponent<Camera>();
            }
        }

        private void Update()
        {
            if (_savedScreenWidth == Screen.width && _savedScreenHeight == Screen.height)
            {
                return;
            }

            _savedScreenWidth = Screen.width;
            _savedScreenHeight = Screen.height;
            if (_zoomCoroutine != null)
            {
                StopCoroutine(_zoomCoroutine);
            }

            _zoomCoroutine = StartCoroutine(ZoomCoroutine());
        }

        private IEnumerator ZoomCoroutine()
        {
            var zoomDirection = 0.0f;
            do
            {
                var bounds = _colliderToTrack.bounds;
                var center = bounds.center;
                var firstPoint = _thisCamera.WorldToScreenPoint(center + bounds.extents);
                var points = new Vector3[7];
                points[0] = _thisCamera.WorldToScreenPoint(center - bounds.extents);
                var shift = bounds.extents;
                shift.x = -shift.x;
                points[1] = _thisCamera.WorldToScreenPoint(center + shift);
                points[2] = _thisCamera.WorldToScreenPoint(center - shift);
                shift = bounds.extents;
                shift.y = -shift.y;
                points[3] = _thisCamera.WorldToScreenPoint(center + shift);
                points[4] = _thisCamera.WorldToScreenPoint(center - shift);
                shift = bounds.extents;
                shift.z = -shift.z;
                points[5] = _thisCamera.WorldToScreenPoint(center + shift);
                points[6] = _thisCamera.WorldToScreenPoint(center - shift);

                var minX = firstPoint.x;
                var maxX = firstPoint.x;
                var minY = firstPoint.y;
                var maxY = firstPoint.y;
                foreach (var p in points)
                {
                    if (p.x < minX)
                    {
                        minX = p.x;
                    }

                    if (p.x > maxX)
                    {
                        maxX = p.x;
                    }

                    if (p.y < minY)
                    {
                        minY = p.y;
                    }

                    if (p.y > maxY)
                    {
                        maxY = p.y;
                    }
                }

                var fx = 1 - (maxX - minX) / Screen.width;
                var fy = 1 - (maxY - minY) / Screen.height;
                if (fx < 0 || fy < 0)
                {
                    zoomDirection = Mathf.Min(fx, fy);
                }
                else
                {
                    zoomDirection = Mathf.Max(fx, fy);
                }

                var cameraTransform = _thisCamera.transform;
                cameraTransform.position += cameraTransform.forward * zoomDirection * _zoomSpeed;
                yield return null;
            } 
            while (Mathf.Abs(zoomDirection) > _accuracity);
        }
    }
}