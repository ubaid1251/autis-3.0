using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MakeShapeInstantiate : MonoBehaviour
{
    GameObject Obj;
    
    public GameObject ParentObj,PosObj;
    void Start()
    {
        Obj = GameController.ins.ActiveShape;
      //  PlayerPrefs.SetInt("BuyShape", PlayerPrefs.GetInt("BuyShape") + 1);
        Invoke("wait1", 1.5f);
    }

    // Update is called once per frame
    void wait1()
    {
        transform.GetChild(1).gameObject.SetActive(true);
      GameObject instObj= Instantiate(Obj, PosObj.transform.position, Quaternion.identity, ParentObj.transform);

        instObj.transform.localScale = new Vector3(100, 100, 100);
        if(instObj.tag=="Spaceship")
        {
            instObj.transform.localPosition = new Vector3(0, -28, 0);
        }
        else if (instObj.tag == "Horse"|| instObj.tag == "Apple")
        {
            instObj.transform.localPosition = new Vector3(0, -92, 0);
        }
        else if (instObj.tag == "Carrot")
        {
            instObj.transform.localScale = new Vector3(85, 85, 85); 
        }
        else if (instObj.tag == "Drumstick")
        {
            instObj.transform.localPosition = new Vector3(20, 30, 0);
            instObj.transform.localScale = new Vector3(115, 115, 115);
        }
        else if (instObj.tag == "Lemon")
        {
            instObj.transform.localPosition = new Vector3(0, -85, 0);
        }
        else if (instObj.tag == "Spacess")
        {
            instObj.transform.localPosition = new Vector3(10, 2.4f, 0);
        }
        else if (instObj.tag == "PineApple")
        {
            instObj.transform.localPosition = new Vector3(0, -110f, 0);
        }
        //else if (instObj.tag == "Apple")
        //{
        //    instObj.transform.localPosition = new Vector3(0, -80, 0);
        //}
        // instObj.AddComponent<SortingGroup>();
        // instObj.GetComponent<SortingGroup>().sortingLayerName = "Foreground";
        instObj.GetComponent<SortingGroup>().sortingOrder = 500;
    }
}
