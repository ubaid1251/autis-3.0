using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    public GameObject ScrollBar,nxtbtn,prevbtn,gamePlay,ScrollerObj;
    float ScrolPos = 0;
    float[] pos;
    int PosVal=0;
    public GameObject[] AllObjects;
    public static Swipe ins;
    void Start()
    {
        ins = this;
       
        if (PlayerPrefs.GetInt("Purchased") == 1)
        {
            for (int i = 6; i < AllObjects.Length; i++)
            {
                AllObjects[i].GetComponent<Button>().enabled = true;
            }
        }
    }
    public void next()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        if (PosVal < pos.Length - 1)
        {
            PosVal += 1;
            ScrolPos = pos[PosVal];
            prevbtn.GetComponent<Button>().interactable = true;

        }
        else
        {
            nxtbtn.GetComponent<Button>().interactable = false;
        }
        for (int i = 0; i < AllObjects.Length; i++)
        {
            AllObjects[i].GetComponent<Button>().interactable = false;
        }
    }
    public void prev()
    {
        SoundManager.instance.PlayEffect_Instance(4);

        if (PosVal >0)
        {
            PosVal -= 1;
            ScrolPos = pos[PosVal];
            nxtbtn.GetComponent<Button>().interactable = true;
        }
        else
        {
            prevbtn.GetComponent<Button>().interactable = false;
        }
        for (int i = 0; i < AllObjects.Length; i++)
        {
            AllObjects[i].GetComponent<Button>().interactable = false;
        }
    }
    public void PuzzleNum(int num)
    {
        SoundManager.instance.PlayEffect_Instance(5);
        PlayerPrefs.SetInt("MyPuzzle", num);
        gamePlay.SetActive(true);
        ScrollerObj.SetActive(false);
    }
    public void PlayPuzzleNum()
    {
        SoundManager.instance.PlayEffect_Instance(5);
        //PlayerPrefs.SetInt("MyPuzzle",1);
        gamePlay.SetActive(true);
        ScrollerObj.SetActive(false);
    }
    void Update()
    {
        pos = new float[transform.childCount];
        float dis = 1f / (pos.Length - 1f);
        for(int i=0; i<pos.Length;i++)
        {
            pos[i] = dis * i;
        }

        if (Input.GetMouseButton(0))
        {
            ScrolPos = ScrollBar.GetComponent<Scrollbar>().value;

        }
        else 
        {
        for(int i=0;i<pos.Length;i++)
            {
                if(ScrolPos<pos[i]+(dis/2)&&ScrolPos>pos[i]-(dis/2))
                {
                    ScrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(ScrollBar.GetComponent<Scrollbar>().value,pos[i],0.15f);
                    PosVal = i;
                }
                //for (int J = 0;J < AllObjects.Length; J++)
                //{
                //    AllObjects[J].GetComponent<Button>().interactable = false;
                //}
            }
        }
        if(PosVal==11)
        {
            //print(PosVal+ "==PosVal");
            nxtbtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            if (PosVal > 0)
            {
                //print("111");
                nxtbtn.GetComponent<Button>().interactable = true;
                prevbtn.GetComponent<Button>().interactable = true;
            }
            else
            {
                //print("22");
                prevbtn.GetComponent<Button>().interactable = false;
                //nxtbtn.GetComponent<Button>().interactable = false;
            }
        }


        //if (PosVal < pos.Length - 1)
        //{
        //    prevbtn.GetComponent<Button>().interactable = true;
        //}
        //else
        //{
        //    nxtbtn.GetComponent<Button>().interactable = false;
        //}

    }
}
