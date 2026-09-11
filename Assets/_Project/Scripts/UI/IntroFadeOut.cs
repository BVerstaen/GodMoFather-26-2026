using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class IntroFadeOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup _screenFade;
    [SerializeField] private float _fadeTime;

    private void Start()
    {
        UIAnimations.Instance.FadeIn(_screenFade, _fadeTime, false, null);
    }
}
