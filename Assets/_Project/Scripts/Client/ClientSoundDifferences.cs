using PLIbox.Extensions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClientSoundDifferences : MonoBehaviour
{
    [System.Serializable]
    public struct SoundDiff
    {
        public AudioClip Sound;
        public float DiffBetweenSound;
    }

    [Header("References")]
    [SerializeField] private AudioSource _soundSource;
    [Space(5)]
    [SerializeField] private List<SoundDiff> _soundDataList;

    private SoundDiff _soundData;
    private Coroutine _soundPlayCoroutine;

    private void OnDisable()
    {
        if (_soundPlayCoroutine != null)
        {
            StopCoroutine(_soundPlayCoroutine);
            _soundPlayCoroutine = null;
        }
    }

    public void TriggerSoundEffect()
    {
        _soundData = _soundDataList.GetRandomItem();
        _soundPlayCoroutine = StartCoroutine(SoundPlayRoutine());
    }
    public void TriggerSoundEffect(SoundDiff InvalidSoundDiff)
    {
        _soundData = InvalidSoundDiff;
        _soundPlayCoroutine = StartCoroutine(SoundPlayRoutine());
    }

    private IEnumerator SoundPlayRoutine()
    {
        _soundSource.clip = _soundData.Sound;
        while (true)
        {
            _soundSource.Play();
            yield return new WaitForSeconds(_soundSource.clip.length + _soundData.DiffBetweenSound);
        }
    }
}
