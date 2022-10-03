using UnityEngine;
using Medi;
using System.Collections;
using System.Collections.Generic;

public class King : MonoBehaviour
{
    private const string EMISSION_PROP = "_EmissionColor";

    public Bell bell;
    public Transform shakeTransform;
    public List<Rigidbody> rigidbodies;
    public float damageEffectDuration = 1f;
    public float damageShakeAmplitude = 0.5f;
    public float deathEffectDuration = 5f;
    public float deathShakeAmplitude = 1f;
    public float deathUpVelocity = 1f;
    public float deathVelocityVariance = 0.05f;
    public float deathTorque = 5f;
    public float fallDuration = 5f;

    [Header("Phasing")]
    public Transform moveSlerpOrigin;
    public float moveSpeed = 2.5f;
    public Transform phase2KingPos;


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
    private Coroutine moveCoroutine;

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

    public void MoveToPosition(Transform kingPos)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(RunMove(kingPos.position));
    }

    [ContextMenu("TestMove")]
    private void TestMove()
    {
        MoveToPosition(phase2KingPos);
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
        App.Cycle.Stop();
        deathSound.Play();
        yield return Helpers.RunDecayingPositionNoise(shakeTransform, deathEffectDuration, deathShakeAmplitude, reverse: true);
        lightMaterial.SetColor("_EmissionColor", deadColor);
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
            float deathSpeed = Random.Range(deathUpVelocity - deathVelocityVariance, deathUpVelocity + deathVelocityVariance);
            rb.AddForce(Vector3.up * deathSpeed, ForceMode.VelocityChange);
            rb.AddTorque(Random.insideUnitSphere * deathTorque, ForceMode.VelocityChange);
        }
        yield return YieldInstructionCache.WaitForSeconds(fallDuration);
        Debug.Log("king has died");
    }

    private IEnumerator RunMove(Vector3 targetPos)
    {
        Vector3 startPos = transform.position;
        Vector3 slerpStart = startPos - moveSlerpOrigin.position;
        Vector3 slerpEnd = targetPos - moveSlerpOrigin.position;

        const int SLERP_STEPS = 30;

        for (int i = 0; i <= SLERP_STEPS; ++i)
        {
            float progress = (float)i / (float)SLERP_STEPS;
            Vector3 currentMoveTarget;
            if (i == SLERP_STEPS)
            {
                // i had these clauses swapped for like 30 minutes because i'm an idiot
                currentMoveTarget = targetPos;
            }
            else
            {
                currentMoveTarget = moveSlerpOrigin.position + Vector3.Slerp(slerpStart, slerpEnd, progress);
            }

            while (transform.position != currentMoveTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentMoveTarget, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        moveCoroutine = null;
    }
}
