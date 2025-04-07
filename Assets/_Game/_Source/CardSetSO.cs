using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCardSet", menuName = "SO/CardSet")]
public class CardSetSO : ScriptableObject
{
    [Header("Туз первый, дальше от 2 до Короля. Чередовать черные и красные!")]
    [SerializeField] private Sprite _backSprite;
    [SerializeField] private List<Sprite> _frontSprites1;
    [SerializeField] private List<Sprite> _frontSprites2;
    [SerializeField] private List<Sprite> _frontSprites3;
    [SerializeField] private List<Sprite> _frontSprites4;

    public Sprite BackSprite => _backSprite;
    public List<Sprite> FrontSprites1 => _frontSprites1;
    public List<Sprite> FrontSprites2 => _frontSprites2;
    public List<Sprite> FrontSprites3 => _frontSprites3;
    public List<Sprite> FrontSprites4 => _frontSprites4;
}
