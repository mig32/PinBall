using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LazyBalls
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera thisCamera;
        [SerializeField] private float aspectRatioToFit = 0.5f;
        [SerializeField] private float zoomOutKoeff = 0.5f;

        private void Start()
        {
            if (thisCamera.aspect < aspectRatioToFit)
            {
                var ratio = (aspectRatioToFit / thisCamera.aspect) - 1;
                var cameraTransform = thisCamera.transform;
                cameraTransform.position -= zoomOutKoeff * ratio * cameraTransform.forward;
            }
        }
    }
}