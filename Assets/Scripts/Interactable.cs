using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public event Action PlayerUsed;

    public void Use()
    {
        PlayerUsed?.Invoke();
    }
}
