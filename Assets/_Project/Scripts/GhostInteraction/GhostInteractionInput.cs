using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GhostInteractionInput : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference _ghostInteraction_0;
    [SerializeField] private InputActionReference _ghostInteraction_1;
    [SerializeField] private InputActionReference _ghostInteraction_2;
    [SerializeField] private InputActionReference _ghostInteraction_3;
    [SerializeField] private InputActionReference _ghostInteraction_4;
    [SerializeField] private InputActionReference _ghostInteraction_5;
    [SerializeField] private InputActionReference _ghostInteraction_6;
    [SerializeField] private InputActionReference _ghostInteraction_7;

    [Header("UnityEvents")]
    [SerializeField] private UnityEvent _onInteraction_0;
    [SerializeField] private UnityEvent _onInteraction_1;
    [SerializeField] private UnityEvent _onInteraction_2;
    [SerializeField] private UnityEvent _onInteraction_3;
    [SerializeField] private UnityEvent _onInteraction_4;
    [SerializeField] private UnityEvent _onInteraction_5;
    [SerializeField] private UnityEvent _onInteraction_6;
    [SerializeField] private UnityEvent _onInteraction_7;

    private void OnEnable()
    {
        _ghostInteraction_0.action.started += TriggerInteraction0;
        _ghostInteraction_1.action.started += TriggerInteraction1;
        _ghostInteraction_2.action.started += TriggerInteraction2;
        _ghostInteraction_3.action.started += TriggerInteraction3;
        _ghostInteraction_3.action.started += TriggerInteraction4;
        _ghostInteraction_3.action.started += TriggerInteraction5;
        _ghostInteraction_3.action.started += TriggerInteraction6;
        _ghostInteraction_3.action.started += TriggerInteraction7;
    }

    private void OnDisable()
    {
        _ghostInteraction_0.action.started -= TriggerInteraction0;
        _ghostInteraction_1.action.started -= TriggerInteraction1;
        _ghostInteraction_2.action.started -= TriggerInteraction2;
        _ghostInteraction_3.action.started -= TriggerInteraction3;
        _ghostInteraction_3.action.started -= TriggerInteraction4;
        _ghostInteraction_3.action.started -= TriggerInteraction5;
        _ghostInteraction_3.action.started -= TriggerInteraction6;
        _ghostInteraction_3.action.started -= TriggerInteraction7;
    }

    private void TriggerInteraction0(InputAction.CallbackContext context) => _onInteraction_0?.Invoke();
    private void TriggerInteraction1(InputAction.CallbackContext context) => _onInteraction_1?.Invoke();
    private void TriggerInteraction2(InputAction.CallbackContext context) => _onInteraction_2?.Invoke();
    private void TriggerInteraction3(InputAction.CallbackContext context) => _onInteraction_3?.Invoke();
    private void TriggerInteraction4(InputAction.CallbackContext context) => _onInteraction_4?.Invoke();
    private void TriggerInteraction5(InputAction.CallbackContext context) => _onInteraction_5?.Invoke();
    private void TriggerInteraction6(InputAction.CallbackContext context) => _onInteraction_6?.Invoke();
    private void TriggerInteraction7(InputAction.CallbackContext context) => _onInteraction_7?.Invoke();
}
