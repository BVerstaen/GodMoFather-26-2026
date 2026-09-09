using System.Collections;
using UnityEngine;

public class WingedVictory : MonoBehaviour
{
    [Header("Wing")]
    [SerializeField] private Transform _leftWing;
    [SerializeField] private Transform _rightWing;
    [SerializeField] private float _wingSpeed = 1.0f;
    [SerializeField] private float _wingAmplitude = 2.0f;

    [Header("Hovering around point")]
    [SerializeField] private AnimationCurve _hoveringCurve;
    [SerializeField] private float _hoveringTime = 1f;
    [SerializeField] private float _hoveringRadius = 1f;

    private Vector3 _leftWingRotation;
    private Vector3 _rightWingRotation;
    private float _timerWing;

    private RectTransform _rect => GetComponent<RectTransform>();
    private Vector2 _defaultPosition;
    private float _angle;

    private Coroutine _hoveringCoroutine;

    private void Awake()
    {
        _defaultPosition = _rect.anchoredPosition;
        _hoveringCoroutine = StartCoroutine(MovementCoroutine());
    }

    private void Update()
    {
        _timerWing += Time.deltaTime * _wingSpeed;
        _leftWingRotation.z = Mathf.Sin(_timerWing) * _wingAmplitude;
        _rightWingRotation.z = -Mathf.Sin(_timerWing) * _wingAmplitude;

        _leftWing.eulerAngles = _leftWingRotation;
        _rightWing.eulerAngles = _rightWingRotation;
    }

    private IEnumerator MovementCoroutine()
    {
        float timeElapsed = 0.0f;
        float angle;
        Vector2 targetPosition;
        Vector2 basePosition;
        while (true)
        {
            timeElapsed = 0.0f;
            angle = Random.Range(0, 360);
            basePosition = _rect.anchoredPosition;
            targetPosition.x = _defaultPosition.x + Mathf.Cos(angle) * _hoveringRadius;
            targetPosition.y = _defaultPosition.y + Mathf.Sin(angle) * _hoveringRadius;
            
            while (timeElapsed <= _hoveringTime)
            {
                timeElapsed += Time.deltaTime;
                _rect.anchoredPosition = Vector2.Lerp(basePosition, targetPosition, _hoveringCurve.Evaluate(timeElapsed / _hoveringTime));
                yield return null;
            }
        }
    }
}
