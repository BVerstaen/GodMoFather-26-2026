using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefeatPanel : MonoBehaviour
{
    [SerializeField] private Sprite _onHumanEnter;
    [SerializeField] private Sprite _onMonsterEjected;

    [SerializeField] private Image _defeatImg;
    [SerializeField] private Animator _defeatAnimator;


    public void DisplayDefeatPanel(bool HumanEnter)
    {
        _defeatImg.sprite = HumanEnter ? _onHumanEnter : _onMonsterEjected;

        _defeatAnimator.SetTrigger("display");
    }
}
