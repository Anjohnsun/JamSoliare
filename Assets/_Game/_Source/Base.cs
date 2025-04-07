using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : ACardHolder
{
    public bool IsFull => Cards.Count == 13;

    public override void RemoveCard(Card card)
    {
        throw new System.Exception("Card cannot be removed from Base");
    }

    public override void AddCard(Card card)
    {
        base.AddCard(card);
        card.GetComponent<Collider2D>().enabled = false;
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
