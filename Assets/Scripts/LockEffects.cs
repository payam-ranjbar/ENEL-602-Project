using DG.Tweening;
using UnityEngine;

public class LockEffects : MonoBehaviour
{
        [SerializeField] private Transform lockTransform;
        [SerializeField] private float subtleShakeDuration = 1f;
        [SerializeField] private float subtleShakeStrength = 1f;
        [SerializeField] private int subtleShakeVibrations = 1;

        [Header("Rotation Shake")]
        [SerializeField] private float rotationShakeDuration = 1f;
        [SerializeField] private float rotationShakeStrength = 8f;
        [SerializeField] private int rotationShakeVibrations = 1;
        [Header("Camera Shake")]
        [SerializeField] private float camShakeDuration = 1f;
        [SerializeField] private float camShakeStrength = 1f;
        [SerializeField] private int camShakeVibrations = 1;

        [Header("Camera Motion")] 
        [SerializeField] private float camZoomFov = 57;
        [SerializeField] private float camZoomDuration = 0.5f;
        [SerializeField] private float camZoomBackDuration = 1f;
        [SerializeField] private float camDefaultFov = 62;

        private bool _rotating;
        public void LockSubtleShake(float intensity)
        {
                var shakeAmplitude = intensity * subtleShakeStrength;
                lockTransform.DOShakePosition(subtleShakeDuration, shakeAmplitude, subtleShakeVibrations);
        }

        public void LockRotationShake()
        {
                if(_rotating ) return;
                _rotating = true;
                lockTransform.DOPunchRotation(rotationShakeStrength * Vector3.one, rotationShakeDuration,
                                rotationShakeVibrations)
                        .onComplete += () => _rotating = false;
        }
        
        public void CameraShake()=> Camera.main.DOShakePosition(camShakeDuration, camShakeStrength, camShakeVibrations);

        public void CameraZoom()
        {
                var cam = Camera.main;
                var seq = DOTween.Sequence();

                seq.Append(cam.DOFieldOfView(camZoomFov, camZoomDuration))
                        .Append(cam.DOFieldOfView(camDefaultFov, camZoomBackDuration));

                seq.Play();
        }
}