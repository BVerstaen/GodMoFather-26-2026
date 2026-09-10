using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; protected set; }

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float LetterGapTime;
    [SerializeField] private CanvasGroup dialogueCG;
    [SerializeField] private float DialoguePanelFadeDuration;

    private Coroutine _currentCoroutine = null;
    private int _currentLineIndex;
    private List<string> _currentDialogue = new List<string>();

    public bool IsInDialog { get; private set; }

    private Action OnPanelFaded;

    public Action<(bool isWriting, float timeGap)> OnDialogWriting;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;
        OnPanelFaded += Display;
    }

    private void OnDestroy()
    {
        OnPanelFaded -= Display;
    }

    public void LaunchDialogue(List<string> newDialog)
    {
        bool wasInDialog = IsInDialog;
        if (IsInDialog && _currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        _currentDialogue = newDialog;
        dialogueText.text = "";

        _currentLineIndex = 0;
        IsInDialog = true;

        //if (!wasInDialog)
        //    UIAnimations.Instance.FadeIn(dialogueCG, DialoguePanelFadeDuration, true, OnPanelFaded);
        //else


        Display();
    }

    private void Display()
    {
        _currentCoroutine = StartCoroutine(DisplayLine(_currentDialogue[_currentLineIndex]));
    }

    public void DisplayNextLine()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
            dialogueText.text = _currentDialogue[_currentLineIndex];
            IsInDialog = false;
            OnDialogWriting?.Invoke((false, LetterGapTime));
        }
        else
        {
            _currentLineIndex++;

            if (_currentLineIndex >= _currentDialogue.Count) // fin du dialogue
            {
               // UIAnimations.Instance.FadeIn(dialogueCG, DialoguePanelFadeDuration, false);
                dialogueText.text = "";
                IsInDialog = false;
                OnDialogWriting?.Invoke((false, LetterGapTime));
            }
            else 
                Display();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        string newLine = "";
        OnDialogWriting?.Invoke((true, LetterGapTime));
        foreach (char c in line)
        {
            yield return new WaitForSeconds(LetterGapTime);
            newLine += c;
            dialogueText.text = newLine;
        }
        OnDialogWriting?.Invoke((false, LetterGapTime));
        _currentCoroutine = null;
    }


}
