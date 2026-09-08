using UnityEngine;
using PLIbox.Extensions;
using System.Collections.Generic;

public class ClientBehaviour : MonoBehaviour
{
    [Header("Anomalies")]
    [SerializeField] private ClientVisualAnomalies _visualAnomalies;
    [SerializeField] private ClientSoundDifferences _soundAnomalies;

    [Space(5)]
    [SerializeField] private List<FakeClientSO> _fakeClientList;

    private FakeClientSO _fakeClient;

    public bool IsAnomaly
    {
        get;
        private set;
    }

    private void Awake()
    {
        IsAnomaly = RandomExtensions.RandomBool();

        _visualAnomalies.SetupSprite(_fakeClient ? _fakeClient.InvalidVisual : null);
        _soundAnomalies.TriggerSoundEffect(_fakeClient ? _fakeClient.InvalidSound : null);
    }
}