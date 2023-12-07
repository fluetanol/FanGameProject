using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerTrace : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform _traceObject;
    [SerializeField] private float _distance;

    // Update is called once per frame
    void Update()
    {
        Vector3 tracePosition = _traceObject.position;
        tracePosition.z = _distance;
        this.transform.position = tracePosition;

    }
}
