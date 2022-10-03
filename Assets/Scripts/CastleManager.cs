using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleManager : MonoBehaviour
{
    private void Awake()
    {
        App.Request(OnAppExists);
    }

    private void OnAppExists()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (!App.Cycle.gameObject.activeSelf)
        {
            App.Cycle.gameObject.SetActive(true);
        }
    }
}
