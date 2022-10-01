using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public KillCollider killLaser;
    
    public void SetLaserActive(bool active)
    {
        killLaser.gameObject.SetActive(active);
    }
}
