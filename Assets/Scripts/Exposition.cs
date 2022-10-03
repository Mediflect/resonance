using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Medi;
using UnityEngine.SceneManagement;

public class Exposition : MonoBehaviour
{   
    public Typewriter typewriter;
    public List<ExpositionLine> lines;
    public string nextSceneName = "Castle";
    public AudioSource sound;
    public float outTransitionDuration = 5f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2.5f);
        App.BlackFadeVolume.weight = 0f;
        float soundMaxVolume = sound.volume;
        sound.Play();
        StartCoroutine(Helpers.RunAudioFade(sound, outTransitionDuration, 0f, soundMaxVolume));
        foreach (var expositionLine in lines)
        {
            typewriter.SetText(expositionLine.line);
            typewriter.Play();
            yield return typewriter.PlayCoroutine;
        }
        StartCoroutine(Helpers.RunAudioFade(sound, outTransitionDuration, soundMaxVolume, 0f));
        yield return Helpers.RunTextFade(typewriter.TextUi, outTransitionDuration, false);
        sound.Stop();
        App.BlackFadeVolume.weight = 1f;
        SceneManager.LoadScene(nextSceneName);
    }
}
