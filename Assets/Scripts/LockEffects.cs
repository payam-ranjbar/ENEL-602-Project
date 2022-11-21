using DG.Tweening;
using UnityEngine;

public class LockEffects : MonoBehaviour
{
        [SerializeField] private Transform lockTransform;
        [SerializeField] private float subtleShakeDuration = 1f;
        [SerializeField] private float subtleShakeStrength = 1f;
        [SerializeField] private int subtleShakeVibrations = 1;

        public void LockSubtleShake(float intensity)
        {
                var shakeAmplitude = intensity * subtleShakeStrength;
                lockTransform.DOShakePosition(subtleShakeDuration, shakeAmplitude, subtleShakeVibrations);
                Debug.Log($"intensity: {intensity}");
        }
}