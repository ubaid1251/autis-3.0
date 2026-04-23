using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MouseDirection : MonoBehaviour
{
    private Vector3 lastMousePosition;
    public Vector3[] Position;
    public float speed = 0.5f,wait,delay;
    public GameObject ColorObject;
    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }
    //int i;
    void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 mouseDirection = currentMousePosition - lastMousePosition;

        if (mouseDirection != Vector3.zero)
        {
            if (Mathf.Abs(mouseDirection.x) > Mathf.Abs(mouseDirection.y))
            {
                if (mouseDirection.x > 0)
                {
                    //  Debug.Log("Mouse is moving right");
                    //  transform.GetChild(0).localPosition = Position[0];
                    //i = 0;
                    Invoke("wait1", wait);
                   // transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, Position[0], lerpSpeed * Time.deltaTime);
                }
                else
                {
                    //   Debug.Log("Mouse is moving left");
                    //   transform.GetChild(0).localPosition = Position[1];
                 //   i = 1;
                    Invoke("wait1", wait);
                    //transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, Position[1], lerpSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (mouseDirection.y > 0)
                {
                    // Debug.Log("Mouse is moving up");
                    //   transform.GetChild(0).localPosition = Position[2];
                 //   i = 2;
                    Invoke("wait1", wait);
                   // transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, Position[2], lerpSpeed * Time.deltaTime);
                }
                else
                {
                    // Debug.Log("Mouse is moving down");
                    //   transform.GetChild(0).localPosition = Position[3];
                 //   i = 3;
                    Invoke("wait1", wait);
                    //transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, Position[3], lerpSpeed * Time.deltaTime);
                }
            }
        }

        lastMousePosition = currentMousePosition;
    }
    Vector3 pos;
    void wait1()
    {
        //    transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, Position[i], lerpSpeed * Time.deltaTime);
        //        ColorObject.transform.localPosition = Vector3.Lerp(ColorObject.transform.localPosition, Position[i], lerpSpeed * Time.deltaTime);
     //   ColorObject.transform.DOScale(new Vector3(3.1f, 2, 0), speed).SetEase(Ease.Linear);
    // ColorObject.transform.DOLocalMove((Position[i]), speed).SetEase(Ease.Linear).SetDelay(delay)
    //.OnComplete(() =>
    //{
    //    ColorObject.transform.DOLocalMove((Position[4]), speed).SetEase(Ease.Linear);
    //    //ColorObject.transform.DOScale(new Vector3(1.25f, 1, 0), speed).SetEase(Ease.Linear);
    //});

        ColorObject.transform.DOLocalMove((Position[0]), speed).SetEase(Ease.Linear)

    .OnComplete(() =>
   {
   ColorObject.transform.DOLocalMove((Position[1]), speed).SetEase(Ease.Linear)
           .OnComplete(() =>
           {
               ColorObject.transform.DOLocalMove((Position[2]), speed).SetEase(Ease.Linear).SetDelay(delay)
                          .OnComplete(() =>
                          {
                              ColorObject.transform.DOLocalMove((Position[3]), speed).SetEase(Ease.Linear);

                          });
           });
   });
    }
}
