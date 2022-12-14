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
    public Interactable interactable;
    public string turnOnPrompt = "power on";
    public string turnOffPrompt = "power off";

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
        App.Request(OnAppExists);
    }

    private void OnAppExists()
    {
        if (isOn)
        {
            handleObj.position = handleOffTarget.position;
        }
        else
        {
            handleObj.position = handleOffTarget.position;
        }
        interactable.usePrompt = isOn ? turnOffPrompt : turnOnPrompt;
        InvokeStateChange(playSounds: false);
    }


    private IEnumerator RunToggle()
    {
        interactable.enabled = false;
        isOn = !isOn;
        yield return RunHandleMove();
        while (App.Cycle.IsPaused)
        {
            yield return null;
        }
        InvokeStateChange(playSounds: true);
        interactable.enabled = true;
        interactable.usePrompt = isOn ? turnOffPrompt : turnOnPrompt;
        toggleCoroutine = null;
    }

    private IEnumerator RunHandleMove()
    {
        if (isOn)
        {
            yield return Helpers.RunSmoothMoveTo(handleObj, handleOffTarget, handleOnTarget, handleTransitionTime);
        }
        else
        {
            yield return Helpers.RunSmoothMoveTo(handleObj, handleOnTarget, handleOffTarget, handleTransitionTime);
        }
    }

    private void InvokeStateChange(bool playSounds)
    {
        onLight.SetActive(isOn);
        offLight.SetActive(!isOn);
        if (isOn)
        {
            if (playSounds)
            {
                onSound.Play();
            }
            TurnedOn.Invoke();

        }
        else
        {
            if (playSounds)
            {
                offSound.Play();
            }
            TurnedOff.Invoke();
        }
    }
}
