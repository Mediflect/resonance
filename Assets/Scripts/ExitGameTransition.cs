using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGameTransition : MonoBehaviour
{
    public King king;
    public string sceneToLoad = "Credits";
    public float fadeDuration = 10f;

    private void Awake()
    {
        king.Defeated += OnKingDefeated;
    }

    private void OnKingDefeated()
    {
        StartCoroutine(RunExitTransition());
    }

    private IEnumerator RunExitTransition()
    {
        yield return Helpers.RunVolumeWeightTransition(App.PreTimestopEffectVolume, fadeDuration, 0, 1, smooth: false);
        SceneManager.LoadScene(sceneToLoad);
    }
}
