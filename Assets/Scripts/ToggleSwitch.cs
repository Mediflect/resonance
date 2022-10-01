using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour
{
    public Interactable interactable;
    public GameObject toggleObject;

    private void Awake()
    {
        interactable.PlayerUsed += OnToggled;
    }

    private void OnToggled()
    {
        toggleObject.SetActive(!toggleObject.activeSelf);
    }
}
