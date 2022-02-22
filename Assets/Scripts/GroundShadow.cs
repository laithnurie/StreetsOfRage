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
    
    public void MoveShadow(Vector3 bodyPosition)
    {
        var yPosition = _lockYAxis ? _lastYPosition : bodyPosition.y;
        transform.position = new Vector3(bodyPosition.x, yPosition - _offset);
        if (!_lockYAxis)
        {
            _lastYPosition = bodyPosition.y;
        }
    }

    public void Jump()
    {
        _lockYAxis = true;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        UpdateGroundStatus(col, true);
        _lockYAxis = false;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        UpdateGroundStatus(col, false);
    }

    private void UpdateGroundStatus(Collision2D col, bool isGround)
    {
        if (!(col.gameObject.GetComponent(typeof(BodyController)) is BodyController)) return;
        var collidedBodyController = col.gameObject.GetComponent<BodyController>();
        if (collidedBodyController != bodyController) return;
        bodyController.UpdateIsGround(isGround);
    }
}
