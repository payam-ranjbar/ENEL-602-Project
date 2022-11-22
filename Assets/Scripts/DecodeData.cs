using System;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class DecodeData
{

    [SerializeField] public float percentageToActive;

    [SerializeField] public float requiredVelocity;

    
    public float GetPercentageEffect(float percentage) => Mathf.Abs(percentageToActive - percentage);
    public float GetVelocityEffect(float v) => Mathf.Abs(requiredVelocity - v);
}