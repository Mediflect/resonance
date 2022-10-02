using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent PlayerInteracted;
    public event System.Action<Player> PlayerInteractedNative;

    public void Use(Player player)
    {
        PlayerInteracted.Invoke();
        PlayerInteractedNative?.Invoke(player);
    }
}
