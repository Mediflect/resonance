using UnityEngine;
using Medi;
using System.Collections;

public class King : MonoBehaviour
{
    private const string EMISSION_PROP = "_EmissionColor";

    public Bell bell;
    public Transform shakeTransform;
    public float damageEffectDuration = 1f;
    public float damageShakeAmplitude = 0.5f;
    public float deathEffectDuration = 5f;
    public float deathShakeAmplitude = 1f;

    [Header("Lights")]
    public Material lightMaterial;
    [ColorUsage(true, true)]
    public Color healthyLightColor;
    [ColorUsage(true, true)]
    public Color woundedLightColor;
    [ColorUsage(true, true)]
    public Color aboutToDieLightColor;
    [ColorUsage(true, true)]
    public Color deadColor;

    [Header("Sounds")]
    public AudioSource damageSound;
    public AudioSource deathSound;

    private int hitsLeftToKill = 3;

    [ContextMenu("Damage")]
    public void TakeDamage()
    {
        --hitsLeftToKill;
        if (hitsLeftToKill == 2)
        {
            lightMaterial.SetColor(EMISSION_PROP, woundedLightColor);
            StartCoroutine(RunHurt());
        }
        else if (hitsLeftToKill == 1)
        {
            lightMaterial.SetColor(EMISSION_PROP, aboutToDieLightColor);
            StartCoroutine(RunHurt());
        }
        else
        {
            StartCoroutine(RunDeath());
        }
    }

    private void Awake()
    {
        App.Request(OnAppExists);
        lightMaterial.SetColor(EMISSION_PROP, healthyLightColor);
    }

    private void OnDestroy()
    {
        lightMaterial.SetColor(EMISSION_PROP, healthyLightColor);
    }

    private void OnAppExists()
    {
        App.Cycle.CycleStarted += OnCycleStarted;
    }

    private void Update()
    {
        if (bell == null)
        {
            return;
        }
        transform.forward = transform.position.DirTo(bell.lookAtTransform.position);
    }

    private void OnCycleStarted()
    {
        Debug.Log("strike");
        bell.StrikeFromPoint(transform.position);
        // shoot a laser or something idk
    }

    private IEnumerator RunHurt()
    {
        damageSound.Play();
        yield return Helpers.RunDecayingPositionNoise(shakeTransform, damageEffectDuration, damageShakeAmplitude);
    }

    private IEnumerator RunDeath()
    {
        deathSound.Play();
        yield return Helpers.RunDecayingPositionNoise(shakeTransform, deathEffectDuration, deathShakeAmplitude, reverse: true);
        lightMaterial.SetColor("_EmissionColor", deadColor);
    }

    [ContextMenu("Heal")]
    private void DebugResetHealth()
    {
        hitsLeftToKill = 3;
        lightMaterial.SetColor(EMISSION_PROP, healthyLightColor);
    }
}
