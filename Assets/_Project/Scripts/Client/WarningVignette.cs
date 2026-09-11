using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class WarningVignette : MonoBehaviour
{
    [SerializeField] private float _pulseStartDuration;
    [SerializeField] private float _pulseMaxDuration;
    [SerializeField] private float _pulseAddIntensity;
    [SerializeField] private float _pulseDurationStep = 0.1f;

    private float _currentPulseDuration;

    private Vignette _vignetteComponent;

    private void Awake()
    {
        var volume = GetComponent<Volume>();
        if (!volume.profile.TryGet(out _vignetteComponent))
        {
            print("Disabled warning vignette");
            enabled = false;
            return;
        }

        _vignetteComponent.intensity.value = 0;
        _vignetteComponent.intensity.overrideState = true;

        _currentPulseDuration = _pulseStartDuration;

    }
 

    public void StartPulse()
    {
        StartCoroutine(Bounce());
    }



    private IEnumerator Bounce()
    {
        while (true)
        {
            float time = 0f;

            _currentPulseDuration = Mathf.Clamp(_currentPulseDuration - _pulseDurationStep, _pulseMaxDuration, _pulseStartDuration);

            while (time < _currentPulseDuration)
            {
                time += Time.deltaTime;
                float t = time / _currentPulseDuration;

                _vignetteComponent.intensity.value = 0 + Mathf.Sin(t * Mathf.PI) * _pulseAddIntensity;

                yield return null;
            }
            yield return new WaitForSeconds(_currentPulseDuration / 4);
        }
    }
}
