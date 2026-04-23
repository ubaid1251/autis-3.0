using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    /*
     * Enable it if coins system is needed
    [SerializeField] Transform targetPosTransForm;
    [SerializeField] Transform MoveCoinsTransForm;
    [SerializeField] Text totalcoinsText;
    [HideInInspector()]
    [SerializeField] Text totalcoinsText2;
    [SerializeField] GameObject coinsSetParentObj;
    [Space(5)]
    public int numberOfCoins=10;
    List<GameObject> coinsSpawnlist;
    */
    public static  CoinsManager INSTANCE;
    
    public bool isGamePlayscreen;
      

    Action _CompleteCallBack;
    int counter;
    // Start is called before the first frame update
    private void Awake()
    {
        //if (INSTANCE == null)
            INSTANCE = this;

            counter = 0;
    }
    /**
     * Enable This Logic If You Want to give coins
     void Start()
     {


         coinsSpawnlist = new List<GameObject>();
         totalcoinsText.text = PlayerPrefs.GetInt("coins").ToString();
         if (totalcoinsText2 != null)
             totalcoinsText2.text = PlayerPrefs.GetInt("coins").ToString();


     }
     public void GiveCoinsToWinner()
     {
         StartCoroutine(CoinsSpawnAndSpreedOnScreen(numberOfCoins, targetPosTransForm.gameObject));
     }
   
     public IEnumerator CoinsSpawnAndSpreedOnScreen(int count,GameObject targetPosTransForm_Param=null)
     {

         for (int i = 0; i < count; i++)
         {
             GameObject coins = Instantiate(MoveCoinsTransForm.gameObject);
             coins.transform.SetParent(coinsSetParentObj.transform);
             float xpos =UnityEngine.Random.Range(-Screen.width, Screen.width) * 0.2f;
             float YPos =UnityEngine.Random.Range(-Screen.height, Screen.height) * 0.08f;
            
             Vector2 spawnPosition = new Vector2(xpos, YPos);
             coins.transform.localPosition = spawnPosition;
             coinsSpawnlist.Add(coins);
          
             if(i>=count-1)
             {
                 yield return new WaitForSeconds(0.01f);
                 // move coins to target ..
                 for (int k = 0; k < coinsSpawnlist.Count; k++)
                 {
                     if (targetPosTransForm_Param == null)
                         targetPosTransForm_Param = targetPosTransForm.gameObject;

                     coinsSpawnlist[k].GetComponent<cois>().targetPosTransForm = targetPosTransForm_Param.transform;
                     coinsSpawnlist[k].GetComponent<cois>().IsMovECoins = true;
                    // coinsSpawnlist[k].GetComponent<AudioSource>().Play();
                     SetCoinsInPrefs(1);
                     yield return new WaitForSeconds(0.02f);
                    // Debug.Log(k + " : " + coinsSpawnlist.Count);
                     if(k>=coinsSpawnlist.Count-1)
                     {
                         yield return new WaitForSeconds(0.3f);
                         _CompleteCallBack?.Invoke();
                         coinsSpawnlist.Clear();
                     }
                 }
             }
         }
     }

     public  void SetCoinsInPrefs(int coinsSetValue)
     {
         PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + coinsSetValue);
         totalcoinsText.text =PlayerPrefs.GetInt("coins").ToString();

         if (totalcoinsText2 != null)
             totalcoinsText2.text = PlayerPrefs.GetInt("coins").ToString();
     }
    public int GetTotalCoins()
     {
         return PlayerPrefs.GetInt("coins");

     }
    float previouscliockTime;
    public void GiveCoinsOnMouseUpTap()
    {
        counter++;
        if(counter>=5 && Time.time >=previouscliockTime)
        {
            previouscliockTime = Time.time+10;

            GiveCoinsToWinner();
            counter = 0;

        }
    }
     **/



    public void GiveCoinsCallBackFun(Action actionCallBack)
    {
        _CompleteCallBack = actionCallBack;
                //SoundManager.Instance.PlayOneShot("PopitLevelUp");
                //Popit2DManager.INSTANCE.FileUserExpericenBar(3.6f,transform.gameObject.name);
        // Use Some Particles Here instead of Coins Spreading
       // GiveCoinsToWinner();
    }

    

    
}
