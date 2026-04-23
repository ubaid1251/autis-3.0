using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBear : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public float xMin,xMax, yMin,yMax;
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) - offset;
        transform.position = curPosition;
        transform.position = new Vector3(Mathf.Clamp(transform.localPosition.x, xMin, xMax), Mathf.Clamp(transform.localPosition.y, yMin, yMax), 0);

    }
}
