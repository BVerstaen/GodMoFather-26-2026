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

    private bool _isFlashing = false;
    private Coroutine _flashCoroutine;

    [Button("DEBUG - Flash screen")]
    public void StartFlashEffect()
    {
        if (_isFlashing)
            return;

        _flashCoroutine = StartCoroutine(FlashEffectRoutine());
    }

    private IEnumerator FlashEffectRoutine()
    {
        _isFlashing = true;
        float timeElapsed = 0.0f;
        Color flashColor = Color.white;
        _flashEffectImage.gameObject.SetActive(true);
        while (timeElapsed <= _flashTime)
        {
            flashColor.a = _flashCurve.Evaluate(timeElapsed / _flashTime);
            _flashEffectImage.color = flashColor;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        _flashEffectImage.gameObject.SetActive(false);
        _isFlashing = false;
    }
}
