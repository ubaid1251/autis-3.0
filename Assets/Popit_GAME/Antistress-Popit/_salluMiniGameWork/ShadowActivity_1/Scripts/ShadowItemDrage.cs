using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ShadowItemDrage : MonoBehaviour
{
    public int shapeId;

    public bool IsDrageServiceActive;
    [HideInInspector]
    public  bool IsMatchedItem;
    [HideInInspector]
    public GameObject matchedShapeObj;


    #region DrageItem

    Vector2 offset = new Vector3(0, 0.5f);
    Vector2 startposition;
    bool IsClick = false;

    private void OnMouseDown()
    {
        if(IsDrageServiceActive)
        {
            startposition = transform.position;
            IsClick = true;
            ShadowFindManager.Instance.GetDeActiveTutorialHand();
        }
    }

    private void OnMouseDrag()
    {
        if(IsClick)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos + offset;
        }
    }

    private void OnMouseUp()
    {
        if(IsClick)
        {
            if(IsMatchedItem && matchedShapeObj!=null)
            {
                transform.position = matchedShapeObj.transform.position;

                // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                ShadowFindManager.Instance.GetActiveCompleteDialoge();

            }
            else
            {
                transform.position = startposition;
            }
            IsClick = false;
        }
    }


    #endregion

    bool IsWrong = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject!=this.gameObject)
        {
          //  Debug.Log("suleman 0 ");
            if (collision.transform.GetComponent<ShadowItemDrage>())
            {
              //  Debug.Log("suleman 1 ");
                ShadowItemDrage shadowItemDrage = collision.transform.GetComponent<ShadowItemDrage>();
                if (shadowItemDrage.shapeId==shapeId)
                {
                   // DOTween.KillAll();
                    IsMatchedItem = true;
                    matchedShapeObj = collision.gameObject;
                }
                else if (shadowItemDrage.shapeId != shapeId)
                {
                    if(IsDrageServiceActive)
                    {
                        // Debug.Log("suleman 0 : " + collision.gameObject.name);
                        //transform.DORewind();
                        //transform.DOPunchScale(new Vector3(1, 1, 1), .25f);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsMatchedItem = false;
    }
}
