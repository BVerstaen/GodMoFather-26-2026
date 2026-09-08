using System.Collections.Generic;
using UnityEngine;
using PLIbox.Extensions;

public class ClientVisualAnomalies : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _sprite;

    [Header("Sprites")]
    [SerializeField] private VisualDifferencesSO _visualDiff;

    public void SetupSprite(bool isAnomaly) => _sprite.sprite = isAnomaly ? _visualDiff.AnomaliesSprite.GetRandomItem() : _visualDiff.CorrectSprite;
}
