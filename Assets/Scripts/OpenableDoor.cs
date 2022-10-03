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
    public bool isOpen = false;

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
        isOpen = false;
        blockerCollider.SetActive(true);
        doorObjTransform.gameObject.SetActive(true);

        yield return Helpers.RunSmoothMoveTo(doorObjTransform, doorClosedTarget, doorOpenTarget, transitionTime, waitOnPause: true);

        doorObjTransform.gameObject.SetActive(false);
        blockerCollider.SetActive(false);
        changeStateCoroutine = null;
        isOpen = true;
    }

    private IEnumerator RunClose()
    {
        isOpen = true;
        blockerCollider.SetActive(true);
        doorObjTransform.gameObject.SetActive(true);
        float timer = transitionTime;

        yield return Helpers.RunSmoothMoveTo(doorObjTransform, doorOpenTarget, doorClosedTarget, transitionTime, waitOnPause: true);

        changeStateCoroutine = null;
        isOpen = false;
    }
}
