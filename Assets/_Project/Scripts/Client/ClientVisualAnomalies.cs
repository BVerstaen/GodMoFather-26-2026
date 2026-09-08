using System.Collections.Generic;
using UnityEngine;
using PLIbox.Extensions;

public class ClientVisualAnomalies : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _sprite;

    [Header("Sprites")]
    [SerializeField] private Sprite _correctSprite;
    [SerializeField] private List<Sprite> _anomaliesSprite;


    public void SetupSprite(bool isAnomaly)
    {
        if(isAnomaly)
            _sprite.sprite = _anomaliesSprite.GetRandomItem();
        else
            _sprite.sprite = _correctSprite;
    }
}
