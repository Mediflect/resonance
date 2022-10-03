using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctumLock : MonoBehaviour
{
    private const string EMISSION_PROP = "_EmissionColor";

    public event System.Action ActivatedStateChanged;

    public Material lockMaterial;
    [ColorUsage(true, true)]
    public Color activatedColor;
    [ColorUsage(true, true)]
    public Color inactivatedColor;

    public bool isActivated { get; private set; } = false;

    private void Awake()
    {
        isActivated = false;
        lockMaterial.SetColor(EMISSION_PROP, inactivatedColor);
    }

    private void OnDestroy()
    {
        lockMaterial.SetColor(EMISSION_PROP, inactivatedColor);
    }

    public void SetActivated(bool activated)
    {
        if (activated == isActivated)
        {
            return;
        }

        isActivated = activated;
        ActivatedStateChanged?.Invoke();
        lockMaterial.SetColor(EMISSION_PROP, isActivated ? activatedColor : inactivatedColor);
    }
}
