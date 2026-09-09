using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ClientManager _clientSpawner;
    [SerializeField] private EndScrenUI _endScreen;
    [SerializeField] private WingedVictory _victoryButton;

    [ReadOnly][SerializeField]private int _currentScore;

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

        if(_clientSpawner.CurrentClient.IsFakeClient != isAccepted)
        {
            _currentScore++;
        }
        _clientSpawner.GenerateNewClient();
    }

    private void StartVictoryButton()
    {
        _victoryButton.gameObject.SetActive(true);
    }

    public  void EndGame()
    {
        Debug.LogWarning("End of the game");
        _endScreen.ShowEndScreen();
    }
}
