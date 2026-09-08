using System.Collections;
using UnityEngine;

public class ClientSoundDifferences : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _soundSource;
    [Space(5)]
    [SerializeField] private SoundDifferencesSO _soundDiffSO;

    private SoundDifferencesSO.SoundDiff _soundData;
    private Coroutine _soundPlayCoroutine;

    private void OnDisable()
    {
        if (_soundPlayCoroutine != null)
        {
            StopCoroutine(_soundPlayCoroutine);
            _soundPlayCoroutine = null;
        }
    }

    public void TriggerSoundEffect(SoundDifferencesSO.SoundDiff? InvalidSoundDiff)
    {
        _soundData = InvalidSoundDiff.HasValue ? InvalidSoundDiff.Value : _soundDiffSO.PickRandomSoundData();
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
