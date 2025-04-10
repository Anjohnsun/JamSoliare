using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image _switchScenePlane;

    [SerializeField] private RectTransform _credits;
    [SerializeField] private Vector2 _pos1;
    [SerializeField] private Vector2 _pos2;
    [SerializeField] private Vector2 _pos3;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        _switchScenePlane.color = new Color(_switchScenePlane.color.r, _switchScenePlane.color.g, _switchScenePlane.color.b, 1);
        _switchScenePlane.DOColor(new Color(_switchScenePlane.color.r, _switchScenePlane.color.g, _switchScenePlane.color.b, 0), 1.5f).
            OnComplete(() => _switchScenePlane.gameObject.SetActive(false));
        _credits.DOAnchorPos(_pos3, 0);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCor());
    }

    private IEnumerator StartGameCor()
    {
        _switchScenePlane.gameObject.SetActive(true);
        _switchScenePlane.DOColor(new Color(_switchScenePlane.color.r, _switchScenePlane.color.g, _switchScenePlane.color.b, 1), 0.8f);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    public void ShowCredits()
    {
        this.StopCoroutine(ShowCreditsCor());
        this.StartCoroutine(ShowCreditsCor());
    }

    private IEnumerator ShowCreditsCor()
    {
        _credits.DOAnchorPos(_pos1, 1.3f).SetEase(Ease.InOutFlash);

        yield return new WaitForSeconds(3.5f);

        _credits.DOAnchorPos(_pos2, 1.3f).SetEase(Ease.InOutFlash).OnComplete(() => _credits.DOAnchorPos(_pos3, 0));


    }
}
