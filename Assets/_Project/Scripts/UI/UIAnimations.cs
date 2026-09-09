using System;
using System.Collections;
using UnityEngine;

public class UIAnimations : MonoBehaviour
{
    private Coroutine _currentCoroutine = null;
    public static UIAnimations Instance;

    [SerializeField] private float _menuFadeDuration;

    private void Awake()
    {
        Instance = this;
    }

    // acces pour les boutons
    public void FadeInPanel(CanvasGroup cg)
    {
        FadeIn(cg, _menuFadeDuration, true);
    }
    public void FadeOutPanel(CanvasGroup cg)
    {
        FadeIn(cg, _menuFadeDuration, false);
    }

    public void FadeIn(CanvasGroup cg, float duration, bool fade, Action OnFadeFinished = null)
    {
        if (_currentCoroutine == null)
            _currentCoroutine = StartCoroutine(Fade(cg, duration, fade, OnFadeFinished));
    }

    private IEnumerator Fade(CanvasGroup cg, float duration, bool fade, Action OnFadeFinished = null)
    {
        SetCanvasGrpVisibility(!fade, cg);

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time/duration;
            cg.alpha = Mathf.Lerp(fade ? 0 : 1, fade ?  1 : 0, t);

            yield return null;
        }

        SetCanvasGrpVisibility(fade, cg);

        OnFadeFinished?.Invoke();
        _currentCoroutine = null;
    }

    private void SetCanvasGrpVisibility(bool visible, CanvasGroup cg)
    {
        cg.alpha = visible ? 1 : 0;
        cg.interactable = visible;
        cg.blocksRaycasts = visible;
    }

}
