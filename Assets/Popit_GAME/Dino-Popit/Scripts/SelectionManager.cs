using Antistress;
using Dino;
using GameWork.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SelectionManager : MonoBehaviour
{

    public GameObject[] levels;
    public GameObject[] SelectionButtons;
    private int currentLevel;
    public GameObject SelectionPanel;
    public GameObject Hud;
    public LoadingScreen loadingScreen;
    public GameObject SettingUI;
    public GameObject RatingbarUI;
    public GameObject ExitScreen;
    public Text coinsLable;
    public GameObject HomeBtn, ResetBtn;
    public GameObject MMExitBtn, MMSettingBtn/*, resetBtn, nextBtn, gamePanel*/, confettiParticles;

    //public ParticleSystem confettiParticlesss;

    public static SelectionManager selectManagerInstance;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        HomeBtn.SetActive(false);
        ResetBtn.SetActive(false);
        Hud.SetActive(false);
        SelectionPanel.SetActive(true);
        coinsLable.text = GlobalVars.Instance.TotalCoins.ToString();
        //InitSelectionButtonCoins();   uncomment later


        if(selectManagerInstance == null)
        {
            selectManagerInstance = this;
        }


        //confettiParticlesss = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        loadingScreen.ShowLoadingScreen().OnComplete(() =>
        {
            SelectionPanel.SetActive(true);
            coinsLable.text = GlobalVars.Instance.TotalCoins.ToString();
        });
    }

    public void QuitApplication()
    {
        // GoogleAdMobController.THIS.ShowInterstitialAd();
        Debug.Log("this is intersitial add");
        Application.Quit();
    }
    
    public void Home()
    {

        loadingScreen.ShowLoadingScreen().OnComplete(() =>
        {
            coinsLable.text = GlobalVars.Instance.TotalCoins.ToString();
            levels[currentLevel].SetActive(false);
            SelectionPanel.SetActive(true);
            ResetBtn.SetActive(false);
            HomeBtn.SetActive(true);
            Hud.SetActive(false);
            //InitSelectionButtonCoins();        // uncomment
            if (!GlobalVars.Instance.Rated)
            {
                GlobalVars.Instance.RateCount++;
                if (GlobalVars.Instance.RateCount > 5)
                {
                    Invoke("ShowRatingbar" , 5f);
                    GlobalVars.Instance.RateCount = 0;
                }
            }
        });
        //GoogleAdMobController.THIS.ShowInterstitialAd(); //AdCallPosition
        Time.timeScale = 1.0f;
        Debug.Log("this is intersitial add");
    }
    int rateCount;
    public void SelectLevel(int index)
    {
        // GoogleAdMobController.THIS.ShowInterstitialAd(); //AdCallPosition

        var selectedObj = EventSystem.current.currentSelectedGameObject;
        var rewardBase = selectedObj.transform.GetComponent<RewardBaseUnlock>();

        if (rewardBase.isLocked)
        {
            //if (GMAdsManager.Instance)
            //    GMAdsManager.Instance.ShowBothRewarded(rewardBase.UnlockedLevel);
        }
        else
        {
            Time.timeScale = 1.0f;
            MMExitBtn.SetActive(false);
            MMSettingBtn.SetActive(true);
            bool showAd = true;
            if (rateCount % 3 != 0)
                showAd = false;
            rateCount++;
            loadingScreen.ShowLoadingScreen().OnComplete(() =>
            {
                SelectionPanel.SetActive(false);
                levels[index].SetActive(true);
                Hud.SetActive(true);
                coinsLable.text = GlobalVars.Instance.GetLevelCoins(levels[index].name).ToString();
                currentLevel = index;
                HomeBtn.SetActive(true);
                ResetBtn.SetActive(true);
            }).OnUpdate((progress) =>
            {
                if (progress > 0.3f && showAd)
                {
                    showAd = false;
                    // GoogleAdMobController.THIS.ShowInterstitialAd();
                }
            });

        }
    }
    public void ResetPopit()
    {
        // GoogleAdMobController.THIS.ShowInterstitialAd(); //AdCallPosition
        Invoke("SetLevel" , 1.0f);
    }
    void SetLevel()
    {
        Time.timeScale = 1.0f;
        Debug.Log("this is intersitial add");

       
        // levels[currentLevel].GetComponent<>().EnablePopit();
    }
    public void Rate()
    {

        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
    }

    public void ShowSetting()
    {
        SettingUI.SetActive(true);
    }
    public void ShowExit()
    {
        // GoogleAdMobController.THIS.ShowInterstitialAd(); //AdCallPosition
        Time.timeScale = 1.0f;
        Debug.Log("this is intersitial add");
        ExitScreen.SetActive(true);
    }

    public void ShowRatingbar()
    {
        // GoogleAdMobController.THIS.ShowInterstitialAd();
        Debug.Log("this is intersitial add");
        RatingbarUI.SetActive(true);
    }
    public void UpdateUI()
    {
        coinsLable.text = GlobalVars.Instance.GetLevelCoins(levels[currentLevel].name).ToString();
    }

    void InitSelectionButtonCoins()
    {
        for (int i = 0; i < SelectionButtons.Length; i++)
        {
            SelectionButtons[i].GetComponentInChildren<Text>().text = GlobalVars.Instance.GetLevelCoins(levels[i].name).ToString();
        }
    }

    public void PlayParticles()
    {
        confettiParticles.SetActive(true);
        Invoke("StopParticles",3f);
        //confettiParticlesss.Play();
    }

    public void StopParticles()
    {
        confettiParticles.SetActive(false);
    }


    //public void ResetPopits()
    //{
    //    gamePanel.SetActive(false);
    //}

    //public void NextLevel()
    //{
    //    gamePanel.SetActive(false);
    //    currentLevel++;
       
    //}

    //public void GamePanelActivate()
    //{
    //    gamePanel.SetActive(true);
    //}



}
