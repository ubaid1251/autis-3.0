using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Antistress;

public class ShadowFindManager : MonoBehaviour
{
    public Sprite[] itemsSpriteArr;
    public GameObject[] positionObj;
    public GameObject itemPrefabObj;
    public GameObject itemSpanwPosObj;

    public GameObject completeDb;

    public GameObject tutorialhandObj;

    public static ShadowFindManager Instance;

    public bool IsTutorialCall;

    private void Awake()
    {
        tutorialhandObj.SetActive(false);
        if (Instance==null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetActiveLevel();
    }


    void GetActiveLevel()
    {
        int randomPostionPariObj = Random.Range(0, positionObj.Length);
        GameObject postionObj = positionObj[randomPostionPariObj];

        List<int> tempSpritenumberlist = new List<int>();
        if(postionObj.transform.childCount>0)
        {
            for (int i = 0; i < postionObj.transform.childCount; i++)
            {
                // random found but not same found again code check
                int r = Random.Range(0, itemsSpriteArr.Length);
                while(tempSpritenumberlist.Contains(r))
                {
                    r= Random.Range(0, itemsSpriteArr.Length);
                }
                tempSpritenumberlist.Add(r);
                //-----------

                SpawnItemPrefabs(r, itemsSpriteArr[r], postionObj.transform.GetChild(i).transform.position, true);
            }
        }
        int r2= Random.Range(0, tempSpritenumberlist.Count);
        // final move shape spawn
        SpawnItemPrefabs(tempSpritenumberlist[r2], itemsSpriteArr[tempSpritenumberlist[r2]], itemSpanwPosObj.transform.position, false);

    }

    void SpawnItemPrefabs(int itemid,Sprite itemSprite,Vector3 itemPos,bool IsShadowItem)
    {
        GameObject item = Instantiate(itemPrefabObj, itemPos, Quaternion.identity);

        item.GetComponent<SpriteRenderer>().sprite = itemSprite;
        item.GetComponent<ShadowItemDrage>().shapeId = itemid;
        item.AddComponent<PolygonCollider2D>().isTrigger = true;
        //item.GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.8f);
        item.name = itemid.ToString();

        if (IsShadowItem)
        {
            item.GetComponent<SpriteRenderer>().color = Color.black;
            item.GetComponent<ShadowItemDrage>().IsDrageServiceActive = false;
        }
        else
        {
            item.GetComponent<SpriteRenderer>().color = Color.white;
            item.GetComponent<ShadowItemDrage>().IsDrageServiceActive = true;
            item.GetComponent<SpriteRenderer>().sortingOrder = 2;
            handStarPosition = item.transform.position;
            GetActiveTutorialCall();
        }

    }

    public void GetActiveCompleteDialoge()
    {
        SoundManager.instance.PlayEffect_Instance(1);
        CoinsManager.INSTANCE.GiveCoinsCallBackFun(() =>
        {
            UIManager_Sallu.INSTANCE.OpenDialogeAnimation(completeDb);
        });
    }

    public void GetDeActiveTutorialHand()
    {
        tutorialhandObj.SetActive(false);
    }

    Vector3 handStarPosition;
   
    void GetActiveTutorialCall()
    {
        if(PlayerPrefs.GetInt("HandTuto")==0)
        {
            if(IsTutorialCall)
            {
                tutorialhandObj.SetActive(true);
                tutorialhandObj.transform.DOMove(handStarPosition, 0.3f).OnComplete(HandUpMovementAnimatio);
            }

            PlayerPrefs.SetInt("HandTuto", 1);
        }
    }

    void HandUpMovementAnimatio()
    {
        if (!IsTutorialCall)
            return;
            
            tutorialhandObj.transform.GetComponent<SpriteRenderer>().DOFade(1, 0.01f);
            tutorialhandObj.transform.DOMove(new Vector3(handStarPosition.x, handStarPosition.y + 4f, 0), 0.5f)
            .OnComplete(() =>
            {

                tutorialhandObj.transform.GetComponent<SpriteRenderer>().DOFade(0, 0.7f).OnComplete(() =>
                {
                    HandUpMovementAnimatio();
                    tutorialhandObj.transform.position = handStarPosition;
                });
            });
        }



}
