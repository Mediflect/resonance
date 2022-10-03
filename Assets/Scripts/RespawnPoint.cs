using System;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{   
    public enum ActiveState
    {
        Player,
        Object,
        Inactive
    }

    public ActiveState state = ActiveState.Inactive;
    public Transform spawnTransform;
    public GameObject activePlayerLights;
    public GameObject activeObjectLights;
    public GameObject inactiveLights;
    public Interactable interactable;

    public void SetActiveState(ActiveState state)
    {
        this.state = state;
        activePlayerLights.SetActive(state == ActiveState.Player);
        activeObjectLights.SetActive(state == ActiveState.Object);
        inactiveLights.SetActive(state == ActiveState.Inactive);
        interactable.gameObject.SetActive(state == ActiveState.Inactive);
    }

    private void Awake()
    {
        SetActiveState(state);
        interactable.PlayerInteractedNative += OnInteraction;
    }

    private void OnInteraction(Player player)
    {
        player.SetSpawnPoint(this, playSound: true);
    }

    [ContextMenu("RefreshState")]
    private void DebugRefreshState()
    {
        SetActiveState(state);
    }
}
