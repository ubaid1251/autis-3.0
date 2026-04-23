using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draging_Asf : MonoBehaviour {

	Vector3 Initial_posoition, screenPoint;
	private Vector3 offset;
	bool check=false;
	public float xMin, xMax, yMin, yMax;
    public GameObject indicator;
    int initlsort;

    /*
    void OnMouseDown()
    {
        try
        {
            offset = gameObject.transform.position -
                    Camera.main.ScreenToWorldPoint(
                        new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            check = true;
            Initial_posoition = transform.position;
            if (indicator)
                indicator.SetActive(false);
        }
        catch (System.Exception ex)
        {
            print("Draging OnDown :" + ex);
        }
    }


    void OnMouseDrag()
    {
        try
        {

            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + new Vector3(0, 0, offset.z);
            transform.position = Vector3.Lerp(transform.position, curPosition, Time.deltaTime * 2.4f);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y, yMin, yMax), -2);
        }
        catch (System.Exception ex)
        {
            print("Draging_Asf OnDrag :" + ex);
        }
    }
    void OnMouseUp()
    {
        check = false;
    }
    */

    private void Update()
    {
        try
        {


            if (Input.GetMouseButtonDown(0))
            {
                offset = gameObject.transform.position -
                      Camera.main.ScreenToWorldPoint(
                          new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

                check = true;
                Initial_posoition = transform.position;
                if (indicator)
                    indicator.SetActive(false);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                //Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + new Vector3(0, 0, offset.z);

                //if (curPosition.x > xMin && curPosition.x < xMax &&
                //    curPosition.y > yMin && curPosition.y < yMax)
                //{

                transform.position = Vector3.Lerp(transform.position, curPosition, Time.deltaTime * 2.1f);
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y, yMin, yMax), -2);


                //}
            }

            if (Input.GetMouseButtonUp(0))
            {
                check = false;


            }


        }
        catch (System.Exception ex)
        {
            print("Draging OnDrag :" + ex);
        }


    }
}