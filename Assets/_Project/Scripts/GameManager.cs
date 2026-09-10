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
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [ReadOnly][SerializeField] private int _currentScore;

    [SerializeField] private int _GameTime;
    private float _currentTimer;
    private bool isEndOfGame = false;

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
        if (isEndOfGame)
            return;


        if (_currentTimer <= 0)
        {
            StartVictoryButton();
            isEndOfGame = true;
        }
        if (!PauseManager.IsPaused)
        {
            _currentTimer -= Time.deltaTime;
            _timerText.text = ((int)_currentTimer).ToString();
        }
           
    }

    private void Start()
    {
        //Generate first client
        _clientSpawner.GenerateNewClient();
    }

    public void ResolveClient(bool isAccepted)
    {
        if(_clientSpawner.CurrentClient == null)
            return;
        if (!_clientSpawner.CurrentClient.HasDoneMoving)
            return;


        bool isValid = _clientSpawner.CurrentClient.IsFakeClient != isAccepted;
        if (isValid)
        {
            _currentScore++;
            _scoreText.text = _currentScore.ToString();
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
