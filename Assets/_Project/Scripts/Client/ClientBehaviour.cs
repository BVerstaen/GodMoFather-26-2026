using UnityEngine;
using PLIbox.Extensions;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ClientBehaviour : MonoBehaviour, IPointerDownHandler
{
    [Header("Anomalies")]
    [SerializeField] private ClientVisualAnomalies _visualAnomalies;
    [SerializeField] private ClientSoundDifferences _soundAnomalies;
    [SerializeField] private ClientDialog _clientDialog;

    [Space(5)]
    [SerializeField] private List<FakeClientSO> _fakeClientList;

    private FakeClientSO _fakeClientSO = null;

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
            _fakeClientSO = _fakeClientList.GetRandomItem();

        _visualAnomalies.SetupSprite(_fakeClientSO ? _fakeClientSO.InvalidVisual : null);
        if (_fakeClientSO != null && _fakeClientSO.InvalidSound.Sound != null)
            _soundAnomalies.TriggerSoundEffect(_fakeClientSO.InvalidSound);
        else
            _soundAnomalies.TriggerSoundEffect();

        //TODO : play at end of animation
        TriggerClientDialog();
    }

    public void OnPointerDown(PointerEventData eventData) => TriggerClientDialog();

    public void TriggerClientDialog() => _clientDialog.PlayDialog(_fakeClientSO ? _fakeClientSO.InvalidDialog : null);
}