using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destbalon : MonoBehaviour
{
  
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Ballon")
        {
            Destroy(collision.transform.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
