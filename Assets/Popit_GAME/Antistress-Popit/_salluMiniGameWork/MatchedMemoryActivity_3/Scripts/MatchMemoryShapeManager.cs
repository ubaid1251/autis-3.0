using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Antistress;

public class MatchMemoryShapeManager : MonoBehaviour
{
    public Sprite[] itemsSpriteArr;
    public GameObject itemPrefabObj;
    public GameObject[] positionObj;
    public GameObject completeDialoge;
    public GameObject tempLineObj;
    public GameObject tutorialhandObj;
    public bool IsTutorialCall;
    public static MatchMemoryShapeManager Instance;

    int numberOfObjectleftSide = 0;
    public GameObject brustStarPart;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        //PlayerPrefs.DeleteAll();
    }
   
    // Start is called before the first frame update
    void Start()
    {
        tutorialhandObj.SetActive(false);
        numberOfObjectleftSide = 0;
        GetActiveLevel();
    }


    #region SPAWNING LEVEL

    void GetActiveLevel()
    {
        int randomPostionPariObj = Random.Range(0, positionObj.Length);

        if(PlayerPrefs.GetInt("FirstTime_TurialPlay")==0)
        {
            randomPostionPariObj = 0;
            PlayerPrefs.SetInt("FirstTime_TurialPlay", 1);
        }


        GameObject postionObj = positionObj[randomPostionPariObj];

        List<int> tempSpritenumberlist = new List<int>();
        if (postionObj.transform.childCount > 0)
        {
            numberOfObjectleftSide = postionObj.transform.childCount;
            for (int i = 0; i < postionObj.transform.childCount; i++)
            {
                // random found but not same found again code check
                int r = Random.Range(0, itemsSpriteArr.Length);
                while (tempSpritenumberlist.Contains(r))
                {
                    r = Random.Range(0, itemsSpriteArr.Length);
                }
                tempSpritenumberlist.Add(r);

                //1 ----------- use for Tutorial

                if(!adstartPosFirstTime)
                {
                    handStarPosition = postionObj.transform.GetChild(i).transform.position;
                    adstartPosFirstTime = true;
                }
                //end 1 ------

                SpawnItemPrefabs(r, itemsSpriteArr[r], postionObj.transform.GetChild(i).transform.position, false);
            }

           
            // infrom item spawn

            List<int> tempinfrontindexsave = new List<int>();
            for (int i = 0; i < postionObj.transform.childCount; i++)
            {
                // random found but not same found again code check
                int r = Random.Range(0, tempSpritenumberlist.Count);
                while (tempinfrontindexsave.Contains(r))
                {
                    r = Random.Range(0, tempSpritenumberlist.Count);
                }
                tempinfrontindexsave.Add(r);
                Vector3 Pos = new Vector3(-postionObj.transform.GetChild(i).transform.position.x, postionObj.transform.GetChild(i).transform.position.y,0);
                int id = tempSpritenumberlist[r];
                
                //2 ----------- use for tutorial 
                if (tempId1 == id)
                {
                    //--end 2
                    handNextPosition = Pos;
                }
                SpawnItemPrefabs(id, itemsSpriteArr[tempSpritenumberlist[r]],Pos , true);
            }
        }

        GetActiveTutorialCall();


    }


    void SpawnItemPrefabs(int itemid, Sprite itemSprite, Vector3 itemPos, bool IsShadowItem)
    {
        GameObject item = Instantiate(itemPrefabObj, itemPos, Quaternion.identity);
        item.transform.SetParent(transform);
        item.GetComponent<SpriteRenderer>().sprite = itemSprite;
        item.GetComponent<Drag_MatchedItem>().shapeId = itemid;
        item.AddComponent<PolygonCollider2D>().isTrigger = true;
        // --1  use for tutorial 
        if(AdIdFirstTime==false)
        {
            tempId1 = item.GetComponent<Drag_MatchedItem>().shapeId;
            AdIdFirstTime = true;
        }
        

        if (IsShadowItem)
        {
            item.GetComponent<SpriteRenderer>().color = Color.black;
            item.GetComponent<SpriteRenderer>().sortingOrder = 2;
            item.GetComponent<Drag_MatchedItem>().isLeftSidedObj = true;
        }
        else
        {
            item.GetComponent<SpriteRenderer>().color = Color.white;
            item.GetComponent<SpriteRenderer>().sortingOrder = 2;
           


        }

    }

    #endregion

    #region COMPLETE COUNT CHECK -GAMEOVER

    public static int correnlineConnectCounter;
    
    public void CheckAllLineGreenActive()
    {
        if(correnlineConnectCounter>=numberOfObjectleftSide)
        {
            SoundManager.instance.PlayEffect_Instance(1);
            GetActiveCompleteDialoge();
            Debug.Log("Yeah Called");
            correnlineConnectCounter = 0;
        }
    }

    public void GetActiveCompleteDialoge()
    {
        CoinsManager.INSTANCE.GiveCoinsCallBackFun(() =>
        {
            UIManager_Sallu.INSTANCE.OpenDialogeAnimation(completeDialoge);
        });
    }

    public  void SpawnStarBrustEffect(Vector3 Pos)
    {
        Debug.Log("YUPPP");
       GameObject star= Instantiate(brustStarPart, Pos, Quaternion.identity);
        Destroy(star, 1f);
    }
    #endregion


    #region HAND  TUTORIAL 

    public int tempId1;
    bool AdIdFirstTime = false;
    bool adstartPosFirstTime = false;

    Vector3 handStarPosition;
    Vector3 handNextPosition;
    

    public void GetDeActiveTutorialHand()
    {
        tutorialhandObj.SetActive(false);
        tempLineObj.SetActive(false);
    }
   
    void GetActiveTutorialCall()
    {
        if (PlayerPrefs.GetInt("HandTuto1") == 0)
        {
            if (IsTutorialCall)
            {
                tempLineObj.SetActive(true);
                tutorialhandObj.SetActive(true);
                tutorialhandObj.transform.DOMove(handStarPosition, 0.3f).OnComplete(HandUpMovementAnimatio);
                tempLineObj.GetComponent<LineRenderer>().SetPosition(0, handStarPosition);
                tempLineObj.GetComponent<LineRenderer>().SetPosition(1, handNextPosition);
            }

            PlayerPrefs.SetInt("HandTuto1", 1);
        }
    }

    void HandUpMovementAnimatio()
    {
        if (!IsTutorialCall)
            return;

        tutorialhandObj.transform.GetComponent<SpriteRenderer>().DOFade(1, 0.01f);
        tutorialhandObj.transform.DOMove(handNextPosition, 0.5f)
        .OnComplete(() =>
        {

            tutorialhandObj.transform.GetComponent<SpriteRenderer>().DOFade(0, 0.7f).OnComplete(() =>
            {
                HandUpMovementAnimatio();
                tutorialhandObj.transform.position = handStarPosition;
            });
        });
    }

    #endregion




}
