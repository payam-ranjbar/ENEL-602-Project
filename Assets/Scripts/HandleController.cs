using System.Collections;
using Unity.VisualScripting;
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
        public UnityEvent onVelocityFeedback;
        public UnityEvent onPositionFeedback;

        [SerializeField] private GameStatus gameStatus;
        [SerializeField] private DecodeData data;


        private bool _velcoityFeedbackInvoked;
        private bool _positionFeedbackInvoked;
        private float _percentage;


        public float Velocity { get; private set; }
        private float HandleX => handleTransform.position.x;

        public bool UpdateVelocity { get; private set;}
        private void Start()
        {
            StartCoroutine(GetVelocity());
            AdjustInitialPositionOfHandle();
            RotateCircle();
            data = gameStatus.CurrentData;
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
            
            UnlockTheLock();
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
            _percentage = GetPercentage();

            var desiredX = Mathf.InverseLerp(xMovementBounds.x, xMovementBounds.y, data.percentageToActive);
            var positionIntensity = Mathf.Abs(data.percentageToActive - _percentage);
            var velocityIntensity = Mathf.Abs(Mathf.Abs(Velocity) - data.requiredVelocity);
            
            var positionFeedback = positionIntensity <= positionFeedbackThreshold;
            var velocityFeedback = velocityIntensity <= velocityFeedbackThreshold;

            if (velocityFeedback)
            {
                if (!_velcoityFeedbackInvoked)
                {
                    onVelocityFeedback?.Invoke();
                    _velcoityFeedbackInvoked = true;
                }
            }
            else
            {
                _velcoityFeedbackInvoked = false;
            }

            if (positionFeedback)
            {
                if (!_positionFeedbackInvoked)
                {
                    onPositionFeedback?.Invoke();
                    _positionFeedbackInvoked = true;
                }
            }
            else
            {
                _positionFeedbackInvoked = false;

            }
        }
        private IEnumerator CheckWin()
        {
             _percentage = GetPercentage();
            var reachedPosition = Mathf.Abs(data.percentageToActive - _percentage) <= positionSensitivity;
            var reachVelocity = Mathf.Abs(Mathf.Abs(Velocity) - data.requiredVelocity) <= velocitySensitivity;

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

        private void UnlockTheLock()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;

            if (!_positionFeedbackInvoked || !_velcoityFeedbackInvoked)
            {
                // error
                gameStatus.Error();
                return;
            }
            var newData = gameStatus.Decode();
            if (newData != null) data = newData;
            _positionFeedbackInvoked = false;
            _velcoityFeedbackInvoked = false;
            AdjustInitialPositionOfHandle();
            // UpdateVelocity = false;
            //win

        }
    }
}