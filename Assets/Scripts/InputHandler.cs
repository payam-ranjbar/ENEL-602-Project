using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InputHandler : MonoBehaviour
{
    [SerializeField] protected string handleTag = "Handle";
    [SerializeField] public UnityEvent onHandleClicked;
    [SerializeField] public UnityEvent onHandleReleased;

}
