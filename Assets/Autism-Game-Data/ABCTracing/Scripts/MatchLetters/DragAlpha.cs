using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DragAlpha : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private RectTransform _rect;
    private Vector3 _pos;
    private GameObject _child;
    private Image _my;
    private Canvas _myCan;
    public RectTransform moveTo;

    private bool _completed = false;
    private bool _up = false;

    private void Start()
    {
        _my = GetComponent<Image>();
        _myCan = GetComponent<Canvas>();
        _rect = GetComponent<RectTransform>();
        _child = transform.GetChild(0).gameObject;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _up = false;
        _pos = _rect.anchoredPosition;
        _myCan.sortingOrder = 10;
        _child.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rect.anchoredPosition += eventData.delta;

        // Check if the distance to moveTo is less than 1
        float distance = Vector2.Distance(_rect.position, moveTo.position);
        // print(distance); // For debugging purposes
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _up = true;
        float distance = Vector2.Distance(_rect.position, moveTo.position);
        
        if (distance < 2.2f && !_completed)
        {
            //Vibration.Vibrate(50);
            if (MatchingManager.Instance.myS.enabled)
                MatchingManager.Instance.myS.PlayOneShot(MatchingManager.Instance.correct);
            if (MatchingManager.Instance.myS.enabled)
                MatchingManager.Instance.myS.PlayOneShot(MatchingManager.Instance.voiceOver[Random.Range(0,
                    MatchingManager.Instance.voiceOver.Length)]);
            // Logic for successful match
            _completed = true;
            _my.raycastTarget = false;
            _rect.DOMove(moveTo.position,.5f);
            IndicationHandler.Instance.timeSinceLastInput = 0;
            IndicationHandler.Instance.allIndi.Remove(transform.GetChild(1).GetComponent<RectTransform>());
            _rect.DOScale(0, .5f).OnComplete(() =>
            {
                MatchingManager.Instance.matched++;
                gameObject.SetActive(false);
                if (MatchingManager.Instance.size == MatchingManager.Instance.matched)
                {
                    MatchingManager.Instance.EndAnim();
                }
            });
        }
        else
        {
            // Handle drag end logic including animation and reset
            _child.SetActive(false);
            _my.raycastTarget = false;
            //Vibration.Vibrate(50);
            if (MatchingManager.Instance.myS.enabled)
                MatchingManager.Instance.myS.PlayOneShot(MatchingManager.Instance.wrong);
            _rect.DORotate(new Vector3(0, 0, -10), .1f).OnComplete(() =>
            {
                _rect.DORotate(new Vector3(0, 0, 10), .1f).OnComplete(() =>
                {
                    _rect.DORotate(new Vector3(0, 0, 0), .1f).OnComplete(() =>
                    {
                        _rect.DOAnchorPos(_pos, .5f).OnComplete(() =>
                        {
                            _myCan.sortingOrder = 5;
                            _my.raycastTarget = true;
                        });
                    });
                });
            });
        }
    }
}
