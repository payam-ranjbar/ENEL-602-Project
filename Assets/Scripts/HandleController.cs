using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class HandleController : MonoBehaviour
    {
        [SerializeField] private InputHandler inputHandler;
        
        [SerializeField] private Vector2 xMovementBounds;
        [SerializeField] private Vector2 zRotationBounds;
        [SerializeField] private Transform handleTransform;
        [SerializeField] private Transform circleTransform;
        [SerializeField] private int framesToCalculateVelocity = 1;
        [SerializeField] private float velocitySensitivity = 0.01f;
        [SerializeField] private float positionSensitivity = 0.01f;

        [Header("Sensitivity")] [SerializeField]
        private float positionFeedbackThreshold;
        [SerializeField] private float velocityFeedbackThreshold;
        public UnityEvent<float> onVelocityFeedback;
        public UnityEvent<float> onPositionFeedback;
        [SerializeField] private LockEvent @event;
        public float Velocity { get; private set; }
        private float HandleX => handleTransform.position.x;

        public bool UpdateVelocity { get; private set;}
        private void Start()
        {
            StartCoroutine(GetVelocity());
            AdjustInitialPositionOfHandle();
            RotateCircle();
        }

        private void AdjustInitialPositionOfHandle()
        {
            var initPos = (xMovementBounds.x + xMovementBounds.y) / 2;
            var position = handleTransform.position;
            position = new Vector3(initPos, position.y, position.z);
            handleTransform.position = position;
        }

        private void Update()
        {
            if(UpdateVelocity) RotateCircle();
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

                FeedBacks();
                yield return CheckWin();
                yield return CalculateVelocity();
            }
        }

        private void FeedBacks()
        {
            var percentage = GetPercentage();
            
            var positionIntensity = Mathf.Abs(@event.percentageToActive - percentage);
            var velocityIntensity = Mathf.Abs(Mathf.Abs(Velocity) - @event.requiredVelocity);
            
            var positionFeedback = positionIntensity <= positionFeedbackThreshold;
            var velocityFeedback = velocityIntensity <= velocityFeedbackThreshold;

            if (velocityFeedback) onVelocityFeedback?.Invoke(velocityIntensity);
            if (positionFeedback) onPositionFeedback?.Invoke(positionIntensity);
        }
        private IEnumerator CheckWin()
        {
            var percentage = GetPercentage();
            var reachedPosition = Mathf.Abs(@event.percentageToActive - percentage) <= positionSensitivity;
            var reachVelocity = Mathf.Abs(Mathf.Abs(Velocity) - @event.requiredVelocity) <= velocitySensitivity;

            if (reachedPosition)
            {
                Debug.Log("Reach Position");
            }

            if (reachVelocity)
            {
                Debug.Log("reached velocity");
            }
            if (reachedPosition && reachVelocity)
            {
                @event.Trigger();
                Debug.Log("Win");
            }

            yield return null;
        }
        private IEnumerator CalculateVelocity()
        {
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
            var minZ = zRotationBounds.x;
            var maxZ = zRotationBounds.y;
            var t = GetPercentage();
            var angle = Mathf.Lerp(minZ, maxZ, t);
            var newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            circleTransform.rotation = newRotation;
        }
    }
}