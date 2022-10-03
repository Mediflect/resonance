using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;
using TMPro;

public class CreditsScreen : MonoBehaviour
{
    public AudioSource ambienceAudio;
    public TextMeshProUGUI cycleText;
    public float visualFadeInTime = 10f;
    public float audioStartVolume = 0f;
    public float audioFadeInTime = 5f;

    private void Awake()
    {
        App.Request(() => StartCoroutine(RunCredits()));
    }

    private IEnumerator RunCredits()
    {
        cycleText.SetText($"the bell rang {App.Cycle.totalCycles} times before the king was put to rest");
        App.PreTimestopEffectVolume.weight = 1f;
        float ambienceTargetVolume = ambienceAudio.volume;

        ambienceAudio.Play();
        StartCoroutine(Helpers.RunAudioFade(ambienceAudio, audioFadeInTime, audioStartVolume, ambienceTargetVolume));
        yield return Helpers.RunVolumeWeightTransition(App.PreTimestopEffectVolume, visualFadeInTime, 1, 0, smooth: false);
    }
}
