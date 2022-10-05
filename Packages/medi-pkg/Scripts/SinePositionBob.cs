using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Medi
{
    public class SinePositionBob : MonoBehaviour
    {
        private static VerboseLogger Verbose = VerboseLogger.Get(nameof(SinePositionBob));

        [SerializeField]
        private SinePulse pulse;

        [SerializeField]
        private Axis bobAxis = Axis.y;

        private float baseLocalPos;

        private void Awake()
        {
            switch (bobAxis)
            {
                case Axis.x:
                    baseLocalPos = transform.localPosition.x;
                    break;
                case Axis.y:
                    baseLocalPos = transform.localPosition.y;
                    break;
                case Axis.z:
                    baseLocalPos = transform.localPosition.z;
                    break;
            }
        }

        private void OnEnable()
        {
            if (pulse == null)
            {
                Verbose.LogWarning("No pulse is assigned");
                enabled = false;
            }
        }

        private void Update()
        {
            switch (bobAxis)
            {
                case Axis.x:
                    transform.localPosition = transform.localPosition.WithX(baseLocalPos + pulse.Value);
                    break;
                case Axis.y:
                    transform.localPosition = transform.localPosition.WithY(baseLocalPos + pulse.Value);
                    break;
                case Axis.z:
                    transform.localPosition = transform.localPosition.WithZ(baseLocalPos + pulse.Value);
                    break;
            }

        }
    }
}