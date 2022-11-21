using System;
using System.Collections;
using UnityEngine;

public class MouseHandle : InputHandler
{
    [SerializeField] private Vector2 xMovementBounds;
        
    private Vector3 _screenPoint;
    private Vector3 _offset;
    private Camera _camera;
    private bool _isMouseDown;
    private int _mouseDir;
    private float _lastXPos;
    [SerializeField] private float dragEffectThreshold = 0.1f;

    private float MinBound => xMovementBounds.x;
    private float MaxBound => xMovementBounds.y;

    private float X => transform.position.x;
    private bool MouseDirIsRight => _mouseDir >= 0;
    private bool MouseDirIsLeft => _mouseDir <= 0;

    private bool MouseIsStationary => _mouseDir == 0;
    private void Start()
    {
        _camera = Camera.main;
        StartCoroutine(CalculateMouseDir());
    }

    private void OnMouseDown()
    {
        _isMouseDown = true;
        var position = transform.position;
        _screenPoint = _camera.WorldToScreenPoint(position);
        _offset = position - _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, _screenPoint.y, _screenPoint.z));
        onHandleClicked?.Invoke();
    }

    private void OnMouseUp()
    {
        onHandleReleased?.Invoke();
        _isMouseDown = false;
    }

    private void OnMouseDrag()
    {
        var curScreenPoint = new Vector3(Input.mousePosition.x, _screenPoint.y, _screenPoint.z);
        var screenToWorld = _camera.ScreenToWorldPoint(curScreenPoint);
        var curPosition = screenToWorld + _offset;
            
        var faceLeft = curPosition.x < X;
        var faceRight = curPosition.x > X;
            
        if(faceLeft && ReachedLeftBound()) return;
        if(faceRight && ReachedRightBound()) return;
            
        transform.position = curPosition;
        // transform.position = Vector3.Lerp(transform.position, curPosition, Time.deltaTime);
    }

    IEnumerator CalculateMouseDir()
    {
        while (true)
        {
            _lastXPos = Input.mousePosition.x;
            yield return null;
            SetMouseDir();
            if (ReachedLeftBound() && ReachedRightBound()) continue;
            if (!_isMouseDown || MouseIsStationary) continue;
            yield return new WaitForSeconds(dragEffectThreshold);
            onHandleDrag?.Invoke();
        }
    }
    private void SetMouseDir()
    {
        var x = Input.mousePosition.x;
        _mouseDir = Math.Sign(x - _lastXPos);
        _lastXPos = x;
    }
        
    private bool ReachedLeftBound() => X <= MinBound;
    private bool ReachedRightBound() => X >=  MaxBound;
}