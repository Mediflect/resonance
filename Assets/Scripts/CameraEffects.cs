using UnityEngine;
using UnityEngine.UI;

public class CameraEffects : MonoBehaviour
{
    public RawImage deathImage = null;
    public GameObject defaultReticle = null;    
    public GameObject interactReticle = null;

    public void SetDeathEffectActive(bool active)
    {
        deathImage.gameObject.SetActive(active);
    }

    public void SetInteractEffectActive(bool active)
    {
        interactReticle.SetActive(active);
        defaultReticle.SetActive(!active);
    }
}
