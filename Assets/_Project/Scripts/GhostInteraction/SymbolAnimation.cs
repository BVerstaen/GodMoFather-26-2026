using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SymbolAnimation : MonoBehaviour
{
    [SerializeField] private Image _symbolIMG;
    [SerializeField] private AudioClip _symbolAudio;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private Vector2 _XGap;
    [SerializeField] private Vector2 _YGap;
    [SerializeField] private float _animDuration;

    public void Init(Sprite symbol)
    {
        _symbolIMG.sprite = symbol;
        if (_symbolAudio != null) 
            _audioSource.PlayOneShot(_symbolAudio);

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        float targetX = Random.Range(_XGap.x, _XGap.y);
        float targetY = Random.Range(_YGap.x, _YGap.y);

        Vector2 TargetPos = new Vector2(transform.position.x + targetX, transform.position.y + targetY);
        Vector2 StartPos = transform.position;

        float time = 0;

        while (time < _animDuration)
        {
            time += Time.deltaTime;
            float t = time / _animDuration;

            transform.position = Vector2.Lerp(StartPos, TargetPos, t);

            Color symbolColor = _symbolIMG.color;
            symbolColor.a = Mathf.Lerp(0, 1, t);
            _symbolIMG.color = symbolColor;

            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        float timeToDisapear = 0;

        while (timeToDisapear < 1.5f)
        {
            timeToDisapear += Time.deltaTime;
            float t = time / _animDuration;

            Color symbolColor = _symbolIMG.color;
            symbolColor.a = Mathf.Lerp(1, 0, t);
            _symbolIMG.color = symbolColor;

            yield return null;
        }


        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

}
