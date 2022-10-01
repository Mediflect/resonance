using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
    public UnityEvent TurnedOn;
    public UnityEvent TurnedOff;

    public bool isOn = true;
    public float handleTransitionTime = 0.5f;
    public Transform handleObj;
    public Transform handleOnTarget;
    public Transform handleOffTarget;
    public GameObject onLight;
    public GameObject offLight;

    private Coroutine toggleCoroutine = null;

    public void Toggle()
    {
        if (toggleCoroutine != null)
        {
            return;
        }
        
        toggleCoroutine = StartCoroutine(RunToggle());
    }

    private void Awake()
    {
        handleObj.position = handleOnTarget.position;
        SetLight();
    }

    private void SetLight()
    {
        onLight.SetActive(isOn);
        offLight.SetActive(!isOn);
    }

    private IEnumerator RunToggle()
    {
        isOn = !isOn;
        SetLight();
        if (isOn)
        {
            TurnedOn.Invoke();
            yield return Helpers.RunSmoothMoveTo(handleObj, handleOffTarget, handleOnTarget, handleTransitionTime);
        }
        else
        {
            TurnedOff.Invoke();
            yield return Helpers.RunSmoothMoveTo(handleObj, handleOnTarget, handleOffTarget, handleTransitionTime);
        }

        toggleCoroutine = null;
    }
}
