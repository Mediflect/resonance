using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent PlayerInteracted;
    public event System.Action<Player> PlayerInteractedNative;

    public bool isUsableDuringCyclePause = false;

    public void Use(Player player)
    {
        if (App.Cycle.IsPaused && !isUsableDuringCyclePause)
        {
            return;
        }

        PlayerInteracted.Invoke();
        PlayerInteractedNative?.Invoke(player);
    }
}
