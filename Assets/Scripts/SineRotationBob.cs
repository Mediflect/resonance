using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

public class SineRotationBob : MonoBehaviour
{
    [SerializeField]
    private SinePulse pulse;

    [SerializeField]
    private Axis bobAxis = Axis.y;

    private Quaternion baseRotation;
    private Vector3 rotationAxis;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        if (pulse == null)
        {
            Debug.LogWarning("No pulse is assigned");
            enabled = false;
        }

        baseRotation = transform.localRotation;
        switch (bobAxis)
        {
            case Axis.x:
                rotationAxis = Vector3.right;
                break;
            case Axis.y:
                rotationAxis = Vector3.up;
                break;
            case Axis.z:
                rotationAxis = Vector3.forward;
                break;
        }
    }

    private void OnDisable()
    {
        transform.localRotation = baseRotation;
    }

    private void Update()
    {
        transform.localRotation = baseRotation * Quaternion.AngleAxis(pulse.Value, rotationAxis);
    }
}
