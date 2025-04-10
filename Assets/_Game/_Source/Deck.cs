using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : ACardHolder
{
    [SerializeField] private List<Card> _cardPrefabs;
    [SerializeField] private CardSetSO _cardSet;
    [SerializeField] private List<Pile> _piles;
    [SerializeField] private GameObject _cardPrefab;

    private bool _isMouseOver;
    [SerializeField] private SharedDeck _sharedDeck;

    [SerializeField] GameManager _gameManager;
    [SerializeField] CardManipulator _cardManipulator;

    private void Start()
    {
        _gameManager.InitCards(_cardPrefabs);
        InitGame();
    }
    public void InitGame()
    {
        if (_cardPrefabs.Count != 52)
        {
            Debug.LogError("Должно быть ровно 52 префаба карт!, а их " + Cards.Count);
            return;
        }

        _cards = _cardPrefabs;


        for (int i = 0; i < 52; i++)
        {
            int suit = i / 13;
            int value = i % 13;
            int spriteIndex = value;

            Sprite[] currentSuitSprites = suit switch
            {
                0 => _cardSet.FrontSprites1.ToArray(),
                1 => _cardSet.FrontSprites2.ToArray(),
                2 => _cardSet.FrontSprites3.ToArray(),
                3 => _cardSet.FrontSprites4.ToArray(),
                _ => throw new System.ArgumentOutOfRangeException()
            };

            if (spriteIndex >= currentSuitSprites.Length)
            {
                Debug.LogError($"Недостаточно спрайтов для масти {suit}");
                return;
            }

            _cards[i].Construct(
                currentSuitSprites[spriteIndex],
                _cardSet.BackSprite,
                suit,
                value,
                false,
                this
            );

            _cards[i].transform.position = transform.position;
        }

    }

    public void StartGame()
    {
        _cardManipulator.CAN_TOUCH_DECK = false;
        Shuffle();
        StartCoroutine(DistributeStartCards());
    }

    private void OnMouseUp()
    {
        if (_cardManipulator.CAN_TOUCH_DECK)
            CardToSharedDeck();
    }

    private void CardToSharedDeck()
    {
        if (_isMouseOver)
        {
            Debug.Log($"В колоде {Cards.Count} карт");
            if (Cards.Count > 0)
            {
                Cards[Cards.Count - 1].MoveAndFlip(_sharedDeck.GetCardPlace(), _sharedDeck, true);
            }
            else
            {
                _sharedDeck.ReturnAllToDeck();
            }
        }
    }

    private void OnMouseEnter()
    {
        _isMouseOver = true;
    }

    private void OnMouseExit()
    {
        _isMouseOver = false;
    }

    public void Shuffle()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            int r = Random.Range(i, Cards.Count);
            Card temp = Cards[i];
            Cards[i] = Cards[r];
            Cards[r] = temp;
        }
    }

    public IEnumerator DistributeStartCards()
    {
        for (int i = 0; i < _piles.Count - 1; i++)
        {
            for (int j = 0; j < _piles.Count - 1 - i; j++)
            {
                yield return new WaitForSeconds(0.1f);
                Cards[Cards.Count - 1].MoveTo(_piles[j].GetCardPlace(), _piles[j]);
            }
        }

        for (int i = 0; i < _piles.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Cards[Cards.Count - 1].MoveAndFlip(_piles[i].GetCardPlace(), _piles[i], true);
        }

        _cardManipulator.CAN_TOUCH_DECK = true;
    }
}
