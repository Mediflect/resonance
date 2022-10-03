using UnityEngine;
using System.Collections;
using Medi;

public class EnterGameTransition : MonoBehaviour
{
    public Player player;
    public float fadeInDuration = 5f;

    private void Awake()
    {
        App.Request(() => StartCoroutine(RunEnterTransition()));
        player.controls.enabled = false;
    }

    private IEnumerator RunEnterTransition()
    {
        StartCoroutine(Helpers.RunVolumeWeightTransition(App.BlackFadeVolume, fadeInDuration, 1, 0, smooth: false));
        yield return YieldInstructionCache.WaitForSeconds(fadeInDuration);
        player.controls.enabled = true;

        // show movement controls
        while (player.controls.CurrentMotion == FPSControls.MotionType.Stationary)
        {
            yield return null;
        }
        App.Cycle.gameObject.SetActive(true);
    }
}
