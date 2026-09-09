using PLIbox.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientDialog : MonoBehaviour
{
    [System.Serializable]
    private struct DialogData
    {
        public List<string> DialogsList;
    }

    [Header("Data")]
    [SerializeField] private List<DialogData> _validDialogs;

    [Header("Animation")]
    [SerializeField] private Transform _graphicsSprite;
    [SerializeField] private float _minYScale;

    private List<string> _currentDialogList;
    private bool _isFirstDialog = true;
    private Vector3 _baseGraphicsScale;

    private Coroutine _dialogAnimation;

    private void Awake()
    {
        _baseGraphicsScale = _graphicsSprite.localScale;
    }

    private void OnEnable()
    {
        DialogueController.Instance.OnDialogWriting += ToggleAnimation;
    }

    private void OnDisable()
    {
        DialogueController.Instance.OnDialogWriting -= ToggleAnimation;
    }

    public void PlayDialog(List<string> invalidData)
    {
        //Define dialog data
        if (_currentDialogList == null)
        {
            if (invalidData != null)
                _currentDialogList = invalidData;
            else
                _currentDialogList = _validDialogs.GetRandomItem().DialogsList;
        }

        //Call dialog controller w/ _currentDialogList OR next line
        if (_isFirstDialog || !DialogueController.Instance.IsInDialog)
            DialogueController.Instance.LaunchDialogue(_currentDialogList);
        else
            DialogueController.Instance.DisplayNextLine();
    }

    private void ToggleAnimation((bool isWriting, float timeGap) data)
    {
        if (!data.isWriting)
        {
            _graphicsSprite.localScale = _baseGraphicsScale;
            if(_dialogAnimation != null)
                StopCoroutine(_dialogAnimation);
            _dialogAnimation = null;
            return;
        }
        _dialogAnimation = StartCoroutine(DialogAnimation(data.timeGap));
    }

    private IEnumerator DialogAnimation(float timeGap)
    {
        float timeElapsed = 0.0f;
        Vector3 newScale = _graphicsSprite.localScale;
        float minScale = _minYScale * _baseGraphicsScale.y;
        while (true)
        {
            timeElapsed += Time.deltaTime;
            newScale.y = Mathf.Lerp(minScale, _baseGraphicsScale.y, (Mathf.Sin(timeElapsed / timeGap) + 1) / 2);
            _graphicsSprite.localScale = newScale;
            yield return new WaitForEndOfFrame();
        }
    }
}
