using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;

public class TimestopPickup : MonoBehaviour
{
    public Transform geoTransform;
    public Interactable interactable;
    public RespawnPoint respawn;
    public LaserGroup lasers;
    public float shakeTransitionTime = 2f;
    public float shakeMaxValue = 0.2f;
    public float holdTime = 2f;
    public float outTransitionTime = 2f;
    public ToastHint tutorialHint;

    private void Awake()
    {
        interactable.PlayerInteractedNative += OnInteract;
    }

    private void OnInteract(Player player)
    {
        interactable.enabled = false;
        player.SetSpawnPoint(respawn);
        StartCoroutine(RunAcquireSequence(player));
    }

    private IEnumerator RunAcquireSequence(Player player)
    {
        // Kick off post process transition
        StartCoroutine(Helpers.RunVolumeWeightTransition(App.PreTimestopEffectVolume, shakeTransitionTime, 0, 1));

        // Shake transition
        float timer = 0f;
        Vector3 startPos = transform.position;

        while (timer < shakeTransitionTime)
        {
            float tValue = Mathf.InverseLerp(0, shakeTransitionTime, timer);
            tValue = Mathf.SmoothStep(0, 1, tValue);
            float shakeAmplitude = Mathf.Lerp(0, shakeMaxValue, tValue);
            geoTransform.localPosition = new Vector3(Random.value * shakeAmplitude, Random.value * shakeAmplitude, Random.value * shakeAmplitude);
            transform.position = Vector3.Lerp(startPos, player.headCamera.transform.position, tValue);
            yield return null;
            timer += Time.deltaTime;
        }

        geoTransform.gameObject.SetActive(false);

        App.Cycle.Pause();
        player.controls.enabled = false;

        yield return YieldInstructionCache.WaitForSeconds(holdTime);

        player.controls.enabled = true;
        lasers.SetLasersActive(true);
        App.Cycle.Resume();

        yield return Helpers.RunVolumeWeightTransition(App.PreTimestopEffectVolume, outTransitionTime, 1, 0);

        player.timestopPower.isUnlocked = true;
        player.toastHintSystem.QueueHint(tutorialHint);
    }
}
