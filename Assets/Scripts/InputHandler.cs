using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private string handleTag = "Handle";
    [SerializeField] public UnityEvent onHandleClicked;
    [SerializeField] public UnityEvent onHandleReleased;

    private bool _objectHolding;
    private int _mouseDir;
    public float MouseWorldX => Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

    private float _xOnClick;
    public int MouseDir => _mouseDir;

    private void Start()
    {
        StartCoroutine(MouseDirection());
    }

    private void Update()
    {
        ReadClick();
    }

    private void ReadClick()
    {

        ReadMouseDown();
        ReadMouseUp();
    }

    public float GetOffset(Vector3 position)
    {
        var screenPoint = Camera.main.WorldToScreenPoint(Vector3.positiveInfinity);

        var delta = position -
                    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, screenPoint.y,
                        screenPoint.z));

        var offset = position.x - delta.x;
        return offset;
    }
    void ReadMouseDown()
    {
        var mouseDown = Input.GetMouseButtonDown(0);

        if (!mouseDown) return;

        _xOnClick = Input.mousePosition.x;
        if (!ClickHitsSomething(out var hitInfo)) return;
        
        _objectHolding = CheckHandleClicked(hitInfo);
        if(_objectHolding)
         onHandleClicked?.Invoke();
    }

    IEnumerator MouseDirection()
    {
        while (true)
        {
            if (!_objectHolding)
            {
                yield return null;
                continue;
            }
            var x1 = Input.mousePosition.x;
            yield return null;
            var delta = Input.mousePosition.x - x1;

            _mouseDir = Math.Sign(delta);
        }

    }
    void ReadMouseUp()
    {
        if (!_objectHolding) return;
        
        var mouseUp = Input.GetMouseButtonUp(0);

        if (!mouseUp) return;


        _objectHolding = false;
        onHandleReleased?.Invoke();
    }
    private bool ClickHitsSomething(out RaycastHit hitInfo)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = Physics.Raycast(ray, out hitInfo);

        return hit;
        
    }

    private bool CheckHandleClicked(RaycastHit hitInfo)
    {
        return hitInfo.collider.CompareTag(handleTag);
    }

    private void ReadMovement()
    {
        
    }
}
