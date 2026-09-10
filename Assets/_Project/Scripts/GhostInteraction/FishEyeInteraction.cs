using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class FishEyeInteraction : MonoBehaviour
{
    [Header("FishEye")]
    [SerializeField] private AnimationCurve _fishEyeCurve;
    [SerializeField] private float _fishEyeDuration;

    private VolumeProfile _volumeProfil;
    private LensDistortion _lensDistortionComponent;
    private bool _isFishing;

    private Coroutine _fishEyeCoroutine;

    private void Awake()
    {
        var volume = GetComponent<Volume>();
        _volumeProfil = volume.profile;
        if (!_volumeProfil.TryGet(out _lensDistortionComponent))
        {
            enabled = false;
            return;
        }
    }

    [Button("DEBUG - FishEye effect")]
    public void StartFishEyeEffect()
    {
        if (_isFishing)
            return;
             
        _fishEyeCoroutine = StartCoroutine(FishEyeRoutine());
    }

    private IEnumerator FishEyeRoutine()
    {
        float timeElapsed = 0.0f;
        _isFishing = true;

        _lensDistortionComponent.intensity.value = _fishEyeCurve.Evaluate(0);
        while (timeElapsed <= _fishEyeDuration)
        {
            _lensDistortionComponent.intensity.value = Mathf.Clamp01(_fishEyeCurve.Evaluate(timeElapsed / _fishEyeDuration));
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _lensDistortionComponent.intensity.value = _fishEyeCurve.Evaluate(1);
        _isFishing = false;
    }
}
