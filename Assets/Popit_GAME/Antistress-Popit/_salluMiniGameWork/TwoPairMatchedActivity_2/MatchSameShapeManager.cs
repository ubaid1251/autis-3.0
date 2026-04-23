using Antistress;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSameShapeManager : MonoBehaviour
{
    public Sprite[] itemsSpriteArr;
    public GameObject itemPrefabObj;
    public GameObject completeDialoge;



    public Collider2D[] colliders;
    public float randius;
    [HideInInspector]
    public int numberOfPairspanw;

    public float miniX = -1.8f;
    public float maxX = 1.8f;
    [Space(10)]
    public float miniY =-3;
    public float maxY = 3;
    public static MatchSameShapeManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance==null)
        {
            Instance = this;
        }
       
        GetActiveLevel();
    }


    void GetActiveLevel()
    {
        List<int> tempSpritenumberlist = new List<int>();

        int a= Random.Range(1, 6);
        for (int i = 0; i < a; i++)
        {
           // random found but not same found again code check orignal work
            int r = Random.Range(0, itemsSpriteArr.Length);
            while (tempSpritenumberlist.Contains(r))
            {
                r = Random.Range(0, itemsSpriteArr.Length);
            }

            tempSpritenumberlist.Add(r);
            SpawnItemPrefabs(r, itemsSpriteArr[r]);
            SpawnItemPrefabs(r, itemsSpriteArr[r]);


        }
    }
  
    void SpawnItemPrefabs(int itemid, Sprite itemSprite)
    {
        Vector2 spawnPosition=new Vector2(0,0);//= GetRandomPos();
        bool canSpawnHere=false;
        int safetyNet = 0;

        while(!canSpawnHere)
        {
            float spawnPosX = Random.Range(miniX, maxX);
            float spwanPosY = Random.Range(miniY, maxY);

            spawnPosition = new Vector2(spawnPosX, spwanPosY);

            canSpawnHere = PreventSpawnOverLap(spawnPosition);
            if (canSpawnHere)
                break;
            safetyNet++;
            if(safetyNet>50)
            {
                break;
                Debug.Log("TOO MAY ATTEMPTS");
            }
        }

        GameObject item = Instantiate(itemPrefabObj, spawnPosition, Quaternion.identity);
        item.transform.SetParent(transform);
        item.GetComponent<SpriteRenderer>().sprite = itemSprite;
        item.GetComponent<MemoryMatchItem>().shapeId = itemid;
        item.AddComponent<PolygonCollider2D>().isTrigger = true;

    }

    private bool PreventSpawnOverLap(Vector2 spawnPosition)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, randius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 centerPoint = colliders[i].bounds.center;

            float width = colliders[i].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;

            float leftExtent = centerPoint.x - width;
            float righExtent = centerPoint.x + width;

            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;

            if(spawnPosition.x>=leftExtent && spawnPosition.x<=righExtent)
            {
                if (spawnPosition.y >= lowerExtent && spawnPosition.y <= upperExtent)
                {
                    return false;
                }
            }

           
        }


        return true;
    }

    
    public void CheckISAll_ItemDestoryToCompleteMiniGame()
    {
        Debug.Log("suleman destory all : level complete db call : " + transform.childCount);
        if (transform.childCount == 0)
        {
            SoundManager.instance.PlayEffect_Instance(1);
            CoinsManager.INSTANCE.GiveCoinsCallBackFun(() =>
            {
                Debug.Log("suleman destory all : level complete db call");
                UIManager_Sallu.INSTANCE.OpenDialogeAnimation(completeDialoge);
            });
        }

    }
    
    
}
