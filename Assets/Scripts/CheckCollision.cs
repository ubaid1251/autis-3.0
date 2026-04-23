using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    public static bool Colide=false;
    public static CheckCollision ins;
    public bool HaveObject = true;//, LastObject = false;//PlayParticle=false;
    //public GameObject ShapeNextPart, ShapePreviousPart;
    GameObject obj;
    void Start()
    {
        ins = this;
        Colide = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.name== "Pen" && gameObject.tag== "circle")
        {
            Colide = true;
            //if (LastObject)
            //{
            //  //  ShapePreviousPart.SetActive(false);
            //}
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Pen" && gameObject.tag == "circle")
        {
            obj = gameObject;
            GameController.ins.PlayParticle = true;
            Invoke("WaitForParticles", 0.2f);
        }
    }
    void WaitForParticles()
    {
        if (PlayerPrefs.GetInt("Playpar") == 0)
        {
            GameController.ins.RandomParticles(obj);
            PlayerPrefs.SetInt("Playpar", 1);
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Pen" && gameObject.tag == "circle")
        {
            GetComponent<BoxCollider>().enabled = false;
            GameController.ins.PlayParticle = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameController.ins.PlayParticle == false)
        {
            GameController.ins.OffParticles();
        }
    }
}
