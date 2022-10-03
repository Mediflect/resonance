using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resonance/Toast Hint", fileName = "ToastHint_New")]
public class ToastHint : ScriptableObject
{
    public bool endWithFade = true;
    public bool playAudioOnStart = true;

    [TextArea]
    public List<string> lines;
}
