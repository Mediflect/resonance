using UnityEngine;
using Medi;

public class King : MonoBehaviour
{
    public Bell bell;

    private void Awake()
    {
        App.Request(OnAppExists);
    }

    private void OnAppExists()
    {
        App.Cycle.CycleStarted += OnCycleStarted;
    }

    private void Update()
    {
        if (bell == null)
        {
            return;
        }
        transform.forward = transform.position.DirTo(bell.lookAtTransform.position);
    }

    private void OnCycleStarted()
    {
        Debug.Log("strike");
        bell.StrikeFromPoint(transform.position);
        // shoot a laser or something idk
    }
}
