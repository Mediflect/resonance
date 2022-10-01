using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableDoor : MonoBehaviour
{
    public enum DoorState 
    {
        Open,
        Closed,
        Opening,
        Closing
    }

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
        float timer = transitionTime;
        while (timer > 0f)
        {
            float tValue = Mathf.InverseLerp(transitionTime, 0, timer);
            float tValueSmoothed = Mathf.SmoothStep(0, 1, tValue);
            Debug.Log($"{tValue}, {tValueSmoothed}");
            doorObjTransform.position = Vector3.Lerp(doorClosedTarget.position, doorOpenTarget.position, tValueSmoothed);
            yield return null;
            timer -= Time.deltaTime;
        }
        doorObjTransform.gameObject.SetActive(false);
        blockerCollider.SetActive(false);
        changeStateCoroutine = null;
    }

    private IEnumerator RunClose()
    {
        blockerCollider.SetActive(true);
        doorObjTransform.gameObject.SetActive(true);
        float timer = transitionTime;
        while (timer > 0f)
        {
            float tValueSmoothed = Mathf.SmoothStep(0, 1, Mathf.InverseLerp(transitionTime, 0, timer));
            doorObjTransform.position = Vector3.Lerp(doorOpenTarget.position, doorClosedTarget.position, tValueSmoothed);
            yield return null;
            timer -= Time.deltaTime;
        }

        changeStateCoroutine = null;
    }
}
