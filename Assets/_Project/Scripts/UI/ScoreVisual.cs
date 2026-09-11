using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private float _bouceDuration = 0.3f;
    [SerializeField] private float _add = 60;
    private float _startingSize;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _startingSize = _scoreText.fontSize;
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();

        if (_currentCoroutine != null) 
            StopCoroutine( _currentCoroutine );

        _currentCoroutine = StartCoroutine(Bounce());

    }

    private IEnumerator Bounce()
    {
        float time = 0f;

        while (time < _bouceDuration)
        {
            time += Time.deltaTime;
            float t = time / _bouceDuration;

            _scoreText.fontSize = _startingSize + Mathf.Sin(t * Mathf.PI) * _add;

            yield return null;
        }
    }
}
