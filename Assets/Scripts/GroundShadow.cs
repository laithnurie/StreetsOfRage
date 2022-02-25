using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundShadow : MonoBehaviour
{
    [SerializeField] private BodyController bodyController;
    private Collider2D collider2D;

    private bool _lockYAxis = false;
    private float _lastYPosition;
    private float _offset;

    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
        _offset = bodyController.transform.position.y - transform.position.y;
    }

    public void MoveShadow(Vector3 bodyPosition)
    {
        var yPosition = _lockYAxis ? _lastYPosition : bodyPosition.y;
        if (bodyPosition.y < yPosition)
        {
            yPosition = bodyPosition.y - _offset;
        }
        transform.position = new Vector3(bodyPosition.x, yPosition - _offset);
    }

    public void Jump()
    {
        StartCoroutine(EnableColliderDelay());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _lockYAxis = false;
        collider2D.enabled = false;
        UpdateGroundStatus(col, true);
        Debug.Log("OnCollisionEnter2D");
    }

    private void UpdateGroundStatus(Collision2D col, bool isGround)
    {
        if (!(col.gameObject.GetComponent(typeof(BodyController)) is BodyController)) return;
        var collidedBodyController = col.gameObject.GetComponent<BodyController>();
        if (collidedBodyController != bodyController) return;
        if (!isGround)
        {
            _lastYPosition = col.gameObject.transform.position.y;    
        }

        bodyController.DisableGravity();
        bodyController.UpdateIsGround(isGround);
    }

    private IEnumerator EnableColliderDelay()
    {
        _lockYAxis = true;
        _lastYPosition = bodyController.transform.position.y;
        bodyController.UpdateIsGround(false);
        yield return new WaitForSeconds(0.5f);
        collider2D.enabled = true;
    }
}
