using System;
using System.Collections;
using UnityEngine;

public class UIAnimations : MonoBehaviour
{
    private Coroutine _currentCoroutine = null;
    public static UIAnimations Instance;

    private void Awake()
    {
        Instance = this;
    }


    public void FadeIn(CanvasGroup cg, float duration, bool fade, Action OnFadeFinished = null)
    {
        if (_currentCoroutine == null)
            _currentCoroutine = StartCoroutine(Fade(cg, duration, fade, OnFadeFinished));
    }

    private IEnumerator Fade(CanvasGroup cg, float duration, bool fade, Action OnFadeFinished = null)
    {
        cg.alpha = fade ? 0 : 1;
        cg.interactable = !fade;
        cg.blocksRaycasts = !fade;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time/duration;
            cg.alpha = Mathf.Lerp(fade ? 0 : 1, fade ?  1 : 0, t);

            yield return null;
        }

        cg.alpha = fade ? 1 : 0;
        cg.interactable = fade;
        cg.blocksRaycasts = fade;

        OnFadeFinished?.Invoke();
        _currentCoroutine = null;
    }


}
