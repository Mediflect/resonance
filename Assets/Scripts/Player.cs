using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FPSControls controls;
    public Respawnable respawnable;
    public CameraEffects cameraEffects;

    private void Awake()
    {
        respawnable.Killed += OnKilled;
        respawnable.Respawned += OnRespawned;
    }

    private void OnKilled()
    {
        controls.enabled = false;
        cameraEffects.SetDeathEffectActive(true);
    }

    private void OnRespawned()
    {
        // This aligns the camera to the spawn point as well
        controls.CameraTransform.rotation = respawnable.currentRespawn.spawnTransform.rotation;
        cameraEffects.SetDeathEffectActive(false);
        StartCoroutine(RunReenableControls());
    }

    private IEnumerator RunReenableControls()
    {
        yield return null;
        controls.enabled = true;
    }
}
