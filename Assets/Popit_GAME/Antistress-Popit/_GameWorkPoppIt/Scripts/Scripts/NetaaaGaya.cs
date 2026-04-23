using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetaaaGaya : MonoBehaviour
{
    public Popit2DManagerAntistress p2dm;

    //private void FixedUpdate()
    //{
    //    CheckConnectivity();
    //}
    //// Update is called once per frame
    public void CheckConnectivity()
    {
        if(Application.internetReachability != NetworkReachability.NotReachable)
        {
            gameObject.SetActive(false);
            p2dm.InternetKliay();
        }
    }
}
