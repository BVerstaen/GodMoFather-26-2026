using UnityEngine;

[CreateAssetMenu(fileName = "FakeClientSO", menuName = "Scriptable Objects/FakeClientSO")]
public class FakeClientSO : ScriptableObject
{
    public Sprite InvalidVisual;
    public ClientSoundDifferences.SoundDiff InvalidSound;
}
