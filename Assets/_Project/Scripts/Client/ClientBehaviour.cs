using UnityEngine;
using PLIbox.Extensions;

public class ClientBehaviour : MonoBehaviour
{
    [Header("Anomalies")]
    [SerializeField] private ClientVisualAnomalies _visualAnomalies;
    [SerializeField] private ClientSoundDifferences _soundAnomalies;

    private bool _isVisualAnomaly;
    private bool _isSoundAnomaly;
    private bool _isDialogAnomaly;

    public bool IsAnomaly
    {
        get => _isVisualAnomaly || _isSoundAnomaly || _isDialogAnomaly;
    }

    private void Awake()
    {
        _isVisualAnomaly = RandomExtensions.RandomBool();
        _isSoundAnomaly = RandomExtensions.RandomBool();
        _isDialogAnomaly = RandomExtensions.RandomBool();

        _visualAnomalies.SetupSprite(_isVisualAnomaly);
        _soundAnomalies.TriggerSoundEffect(_isSoundAnomaly);
    }
}