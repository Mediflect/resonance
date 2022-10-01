using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public KillLaser killLaser;
    
    public void SetLaserActive(bool active)
    {
        killLaser.gameObject.SetActive(active);
    }
}
