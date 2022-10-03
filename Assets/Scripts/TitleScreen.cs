using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public AudioSource ambienceAudio;
    public float visualFadeInTime = 10f;
    public float audioStartVolume = .05f;
    public float audioFadeInTime = 5f;
    [Header("Subtitles")]
    public TextMeshProUGUI subtitle1;
    public TextMeshProUGUI subtitle2;
    public float subtitleFadeTime = 2f;
    public float subtitleDelay = 1f;
    [Header("exit")]
    public float exitTransitionTime = 5f;
    public string gameSceneName;
    [Header("DEBUG")]
    public bool goToGameImmediately = false;


    private bool acceptingInput = false;


    private void Awake()
    {
        if (goToGameImmediately)
        {
            SceneManager.LoadScene(gameSceneName);
            return;
        }
        App.Request(OnAppExists);
    }

    private void OnAppExists()
    {
        StartCoroutine(RunTitleSequence());
    }

    private IEnumerator RunTitleSequence()
    {
        App.PreTimestopEffectVolume.weight = 1f;
        float ambienceTargetVolume = ambienceAudio.volume;
        Helpers.SetTextAlpha(subtitle1, 0);
        Helpers.SetTextAlpha(subtitle2, 0);

        ambienceAudio.Play();
        StartCoroutine(Helpers.RunAudioFade(ambienceAudio, audioFadeInTime, audioStartVolume, ambienceTargetVolume));
        yield return Helpers.RunVolumeWeightTransition(App.PreTimestopEffectVolume, visualFadeInTime, 1, 0, smooth: false);

        yield return YieldInstructionCache.WaitForSeconds(subtitleDelay);
        yield return Helpers.RunTextFade(subtitle1, subtitleFadeTime, true, true);
        yield return YieldInstructionCache.WaitForSeconds(subtitleDelay);
        yield return Helpers.RunTextFade(subtitle2, subtitleFadeTime, true, true);

        acceptingInput = true;
        Debug.Log("accepting input");
    }

    private void Update()
    {
        if (acceptingInput && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            acceptingInput = false;
            StartCoroutine(RunExitSequence());
        }
    }

    private IEnumerator RunExitSequence()
    {

        yield return Helpers.RunVolumeWeightTransition(App.BlackFadeVolume, exitTransitionTime, 0f, 1f);
        StartCoroutine(Helpers.RunAudioFade(ambienceAudio, exitTransitionTime, ambienceAudio.volume, 0f));
        // load next scene
        SceneManager.LoadScene(gameSceneName);
    }
}
