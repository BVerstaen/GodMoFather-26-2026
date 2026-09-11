using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class ShakingCamInteraction : MonoBehaviour
{
    [Header("Shake aniamtion")]
    [SerializeField] private AnimationCurve _shakingCurve;
    [SerializeField] private float _shakeDuration = 1.0f;
    [SerializeField] private float _shakeTravelDuration = 1.0f;
    [SerializeField] private float _shakeRadius = 1.0f;

    private bool _isShaking;
    private Vector3 _defaultPosition;
    private Coroutine _cameraShakeCoroutine;

    private void Awake()
    {
        _defaultPosition = transform.localPosition;
    }

    [Button("DEBUG - Play shake cam")]
    public void PlayCameraShake()
    {
        if (_isShaking)
            return;

        _cameraShakeCoroutine = StartCoroutine(MovementCoroutine());
    }

    private IEnumerator MovementCoroutine()
    {
        //Shaking
        _isShaking = true;

        float globalTimeElapsed = 0.0f;
        float timeElapsed = 0.0f;
        float angle;

        Vector3 targetPosition = transform.localPosition;
        Vector3 basePosition = transform.localPosition;

        while (globalTimeElapsed <= _shakeDuration)
        {
            timeElapsed = 0.0f;
            angle = Random.Range(0, 360);
            basePosition = transform.localPosition;
            targetPosition.x = _defaultPosition.x + Mathf.Cos(angle) * _shakeRadius;
            targetPosition.y = _defaultPosition.y + Mathf.Sin(angle) * _shakeRadius;

            while (timeElapsed <= _shakeTravelDuration)
            {
                globalTimeElapsed += Time.deltaTime;
                timeElapsed += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(basePosition, targetPosition, _shakingCurve.Evaluate(timeElapsed / _shakeTravelDuration));
                yield return null;
            }
        }

        _isShaking = false;
        transform.localPosition = _defaultPosition;
    }
}
