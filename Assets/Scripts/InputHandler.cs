using UnityEngine;
using UnityEngine.Events;

public abstract class InputHandler : MonoBehaviour
{
    [SerializeField] protected string handleTag = "Handle";
    [SerializeField] public UnityEvent onHandleClicked;
    [SerializeField] public UnityEvent onHandleReleased;
    [SerializeField] public UnityEvent onHandleDrag;

    protected abstract void OnHandleDown();

}
