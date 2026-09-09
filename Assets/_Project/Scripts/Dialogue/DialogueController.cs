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
        _currentDialogue = newDialog;
        dialogueText.text = "";

        _currentLineIndex = 0;
        IsInDialog = true;

        UIAnimations.Instance.FadeIn(dialogueCG, DialoguePanelFadeDuration, true, OnPanelFaded);
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
        }
        else
        {
            _currentLineIndex++;

            if (_currentLineIndex >= _currentDialogue.Count) // fin du dialogue
            {
                UIAnimations.Instance.FadeIn(dialogueCG, DialoguePanelFadeDuration, false);
                IsInDialog = false;
            }
            else 
                Display();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        string newLine = "";

        foreach(char c in line)
        {
            yield return new WaitForSeconds(LetterGapTime);
            newLine += c;
            dialogueText.text = newLine;
        }
        _currentCoroutine = null;
    }


}
