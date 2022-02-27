using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject leftBounds;
    [SerializeField] private GameObject rightBounds;

    private float _leftBoundsize;
    private float _rightBoundsSize;

    private void Start()
    {
        _leftBoundsize = leftBounds.GetComponent<Collider2D>().bounds.size.x;
        _rightBoundsSize = rightBounds.GetComponent<Collider2D>().bounds.size.x;
    }

    private void Update()
    {
        Debug.Log("_leftBoundsize: " + _leftBoundsize);
        Debug.Log("_rightBoundsSize: " + _rightBoundsSize);
    }

    public float GetLeftBounds() => leftBounds.transform.position.x + _leftBoundsize;

    public float GetRightBounds() => rightBounds.transform.position.x - _rightBoundsSize;
}