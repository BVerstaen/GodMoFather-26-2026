using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace PLI.UI
{
    public class UniversalHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("SizeAnimation")]
        [SerializeField] private AnimationCurve _sizeAnimationCurve;
        [SerializeField] private float _sizeAnimationDuration;
        [SerializeField] private float _sizeAnimtionAmplitude;

        [Header("Events")]
        [SerializeField] private UnityEvent _onHoverStart;
        [SerializeField] private UnityEvent _onHoverEnd;

        private RectTransform _rect;
        private int _currentDirection;
        private float _progress;

        private Vector3 _defaultScale;
        private Coroutine _sizeAnimationCoroutine;

        public Action OnHoverStart;
        public Action OnHoverEnd;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _defaultScale = _rect.localScale;
        }

        private void OnDisable()
        {
            _rect.localScale = _defaultScale;
            _sizeAnimationCoroutine = null;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PlaySizeAnimation(1);
            OnHoverStart?.Invoke();
            _onHoverStart?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PlaySizeAnimation(-1);
            OnHoverEnd?.Invoke();
            _onHoverEnd?.Invoke();
        }

        private void PlaySizeAnimation(int direction)
        {
            _currentDirection = direction;
            if (_sizeAnimationCoroutine == null)
                _sizeAnimationCoroutine = StartCoroutine(SizeAnimationRoutine());
        }

        private IEnumerator SizeAnimationRoutine()
        {
            float scale = _currentDirection == 1 ? 1 : 1f + _sizeAnimtionAmplitude;
            _rect.localScale = _defaultScale * scale;
            while ((_progress <= 1f && _currentDirection == 1) || (_progress >= 0f && _currentDirection == -1))
            {
                _progress += (_currentDirection * Time.deltaTime) / _sizeAnimationDuration;
                _progress = Mathf.Clamp01(_progress);

                scale = 1f + _sizeAnimtionAmplitude * _sizeAnimationCurve.Evaluate(_progress);
                _rect.localScale = _defaultScale * scale;
                yield return null;
            }
            scale = _currentDirection == 1 ? 1f + _sizeAnimtionAmplitude : 1;
            _rect.localScale = _defaultScale * scale;

            _sizeAnimationCoroutine = null;
        }
    }
}
