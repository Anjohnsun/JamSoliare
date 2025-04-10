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
            if (i == Cards.Count - 1)
            {
                Cards[i].MoveAndFlip(_deck.GetCardPlace(), _deck, false);
            }
            else
            {
                Cards[i].MoveAndFlip(_deck.GetCardPlace(), _deck, false, 0.3f, false);
            }
        }
    }
}
