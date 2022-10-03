using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resonance/Exposition", fileName = "Exposition_New")]
public class ExpositionLine : ScriptableObject
{
    [TextArea(50, 100)]
    public string line;
}
