using System.Collections.Generic;
using UnityEngine;

public class CardManipulator : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _cardStackOffset = 0.2f;

    private Camera _mainCamera;
    private Card _draggedCard;
    private List<Card> _draggedCards = new List<Card>();
    private Vector3 _offset;
    private List<Vector2> _lastPositions;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _lastPositions = new List<Vector2>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryStartDrag();

        if (Input.GetMouseButtonUp(0))
            TryDropCard();

        DragCards();
    }

    private void DragCards()
    {
        if (_draggedCards.Count == 0) return;

        Vector3 targetPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + _offset;
        targetPos.z = 0;

        for (int i = 0; i < _draggedCards.Count; i++)
        {
            Vector3 cardOffset = i * _cardStackOffset * Vector3.up;
            _draggedCards[i].transform.position = Vector3.Lerp(
                _draggedCards[i].transform.position,
                targetPos + cardOffset,
                _moveSpeed * Time.deltaTime
            );
        }
    }

    private void TryStartDrag()
    {
        Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        _lastPositions.Clear();

        if (hit.collider != null)
        {
            Card card;
            if (hit.collider.TryGetComponent<Card>(out card))
            {
                Debug.Log($"Dragged card: suit {card.Suit} value {card.Value}");
                if (card != null && card.IsInteractable)
                {
                    _draggedCard = card;
                    _offset = (Vector2)_draggedCard.transform.position - mousePos;

                    _draggedCards = card.Holder.GetCardsAbove(card);

                    foreach (var c in _draggedCards)
                    {
                        _lastPositions.Add(c.transform.position);
                        c.SetIsDragged(true);
                    }
                }
            }
        }
    }

    private void TryDropCard()
    {
        if (_draggedCards.Count == 0) return;
        Debug.Log($"Dragged card: suit {_draggedCards[0].Suit} value {_draggedCards[0].Value}");


        Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

        ACardHolder targetHolder = null;

        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                var holder = hit.collider.GetComponent<ACardHolder>();
                if (holder != null && holder.CanAddCard(_draggedCards[0]))
                {
                    targetHolder = holder;
                    break;
                }
            }
        }

        if (targetHolder != null)
        {
            for (int i = 0; i < _draggedCards.Count; i++)
            {
                _draggedCards[i].MoveTo(targetHolder.GetCardPlace(), targetHolder);
            }
        }
        else
        {
            for (int i = 0; i < _draggedCards.Count; i++)
            {
                _draggedCards[i].MoveBackTo(_lastPositions[i]);
            }
        }

        foreach (var card in _draggedCards)
        {
            card.SetIsDragged(false);
        }

        _draggedCards.Clear();
        _draggedCard = null;
    }
}