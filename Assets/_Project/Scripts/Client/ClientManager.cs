using PLIbox.Extensions;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ClientManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<ClientBehaviour> _clientPrefab;
    [SerializeField] private Transform _spawnPoint;

    private ClientBehaviour _currentClient;

    [Button("DEBUG - Generate new client")]
    public void GenerateNewClient()
    {
        //Kill current client
        if(_currentClient != null)
        {
            Destroy(_currentClient.gameObject);
            _currentClient = null;
        }

        //Create new client
        _currentClient = Instantiate(_clientPrefab.GetRandomItem(), _spawnPoint);
    }
}