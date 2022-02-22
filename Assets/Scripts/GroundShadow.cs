using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundShadow : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private BodyController bodyController;

    private bool _lockYAxis = false;
    private float _lastYAxis;
    private float _offset;

    private void Start()
    {
        _offset = bodyController.transform.position.y - this.transform.position.y;
    }

    private void Update()
    {
        var bodyPosition = bodyController.gameObject.transform.position;
        var yPosition = _lockYAxis ? _lastYAxis : bodyPosition.y;
        gameObject.transform.position = new Vector3(bodyPosition.x, yPosition - _offset);
    }

    public void Jump()
    {
        _lockYAxis = true;
        _lastYAxis = bodyController.transform.position.y;
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
