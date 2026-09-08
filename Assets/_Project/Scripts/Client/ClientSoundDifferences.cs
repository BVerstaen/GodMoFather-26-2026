using System.Collections;
using UnityEngine;

public class ClientSoundDifferences : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _soundSource;
    [Space(5)]
    [SerializeField] private SoundDifferencesSO _soundDiffSO;

    private bool _isAnomaly;
    private Coroutine _soundPlayCoroutine;

    private void OnDisable()
    {
        if(_soundPlayCoroutine != null)
        {
            StopCoroutine(_soundPlayCoroutine);
            _soundPlayCoroutine = null;
        }
    }

    public void TriggerSoundEffect(bool isAnomaly)
    {
        _isAnomaly = isAnomaly;
        _soundPlayCoroutine = StartCoroutine(SoundPlayRoutine());
    }

    private IEnumerator SoundPlayRoutine()
    {
        while(true)
        {
            (AudioClip clipToPlay, float delayAfter) = _isAnomaly ? _soundDiffSO.PickRandomIncorrectSound() : _soundDiffSO.PickRandomCorrectSound();
            _soundSource.clip = clipToPlay;
            _soundSource.Play();
            yield return new WaitForSeconds(_soundSource.clip.length + delayAfter);
        }
    }
}
