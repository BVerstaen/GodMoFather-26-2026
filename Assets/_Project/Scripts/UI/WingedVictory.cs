using System.Collections;
using UnityEngine;

public class WingedVictory : MonoBehaviour
{
    [Header("Wing")]
    [SerializeField] private Transform _leftWing;
    [SerializeField] private Transform _rightWing;
    [SerializeField] private float _wingSpeed = 1.0f;
    [SerializeField] private float _wingAmplitude = 2.0f;

    [Header("SpawingOffset")]
    [SerializeField] private Vector2 _offset;
    [SerializeField] private AnimationCurve _spawnAnimationCurve;
    [SerializeField] private float _spawnTiming;

    [Header("Hovering around point")]
    [SerializeField] private AnimationCurve _hoveringCurve;
    [SerializeField] private float _hoveringTime = 1f;
    [SerializeField] private float _hoveringRadius = 1f;

    [Header("Audio")]
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private float _Sounddelay;

    private Vector3 _leftWingRotation;
    private Vector3 _rightWingRotation;
    private float _timerWing;

    private Vector3 _spawingPosition;

    private RectTransform _rect => GetComponent<RectTransform>();
    private Vector2 _defaultPosition;
    private float _angle;

    private Coroutine _soundCoroutine;
    private Coroutine _hoveringCoroutine;

    private void Awake()
    {
        _defaultPosition = _rect.anchoredPosition;
        _spawingPosition = _rect.anchoredPosition + _offset;
        _hoveringCoroutine = StartCoroutine(MovementCoroutine());
        _soundCoroutine = StartCoroutine(SoundComeGetMeRoutine());
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
        //Spawning routine
        float timeElapsed = 0.0f;
        while(timeElapsed <= _spawnTiming)
        {
            float progress = _spawnAnimationCurve.Evaluate(timeElapsed / _spawnTiming);
            _rect.anchoredPosition = Vector2.Lerp(_spawingPosition, _defaultPosition, progress);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        //Hovering around
        timeElapsed = 0.0f;
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

    private IEnumerator SoundComeGetMeRoutine()
    {
        while (true)
        {
            _soundSource.Play();
            yield return new WaitForSeconds(_soundSource.clip.length + _Sounddelay);
        }
    }
}
