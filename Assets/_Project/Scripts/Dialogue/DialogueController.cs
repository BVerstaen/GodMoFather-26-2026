using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float LetterGapTime;
    [SerializeField] private CanvasGroup dialogueCG;
    [SerializeField] private float DialoguePanelFadeDuration;
    [SerializeField] private List<string> TestDialogue = new List<string>(); // test

    private Coroutine _currentCoroutine = null;
    private int _currentLineIndex;
    private List<string> _currentDialogue = new List<string>();

    private Action OnPanelFaded;

    private void Awake()
    {
        OnPanelFaded += Display;
    }

    private void OnDestroy()
    {
        OnPanelFaded -= Display;
    }

    public void LaunchDialogue()
    {
        _currentDialogue = TestDialogue; // pour l'instant test sans data
        dialogueText.text = "";

        // aller chercher le dialogue du current perso

        _currentLineIndex = 0;

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

            if (_currentLineIndex >= TestDialogue.Count) // fin du dialogue
                UIAnimations.Instance.FadeIn(dialogueCG, DialoguePanelFadeDuration, false);
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
