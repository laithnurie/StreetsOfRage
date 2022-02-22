using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundShadow : MonoBehaviour
{
    [SerializeField] private BodyController bodyController;

    private bool _lockYAxis = false;
    private float _lastYPosition;
    private float _offset = 0.3f;
    
    private Rigidbody2D _rigidbody2D;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void MoveShadow(Vector3 newVelocity)
    {
        var yPosition = _lockYAxis ? _lastYPosition : newVelocity.y;
        _rigidbody2D.velocity = new Vector3(newVelocity.x, yPosition - _offset);
        if (!_lockYAxis)
        {
            _lastYPosition = newVelocity.y;
        }
        Debug.Log("_lockYAxis: " +_lockYAxis);
    }

    public void Jump()
    {
        _lockYAxis = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        UpdateGroundStatus(col, true);
        StartCoroutine(LockAxisDelay(false));
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        UpdateGroundStatus(col, false);
        StartCoroutine(LockAxisDelay(true));
    }

    private IEnumerator LockAxisDelay(bool lockYAxis)
    {
        yield return new WaitForSeconds(0.5f);
        _lockYAxis = lockYAxis;
    }

    private void UpdateGroundStatus(Collision2D col, bool isGround)
    {
        if (!(col.gameObject.GetComponent(typeof(BodyController)) is BodyController)) return;
        var collidedBodyController = col.gameObject.GetComponent<BodyController>();
        if (collidedBodyController != bodyController) return;
        bodyController.UpdateIsGround(isGround);
    }
}
