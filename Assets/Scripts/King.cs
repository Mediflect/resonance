using UnityEngine;
using Medi;

public class King : MonoBehaviour
{
    public bool isTitleScreenVersion = false;
    public Bell bell;

    private void Awake()
    {
        if (isTitleScreenVersion)
        {
            enabled = false;
            return;
        }

        App.Request(OnAppExists);
    }

    private void OnAppExists()
    {
        App.Cycle.CycleStarted += OnCycleStarted;
    }

    private void Update()
    {
        transform.forward = transform.position.DirTo(bell.lookAtTransform.position);
    }

    private void OnCycleStarted()
    {
        Debug.Log("strike");
        bell.StrikeFromPoint(transform.position);
        // shoot a laser or something idk
    }
}
