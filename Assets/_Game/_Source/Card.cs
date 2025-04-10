using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private AudioSource _moveAudio;
    [SerializeField] private AudioSource _flipAudio;

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

        Flip(faceUp, false);
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
            _renderer.sortingOrder = _renderer.sortingOrder - 1000;

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

    public Card MoveTo(Vector2 to, ACardHolder newHolder, bool soundOn = true)
    {
        //move animation
        _renderer.sortingOrder += 10000;

        int futureOrder = newHolder.Cards.Count + 1;
        newHolder.AddCard(this);
        GetComponent<Collider2D>().enabled = false;
        transform.DOMove(new Vector3(to.x, to.y, -(newHolder.Cards.Count + 1)), 0.3f).OnComplete(() =>
        {
            GetComponent<Collider2D>().enabled = true;
            _renderer.sortingOrder = futureOrder;
            if (soundOn)
                _moveAudio.Play();
        });


        //change holder
        Holder.RemoveCard(this);
        _holder = newHolder;

        return this;
    }

    public void MoveAndFlip(Vector2 to, ACardHolder newHolder, bool faceUp, float duration = 0.3f, bool soundOn = true)
    {
        //move animation
        _renderer.sortingOrder = 1000;

        int futureOrder = newHolder.Cards.Count + 1;
        newHolder.AddCard(this);
        GetComponent<Collider2D>().enabled = false;
        transform.DOMove(new Vector3(to.x, to.y, -(newHolder.Cards.Count + 1)), duration).OnComplete(() =>
        {
            GetComponent<Collider2D>().enabled = faceUp;
            _renderer.sortingOrder = futureOrder;
            if (soundOn)
                _flipAudio.Play();
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
        _faceUp = faceUp;

        //change holder
        Holder.RemoveCard(this);
        _holder = newHolder;
    }

    public void MoveBackTo(Vector3 to)
    {
        _renderer.sortingOrder += 1000;
        GetComponent<Collider2D>().enabled = false;
        transform.DOMove(new Vector3(to.x, to.y, to.z), 0.3f).OnComplete(() =>
        {
            _renderer.sortingOrder -= 1000;
            GetComponent<Collider2D>().enabled = true;
        });

    }


    public void Flip(bool faceUp, bool soundOn = true)
    {
        _faceUp = faceUp;

        if (soundOn)
            _flipAudio.Play();

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


    /*    public void Flip(bool faceUp)
        {
            _faceUp = faceUp;

            transform.DOScaleX(0.03f, 0.2f).OnComplete(() =>
            {
                ChangeSprite(faceUp);
                transform.DOScaleX(1, 0.2f).OnComplete(() =>
                {
                    if (faceUp)
                        GetComponent<Collider2D>().enabled = true;
                    else
                        GetComponent<Collider2D>().enabled = false;
                });
            });

            //flip animation
            if (faceUp)
            {
                GetComponent<Collider2D>().enabled = true;
            }
            else
            {
                GetComponent<Collider2D>().enabled = false;
            }
        }


        public void ChangeSprite(bool faceUp)
        {
            if (faceUp)
            {
                _renderer.sprite = _frontSprite;
            }
            else
            {
                _renderer.sprite = _backSprite;
            }
        }*/
}
