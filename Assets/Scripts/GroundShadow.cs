using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundShadow : MonoBehaviour
{
    [SerializeField] private BodyController bodyController;

    private bool _lockYAxis = false;
    private float _lastYPosition;
    private float _offset;

    private void Start()
    {
        _offset = bodyController.transform.position.y - transform.position.y;
    }

    public void MoveShadow(Vector3 bodyPosition)
    {
        var yPosition = _lockYAxis ? _lastYPosition : bodyPosition.y;
        transform.position = new Vector3(bodyPosition.x, yPosition - _offset);
    }

    public void Jump()
    {
        _lockYAxis = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        UpdateGroundStatus(col, true, false);
        Debug.Log("OnCollisionEnter2D");
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        UpdateGroundStatus(col, false, true);
        Debug.Log("OnCollisionExit2D");
    }

    private void UpdateGroundStatus(Collision2D col, bool isGround, bool enabledGravity)
    {
        if (!(col.gameObject.GetComponent(typeof(BodyController)) is BodyController)) return;
        var collidedBodyController = col.gameObject.GetComponent<BodyController>();
        if (collidedBodyController != bodyController) return;
        if (!isGround)
        {
            _lastYPosition = col.gameObject.transform.position.y;    
        }

        _lockYAxis = !isGround;
        bodyController.UpdateGravityScale(enabledGravity);
        
        // bodyController.UpdateIsGround(isGround);
    }
}
