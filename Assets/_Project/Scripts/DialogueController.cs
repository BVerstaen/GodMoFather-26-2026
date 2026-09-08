using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float LetterGapTime;
    [SerializeField] private List<string> TestDialogue = new List<string>(); // test

    private Coroutine _currentCoroutine;
    private int _currentLineIndex;
    private List<string> _currentDialogue = new List<string>();

    public void LaunchDialogue()
    {
        _currentDialogue = TestDialogue; // pour l'instant test sans data

        // aller chercher le dialogue du current perso

        _currentLineIndex = 0;

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
            _currentCoroutine = StartCoroutine(DisplayLine(_currentDialogue[_currentLineIndex]));
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
