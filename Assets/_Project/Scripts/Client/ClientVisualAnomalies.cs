using System.Collections.Generic;
using UnityEngine;
using PLIbox.Extensions;

public class ClientVisualAnomalies : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _sprite;

    [Header("Sprites")]
    [SerializeField] private List<Sprite> _visualList;

    public void SetupSprite(Sprite invalidSprite) => _sprite.sprite = invalidSprite ? invalidSprite : _visualList.GetRandomItem();
}
