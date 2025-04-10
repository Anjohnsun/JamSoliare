using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACardHolder : MonoBehaviour
{
    protected List<Card> _cards;

    public List<Card> Cards => _cards;

    private void Awake()
    {
        _cards = new List<Card>();
    }
    public virtual void AddCard(Card card)
    {
        Cards.Add(card);
    }
    public virtual void RemoveCard(Card card)
    {
        Cards.Remove(card);
    }
    public virtual Vector2 GetCardPlace()
    {
        return transform.position;
    }

    public virtual bool CanAddCard(Card card)
    {
        return false;
    }

    public virtual List<Card> GetCardsAbove(Card card)
    {
        List<Card> result = new List<Card>();
        int index = Cards.IndexOf(card);

        if (index >= 0)
        {
            for (int i = index; i < Cards.Count; i++)
            {
                result.Add(Cards[i]);
            }
        }

        return result;
    }
}
