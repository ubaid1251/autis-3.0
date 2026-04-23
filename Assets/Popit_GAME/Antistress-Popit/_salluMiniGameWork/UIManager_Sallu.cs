
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;
public class UIManager_Sallu : MonoBehaviour
{
    public static UIManager_Sallu INSTANCE;
    public bool IsMainMenuScreen;

    public GameObject loadingScreenPrefab;

    public bool isMiniGame;
    private void Awake()
    {
        if(INSTANCE==null)
        {
            INSTANCE = this;
        }
    }


    public static bool IsFromMiniGamecomplete;

    public void LoadNextLevelPopitForPlay()
    {
        clickSound();       
        IsFromMiniGamecomplete = true;
        Popit2DManagerAntistress.DisableMM = true;
        loadingScreenPrefab.SetActive(true);
        
    }
    
    //void Fun()
    //{
    //    SceneManager.LoadScene("PopitMain");
    //}

    public void SelectedSceneLoad(string nameScene)
    {
        clickSound();
        if (!isMiniGame)
        {
            StartCoroutine(SelectedSceneLoadDelay(nameScene));
        }            
        else
        {
            loadingScreenPrefab.SetActive(true);
        }
            
    }
    IEnumerator SelectedSceneLoadDelay(string nScene)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nScene);
    }
    public void ReloadCurrentScene()
    {
        clickSound();
        StartCoroutine("loadcurrenSceneWithDelay");
    }
    IEnumerator loadcurrenSceneWithDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StopCoroutine(loadcurrenSceneWithDelay());
    }

    public void OpenDialogeAnimation(GameObject db)
    {
       // clickSound();
        db.SetActive(true);
        //SoundManager.Instance.PlayOneShot("miniCompleteSound");
        db.GetComponent<Image>().DOFade(0.9f, 0.5f).OnComplete(() =>
        {
            db.transform.GetChild(0).transform.gameObject.transform.DOLocalMoveX(0, 0.3f);

        });
    }

    public void CloseDialogeAnim(GameObject db)
    {
        clickSound();
        db.transform.GetChild(0).transform.gameObject.transform.DOLocalMoveX(-1260f, 0.3f).OnComplete(() => 
        {
            db.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(() =>
            {
                db.gameObject.SetActive(false);
                db.transform.GetChild(0).transform.gameObject.transform.localPosition = new Vector3(1260, 0, 0);
            });
        });

    }

    public void MenuDilageOpenclosed(GameObject db)
    {
        if(db.activeInHierarchy)
        {
            db.SetActive(false);
        }
        else
        {
            db.SetActive(true);
        }
    }







    void clickSound()
    {
  
            SoundManager.instance.PlayEffect_Instance(7);
        /// if(IsMainMenuScreen)
        //   SoundManager.Instance.PlayAudio("click", false);
    }

    
    








    /*
     * Enable it for Coin Management System
    int coinsValue = 50;
   
    void SetCoinsOnWatchAdd()
    {
       
        CoinsManager.INSTANCE.numberOfCoins = 25;//PurchasedCoins;
        CoinsManager.INSTANCE.GiveCoinsToWinner();
        CoinsManager.INSTANCE.SetCoinsInPrefs((coinsValue - 25));
    }
    
    public void Get50CoinsWatchAdd(int watchaddCoins)
    {
        coinsValue = watchaddCoins;
        
    }

    public void PurchasedCoins_InApp(int PurchasedCoins)
    {
        CoinsManager.INSTANCE.numberOfCoins = 25;//PurchasedCoins;
        CoinsManager.INSTANCE.GiveCoinsToWinner();
        CoinsManager.INSTANCE.SetCoinsInPrefs((PurchasedCoins - 25));

    }

    public void RemoveAdd()
    {
        PlayerPrefs.SetInt("RemoveAd", 1);
      //  CustomYodoAdsManager.INSTANCE_YAM.HideBannerAd();

    }

    */

}
