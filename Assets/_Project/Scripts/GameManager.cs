using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ClientManager _clientSpawner;
    [SerializeField] private EndScrenUI _endScreen;
    [SerializeField] private WingedVictory _victoryButton;
    [SerializeField] private DefeatPanel _defeatPanel;

    [ReadOnly][SerializeField] private int _currentScore;

    private void OnEnable()
    {
        _clientSpawner.OnOutOfClient += StartVictoryButton;
    }

    private void OnDisable()
    {
        _clientSpawner.OnOutOfClient -= StartVictoryButton;
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
            _currentScore++;
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
