using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : ACardHolder
{
    [SerializeField] GameManager _gameManager;
    public bool IsFull => Cards.Count == 13;
    
    public override void AddCard(Card card)
    {
        base.AddCard(card);
        card.GetComponent<Collider2D>().enabled = false;

        if (Cards.Count == 13)
            _gameManager.FillBase();
    }

    public override List<Card> GetCardsAbove(Card card)
    {
        return null;
    }

    public override bool CanAddCard(Card card)
    {

        if (Cards.Count == 0 && card.Value == 0)
            return true;
        else if ((Cards.Count > 0) && (card.Suit == Cards[0].Suit) && (card.Value == Cards[Cards.Count - 1].Value + 1))
            return true;
        else return false;
    }
}
