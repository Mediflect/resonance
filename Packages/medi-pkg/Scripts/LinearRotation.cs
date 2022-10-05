using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Medi
{
    public class LinearRotation : MonoBehaviour
    {
        [SerializeField]
        private Axis axis = Axis.y;

        [SerializeField]
        private Space space = Space.Self;

        [SerializeField]
        private bool reverse = false;

        [SerializeField]
        private float rotationsPerSecond = 0.5f;

        private void Update()
        {
            float degreesPerSecond = 360 * rotationsPerSecond;
            float degreesDelta = degreesPerSecond * Time.deltaTime;
            if (reverse)
            {
                degreesDelta *= -1f;
            }

            switch (axis)
            {
                case Axis.x:
                    transform.Rotate(Vector3.right, degreesDelta, space);
                    break;
                case Axis.y:
                    transform.Rotate(Vector3.up, degreesDelta, space);
                    break;
                case Axis.z:
                    transform.Rotate(Vector3.forward, degreesDelta, space);
                    break;
            }
        }
    }
}