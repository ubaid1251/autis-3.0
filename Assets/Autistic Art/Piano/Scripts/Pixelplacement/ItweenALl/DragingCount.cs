using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DragingCount : MonoBehaviour {

    private Vector3 screenPoint;
	Vector3 Initial_posoition;
    private Vector3 offset;
	public float moveAway=0f;
	public bool check=false;
	public GameObject clickOff=null;
	public GameObject clickOn = null;
	public Sprite[] spriteToChange;
	public bool changeSorting;
	public int sortingLayer;
	int initlsort;
	public bool wrong;
    private void OnMouseDown()
    {
        offset = gameObject.transform.position -
      Camera.main.ScreenToWorldPoint(
          new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        check = true;
        Initial_posoition = transform.position;
        if (clickOff != null)
        {
            clickOff.SetActive(false);
        }
        if (changeSorting)
        {
            initlsort = GetComponent<SpriteRenderer>().sortingOrder;
            GetComponent<SpriteRenderer>().sortingOrder = sortingLayer;
        }
        if (clickOn != null)
        {
            clickOn.SetActive(true);
        }
        if (spriteToChange.Length != 0)
        {
            GetComponent<SpriteRenderer>().sprite = spriteToChange[1];
        }
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + new Vector3(0, 0, offset.z);
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        check = false;
        transform.DOJump(Initial_posoition, 1, 1, .2f);
        if (spriteToChange.Length != 0)
        {
            GetComponent<SpriteRenderer>().sprite = spriteToChange[0];
        }
        if (clickOff != null)
        {
            clickOff.SetActive(true);
        }
        if (changeSorting)
            GetComponent<SpriteRenderer>().sortingOrder = initlsort;
        if (wrong)
        {
            wrong = false;
            iTween2.ShakeRotation(gameObject, iTween2.Hash("z", 30f, "time", .2f, "delay", .2f, "easetype", iTween2.EaseType.linear));
        }
        if (clickOn != null)
        {
            clickOn.SetActive(false);
        }
    }
}