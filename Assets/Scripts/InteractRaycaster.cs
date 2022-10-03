using System;
using UnityEngine;

public class InteractRaycaster : MonoBehaviour
{
    private static RaycastHit[] cachedRaycastHits = new RaycastHit[10];

    public event Action InteractableFound;
    public event Action InteractableLost;

    public Interactable CurrentInteractable { get; private set; } = null;

    public float distance = 1f;
    public LayerMask interactLayers = Physics.DefaultRaycastLayers;

    private void Update()
    {
        Interactable closestInteractable = null;
        int numHits = Physics.RaycastNonAlloc(transform.position, transform.forward, cachedRaycastHits, distance, interactLayers, QueryTriggerInteraction.Collide);
        if (numHits == 10)
        {
            Debug.LogError("reached max interactables found; turn dist down or remove some interactables lol");
        }

        if (numHits > 0)
        {
            float closestDist = float.PositiveInfinity;
            for (int i = 0; i < numHits; ++i)
            {
                Interactable interactable = cachedRaycastHits[i].collider.gameObject.GetComponent<Interactable>();
                if (interactable == null || !interactable.enabled)
                {
                    continue;
                }
                if (cachedRaycastHits[i].distance < closestDist)
                {
                    closestInteractable = interactable;
                    closestDist = cachedRaycastHits[i].distance;
                }
            }
        }
        
        if (closestInteractable != CurrentInteractable)
        {
            CurrentInteractable = closestInteractable;
            if (CurrentInteractable != null)
            {
                InteractableFound?.Invoke();
            }
            else
            {
                InteractableLost?.Invoke();
            }
        }
    }
}
