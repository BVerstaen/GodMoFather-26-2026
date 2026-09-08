using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VisualDifferencesSO", menuName = "Scriptable Objects/VisualDifferencesSO")]
public class VisualDifferencesSO : ScriptableObject
{
    public Sprite CorrectSprite;
    public List<Sprite> AnomaliesSprite;
}
