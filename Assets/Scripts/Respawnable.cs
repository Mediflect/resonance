using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour
{
    public event Action Killed = null;
    public event Action Respawned = null;
    public bool isDead { get; private set; } = false;

    public RespawnPoint currentRespawn = null;

    [ContextMenu("Kill")]
    public void Kill()
    {
        Killed?.Invoke();
        isDead = true;
    }

    private void Awake()
    {
        App.Request(OnAppExists);
    }

    private void OnAppExists()
    {
        App.Cycle.CycleStarted += OnCycle;
    }

    private void OnCycle()
    {
        if (isDead)
        {
            Respawn();
        }
    }

    [ContextMenu("Respawn")]
    private void Respawn()
    {
        if (currentRespawn == null)
        {
            Debug.LogError("Respawnable has no respawn point -- cannot respawn!!");
            return;
        }

        transform.position = currentRespawn.spawnTransform.position;
        transform.rotation = currentRespawn.spawnTransform.rotation;
        Respawned?.Invoke();
        isDead = false;
    }
}
