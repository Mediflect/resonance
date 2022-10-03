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
        baseRotation = transform.rotation;
        switch (bobAxis)
        {
            case Axis.x:
                rotationAxis = transform.right;
                break;
            case Axis.y:
                rotationAxis = transform.up;
                break;
            case Axis.z:
                rotationAxis = transform.forward;
                break;
        }
    }

    private void OnEnable()
    {
        if (pulse == null)
        {
            Debug.LogWarning("No pulse is assigned");
            enabled = false;
        }
    }

    private void OnDisable()
    {
        transform.rotation = baseRotation;
    }

    private void Update()
    {
        transform.rotation = baseRotation * Quaternion.AngleAxis(pulse.Value, rotationAxis);
    }
}
