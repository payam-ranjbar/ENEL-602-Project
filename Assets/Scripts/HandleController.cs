using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class HandleController : MonoBehaviour
    {
        [SerializeField] private InputHandler inputHandler;
        
        [SerializeField] private Vector2 xMovementBounds;
        [SerializeField] private Transform handleTransform;
        [SerializeField] private Transform circleTransform;
        [SerializeField] private int framesToCalculateVelocity = 1;
        [SerializeField] private LockEvent @event;
        public float Velocity { get; private set; }
        private float HandleX => handleTransform.position.x;

        public bool UpdateVelocity { get; private set;}
        private void Start()
        {
            StartCoroutine(GetVelocity());
            var initPos = (xMovementBounds.x + xMovementBounds.y) / 2;
            handleTransform.position = new Vector3(initPos, handleTransform.position.y, handleTransform.position.z);
            RotateCircle();
        }

        
        private void OnEnable()
        {
            inputHandler.onHandleClicked.AddListener(SetUpdateVelocity);
            inputHandler.onHandleReleased.AddListener(SetNotUpdateVelocity);
        }

        private void OnDisable()
        {
            inputHandler.onHandleClicked.RemoveListener(SetUpdateVelocity);
            inputHandler.onHandleReleased.RemoveListener(SetNotUpdateVelocity);
        }

        private void SetUpdateVelocity() => UpdateVelocity = true;
        private void SetNotUpdateVelocity() => UpdateVelocity = false;

        private IEnumerator GetVelocity()
        {
            while (true)
            {

                if (!UpdateVelocity)
                {
                    yield return null;
                    continue;
                }
                
                var x = HandleX;
                var t1 = Time.time;
                
                for (var i = 0; i < framesToCalculateVelocity; i++)
                {
                    yield return null;
                }

                var t2 = Time.time;
                var deltaX = HandleX - x;
                var deltaT = t2 - t1;
                Velocity = deltaX / deltaT;
                
                Debug.Log($"Velocity is: {Velocity}");
                
                RotateCircle();
            }

        }
        private float GetPercentage()
        {
            var minBound = xMovementBounds.x;
            var maxBound = xMovementBounds.y;
            var tValue = Mathf.InverseLerp(minBound, maxBound, HandleX);
            return tValue;
        }

        private void RotateCircle()
        {
            var rotation = circleTransform.rotation;
            var minRotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
            var maxRotation = Quaternion.Euler(rotation.x, rotation.y, 360f);
            circleTransform.rotation = Quaternion.Slerp(minRotation, maxRotation, GetPercentage());
        }
    }
}