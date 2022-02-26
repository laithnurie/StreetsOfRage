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
        var currentDistance = bodyController.transform.position.y - transform.position.y;
        
        if (currentDistance <= _offset)
        {
            _lockYAxis = false;
            bodyController.ResetPositionRelativeToShadow();
        }
        else
        {
            _lockYAxis = true;
        }
    }

    public float Offset => _offset;
}
