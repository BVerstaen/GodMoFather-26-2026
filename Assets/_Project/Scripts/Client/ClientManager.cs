using PLIbox.Extensions;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;
using Random = UnityEngine.Random;

public class ClientManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<ClientBehaviour> _clientPrefab;
    [SerializeField] private Transform _spawnPoint;
    [Space(5)]
    [SerializeField] private int _numberOfClients;

    private bool _hasReachedLimit;
    private int _clientCount;
    private ClientBehaviour _currentClient;
    private int _previousClientIndex = -1;

    public Action<int /*client count*/> OnNewClient;
    public Action OnOutOfClient;

    public ClientBehaviour CurrentClient
    {
        get => _currentClient;
    }

    private void OnValidate()
    {
        _numberOfClients = Mathf.Max(1, _numberOfClients);
    }

    private void Awake()
    {
        _clientCount = _numberOfClients;
    }

    [Button("DEBUG - Generate new client")]
    public void GenerateNewClient(bool wasAccepted = true)
    {
        if (_clientCount <= 0 && !_hasReachedLimit)
        {
            print("No more clients");
            OnOutOfClient?.Invoke();
            _hasReachedLimit = true;
        }
        _clientCount--;

        //Kill current client
        if (_currentClient != null)
        {
            //Destroy(_currentClient.gameObject);
            _currentClient.Move(false, wasAccepted);
            _currentClient = null;
        }

        //Create new client
        int foundIndex = Random.Range(0, _clientPrefab.Count);
        while(_previousClientIndex == foundIndex)
            foundIndex = Random.Range(0, _clientPrefab.Count);
        _previousClientIndex = foundIndex;

        _currentClient = Instantiate(_clientPrefab[foundIndex], _spawnPoint);
        _currentClient.Move(true);
        OnNewClient?.Invoke(_clientCount);
    }

    public void KillClient()
    {
        if (_currentClient != null)
            Destroy(_currentClient.gameObject);
    }
}