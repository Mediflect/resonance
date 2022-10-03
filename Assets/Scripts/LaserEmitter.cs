using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public GameObject killLaser;
    public bool isLaserActive => killLaser.activeInHierarchy;
    
    public void SetLaserActive(bool active)
    {
        killLaser.SetActive(active);
    }
}
