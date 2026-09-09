using PLIbox.Extensions;
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

    private List<string> _currentDialogList;
    private bool _isFirstDialog = true;

    public void PlayDialog(List<string> invalidData)
    {
        //Define dialog data
        if(_currentDialogList == null)
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
}
