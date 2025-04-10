using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int _baseFilled;
    [SerializeField] private Deck _deck;
    [SerializeField] private CardManipulator _cardManipulator;

    private List<Card> _allCards;

    private bool _isMenuPanelVisible;
    [SerializeField] private RectTransform _menuPanel;
    [SerializeField] private Vector2 _fromPosition1;
    [SerializeField] private Vector2 _toPosition1;

    [SerializeField] private Image _switchScenePanel;

    [SerializeField] private RectTransform _menuButton;
    [SerializeField] private Vector2 _fromPosition2;
    [SerializeField] private Vector2 _toPosition2;


    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _winSound;


    private IEnumerator Start()
    {
        _music.Play();
        _switchScenePanel.color = new Color(_switchScenePanel.color.r, _switchScenePanel.color.g, _switchScenePanel.color.b, 1);
        _menuButton.DOAnchorPos(_toPosition2, 0.6f);
        _isMenuPanelVisible = false;
        _menuPanel.DOAnchorPos(_fromPosition1, 0);

        _switchScenePanel.DOColor(new Color(_switchScenePanel.color.r, _switchScenePanel.color.g, _switchScenePanel.color.b, 0), 1.2f).
            OnComplete(() => _switchScenePanel.gameObject.SetActive(false));


        _baseFilled = 0;

        yield return new WaitForSeconds(0.8f);


        _menuButton.DOAnchorPos(_toPosition2, 0.6f);
        _deck.StartGame();

        yield return null;
    }

    public void InitCards(List<Card> cards)
    {
        _allCards = new List<Card>(cards);
    }

    public void FillBase()
    {
        _baseFilled++;
        if (_baseFilled == 4)
        {
            _music.Stop();
            _winSound.Play();
            ShowMenuPanel(true);
        }
    }

    public void ShowMenuPanel(bool value)
    {
        if (value)
        {
            _menuPanel.DOAnchorPos(_toPosition1, 0.5f);
            _menuButton.DOAnchorPos(_fromPosition2, 0.5f);
            _cardManipulator.CAN_TOUCH_MENU = false;
        }
        else
        {
            _menuPanel.DOAnchorPos(_fromPosition1, 0.5f);
            _menuButton.DOAnchorPos(_toPosition2, 0.5f);
            _cardManipulator.CAN_TOUCH_MENU = true;
        }
    }

    public void ReloadGame()
    {
        StartCoroutine(ReloadGameCor());
    }

    private IEnumerator ReloadGameCor()
    {
        /*        ShowMenuPanel(false);

                _deck.Cards.Clear();
                foreach (var card in _allCards)
                {
                    card.MoveAndFlip(_deck.GetCardPlace(), _deck, false, 0.2f, false);
                    Debug.Log(card.Holder.GetType().ToString());
                }

                yield return new WaitForSeconds(0.6f);

                _deck.StartGame();

                yield return new WaitForSeconds(1f);

                _music.Play();*/

        ShowMenuPanel(false);
        _menuButton.DOAnchorPos(_fromPosition2, 0.5f);

        _switchScenePanel.gameObject.SetActive(true);
        _switchScenePanel.DOColor(new Color(_switchScenePanel.color.r, _switchScenePanel.color.g, _switchScenePanel.color.b, 1), 0.6f);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
        yield return null;
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadMenuCor());
    }

    private IEnumerator LoadMenuCor()
    {
        _switchScenePanel.gameObject.SetActive(true);
        _switchScenePanel.DOColor(new Color(_switchScenePanel.color.r, _switchScenePanel.color.g, _switchScenePanel.color.b, 1), 0.6f);

        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(0);
        yield return null;
    }
}
