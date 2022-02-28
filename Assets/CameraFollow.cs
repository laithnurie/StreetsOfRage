using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector3 minVal, maxVal;
    [Range(1,10)]
    public float smoothFactor;

    // Update is called once per frame
    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPosition = target.position + offset;

        Vector3 boundsPosition = new Vector3(Mathf.Clamp(targetPosition.x, minVal.x, maxVal.x),
            Mathf.Clamp(targetPosition.y, minVal.y, maxVal.y), Mathf.Clamp(targetPosition.z, minVal.z, maxVal.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundsPosition, smoothFactor * Time.deltaTime);
        transform.position = smoothPosition;
    }
}