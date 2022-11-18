using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class LockEvent
{
    [SerializeField] private UnityEvent @event;

    [SerializeField] public float percentageToActive;

    [SerializeField] public float requiredVelocity;


    public void Trigger()
    {
        @event?.Invoke();
    }


    public void CheckAndTrigger(float currentPercent, float currentVelocity, float error = 0.001f)
    {
        var velocityOK = GetPercentageEffect(currentPercent) < error;
        var percentageOk = GetVelocityEffect(currentVelocity) < error;
            
        if(velocityOK && percentageOk) Trigger();
    }

    public float GetPercentageEffect(float percentage) => Mathf.Abs(percentageToActive - percentage);
    public float GetVelocityEffect(float v) => Mathf.Abs(requiredVelocity - v);
}