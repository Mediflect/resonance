using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAvoidTrigger : MonoBehaviour
{
    public King king;
    public Transform targetKingTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (!king.isAvoidingPlayer)
        {
            return;
        }
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            king.MoveToPosition(targetKingTransform);
        }
    }
}
