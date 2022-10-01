using UnityEngine;
using UnityEngine.UI;

public class CameraEffects : MonoBehaviour
{
    public RawImage deathImage = null;

    public void SetDeathEffectActive(bool active)
    {
        deathImage.gameObject.SetActive(active);
    }
}
