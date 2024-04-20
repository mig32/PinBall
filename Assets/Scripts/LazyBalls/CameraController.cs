using System;
using UnityEngine;

namespace LazyBalls
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera thisCamera;
        [SerializeField] private float aspectRatioToFit = 0.5f;
        [SerializeField] private float zoomOutKoeff = 0.5f;

        private Vector3 _startPos;
        private float _savedAspect;

        private void Start()
        {
            _startPos = thisCamera.transform.position;
        }

        private void Update()
        {
            if (Math.Abs(_savedAspect - thisCamera.aspect) < float.Epsilon)
            {
                return;
            }

            _savedAspect = thisCamera.aspect;
            if (thisCamera.aspect < aspectRatioToFit)
            {
                var ratio = (aspectRatioToFit / thisCamera.aspect) - 1;
                var cameraTransform = thisCamera.transform;
                cameraTransform.position = _startPos - zoomOutKoeff * ratio * cameraTransform.forward;
            }
        }
    }
}