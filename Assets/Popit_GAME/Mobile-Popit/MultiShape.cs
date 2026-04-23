using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShape : MonoBehaviour
{
    public GameObject[] shapes;

   public void ShowShape(int index)
    {
        HideAll();
        shapes[index].gameObject.SetActive(true);
    }

    void HideAll()
    {
        for(int i = 0; i< shapes.Length; i++)
        {
            shapes[i].SetActive(false);
        }
    }
}
