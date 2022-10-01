using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FPSControls controls;
    public Respawnable respawnable;
    public CameraEffects cameraEffects;
    public InteractRaycaster interactRaycaster;

    private void Awake()
    {
        controls.InteractPressed += OnInteract;
        respawnable.Killed += OnKilled;
        respawnable.Respawned += OnRespawned;
        interactRaycaster.InteractableFound += OnInteractableFound;
        interactRaycaster.InteractableLost += OnInteractableLost;
    }

    private void OnInteract()
    {
        if (interactRaycaster.CurrentInteractable != null)
        {
            interactRaycaster.CurrentInteractable.Use();
        }
    }

    private void OnKilled()
    {
        // disable everything
        controls.enabled = false;
        interactRaycaster.enabled = false;
        cameraEffects.SetDeathEffectActive(true);
    }

    private void OnRespawned()
    {
        StartCoroutine(RunRespawn());
    }

    private IEnumerator RunRespawn()
    {
        // This aligns the camera with the spawn point so the player is facing the right way
        controls.CameraTransform.rotation = respawnable.currentRespawn.spawnTransform.rotation;

        // Wait a frame so the character cotnroller doesn't do anything funny
        yield return null;

        // re-enable everything
        controls.enabled = true;
        interactRaycaster.enabled = true;
        cameraEffects.SetDeathEffectActive(false);
    }

    private void OnInteractableFound()
    {
        cameraEffects.SetInteractEffectActive(true);
    }

    private void OnInteractableLost()
    {
        cameraEffects.SetInteractEffectActive(false);
    }
}
