using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraPlayerTrace : MonoBehaviour
{
    // Update is called once per frame
    public Transform targetPosition;
    public float smoothTime = 0.5f;
    public float cameraToTargetDistance = -8f;
    public bool isActive = false;

    private Vector3 realLocation;
    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        isActive = true;
    }

    private void Update()
    {
        realLocation = targetPosition.position;
        realLocation.y = targetPosition.position.y + 1;
        realLocation.z = cameraToTargetDistance;

        if (isActive) transform.position = Vector3.SmoothDamp(transform.position, realLocation, ref _velocity, smoothTime);

        if (Vector3.Distance(realLocation, transform.position) < 0.1f) isActive = false;
        else isActive = true;
    }

}
