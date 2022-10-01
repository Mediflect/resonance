using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent PlayerInteracted;

    public void Use()
    {
        PlayerInteracted.Invoke();
    }
}
