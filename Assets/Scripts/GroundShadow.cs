using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundShadow : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private BodyController bodyController;

    private bool _lockYAxis = false;
    private float _lastYPosition;
    private float _offset = 0.25f;

    public void MoveShadow(Vector3 newVelocity)
    {
        var yPosition = _lockYAxis ? _lastYPosition : newVelocity.y;
        gameObject.transform.position = new Vector3(newVelocity.x, yPosition - _offset);
        if (!_lockYAxis)
        {
            _lastYPosition = newVelocity.y;
        }
    }

    public void Jump()
    {
        _lockYAxis = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        UpdateGroundStatus(col, true);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        UpdateGroundStatus(col, false);
    }

    private void UpdateGroundStatus(Collider2D col, bool isGround)
    {
        if (!(col.gameObject.GetComponent(typeof(BodyController)) is BodyController)) return;
        var collidedBodyController = col.gameObject.GetComponent<BodyController>();
        if (collidedBodyController != bodyController) return;
        bodyController.UpdateIsGround(isGround);
        _lockYAxis = !isGround;
    }
}
