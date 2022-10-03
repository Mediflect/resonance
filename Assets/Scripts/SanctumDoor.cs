using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctumDoor : MonoBehaviour
{
    public OpenableDoor door;
    public List<SanctumLock> locks;

    private void Awake()
    {
        App.Request(OnAppExists);
    }

    private void OnAppExists()
    {
        door.Close();
        foreach (var sLock in locks)
        {
            sLock.ActivatedStateChanged += OnLockChanged;
        }
    }

    private void OnLockChanged()
    {
        foreach (var sLock in locks)
        {
            if (sLock.isActivated == false)
            {
                if (door.isOpen)
                {
                    door.Close();
                }
                return;
            }
        }

        door.Open();
    }
}
