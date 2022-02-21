using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundShadow : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private BodyController bodyController;
    public void Jump()
    {
        // disable movement in the Y Axis when someone jumps ?
    }

    private void Update()
    {
        var bodyPosition = bodyController.gameObject.transform.position;
        gameObject.transform.position = new Vector3(bodyPosition.x, bodyPosition.y - 0.25f);
    }
}
