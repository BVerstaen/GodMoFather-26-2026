using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostInteractionInput : MonoBehaviour
{
    [SerializeField] private Dictionary<InputActionReference, Sprite> _ghostInteractions = new Dictionary<InputActionReference, Sprite>();
    [SerializeField] private GameObject _symbolPrefab;
    [SerializeField] private Transform _symbolParent;

    private void OnEnable()
    {
        foreach (KeyValuePair<InputActionReference, Sprite> kvp in _ghostInteractions)
        {
            kvp.Key.action.performed += act => DisplayInteraction(kvp.Value);
        }
    }

    private void OnDisable()
    {
        foreach (KeyValuePair<InputActionReference, Sprite> kvp in _ghostInteractions)
        {
            kvp.Key.action.performed -= act => DisplayInteraction(kvp.Value);
        }
    }

    private void DisplayInteraction(Sprite symbol)
    {
        GameObject interactionGO = Instantiate(_symbolPrefab, _symbolParent);
        interactionGO.transform.position = _symbolParent.position;
        if (interactionGO.TryGetComponent<SymbolAnimation>(out SymbolAnimation symbolAnim))
        {
            symbolAnim.Init(symbol);
        }
    }

}
