using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Balloon : MonoBehaviour
{
    RectTransform myRect;
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(PopBalloon);
        myRect = GetComponent<RectTransform>();
        float duration = Random.Range(3f, 4f);
        myRect.DOAnchorPosY(900, duration).SetEase(Ease.Linear).OnComplete(()=>
        {
            myRect.DOKill(false);
            Destroy(gameObject);
        });
    }

    void PopBalloon()
    {
        myRect.DOKill(false);
        SoundManager.instance.PlayEffect_Instance(9);
        BallonManager.Instance.PopBalloon(this.gameObject);
    }
   
}
