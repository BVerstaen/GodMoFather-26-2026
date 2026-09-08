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

    private FakeClientSO _fakeClient = null;

    public bool IsFakeClient
    {
        get;
        private set;
    }

    private void Awake()
    {
        //Choose if is fake or not
        IsFakeClient = RandomExtensions.RandomBool();
        if (IsFakeClient)
            _fakeClient = _fakeClientList.GetRandomItem();

        _visualAnomalies.SetupSprite(_fakeClient ? _fakeClient.InvalidVisual : null);
        if (_fakeClient != null && _fakeClient.InvalidSound.Sound != null)
            _soundAnomalies.TriggerSoundEffect(_fakeClient.InvalidSound);
        else
            _soundAnomalies.TriggerSoundEffect();
    }
}