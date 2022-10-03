using UnityEngine;
using System.Collections;
using Medi;

public class EnterGameTransition : MonoBehaviour
{
    public Player player;
    public float fadeInDuration = 5f;
    public ToastHint introNoticeHint;
    public ToastHint movementTutorialHint;

    private void Awake()
    {
        App.Request(() => StartCoroutine(RunEnterTransition()));
        player.controls.enabled = false;
    }

    private IEnumerator RunEnterTransition()
    {
        yield return Helpers.RunVolumeWeightTransition(App.BlackFadeVolume, fadeInDuration, 1, 0, smooth: false);
        player.toastHintSystem.QueueHint(introNoticeHint);
        while (player.toastHintSystem.IsPlayingHint)
        {
            yield return null;
        }
        player.toastHintSystem.QueueHint(movementTutorialHint);
        player.controls.enabled = true;

        while (player.controls.CurrentMotion == FPSControls.MotionType.Stationary)
        {
            yield return null;
        }
        App.Cycle.gameObject.SetActive(true);
    }
}
