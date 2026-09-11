using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndScrenUI : MonoBehaviour
{
    [Header("Video")]
    [SerializeField] private VideoPlayer _bsodPlayer;

    [Header("Fade In")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _duration;

    [Header("Menu screen")]
    [SerializeField] private float _timeBeforeFadeIn;
    [SerializeField] private float _timeBeforeMenu;
    [Scene][SerializeField] private string _menuScene;

    private Coroutine _waitCoroutine;
    public Action _onEndScreenFaded;

    public void PlayBSODVideo()
    {
        _bsodPlayer.gameObject.SetActive(true);
        _bsodPlayer.Play();
        _waitCoroutine = StartCoroutine(WaitAndShowEndScreen());
    }

    public IEnumerator WaitAndShowEndScreen()
    {
        yield return new WaitForSeconds(_timeBeforeFadeIn);
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
