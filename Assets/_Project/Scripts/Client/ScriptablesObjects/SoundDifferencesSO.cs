using System.Collections.Generic;
using UnityEngine;
using PLIbox.Extensions;

[CreateAssetMenu(fileName = "SoundDifferences", menuName = "Scriptable Objects/SoundDifferences")]
public class SoundDifferencesSO : ScriptableObject
{
    [System.Serializable]
    private struct SoundDiff
    {
        public AudioClip Sound;
        public float DiffBetweenSound;
    }

    [SerializeField] private List<SoundDiff> _correctSoundToDiff;
    [SerializeField] private List<SoundDiff> _incorrectsoundToDiff;

    public (AudioClip, float) PickRandomCorrectSound()
    {
        SoundDiff randomStruct = _correctSoundToDiff.GetRandomItem();
        return (randomStruct.Sound, randomStruct.DiffBetweenSound);
    }

    public (AudioClip, float) PickRandomIncorrectSound()
    {
        SoundDiff randomStruct = _incorrectsoundToDiff.GetRandomItem();
        return (randomStruct.Sound, randomStruct.DiffBetweenSound);
    }
}
