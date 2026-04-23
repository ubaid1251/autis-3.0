using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//using DG.Tweening;

public class DragDropObjects : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler

{

    private Canvas canvas;

    private CanvasGroup canvasgroup;
    private RectTransform rectTransform;
    public GameObject[] img;
    GameObject parent;
    //bool picked = false;
    public bool canMoveInYOnly = false;
    public void Start()

    {
        parent = transform.parent.gameObject;
        GameObject obj = gameObject;

        if (GetComponent<Rigidbody2D>() == null)
        {

            gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        }
        if (GetComponent<Collider2D>() != null)

        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        if (gameObject.tag != "Eat")

        {
            if (GetComponent<BoxCollider2D>() != null)

            {
                gameObject.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    private void Awake()

    {
        if (GetComponent<CanvasGroup>() == null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }

        if (canvas == null)
        {
            GameObject can = GameObject.Find("StartGameplayCanvas");
            canvas = can.GetComponent<Canvas>();
        }

        rectTransform = GetComponent<RectTransform>();
        canvasgroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (img.Length > 0)
        {
            for (int z = 0; z < img.Length; z++)
            {
                img[z].transform.gameObject.SetActive(true);
            }
        }
    }
    float lLimit = 100, rLimit = 300;//change it according to r scene
    float yFixedPos = 200;


    public void OnDrag(PointerEventData eventData)
    {
        if (canMoveInYOnly)
        {
            Vector2 pos = rectTransform.anchoredPosition;
            pos += eventData.delta / canvas.scaleFactor;
            if ((pos.y > -267&& (pos.y < 120)))
            {
                rectTransform.anchoredPosition = new Vector2(189.9998f, pos.y);
            }
  
        }
        else
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            if (((rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor).x > lLimit && (rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor).x < rLimit) &&
            ((rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor).y == yFixedPos))
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }
        ////if (gameObject.name == "Lipstick")
        ////{
        ////    ChangeTool();
        ////}
        ////else if (gameObject.name == "BlushOn")
        ////{
        ////    ChangeTool2();
        ////}
        ////else if (gameObject.name == "Mascara")
        ////{
        ////    ChangeTool3();
        ////}
    }

    //void ChangeTool()
    //{
    //    if (GirlMakeupSelected.MakeUpCount == 1)
    //    {
    //        GirlMakeupSelected.ins.PlayItemPartice(new Vector3(1.05f, -0.268f, 0), 0.4912f);
    //        GirlMakeupSelected.MakeUpCount = 0;
    //    }
    // }
    //void ChangeTool2()
    //{
    //    if (GirlMakeupSelected.MakeUpCount == 2)
    //    {
    //        GirlMakeupSelected.ins.PlayItemPartice(new Vector3(0.48f, -0.015f, 0), 0.4912f);
    //        GirlMakeupSelected.ins.PlayItemPartice2(new Vector3(1.544f, -0.015f, 0), 0.4912f);
    //        GirlMakeupSelected.MakeUpCount = 0;
    //    }
    //}
    //void ChangeTool3()
    //{
    //    if (GirlMakeupSelected.MakeUpCount == 3)
    //    {
    //        GirlMakeupSelected.ins.PlayItemPartice(new Vector3(0.454f, 0.909f, 0), 0.4912f);
    //        GirlMakeupSelected.ins.PlayItemPartice2(new Vector3(1.631f, 0.909f, 0), 0.4912f);
    //        GirlMakeupSelected.MakeUpCount = 0;
    //    }
    //}
    ////void WaitTool()
    //{
    //    EyeLash.ins.Scrollers[1].SetActive(true);
    //    EyeLash.ins.TopLines.SetActive(true);
    //    FindObjectOfType<ScratchCard>().ChangeTroughBtn();
    //}



    public void OnEndDrag(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Rigidbody2D>() != null)

        {
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        if (img.Length > 0)
        {
            for (int z = 0; z < img.Length; z++)
            {
                img[z].transform.gameObject.SetActive(false);
            }
        }
        canvasgroup.alpha = 1f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //picked = false;
        //SoundManager.instance.PlayEffect_Instance(6);
        if (gameObject.GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            //    gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (gameObject.tag != "Eat" || gameObject.name != "Basket")

        {
            if (gameObject.GetComponent<Collider2D>() != null)

            {
                gameObject.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        // SoundManager.instance.PlayEffect_Instance(13);
        //picked = true;
        if (gameObject.GetComponent<Collider2D>() != null)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        if (GetComponent<Rigidbody2D>() == null && gameObject.name != "Robot")
        {
            gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        Object.Destroy(GetComponent<Rigidbody2D>());
    }
    public void PlaySoundLoop(int num)
    {
     //   SoundManager.instance.PlayEffect_Loop(num);
    }
    public void StopSoundLoop(int num)
    {
       // SoundManager.instance.StopEffect(num);
    }
    private void Update()
    {
    
    }

}
