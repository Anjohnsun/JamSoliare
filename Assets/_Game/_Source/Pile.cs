using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pile : ACardHolder
{
    [SerializeField] private Vector2 _offset;

    public override Vector2 GetCardPlace()
    {
        return (Vector2)transform.position + _offset * Cards.Count;
    }

    public override bool CanAddCard(Card card)
    {
        if (Cards.Count == 0 && card.Value == 12)
            return true;
        else if (Cards.Count > 0 &&
        (card.Suit % 2 != Cards[Cards.Count - 1].Suit % 2) &&
        (card.Value == Cards[Cards.Count - 1].Value - 1) &&
        IsCorrectArea())
            return true;
        else return false;
    }

    public override void AddCard(Card card)
    {
        base.AddCard(card);
    }

    public override void RemoveCard(Card card)
    {
        Debug.Log($"{Cards.Count} in pile, now -1");
        int index = Cards.IndexOf(card);

        if (index > 0)
            if (!Cards[index - 1].IsInteractable)
                Cards[index - 1].Flip(true);
        base.RemoveCard(card);
    }


    private bool IsCorrectArea()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

        foreach (var hit in hits)
        {
            if (hit.transform.TryGetComponent<Card>(out Card card))
                return true;
        }
        return false;
    }
}
