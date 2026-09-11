using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ClientManager _clientSpawner;
    [SerializeField] private EndScrenUI _endScreen;
    [SerializeField] private WingedVictory _victoryButton;
    [SerializeField] private DefeatPanel _defeatPanel;
    [SerializeField] private TimerVisual _timervisual;
    [SerializeField] private ScoreVisual _scoreVisual;
    [SerializeField] private WarningVignette _warningVignette;
    [SerializeField] private float _startPusleTime;


    [ReadOnly][SerializeField] private int _currentScore;
    private bool _pulseStarted = false;

    [SerializeField] private int _GameTime;
    private float _currentTimer;

    public static bool IsEndOfGame { get; private set; }

    private void Awake()
    {
        IsEndOfGame = false;
    }

    private void OnEnable()
    {
        _currentTimer = _GameTime;
        _clientSpawner.OnOutOfClient += StartVictoryButton;
    }

    private void OnDisable()
    {
        _clientSpawner.OnOutOfClient -= StartVictoryButton;
    }

    private void Update()
    {
        if (IsEndOfGame)
            return;


        if (_currentTimer <= 0)
        {
            StartVictoryButton();
            IsEndOfGame = true;
        }
        if (!PauseManager.IsPaused)
        {
            _currentTimer -= Time.deltaTime;
            if (_currentTimer <= _startPusleTime && !_pulseStarted)
            {
                _warningVignette.StartPulse();
                _pulseStarted = true;
            }

            _timervisual.UpdateTimer(_currentTimer);
        }
           
    }

    private void Start()
    {
        //Generate first client
        _clientSpawner.GenerateNewClient();
    }

    public void ResolveClient(bool isAccepted)
    {
        if (IsEndOfGame)
            return;
        if (_clientSpawner.CurrentClient == null)
            return;
        if (!_clientSpawner.CurrentClient.HasDoneMoving)
            return;


        bool isValid = _clientSpawner.CurrentClient.IsFakeClient != isAccepted;
        if (isValid)
        {
            _currentScore++;
            _scoreVisual.UpdateScore(_currentScore);
        }
        else
        {
            _defeatPanel.DisplayDefeatPanel(_clientSpawner.CurrentClient.IsFakeClient && isAccepted);
        }

        _clientSpawner.GenerateNewClient(isAccepted, isValid);
    }

    private void StartVictoryButton()
    {
        _victoryButton.gameObject.SetActive(true);
    }

    public  void EndGame()
    {
        Debug.LogWarning("End of the game");
        _endScreen.PlayBSODVideo();
    }
}
