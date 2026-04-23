using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Banner : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        /*Debug.Log("Banner Hide_Banner : " + gameObject.name);
        AssignAdIds_CB.instance.HideBanner();*/
        //StartCoroutine(HideBanner());
    }

   /*IEnumerator HideBanner()
    {
        AssignAdIds_CB.instance.HideBanner();
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(HideBanner());
    }*/
}
