using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CameraEffects : MonoBehaviour
{
    public RawImage deathImage = null;
    public GameObject defaultReticle = null;
    public GameObject interactReticle = null;
    public TextMeshProUGUI interactPrompt;
    public TextMeshProUGUI hintText;

    private IEnumerator Start()
    {
        // hack to fix lag spike on load
        yield return null;
        interactPrompt.gameObject.SetActive(false);
        hintText.gameObject.SetActive(false);
    }

    public void SetDeathEffectActive(bool active)
    {
        deathImage.gameObject.SetActive(active);
    }

    public void SetInteractEffectActive(bool active, string prompt = "Interact")
    {
        interactReticle.SetActive(active);
        defaultReticle.SetActive(!active);
        interactPrompt.gameObject.SetActive(active);
        if (prompt != null)
        {
            interactPrompt.SetText($"- {prompt} -");
        }
    }
}
