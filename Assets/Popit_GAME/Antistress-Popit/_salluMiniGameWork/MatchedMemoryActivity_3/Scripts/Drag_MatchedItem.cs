using Antistress;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_MatchedItem : MonoBehaviour
{

    public int shapeId;
    public bool isLeftSidedObj;
    public static GameObject matchedShapeObj;
    public LineRenderer lineRenderer;
    public Drag_MatchedItem connectedObject;

    private void Start()
    {
        lineRenderer = transform.GetChild(0).GetComponent<LineRenderer>();
    }

    #region DrageItem

    public static bool IsClick = false;
    public bool isIsDragingStart = false;
    public bool IsMouseOverlap;

    private void OnMouseDown()
    {
        IsClick = true;
        isIsDragingStart = false;
        MatchMemoryShapeManager.Instance.GetDeActiveTutorialHand();
        if (matchedShapeObj == null && connectedObject == null)
        {
            matchedShapeObj = this.gameObject;
            GetActiveHighlighter_CircleLine();
            GetActive_LineAndPrepare();
        }
        if (connectedObject != null)
        {
            Debug.Log("Connect not null");
            //reset privious selected circle
            connectedObject.GetComponent<Drag_MatchedItem>().RemovePreviousConnection();
            connectedObject = null;

            matchedShapeObj = this.gameObject;
            GetActiveHighlighter_CircleLine();
            GetActive_LineAndPrepare();
        }
    }

    private void OnMouseEnter()
    {
        if (IsClick)
        {
           // Debug.Log(IsClick);
            if (connectedObject != null)
            {
               // Debug.Log("Connect not null : OnMouseEnter");
                //reset privious selected circle
                connectedObject.GetComponent<Drag_MatchedItem>().RemovePreviousConnection();
                //matchedShapeObj = this.gameObject;
                //GetActiveHighlighter_CircleLine();
                //GetActive_LineAndPrepare();
            }
            if (matchedShapeObj != null && matchedShapeObj.GetComponent<Drag_MatchedItem>().isIsDragingStart && matchedShapeObj != this.gameObject)
            {
                GetActiveHighlighter_CircleLine();
                GetDeActive_Line();

                matchedShapeObj.GetComponent<Drag_MatchedItem>().IsMouseOverlap = true;
                matchedShapeObj.GetComponent<Drag_MatchedItem>().LineSet(matchedShapeObj.transform.position, transform.position);
                CheckIsNextShapeIsSameFound(matchedShapeObj, this.gameObject);
            }
        }

    }

    private void OnMouseDrag()
    {
        if (IsClick)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            lineRenderer.SetPosition(1, mousePos);
            isIsDragingStart = true;
        }
    }

    private void OnMouseUp()
    {
        if (isIsDragingStart && connectedObject == null)
        {
            GetDeActive_Line();
        }

        isIsDragingStart = false;
        IsMouseOverlap = false;
        matchedShapeObj = null;
        IsClick = false;
      //  Debug.Log(this.gameObject.name);
    }
    private void DisableLineRenderer()
    {
        if (isIsDragingStart && connectedObject == null)
        {
            GetDeActive_Line();
        }

        isIsDragingStart = false;
        IsMouseOverlap = false;
        matchedShapeObj = null;
        IsClick = false;
    }


    #endregion

    #region Compare Fun AND Heighliter fun

    public void CheckIsNextShapeIsSameFound(GameObject previousSelectedObj, GameObject nextSelectShapeObj)
    {
        Color GoGreen = Color.green;
        Color GoRed = Color.red;

        Drag_MatchedItem preVDragScript = previousSelectedObj.GetComponent<Drag_MatchedItem>();
        Drag_MatchedItem nextDragScript = nextSelectShapeObj.GetComponent<Drag_MatchedItem>();

        if (previousSelectedObj.GetComponent<Drag_MatchedItem>().shapeId == nextSelectShapeObj.GetComponent<Drag_MatchedItem>().shapeId)
        {

            SetShapeColor_OnSelectedDecision(previousSelectedObj, GoGreen);
            SetShapeColor_OnSelectedDecision(nextSelectShapeObj, GoGreen);

            //linking
            previousSelectedObj.GetComponent<Drag_MatchedItem>().connectedObject = nextDragScript;
            nextDragScript.GetComponent<Drag_MatchedItem>().connectedObject = preVDragScript;

            //dective click : dective collider to avoid again click
            previousSelectedObj.GetComponent<Collider2D>().enabled = false;
            nextSelectShapeObj.GetComponent<Collider2D>().enabled = false;

            // effects on correct and complete check to level complete 
            MatchMemoryShapeManager.Instance.SpawnStarBrustEffect(previousSelectedObj.transform.position);
            MatchMemoryShapeManager.Instance.SpawnStarBrustEffect(nextSelectShapeObj.transform.position);

            DisableLineRenderer();
            MatchMemoryShapeManager.correnlineConnectCounter++;
            MatchMemoryShapeManager.Instance.CheckAllLineGreenActive();
            SoundManager.instance.PlayEffect_Instance(2);
        }
        else
        {
            //set color 
            SetShapeColor_OnSelectedDecision(previousSelectedObj, GoRed);
            SetShapeColor_OnSelectedDecision(nextSelectShapeObj, GoRed);

            //linking
            previousSelectedObj.GetComponent<Drag_MatchedItem>().connectedObject = nextDragScript;
            nextDragScript.GetComponent<Drag_MatchedItem>().connectedObject = preVDragScript;

            SoundManager.instance.PlayEffect_Instance(18);

        }
    }

    void SetShapeColor_OnSelectedDecision(GameObject shapeObj, Color newcolor)
    {
        shapeObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = newcolor;
        shapeObj.GetComponent<Drag_MatchedItem>().lineRenderer.material.color = newcolor;
    }

    public void GetActiveHighlighter_CircleLine()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void GetDeActiveHighlighter_CircleLine()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void RemovePreviousConnection()
    {
        GetDeActiveHighlighter_CircleLine();
        GetDeActive_Line();
        connectedObject = null;
    }

    #endregion

    #region Line Fun

    public void GetActive_LineAndPrepare()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    public void GetDeActive_Line()
    {
        lineRenderer.enabled = false;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    public void LineSet(Vector3 Pos1, Vector3 Pos2)
    {
        lineRenderer.SetPosition(0, Pos1);
        lineRenderer.SetPosition(1, Pos2);
    }

    #endregion

}