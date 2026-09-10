using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GhostInteractionInput : MonoBehaviour
{
    [SerializeField] private Dictionary<InputActionReference, UnityEvent> _ghostInteractions = new Dictionary<InputActionReference, UnityEvent>();
    [SerializeField] private GameObject _symbolPrefab;
    [SerializeField] private Transform _symbolParent;
    [SerializeField] private AudioSource _ghostAudioSource;
    private void OnEnable()
    {
        foreach (KeyValuePair<InputActionReference, UnityEvent> kvp in _ghostInteractions)
        {
            kvp.Key.action.performed += act => kvp.Value.Invoke();
        }
    }

    private void OnDisable()
    {
        foreach (KeyValuePair<InputActionReference, UnityEvent> kvp in _ghostInteractions)
        {
            kvp.Key.action.performed -= act => kvp.Value.Invoke();
        }
    }

    public void DisplayInteraction(Sprite symbol)
    {
        GameObject interactionGO = Instantiate(_symbolPrefab, _symbolParent);
        interactionGO.transform.position = _symbolParent.position;
        if (interactionGO.TryGetComponent<SymbolAnimation>(out SymbolAnimation symbolAnim))
        {
            symbolAnim.Init(symbol);
            _ghostAudioSource.Play();
        }
    }

}
