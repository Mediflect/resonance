using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent PlayerInteracted;
    public event System.Action<Player> PlayerInteractedNative;

    public bool isUsableDuringCyclePause = false;
    public string usePrompt = "interact";

    public void Use(Player player)
    {
        if (!enabled)
        {
            return;
        }

        if (App.Cycle.IsPaused && !isUsableDuringCyclePause)
        {
            return;
        }

        PlayerInteracted.Invoke();
        PlayerInteractedNative?.Invoke(player);
    }
}
