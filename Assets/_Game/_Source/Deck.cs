using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : ACardHolder
{
    [SerializeField] private CardSetSO _cardSet;
    [SerializeField] private List<Pile> _piles;
    [SerializeField] private GameObject _cardPrefab;

    private bool _isMouseOver;
    [SerializeField] private SharedDeck _sharedDeck;

    private void Start()
    {
        for (int i = 0; i < 13; i++)
        {
            Card card = Instantiate(_cardPrefab, transform.position, Quaternion.identity).GetComponent<Card>();
            card.Construct(_cardSet.FrontSprites1[i], _cardSet.BackSprite, 0, i, false, this);
            Cards.Add(card);
        }
        for (int i = 0; i < 13; i++)
        {
            Card card = Instantiate(_cardPrefab, transform.position, Quaternion.identity).GetComponent<Card>();
            card.Construct(_cardSet.FrontSprites2[i], _cardSet.BackSprite, 1, i, false, this);
            Cards.Add(card);
        }
        for (int i = 0; i < 13; i++)
        {
            Card card = Instantiate(_cardPrefab, transform.position, Quaternion.identity).GetComponent<Card>();
            card.Construct(_cardSet.FrontSprites3[i], _cardSet.BackSprite, 2, i, false, this);
            Cards.Add(card);
        }
        for (int i = 0; i < 13; i++)
        {
            Card card = Instantiate(_cardPrefab, transform.position, Quaternion.identity).GetComponent<Card>();
            card.Construct(_cardSet.FrontSprites4[i], _cardSet.BackSprite, 3, i, false, this);
            Cards.Add(card);
        }

        Shuffle();
        DistributeStartCards();
    }

    private void OnMouseUp()
    {
        CardToSharedDeck();
    }

    private void CardToSharedDeck()
    {
        if (_isMouseOver)
        {
            Debug.Log($"В колоде {Cards.Count} карт");
            if (Cards.Count > 0)
            {
                Cards[Cards.Count - 1].MoveTo(_sharedDeck.GetCardPlace(), _sharedDeck).Flip(true);
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

    public void DistributeStartCards()
    {
        Debug.Log("Piles: " + _piles.Count);
        for (int i = 0; i < _piles.Count - 1; i++)
        {
            for (int j = 0; j < _piles.Count - 1 - i; j++)
            {
                Cards[Cards.Count - 1].MoveTo(_piles[j].GetCardPlace(), _piles[j]);
            }
        }

        for (int i = 0; i < _piles.Count; i++)
        {
            Cards[Cards.Count - 1].MoveTo(_piles[i].GetCardPlace(), _piles[i]).Flip(true);
        }
    }
}
