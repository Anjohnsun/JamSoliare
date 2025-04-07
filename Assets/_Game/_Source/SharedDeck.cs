using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SharedDeck : ACardHolder
{
    [SerializeField] private Deck _deck;

    public void ReturnAllToDeck()
    {
        for (int i = Cards.Count - 1; i >= 0; i--)
        {
            Cards[i].MoveAndFlip(_deck.GetCardPlace(), _deck, false);
        }
    }

    public override void AddCard(Card card)
    {
        base.AddCard(card);
        for (int i = 0; i < Cards.Count - 2; i++)
        {
            //Cards[i].IsOpenAndLocked = false;
        }
        // Cards[Cards.Count - 1].IsOpenAndLocked = true;
    }

    public override void RemoveCard(Card card)
    {
        base.RemoveCard(card);
        for (int i = 0; i < Cards.Count - 2; i++)
        {
            //     Cards[i].IsOpenAndLocked = false;
        }
        // Cards[Cards.Count - 1].IsOpenAndLocked = true;
    }
}
