using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.Purchasing;

public class LockingScreenSlection : MonoBehaviour
{
    //id use for index base unlocked 
    [HideInInspector]
    public int itemId;
    [HideInInspector]
    public string ItemCategory_Key;
    [HideInInspector]
    public string screenName;
    [Space(5)]
    [HideInInspector]
    [SerializeField] GameObject notEnoughcoins;

    [SerializeField] GameObject watchAdObj_tempprefab;
    [SerializeField] GameObject IP_PricePrefabObj;

    public bool IsInAppPurchased;
    public bool IsUnlockedItemTrue;
    public string marketePrice;
    public string ProductIDStore;

    private void Awake()
    {
        SetIntializedValue();
    }

    private void Start()
    {
       // GetComponent<Button>().onClick.AddListener(ClickButtonlistner);
        LockedApplyCheck();
    }

    void SetIntializedValue()
    {
        itemId = transform.GetSiblingIndex();//int.Parse(transform.name);
        ItemCategory_Key = transform.parent.name;
    }

    void ClickButtonlistner()
    {
        //if(IsUnlockedItemTrue)
        //{
        //    SoundManager.Instance.PlayAudio("click", false);
        //    LoadSceneRequired();
        //}
        //else
        //{
        //    if (CoinsManager.INSTANCE.GetTotalCoins() >= CoinsValue)
        //    {
        //        Debug.Log("CoinsConsumed");
        //        OpenLockedItem_FromAdsORMarkete();
        //    }
        //    else
        //    {
        //        notEnoughcoins.SetActive(true);
        //    }
        //}
    }
    
    #region LOCKED APPLY CHECK

    void LockedApplyCheck()
    {
        // check defalut unlocked item....
        if (IsUnlockedItemTrue)
        {
            PlayerPrefs.SetInt(ItemCategory_Key + itemId, 1);
        }
        //check unlocked item 
        if (PlayerPrefs.GetInt(ItemCategory_Key + itemId) == 1)
        {
            IsUnlockedItemTrue = true;
            IsInAppPurchased = false;
        }

        if (!IsUnlockedItemTrue)
        {
            SpawnWatchAdImage();
        }
        else
        {
            Destorychild();
        }
    }

    void SpawnWatchAdImage()
    {
        if (IsUnlockedItemTrue || IsInAppPurchased)
            return;

       // GameObject tempSpanw = null;
        //if (IsInAppPurchased)
        //{
        //    tempSpanw = IP_PricePrefabObj;
        //    tempSpanw.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = marketePrice;

        //}
        //else
        //    tempSpanw = watchAdObj_tempprefab;

        if (transform.childCount < 3)
        {
            GameObject watchAdObj = Instantiate(watchAdObj_tempprefab);
            watchAdObj.transform.SetParent(transform);
            watchAdObj.transform.SetSiblingIndex(0);
            watchAdObj.transform.localScale = Vector3.one;

            watchAdObj.GetComponent<RectTransform>().localPosition = Vector3.zero; 
        }
    }

    public void OpenLockedItem_FromAdsORMarkete()
    {
        PlayerPrefs.SetInt(ItemCategory_Key + itemId, 1);
        if(transform.childCount>0)
        {
            // transform.GetChild(0).gameObject.SetActive(false);
            if (!IsInAppPurchased)
                Destroy(transform.GetChild(0).gameObject);
            else
                Invoke("Destorychild", 1f);
        }
        IsUnlockedItemTrue = true;
        IsInAppPurchased = false;

    }

    void Destorychild()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
    
    #endregion

    void LoadSceneRequired()
    {
      //  LoadingScreen.scneName = screenName;
       // SceneManager.LoadScene("LoadingScene");
    }
   

}