using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScrenUI : MonoBehaviour
{
    [Header("Fade In")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _duration;

    [Header("Menu screen")]
    [SerializeField] private float _timeBeforeMenu;
    [Scene][SerializeField] private string _menuScene;

    private Coroutine _waitCoroutine;
    private Action _onEndScreenFaded;

    public void ShowEndScreen()
    {
        _canvasGroup.interactable = false;
        _onEndScreenFaded += StartWaitMainMenu;
        UIAnimations.Instance.FadeIn(_canvasGroup, _duration, true, _onEndScreenFaded);
    }

    private void StartWaitMainMenu()
    {
        print("Start waiting");
        _onEndScreenFaded -= StartWaitMainMenu;
        _canvasGroup.interactable = true;
        _waitCoroutine = StartCoroutine(WaitForMainMenu());
    }
    private IEnumerator WaitForMainMenu()
    {
        yield return new WaitForSeconds(_timeBeforeMenu);
        SceneManager.LoadScene(_menuScene);
    }
}
