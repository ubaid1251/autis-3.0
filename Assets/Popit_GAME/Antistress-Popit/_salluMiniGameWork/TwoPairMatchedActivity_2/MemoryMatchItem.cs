using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryMatchItem : MonoBehaviour
{

    public int shapeId;
    public static GameObject matchedShapeObj;


    #region DrageItem
    bool IsClick = false;

    private void OnMouseDown()
    {
        IsClick = true;
        if(MemoryMatchItem.matchedShapeObj==null)
        {
            matchedShapeObj = this.gameObject;

        }
        else if(matchedShapeObj!=null && matchedShapeObj!=this.gameObject)
        {
            if(matchedShapeObj.GetComponent<MemoryMatchItem>().shapeId==shapeId)
            {
                DestroyImmediate(matchedShapeObj);
                DestroyImmediate(this.gameObject);
                MatchSameShapeManager.Instance.CheckISAll_ItemDestoryToCompleteMiniGame();
            }
            else
            {
                matchedShapeObj = this.gameObject;
            }

        }
    }

    #endregion

}
