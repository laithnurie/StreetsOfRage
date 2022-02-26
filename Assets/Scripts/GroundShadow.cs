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

    private void Update()
    {
        var yLocation = _lockYAxis ? _lastYPosition : bodyController.transform.position.y;
        transform.position = new Vector3(bodyController.transform.position.x, yLocation - _offset);
    }

    public void LockYAxis(bool inAir)
    {
        _lockYAxis = inAir;
        if (inAir) { _lastYPosition = bodyController.transform.position.y; }
        else
        {
            bodyController.ResetPositionRelativeToShadow(_offset);
        }
    }
}
