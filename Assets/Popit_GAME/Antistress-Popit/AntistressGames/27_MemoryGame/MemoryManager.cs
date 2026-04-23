using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{
    public MemoryTiles[] memoryTilesArr;
    public Sprite[] itemSpriteArr;
    public SpriteRenderer[] tileSpriteRenderarr;
    public static int count;
    public static MemoryManager Instance;
    // use for save random number to check not repeat
    List<int> rTileNumber = new List<int>();
    public AudioSource AS;
    public AudioClip[] AC;
    public GameObject myObj, completionDB, oldBg;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rTileNumber = new List<int>();

        //  Invoke("SetItemIntoMemoryRandomly", 1f);
        SetItemIntoMemoryRandomly();

    }
    private void OnEnable()
    {
        // GoogleAdMobController.THIS.ShowR1InterstitialAd(); //AdCallPosition
    }
    void SetItemIntoMemoryRandomly()
    {
        for (int i = 0; i < itemSpriteArr.Length; i++)
        {
            int rItem = i;
            for (int k = 0; k < 2; k++)
            {
                int rTile = GetTileIndexNumber_Random();
                tileSpriteRenderarr[rTile].sprite = itemSpriteArr[rItem];



                memoryTilesArr[rTile].tileId = rItem;
                memoryTilesArr[rTile].name = rItem.ToString();

            }
        }
    }



    int GetTileIndexNumber_Random()
    {
        int counter = 0;
        int rTile = Random.Range(0 , memoryTilesArr.Length);

        while (rTileNumber.Contains(rTile) /*&& counter <30*/)
        {
            rTile = Random.Range(0 , memoryTilesArr.Length);
            counter++;
        }
        rTileNumber.Add(rTile);

        return rTile;


    }

    public static int counterCheck;

    public void ResetTile()
    {
        if (counterCheck == itemSpriteArr.Length)
        {
            int counter = 0;
            while (counter < memoryTilesArr.Length)
            {
                memoryTilesArr[counter].FilpTileFaceDown();
                counter++;
            }
            StartCoroutine("DelayToResetSceneLoad");
            counterCheck = 0;
        }

    }

    IEnumerator DelayToResetSceneLoad()
    {
        yield return new WaitForSeconds(1.5f);
        rTileNumber = new List<int>();
        counterCheck = 0;
        //  Invoke("SetItemIntoMemoryRandomly", 1f);
        SetItemIntoMemoryRandomly();
        StopCoroutine(DelayToResetSceneLoad());
    }

}
