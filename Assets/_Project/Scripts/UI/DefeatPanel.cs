using TMPro;
using UnityEngine;

public class DefeatPanel : MonoBehaviour
{
    [SerializeField] private string _onHumanEnterText;
    [SerializeField] private string _onMonsterEjectedText;

    [SerializeField] private TextMeshProUGUI _defeatText;
    [SerializeField] private Animator _defeatAnimator;


    public void DisplayDefeatPanel(bool HumanEnter)
    {
        _defeatText.text = HumanEnter ? _onHumanEnterText : _onMonsterEjectedText;

        _defeatAnimator.SetTrigger("display");
    }
}
