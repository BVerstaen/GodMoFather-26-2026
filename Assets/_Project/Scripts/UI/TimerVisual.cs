using TMPro;
using UnityEngine;

public class TimerVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private int _lastSecondsWarning = 10;
    private bool _isInWarning;

    public void UpdateTimer(float _currentTimer)
    {
        float min = _currentTimer / 60;
        float sec = _currentTimer % 60;

        _timerText.text = (int)min + " : " + (int)sec;

        if (_currentTimer <= _lastSecondsWarning && !_isInWarning)
        {
            _isInWarning = true;

            _timerText.color = Color.red;
        }
    }


}
