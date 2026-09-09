using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private CanvasGroup _screenFade;

    private Action OnScreenFadeFinished;
    private int _sceneToLoad = 0;

    private void Awake()
    {
        OnScreenFadeFinished += LoadScene;
    }

    public void ChangeScene(int sceneIndex)
    {
        _sceneToLoad = sceneIndex;
        if (_screenFade != null)
            UIAnimations.Instance.FadeIn(_screenFade, 1.2f, true, OnScreenFadeFinished);
        else
            LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
