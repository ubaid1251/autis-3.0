using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class SelectShape : MonoBehaviour
{
    public GameObject[] AllShapes;
    static int ShapeunlockNum,TickOnNum;
    //public GameObject Loading;
    public static SelectShape ins;
    private void Awake()
    {
        ins = this;
        //PlayerPrefs.SetInt("UnlockAll", 1);
    }
    void Start()
    {
        if(PlayerPrefs.GetInt("UnlockAll")==1)
        {
            ShapeunlockNum= AllShapes.Length-1;
        }
        else
        {
         //   ShapeunlockNum = PlayerPrefs.GetInt("SetShapePopUp");
            ShapeunlockNum = PlayerPrefs.GetInt("ShapeNumber");
            //print(ShapeunlockNum);
        }

        //  for (int i = 2; i <= ShapeunlockNum; i++)
        // {

        //PlayerPrefs.SetInt(AllShapes[i].transform.name, 1);
        //print("Name = "+AllShapes[i].transform.name);
        // }
        //for (int i = 1; i < AllShapes.Length; i++)
        //{

        //    if (PlayerPrefs.GetInt(AllShapes[i].transform.name) == 1)
        //    {
        //        AllShapes[i].GetComponent<Button>().enabled = true;
        //        AllShapes[i].transform.GetChild(1).gameObject.SetActive(false);
        //    }
        //}

        TickOnNum = PlayerPrefs.GetInt("ShapeNumber");
        //print("Selected Shape No " + TickOnNum);
        //print("All Unlock Shape Numbers " + ShapeunlockNum);
        //for (int i = 0; i <= ShapeunlockNum; i++)
        //{
        //    AllShapes[i].GetComponent<Button>().enabled = true;
        //    AllShapes[i].transform.GetChild(1).gameObject.SetActive(false);
        //}

        AllShapes[TickOnNum].transform.GetChild(2).gameObject.SetActive(true);
        //AllShapes[TickOnNum].transform.GetChild(1).gameObject.SetActive(false);

        //if (PlayerPrefs.GetInt("ShapeFirstTime") == 0)
        //{
        //    AllShapes[0].transform.GetChild(2).gameObject.SetActive(false);
        //    AllShapes[0].transform.GetChild(3).gameObject.SetActive(true);
        //    PlayerPrefs.SetInt("ShapeFirstTime",1);
        //}
    }
    private void OnEnable()
    {
//        IntitializeAdmob.instance.ShowInterstitialAd();
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play("gridd", 0);
    }
    // Update is called once per frame
    public void SelectNewShape(int num)
    {
        PlayerPrefs.SetInt("PLayDrawON", 0);
      //      IntitializeAdmob.instance.ShowInterstitialAd();
        DOTween.KillAll();
        GameController.ins.OffPens();
        SoundManager.instance.PlayEffect_Instance(4);
        PlayerPrefs.SetInt("ShapeIsChosse", 1);
        //Loading.SetActive(true);
        for (int i=0;i< AllShapes.Length;i++)
        {
            AllShapes[i].transform.GetChild(2).gameObject.SetActive(false);
        }
        AllShapes[num].transform.GetChild(2).gameObject.SetActive(true);
        PlayerPrefs.SetInt("ShapeNumber",num);
        SceneManager.LoadScene("ColorGame");
        //Invoke("WaitLoad", 2.5f);
    }
    public void WaitLoad()
    {
        SceneManager.LoadScene("ColorGame");
    }
    public void RewardShape(GameObject obj)
    {
        obj.transform.parent.GetComponent<Button>().enabled = true;
        PlayerPrefs.SetInt(obj.transform.parent.name, 1); 
        obj.SetActive(false);
    }
    public void OffAnim()
    {
        GetComponent<Animator>().enabled = false;
    }
}
