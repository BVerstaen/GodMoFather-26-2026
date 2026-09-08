using System.Collections.Generic;
using UnityEngine;
using PLIbox.Extensions;

[CreateAssetMenu(fileName = "SoundDifferences", menuName = "Scriptable Objects/SoundDifferences")]
public class SoundDifferencesSO : ScriptableObject
{
    [System.Serializable]
    public struct SoundDiff
    {
        public AudioClip Sound;
        public float DiffBetweenSound;
    }

    [SerializeField] private List<SoundDiff> _soundList;

    public SoundDiff PickRandomSoundData()
    {
        return _soundList.GetRandomItem();
    }
}
