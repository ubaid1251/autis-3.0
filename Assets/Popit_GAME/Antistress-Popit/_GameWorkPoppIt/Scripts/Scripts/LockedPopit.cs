using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedPopit : MonoBehaviour
{
    [SerializeField] GameObject watchAdObj_tempprefab;
    public bool IsUnlockedItemTrue;

    public bool showAd;
    public int rvNum;
    public GameObject cautionPanel;
    private void Start()
    {
        if(!showAd)
        IsUnlockedItemTrue = true;
      //  LockedApplyCheck();
    }

    private void OnEnable()
    {
        if (IsUnlockedItemTrue)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    #region LOCKED APPLY CHECK

    void LockedApplyCheck()
    {
        // check defalut unlocked item....
        if (IsUnlockedItemTrue)
        {
            PlayerPrefs.SetInt(transform.gameObject.name, 1);
        }
        //check unlocked item 
        if (PlayerPrefs.GetInt(transform.gameObject.name) == 1)
            IsUnlockedItemTrue = true;

        if (!IsUnlockedItemTrue)
        {
            SpawnWatchAdImage();
        }
    }

    void SpawnWatchAdImage()
    {
       
        if (transform.childCount < 3)
        {
            GameObject watchAdObj = Instantiate(watchAdObj_tempprefab);
            watchAdObj.transform.SetParent(transform);
            watchAdObj.transform.localScale = Vector3.one;
            watchAdObj.transform.localPosition = new Vector3(-32, -240f,0);
        }
    }

    public void OpenLockedItemOnVedioEnd()
    {
        //PlayerPrefs.SetInt(transform.gameObject.name, 1);
        //Destroy(transform.GetChild(1).gameObject);
        IsUnlockedItemTrue = true;
    }

    #endregion


}
