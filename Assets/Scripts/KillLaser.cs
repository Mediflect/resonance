using UnityEngine;

public class KillLaser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("kill");
        Respawnable respawnable = other.gameObject.GetComponent<Respawnable>();
        if (respawnable != null)
        {
            respawnable.Kill();
        }
    }
}
