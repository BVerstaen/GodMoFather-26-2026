using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffectGhostInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _flashEffectImage;

    [Header("Flash")]
    [SerializeField] private AnimationCurve _flashCurve;
    [SerializeField] private float _flashTime;

    [Header("Darken")]
    [SerializeField] private AnimationCurve _darkenCurve;
    [SerializeField] private float _darkenTime;

    private bool _isFlashing = false;
    private Coroutine _flashCoroutine;

    [Button("DEBUG - Flash screen")]
    public void StartFlashEffect()
    {
        if (_isFlashing)
            return;

        _flashCoroutine = StartCoroutine(FlashEffectRoutine(Color.white, _flashCurve, _flashTime));
    }

    [Button("DEBUG - Darken screen")]
    public void StartDarkenEffect()
    {
        if (_isFlashing)
            return;

        _flashCoroutine = StartCoroutine(FlashEffectRoutine(Color.black, _darkenCurve, _darkenTime));
    }

    private IEnumerator FlashEffectRoutine(Color baseColor, AnimationCurve curve, float time)
    {
        _isFlashing = true;
        float timeElapsed = 0.0f;
        Color flashColor = baseColor;
        _flashEffectImage.gameObject.SetActive(true);
        while (timeElapsed <= time)
        {
            flashColor.a = curve.Evaluate(timeElapsed / time);
            _flashEffectImage.color = flashColor;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        _flashEffectImage.gameObject.SetActive(false);
        _isFlashing = false;
    }
}
