using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBodyParts : MonoBehaviour
{
    public bool HaveParts = false;
    public GameObject Parts;
    public static OtherBodyParts ins;
    void Start()
    {
        ins = this;
    }
    public void OnRemainingParts()
    {
        if(HaveParts)
        {
         //   for(int i=0; i<Parts.Length;i++)
       //     {
                Parts.SetActive(true);
        //    }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
