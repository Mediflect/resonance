using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resonance/Toast Hint", fileName = "ToastHint_New")]
public class ToastHint : ScriptableObject
{
    [TextArea]
    public string hint;
}
