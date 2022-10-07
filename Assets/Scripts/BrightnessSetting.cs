using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class BrightnessSetting : MonoBehaviour
{
    [SerializeField]
    private Volume brightnessVolume;

    private float currentBrightness = 0.1f;

    private void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.equalsKey.wasPressedThisFrame)
        {
            currentBrightness += 0.1f;
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.minusKey.wasPressedThisFrame)
        {
            currentBrightness -= 0.1f;
        }
        currentBrightness = Mathf.Clamp01(currentBrightness);
        brightnessVolume.weight = currentBrightness;
    }
}
