using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrigerPic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "PuzlePic" && gameObject.name == "EmptyFrame")
        {
            //print(collision.name + "Puzzleno");
            int puzzleNumber;
            if (int.TryParse(collision.name, out puzzleNumber))
            {
                PlayerPrefs.SetInt("MyPuzzle", puzzleNumber);
                //print("Saved Puzzle Number: " + puzzleNumber);
            }
            collision.GetComponent<Button>().interactable = true;
        }
        if (collision.tag == "PuzlePic" && gameObject.name == "OnFrame")
        {
            //print("onCOllide");
            collision.GetComponent<Button>().interactable = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PuzlePic" && gameObject.name == "EmptyFrame")
        {
            //print("22222yesCOllide");
            collision.GetComponent<Button>().interactable = true;
        }
        if (collision.tag == "PuzlePic" && gameObject.name == "OnFrame")
        {
            //print("onCOllide");
            collision.GetComponent<Button>().interactable = false;
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "PuzlePic" && gameObject.name == "EmptyFrame")
    //    {
    //        print("22222yesCOllide");
    //        //collision.GetComponent<Button>().interactable = true;
    //    }
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "PuzlePic" && gameObject.name == "EmptyFrame")
    //    {
    //        print("22222222442yesCOllide");
    //        //collision.GetComponent<Button>().interactable = true;
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
