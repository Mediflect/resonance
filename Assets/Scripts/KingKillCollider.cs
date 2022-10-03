using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingKillCollider : MonoBehaviour
{
    public bool canHurtKing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!canHurtKing)
        {
            return;
        }

        King king = other.gameObject.GetComponentInParent<King>();
        if (king != null)
        {
            king.TakeDamage();
            canHurtKing = false;
        }
    }

    private void OnEnable()
    {
        canHurtKing = true;
    }

    private void OnDisable()
    {
        canHurtKing = false;
    }

    private void Update()
    {
        return;
    }
}
