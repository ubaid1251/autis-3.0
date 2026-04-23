using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class SelectScene : MonoBehaviour
{
    public ScrollRect rct;
    public float MoveToX;
    public float MoveInTime;
    public float MoveOutTime;
    public static bool FreeGame_bool;
    public GameObject[] _InApBtns;
    public static bool AdShow;
    public GameObject Sound2;
    public GameObject purchased_text;
    public static List<FreeGames> _FreeGames = new List<FreeGames>();

    public ScrollRect Scroll;
    public AudioSource[] Clicksound_Array;
    public GameObject disable_Panel;
    public static Vector3 rect_value;
    public GameObject blackimage;
    public RectTransform Deafult;
    public RectTransform ContainAb;
    public ScrollRect ScrollRect;
    public GameObject unlockAds;
    //private void OnDisable()
    //{
    //    AssignAdIds_CB.instance.HideBanner();
    //}
    private void OnEnable()
    {
        //PlayerPrefs.SetInt("NewFree_G", 1);
        //PlayerPrefs.SetInt("unlockall", 1);
        //PlayerPrefs.SetInt("UnlockGame", 1);
        if (PlayerPrefs.GetInt("CallAds") == 0)
        {
            //AdsInitilizer.instance.CallAdsNow();
            PlayerPrefs.SetInt("CallAds", 1);
        }
        if(PlayerPrefs.GetInt("UnlockGame")==1&&PlayerPrefs.GetInt("RemoveAds")==0)
        {
            unlockAds.SetActive(true);
        }

        //LoadingHandler._LoadsceneName = "SelectionScreenNew";
        if (PlayerPrefs.GetInt("RemoveAds") == 1 || PlayerPrefs.GetInt("unlockall") == 1)
        {
            blackimage.SetActive(false);
            purchased_text.SetActive(true);
        }
        //AssignAdIds_CB.instance.ShowBanner();
        if (PlayerPrefs.GetInt("unlockall") == 1)
        {

            for (int a = 0; a < _InApBtns.Length; a++)
            {
                if (_InApBtns[a] != null)
                    _InApBtns[a].SetActive(false);
            }
        }
        SetLockState(false);
    }
    public GameObject eventsystem;
    static bool is_Pos = false;
    private void Start()
    {
        if (FreeGame_bool == true)
        {
            subSelecCanvas.SetActive(true);
            SelecCanvas.SetActive(false);
            FreeGame_bool = true;
        }
        else
        {
            subSelecCanvas.SetActive(false);
            SelecCanvas.SetActive(true);
            FreeGame_bool = false;
        }
       
        if (!is_Pos)
        {
            if (PlayerPrefs.GetInt("NewFree_G") == 1)
            {
                ContainAb.gameObject.SetActive(true);
                ScrollRect.content = ContainAb;
                ContainAb.DOAnchorPosX(3000.259f, 2f, false).SetEase(Ease.Linear).OnComplete(() => { eventsystem.SetActive(true); });
            }
            else
            {
                Deafult.gameObject.SetActive(true);
                ScrollRect.content = Deafult;
                Deafult.DOAnchorPosX(2860.259f, 2f, false).SetEase(Ease.Linear).OnComplete(() => { eventsystem.SetActive(true); });
            }
        }
        else
        {
            eventsystem.SetActive(true);
            if (PlayerPrefs.GetInt("NewFree_G") == 1)
            {
                ContainAb.gameObject.SetActive(true);
                ScrollRect.content = ContainAb;
                ContainAb.DOAnchorPos(rect_value, 0.0f, false);
            }
            else
            {
                Deafult.gameObject.SetActive(true);
                ScrollRect.content = Deafult;
                Deafult.DOAnchorPos(rect_value, 0.0f, false);
            }

        }
        is_Pos = true;
    }
    public void GotoSubScene(int value)
    {
        //activeGameMini.activeObj = value;
    }
    public GameObject NotClick;
    public void GotoScene(string sceneName)
    {
        NotClick.SetActive(true);
        SceneManager.LoadScene(sceneName);
        //EventsMatrica.instance.SubsceneName = sceneName;
        //EventsMatrica.instance.SendAnalytics_Matrica(EventsMatrica.instance.SceneName, EventsMatrica.instance.SubsceneName, "From_Selection");
    }
    public void SetLockState(bool status)
    {
        //IsLocked.LockScene = status;
    }
    public void UnlockAll_BuyClick()
    {
        //IAPPruchashingManager.instance.BuyProductID(0);
    }
    public void Privacypolicy_Click()
    {
        Application.OpenURL("https://taptoy.io/privacy/");
    }
    public void TermOfService_Click()
    {
        Application.OpenURL("https://taptoy.io/privacy/");
    }
    public void ResotrePurchase_Click()
    {
        //unityInAppPurchase_CB.instance.RestorePurchases();
    }
    public void MainSel(int a)
    {
        rect_value = ContainAb.transform.localPosition;
        if (PlayerPrefs.GetInt("MainSelectionTest") == 0)
        {
            Debug.Log("Error CameFSubSel: " + 0);
            SceneManager.LoadScene("New_SubSelection");
        }
        else
        {
            Debug.Log("Error CameFSubSel: " + 0);
            SceneManager.LoadScene("ABTestScene");
        }
        //if (PlayerPrefs.GetInt("New_Selection") == 1)
        //{
        //    SceneManager.LoadScene("New_SubSelection");
        //}
        //else
        //{
        //    index = a;
        //    disable_Panel.SetActive(true);
        //    SoundPlay(a);
        //    Invoke("CallDelay", 1.5f);
        //}
    }
    int index;
    void CallDelay()
    {
        //splashnew.SubSelection_num = index;
        //splashnew.SubSelection_bool = true;
        disable_Panel.SetActive(false);
    }
    void SoundPlay(int index)
    {
        PlayerPrefs.SetInt("CallBack", index);
        if (PlayerPrefs.GetInt("CallBack") == 3)
        {
            Clicksound_Array[1].Play();
        }
        if (PlayerPrefs.GetInt("CallBack") == 1)
        {
            Clicksound_Array[2].Play();
        }
        if (PlayerPrefs.GetInt("CallBack") == 2)
        {
            Clicksound_Array[4].Play();
        }
        if (PlayerPrefs.GetInt("CallBack") == 0)
        {
            Clicksound_Array[3].Play();
        }
        if (PlayerPrefs.GetInt("CallBack") == 4)
        {
            Clicksound_Array[5].Play();
        }
        if (PlayerPrefs.GetInt("CallBack") == 5)
        {
            Clicksound_Array[0].Play();
        }

    }
    public void FullVersionClick()
    {
        //IAPPruchashingManager._ActiveSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("InApScene");
    }
    public GameObject SelecCanvas;
    public GameObject subSelecCanvas;
    public Animator animators;
    public void MainSelFreegame()
    {
        SelectScene.FreeGame_bool = true;
        disable_Panel.SetActive(true);
        //SoundPlay();
        rect_value = ContainAb.transform.localPosition;
        Invoke("CallDelayFreegame", 0.5f);
    }
    void CallDelayFreegame()
    {
        subSelecCanvas.SetActive(true);
        SelecCanvas.SetActive(false);
        //splashnew.SubSelection_num = index;
        //splashnew.SubSelection_bool = true;
        FreeGame_bool = true;
        disable_Panel.SetActive(false);
    }
    public void HomeBtn_Subselec()
    {
        SelectScene.FreeGame_bool = false;
        animators.SetTrigger("Reverse");
        Invoke("WaitPanel", 0.8f);
    }
    void WaitPanel()
    {
        SelecCanvas.SetActive(true);
        subSelecCanvas.SetActive(false);
        //splashnew.SubSelection_bool = false;
    }
    //public void AdCall()
    //{
    //    try
    //    {
    //        if (PlayerPrefs.GetInt("unlockall", 0) != 1)
    //        {
    //            if (Application.internetReachability != NetworkReachability.NotReachable)
    //            {
    //                if (IntitializeAdmobAds_CB._instance.HasAdmobInterstialAvaible())
    //                {
    //                    IntitializeAdmobAds_CB._instance.ShowAdmobInterstialAd();
    //                }
    //            }
    //        }
    //    }
    //    catch (System.Exception ex)
    //    {
    //    }
    //}
    public GameObject ParentalGate;
    public void LocksBtnClick()
    {
        //IAPPruchashingManager._ActiveSceneName = SceneManager.GetActiveScene().name;
        ParentalGate.SetActive(true);
        //SceneManager.LoadScene("InApScene");
    }
    public void PurchaseClick()
    {

    }
    public void ShareClick()
    {
        //GeneralScript._instance.TakeScreenShot();
    }
    public void MatchPlayClick()
    {
        SceneManager.LoadScene("MatchingGame");
    }
    public void rateClick()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.tt.toddlergames.preschoollearning.educationalgames");
    }
    public void CrossPro()
    {
        Clicksound_Array[7].Play();
        Application.OpenURL("https://play.google.com/store/apps/dev?id=8854046095342860840");
        //EventsMatrica.instance.SendAnalytics_Matrica("MoreGames Promotion", "", "");
    }
    public void LearningPro()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.tt.toddlergames.preschoollearning.educationalgames");
        //EventsMatrica.instance.SendAnalytics_Matrica("Learning Promotion", "", "");
    }
    public void OinCheckLoadMusicVine()
    {
        rect_value = ContainAb.transform.localPosition;
        if (PlayerPrefs.GetInt("MainSelectionTest") == 0)
        {
            Debug.Log("Error CameFSubSel: " + 0);
            SceneManager.LoadScene("New_SubSelection");
        }
        else
        {
            Debug.Log("Error CameFSubSel: " + 0);
            SceneManager.LoadScene("ABTestScene");
        }
    }
    public void OnCheckOctaPus()
    {
        rect_value = ContainAb.transform.localPosition;
        if (PlayerPrefs.GetInt("MainSelectionTest") == 0)
        {
            Debug.Log("Error CameFSubSel: " + 0);
            SceneManager.LoadScene("New_SubSelection");
        }
        else
        {
            Debug.Log("Error CameFSubSel: " + 0);
            SceneManager.LoadScene("ABTestScene");
        }
    }
    public void OnCheckValueset(int index)
    {
        PlayerPrefs.SetInt("FirstTiemIndex", 0);
        PlayerPrefs.SetInt("save_Index", index);
    }
}


[Serializable]

public class FreeGames
{
    public string SceneName;
    public int SceneVal;

    public FreeGames(string SceneName, int SceneVal)
    {
        this.SceneName = SceneName;
        this.SceneVal = SceneVal;
    }

}

