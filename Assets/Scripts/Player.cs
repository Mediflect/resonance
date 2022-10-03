using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FPSControls controls;
    public Respawnable respawnable;
    public Camera headCamera;
    public CameraEffects cameraEffects;
    public InteractRaycaster interactRaycaster;
    public PlayerSounds sounds;
    public TimestopPower timestopPower;

    public void SetSpawnPoint(RespawnPoint point, bool playSound = false)
    {
        if (respawnable.currentRespawn == point)
        {
            return;
        }

        respawnable.currentRespawn.SetActiveState(RespawnPoint.ActiveState.Inactive);
        respawnable.currentRespawn = point;
        respawnable.currentRespawn.SetActiveState(RespawnPoint.ActiveState.Player);
    }

    private void Awake()
    {
        controls.InteractPressed += OnInteract;
        controls.PowerPressed += OnPowerActivated;
        respawnable.Killed += OnKilled;
        respawnable.Respawned += OnRespawned;
        interactRaycaster.InteractableFound += OnInteractableFound;
        interactRaycaster.InteractableLost += OnInteractableLost;
    }

    private void OnInteract()
    {
        if (interactRaycaster.CurrentInteractable != null)
        {
            interactRaycaster.CurrentInteractable.Use(this);
        }
    }

    private void OnPowerActivated()
    {
        timestopPower.Activate();
    }

    private void OnKilled()
    {
        // disable everything
        controls.enabled = false;
        interactRaycaster.enabled = false;
        cameraEffects.SetDeathEffectActive(true);
        sounds.PlayDeathSound();
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
        sounds.PlayRespawnSound();
    }

    private void OnInteractableFound()
    {
        cameraEffects.SetInteractEffectActive(true, interactRaycaster.CurrentInteractable.usePrompt);
    }

    private void OnInteractableLost()
    {
        cameraEffects.SetInteractEffectActive(false);
    }
}
