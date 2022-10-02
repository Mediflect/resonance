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

    [Header("Sounds")]
    public PitchRandomizer onSound;
    public PitchRandomizer offSound;

    private Coroutine toggleCoroutine = null;

    [ContextMenu("Toggle")]
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
        if (isOn)
        {
            handleObj.position =  handleOffTarget.position;
            TurnedOn.Invoke();
        }
        else
        {
            handleObj.position = handleOffTarget.position;
            TurnedOff.Invoke();
        }
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
            onSound.Play();
            TurnedOn.Invoke();
            yield return Helpers.RunSmoothMoveTo(handleObj, handleOffTarget, handleOnTarget, handleTransitionTime);
        }
        else
        {
            offSound.Play();
            TurnedOff.Invoke();
            yield return Helpers.RunSmoothMoveTo(handleObj, handleOnTarget, handleOffTarget, handleTransitionTime);
        }

        toggleCoroutine = null;
    }
}
