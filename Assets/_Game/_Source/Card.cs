using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer _renderer;
    private Sprite _frontSprite;
    private Sprite _backSprite;


    private int _suit;
    private int _value;

    private bool _isDragged;
    private bool _faceUp;
    private ACardHolder _holder;

    public int Suit => _suit;
    public int Value => _value;

    // public bool IsOpenAndLocked;
    public bool IsInteractable => _faceUp; // && !IsOpenAndLocked; 
    public ACardHolder Holder => _holder;


    public void Construct(Sprite frontSprite, Sprite backSprite, int suit, int value, bool faceUp, ACardHolder holder)
    {
        _frontSprite = frontSprite;
        _backSprite = backSprite;
        _suit = suit;
        _value = value;
        _faceUp = faceUp;
        _holder = holder;

        Flip(faceUp);
        _renderer.sortingOrder = Holder.Cards.Count;
    }

    public void SetIsDragged(bool v)
    {
        _isDragged = v;
        if (v)
        {
            GetComponent<Collider2D>().enabled = false;
            _renderer.sortingOrder = 1000 + _renderer.sortingOrder;
        }
        else
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnMouseEnter()
    {
        //check & animate
        if (Holder.Cards[Holder.Cards.Count - 1].Equals(this))
        {
            //animate
        }
    }
    private void OnMouseExit()
    {
        //check & animate
        if (Holder.Cards[Holder.Cards.Count - 1].Equals(this))
        {
            //animate
        }
    }

    public Card MoveTo(Vector2 to, ACardHolder newHolder)
    {
        //move animation
        _renderer.sortingOrder = 1000;

        int futureOrder = newHolder.Cards.Count+1;
        newHolder.AddCard(this);
        GetComponent<Collider2D>().enabled = false;
        transform.DOMove(new Vector3(to.x, to.y, -(newHolder.Cards.Count + 1)), 0.3f).OnComplete(() =>
        {
            GetComponent<Collider2D>().enabled = true;
            _renderer.sortingOrder = futureOrder;

        });


        //change holder
        Holder.RemoveCard(this);
        _holder = newHolder;

        return this;
    }

    public void MoveAndFlip(Vector2 to, ACardHolder newHolder, bool faceUp)
    {
        //move animation
        _renderer.sortingOrder = 1000;

        int futureOrder = newHolder.Cards.Count+1;
        newHolder.AddCard(this);
        GetComponent<Collider2D>().enabled = false;
        transform.DOMove(new Vector3(to.x, to.y, -(newHolder.Cards.Count + 1)), 0.3f).OnComplete(() =>
        {
            GetComponent<Collider2D>().enabled = faceUp;
            _renderer.sortingOrder = futureOrder;
        });

        //flip animation
        if (faceUp)
        {
            _renderer.sprite = _frontSprite;
        }
        else
        {
            _renderer.sprite = _backSprite;
        }


        //change holder
        Holder.RemoveCard(this);
        _holder = newHolder;
    }

    public void MoveBackTo(Vector2 to)
    {
        _renderer.sortingOrder = 1000;
        GetComponent<Collider2D>().enabled = false;
        transform.DOMove(new Vector3(to.x, to.y, -_holder.Cards.Count), 0.3f).OnComplete(() =>
        {
            _renderer.sortingOrder = Holder.Cards.Count;
            GetComponent<Collider2D>().enabled = true; _renderer.sortingOrder = Holder.Cards.Count;
        });

    }


    public void Flip(bool faceUp)
    {
        _faceUp = faceUp;

        //flip animation
        if (faceUp)
        {
            _renderer.sprite = _frontSprite;
            GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            _renderer.sprite = _backSprite;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
