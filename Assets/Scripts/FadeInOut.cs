using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FadeInOut : MonoBehaviour
{
    public static FadeInOut ins;
    public bool Isfade = true;
    private void Awake()
    {
        ins = this;
        Isfade = true;
    }
    void Start()
    {
        StartCoroutine(FadingObj());
    }
    IEnumerator FadingObj()
    {
        yield return null;
        GetComponent<SpriteRenderer>().DOFade(1, 0.8f)
         .OnComplete(() =>
         {
             GetComponent<SpriteRenderer>().DOFade(0, 0.8f)
                      .OnComplete(() =>
                      {
                          if (Isfade == true)
                          {
                              StartCoroutine(FadingObj());
                          }
                      });
         });
    }
    // Update is called once per frame
    public void FadingObjStop()
    {
        StopCoroutine("FadingObj");
        Isfade = false;
        gameObject.SetActive(false);

    }


}
