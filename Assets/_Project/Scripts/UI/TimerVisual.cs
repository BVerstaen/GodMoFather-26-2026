using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private int _lastSecondsWarning = 10; 
    [SerializeField] private float _bouceDuration = 0.3f;
    [SerializeField] private float _timeBtwBounce = 0.7f;
    [SerializeField] private float _add = 15;
    private float _startingSize;
    private bool _isInWarning;

    private void Awake()
    {
        _timerText.gameObject.SetActive(false);
        _startingSize = _timerText.fontSize;
    }

    public void UpdateTimer(float _currentTimer)
    {
        float min = _currentTimer / 60;
        float sec = _currentTimer % 60;

        if ((int)min <= 0)
            _timerText.text = ((int)sec).ToString();
        else 
            _timerText.text = (int)min + " : " + (int)sec;

        if ((int)_currentTimer <= _lastSecondsWarning && !_isInWarning)
        {
            _timerText.gameObject.SetActive(true);
            _isInWarning = true;

            _timerText.color = Color.red;
            StartCoroutine(Bounce());
        }
    }

    private IEnumerator Bounce()
    {
        while (true)
        {
            float time = 0f;
            while (time < _bouceDuration)
            {
                time += Time.deltaTime;
                float t = time / _bouceDuration;

                _timerText.fontSize = _startingSize + Mathf.Sin(t * Mathf.PI) * _add;

                yield return null;
            }
            yield return new WaitForSeconds(_timeBtwBounce);
        } 
    }


}
