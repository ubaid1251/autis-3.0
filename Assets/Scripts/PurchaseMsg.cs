using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PurchaseMsg : MonoBehaviour
{
    public static PurchaseMsg instance;
    Image myImg;
    
    void Start()
    {
        instance=this;
        myImg = GetComponent<Image>();
        myImg.enabled = false;
        myImg.color = new Color(1, 1, 1, 0);
    }

    public void ShowToast()
    {
        if (!myImg.enabled)
        {
            myImg.enabled = true;
            myImg.DOColor(new Color(1, 1, 1, 1), 1).OnComplete(() =>
            {
                myImg.DOColor(new Color(1, 1, 1, 0), 1).OnComplete(() =>
                {
                    myImg.enabled = false;
                });
            });
        }
    }
}
