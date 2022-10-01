using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableDoor : MonoBehaviour
{
    public Transform doorObjTransform;
    public Transform doorOpenTarget;
    public Transform doorClosedTarget;
    public GameObject blockerCollider;
    public float transitionTime = 1f;

    private Coroutine changeStateCoroutine = null;

    [ContextMenu("Open")]
    public void Open()
    {
        if (changeStateCoroutine != null)
        {
            StopCoroutine(changeStateCoroutine);
        }
        StartCoroutine(RunOpen());
    }

    [ContextMenu("Close")]
    public void Close()
    {
        if (changeStateCoroutine != null)
        {
            StopCoroutine(changeStateCoroutine);
        }
        StartCoroutine(RunClose());
    }

    private IEnumerator RunOpen()
    {
        blockerCollider.SetActive(true);
        doorObjTransform.gameObject.SetActive(true);

        yield return Helpers.RunSmoothMoveTo(doorObjTransform, doorClosedTarget, doorOpenTarget, transitionTime);

        doorObjTransform.gameObject.SetActive(false);
        blockerCollider.SetActive(false);
        changeStateCoroutine = null;
    }

    private IEnumerator RunClose()
    {
        blockerCollider.SetActive(true);
        doorObjTransform.gameObject.SetActive(true);
        float timer = transitionTime;

        yield return Helpers.RunSmoothMoveTo(doorObjTransform, doorOpenTarget, doorClosedTarget, transitionTime);

        changeStateCoroutine = null;
    }
}
