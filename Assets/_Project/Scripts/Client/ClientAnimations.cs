using System.Collections;
using UnityEngine;

public class ClientAnimations : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Transform _graphicsSprite;
    [SerializeField] private float _waveSpeed;
    [SerializeField] private float _waveAmplitude;

    private Vector3 _defaultLocalePosition;
    private Coroutine _waveAnimation;

    private void Awake()
    {
        _defaultLocalePosition = _graphicsSprite.localPosition;
        _waveAnimation = StartCoroutine(WaveAnimation());
    }

    private IEnumerator WaveAnimation()
    {
        float timeElapsed = 0.0f;
        Vector3 newPosition = _graphicsSprite.localPosition;
        while (true)
        {
            timeElapsed += Time.deltaTime * _waveSpeed;
            newPosition.y = Mathf.Sin(timeElapsed) * _waveAmplitude;
            _graphicsSprite.localPosition = newPosition;
            yield return new WaitForEndOfFrame();
        }
    }
}
