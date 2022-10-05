using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Medi
{
    public class SinePulse : MonoBehaviour
    {
        public float Value => currentValue;

        [SerializeField]
        private float period = 1f;

        [SerializeField]
        private float amplitude = 1f;

        [SerializeField]
        private bool randomizePeriod = false;

        private float timer = 0f;
        private float currentValue = 0f;

        private void Awake()
        {
            if (randomizePeriod)
            {
                timer = period * Random.value;
            }
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > period)
            {
                timer -= period;
            }
            float t = Mathf.InverseLerp(0, period, timer);
            float sinPos = Mathf.Lerp(0, Mathf.PI * 2f, t);
            currentValue = Mathf.Sin(sinPos) * amplitude;
        }
    }
}