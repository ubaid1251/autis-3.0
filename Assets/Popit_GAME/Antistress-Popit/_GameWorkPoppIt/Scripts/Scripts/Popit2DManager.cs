using DG.Tweening;
using GameWork.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Antistress
{
    public class Popit2DManager : MonoBehaviour
    {
        public GameObject CurrentBG, SelectionUI, EpopsSelectionUI, Home, ThemeButton, Can, completeDialoge, MM, InternetPanel, confettiParticles;
        public GameObject[] Levels;
        public GameObject[] ButtonUI;

        public string[] moregameLink;
        int levelPlayed;

        public static int num;
        public static int Adcounter;
        public static bool DisableMM;
        int AdCount;

        public GameObject SimpleGameplayExitBtn, E_GameplayExitBtn;

        public static Popit2DManager anitstressPopit2dManagerInstance;
        private bool playPopit;

        public static bool Epopping;
        public delegate void EpopReseting();
        public event EpopReseting OnEpopReset;
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        WaitForSeconds waitFiveSec = new WaitForSeconds(5f);
        WaitForSeconds waitOneAndHalfSec = new WaitForSeconds(1.5f);
        [HideInInspector]
        public float value;
        private void Awake()
        {
            anitstressPopit2dManagerInstance = this;


        }

        // Start is called before the first frame update
        void Start()
        {
            Epopping = true;
            if (PlayerPrefs.GetInt("NoAds") == 1)
            {
                RemoveAdIcon();
            }
            FilImageAmountCall();
            if (DisableMM)
            {
                MM.SetActive(false);
            }
        }

        private void Update()
        {
            if (UIManager_Sallu.IsFromMiniGamecomplete)
            {
                NextPopitPlay();
                UIManager_Sallu.IsFromMiniGamecomplete = false;
            }

        }
        [SerializeField] List<int> obseletelevelPopitIndex;
        void StartToPlay()
        {
            while (obseletelevelPopitIndex.Contains(num))
            {
                num++;
            }
            if (num > Levels.Length)
                num = 0;

            Levels[num].SetActive(true);
            if (CurrentBG != null)
                CurrentBG.SetActive(false);

            CurrentBG = Levels[0].GetComponent<Popit2D>().BG;
            if (CurrentBG != null)
                CurrentBG.SetActive(true);

        }

        GameObject button;
        LockedPopit Lp;
        int RVtype;
        public void selectionButtons(int ButtonNum)
        {

            var selectedObj = EventSystem.current.currentSelectedGameObject;
            var rewardBase = selectedObj.transform.GetComponent<RewardBaseUnlock>();

            if (rewardBase.isLocked)
            {
                Debug.Log("hello");
                SimpleGameplayExitBtn.SetActive(false);
                E_GameplayExitBtn.SetActive(false);
                //if(GMAdsManager.Instance)
                //GMAdsManager.Instance.ShowBothRewarded(rewardBase.UnlockedLevel);
            }

            else
            {
                Debug.Log("hwwwwwwwwwwwwwww");
                SimpleGameplayExitBtn.SetActive(true);
                E_GameplayExitBtn.SetActive(true);
                num = ButtonNum;
                if (EventSystem.current.currentSelectedGameObject)
                    button = EventSystem.current.currentSelectedGameObject;
                if (button != null)
                    Lp = button.GetComponent<LockedPopit>();
                if (Lp != null)
                    RVtype = Lp.rvNum;


                SoundManager.instance.PlayEffect_Instance(5);
                if (Lp)
                {
                    //if (GoogleAdMobController.THIS.bannerView != null)
                    //{
                    //    GoogleAdMobController.THIS.bannerView.Destroy();  //AdCallPosition
                    //}
                    if (Lp.IsUnlockedItemTrue)
                    {

                        StartCoroutine(AdsShow());
                    }
                    else
                    {
                        if (Application.internetReachability == NetworkReachability.NotReachable)
                        {
                            //InternetPanel.SetActive(true); //ooo
                            playPopit = true;
                        }
                        else
                        {
                            //RewardAd();
                        }


                    }
                }
            }
        }
        public GameObject AdCautionImage;
        public void RewardAd()
        {
            StartCoroutine(ShowAdPanel());
        }
        IEnumerator ShowAdPanel()
        {
            playPopit = false;
            AdCautionImage.SetActive(true);  
            if (RVtype == 1)
            {
                try
                {
                    //if (GoogleAdMobController.THIS.rewardedAd == null)
                    //{
                    //    GoogleAdMobController.THIS.RequestAndLoadRewardedAd(); //AdCallPosition
                    //}
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            else
            {
                try
                {
                    //if (GoogleAdMobController.THIS.rewardedAD_Secondary == null)
                    //{
                    //    GoogleAdMobController.THIS.RequestAndLoadRewardedAd_Secondary(); //AdCallPosition
                    //}
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            yield return waitFiveSec;
            if (RVtype == 1)
            {
                try
                {
                    //GoogleAdMobController.THIS.ShowRewardedAd(() =>
                    //{
                    //    GiveReward();

                    //} , () =>
                    //{
                    //    GoogleAdMobController.THIS.ShowR1InterstitialAd(); //AdCallPosition

                    //});
                }
                catch (System.Exception)
                {
                    GiveReward();
                    throw;
                }


            }
            else
            {
                try
                {
                    //GoogleAdMobController.THIS.showSecondaryRewardedAD(() =>
                    //{
                    //    GiveReward();

                    //} , () =>
                    //{
                    //    GoogleAdMobController.THIS.ShowR1InterstitialAd(); //AdCallPosition

                    //});
                }
                catch (System.Exception)
                {
                    GiveReward();
                    throw;
                }

            }
        }
        void GiveReward()
        {
            if (Lp == null)
            {
                return;
            }
            else
            {
                Lp.OpenLockedItemOnVedioEnd();
                //       LoadLoadingScreen();
                StartCoroutine(AdsShow());
            }

        }
        public void SelectRewardedButton(int buttonNum)
        {
            if (PlayerPrefs.GetInt("Rewarded" + buttonNum) == 1 || PlayerPrefs.GetInt("NoAds") == 1)
                selectionButtons(buttonNum);
            else
            {
                num = buttonNum;
                StartCoroutine(ShowRewardedAd());

            }
        }

        IEnumerator ShowRewardedAd()
        {

            SelectionUI.SetActive(false);
            Home.SetActive(true);
            ThemeButton.SetActive(true);
            StartToPlay();
            StopCoroutine(ShowRewardedAd());
            yield return null;
        }
        public void EpopSelectionHome()
        {
            Epopping = false;
            // GoogleAdMobController.THIS.ShowInterstitialAd(); //AdCallPosition
            MM.SetActive(true);
            //ButtonUI[5].SetActive(true);
            EpopsSelectionUI.SetActive(false);
        }
        IEnumerator AdsShow()
        {
            try
            {
                //if (GoogleAdMobController.THIS.bannerView == null) //AdCallPosition
                //    GoogleAdMobController.THIS.RequestBannerAd();
            }
            catch (System.Exception)
            {

                throw;
            }
            print("Epopit True Hona Chahiay" + Epopping);
            if (Epopping == false)
            {
                SelectionUI.SetActive(false);
                ThemeButton.SetActive(true);
            }
            else
            {
                EpopsSelectionUI.SetActive(false);
            }
            Home.SetActive(true);
            levelPlayed++;
            StartToPlay();
            yield return waitOneAndHalfSec;
            StopCoroutine(AdsShow());
        }

        public void OnRewardComplete()
        {
            SelectionUI.SetActive(false);
            Home.SetActive(true);
            ThemeButton.SetActive(true);
            StartToPlay();
        }

        public void HomeButtonFun()
        {
            SoundManager.instance.PlayEffect_Instance(7);
            if (!Epopping)
            {
                if (!SelectionUI.activeInHierarchy)
                {
                    for (int i = 0; i < Levels.Length; i++)
                    {
                        Levels[i].SetActive(false);
                    }
                    SelectionUI.SetActive(true);
                    completeDialoge.SetActive(false);
                    Home.SetActive(false);
                    ThemeButton.SetActive(false);

                }
                else
                {
                    MM.SetActive(true);
                    //ButtonUI[5].SetActive(true);
                    SelectionUI.SetActive(false);
                }
                //GoogleAdMobController.THIS.ShowInterstitialAd(); //AdCallPosition
                Time.timeScale = 1f;
                //  Rotation = 0;
                ///Levels[num].GetComponent<Popit2D>().EnablePopit();

            }
            else
            {
                OnEpopReset?.Invoke();
                //  Levels[num].GetComponent<Popit2D>().HomeBtnReset();
                //  Invoke("ClosePoput", 2.0f);
            }




        }
        public void SimpleHomeBtn()
        {
            ButtonUI[0].SetActive(true);
            ButtonUI[1].SetActive(true);
            ButtonUI[5].SetActive(false);
            ButtonUI[2].SetActive(false);
            ButtonUI[3].SetActive(false);
        }
        void ClosePoput()
        {
            for (int i = 0; i < Levels.Length; i++)
            {
                Levels[i].SetActive(false);
            }
            EpopsSelectionUI.SetActive(true);
        }
        public void ResetPopits()
        {
            // GoogleAdMobController.THIS.ShowInterstitialAd(); //AdCallPosition
            CloseDialogeAnim(completeDialoge , Levels[num].GetComponent<Popit2D>().EnablePopit);
            // completeDialoge.SetActive(false);
            // Levels[num].GetComponent<Popit2D>().EnablePopit();
        }

        public void NextPopitPlay()
        {
            Levels[num].SetActive(false);
            CloseDialogeAnim(completeDialoge , IsnextPopitPlayAvailable);
        }

        void IsnextPopitPlayAvailable()
        {
            int nextpoppitnumber = num;
            if (nextpoppitnumber < Levels.Length)
            {
                num = nextpoppitnumber;
                mainScreenPanel.SetActive(false);
                SelectionUI.SetActive(false);
                Home.SetActive(true);
                levelPlayed++;
                StartToPlay();

            }
            else
            {
                num = 0;
                mainScreenPanel.SetActive(false);
                SelectionUI.SetActive(false);
                Home.SetActive(true);
                levelPlayed++;
                StartToPlay();
            }
        }


        public void DisableAd()
        {
            PlayerPrefs.SetInt("NoAds" , 1);
            print("Ad Removed");
            RemoveAdIcon();
        }

        void RemoveAdIcon()
        {
            foreach (GameObject go in ButtonUI)
            {
                if (go.transform.childCount > 0)
                    go.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        // suleman code
        [Header("SALLU HERE VARIABLE ")]
        [Space(10)]
        [SerializeField] GameObject selectionPanel;
        [SerializeField] GameObject mainScreenPanel;
        [SerializeField] string[] minigameNameArr;
        // user experience ad 
        public Image fillAmountImage;
        public float maxUserExperiencelimit = 100;
        // ui call fun 
        public void LoadSelectionPanel()
        {
            Epopping = false;
            mainScreenPanel.SetActive(false);
            selectionPanel.SetActive(true);
            fillAmountImage.transform.gameObject.SetActive(true);
            SoundManager.instance.PlayEffect_Instance(7);
            //   LoadLoadingScreen();


        }
        public void LoadEpopitSelection()
        {
            Adcounter = 0;
            mainScreenPanel.SetActive(false);
            EpopsSelectionUI.SetActive(true);
            Epopping = true;
            fillAmountImage.transform.gameObject.SetActive(false);
            SoundManager.instance.PlayEffect_Instance(7);
            //GoogleAdMobController.THIS.ShowInterstitialAd();
        }

        public void PlayBtn()
        {
            for (int i = 0; i < ButtonUI.Length; i++)
            {
                if (i < 2)
                {
                    ButtonUI[i].SetActive(false);
                }
                if (i > 1)
                {
                    ButtonUI[i].SetActive(true);
                }
            }
        }
        public void InternetKliay()
        {
            mainScreenPanel.SetActive(false);
            if (playPopit)
            {
                StartCoroutine(ShowAdPanel());
            }
        }
        // ui call fun 
        public void LoadMainScreenPanel()
        {
            mainScreenPanel.SetActive(true);
            selectionPanel.SetActive(false);
        }
        public void FileUserExpericenBar(float plusvalue , string activePopitName)
        {
            if (PlayerPrefs.GetInt(activePopitName) == 0)
            {
                PlayerPrefs.SetFloat("userPlayValue" , PlayerPrefs.GetFloat("userPlayValue") + plusvalue);
                FilImageAmountCall();
                PlayerPrefs.SetInt(activePopitName , 1);
                Debug.Log("Fill call");
            }
            //CoinsManager.INSTANCE.GiveCoinsCallBackFun(GetCallMiniGameOnCompletePopHit);
            ;
        }

        public void FilImageAmountCall()
        {
            // check is not played before 
            // float value = PlayerPrefs.GetFloat("userPlayValue") / maxUserExperiencelimit;

            fillAmountImage.fillAmount += value;
            // GetCallMiniGameOnCompletePopHit();
        }

        public void GetCallMiniGameOnCompletePopHit()
        {
            print("........." + "GetActive_RandomMiniGame" + ".........");
            StartCoroutine("GetActive_RandomMiniGame");
            //GetActive_RandomMiniGame();
        }
        public void MoreGameLink(int num)
        {
            Application.OpenURL(moregameLink[num]);
        }

        public GameObject MoreGamePanel;
        public void MoregamePanel()
        {
            MoreGamePanel.SetActive(true);

        }
        IEnumerator GetActive_RandomMiniGame()
        {
            yield return wait;
            string name = minigameNameArr[Random.Range(0 , minigameNameArr.Length)];
            SceneManager.LoadScene(name);
            StopCoroutine(GetActive_RandomMiniGame());
        }

        public void OpenDialogeAnimation(GameObject db)
        {
            db.SetActive(true);
            db.GetComponent<Image>().DOFade(1f , 0.5f).OnComplete(() =>
            {
                db.transform.GetChild(0).transform.gameObject.transform.DOLocalMoveX(0 , 0.3f);
            });
        }

        public void CloseDialogeAnim(GameObject db , System.Action actioncallBack = null)
        {
            db.transform.GetChild(0).transform.gameObject.transform.DOLocalMoveX(-1260f , 0.3f).OnComplete(() =>
            {
                db.GetComponent<Image>().DOFade(0 , 0.5f).OnComplete(() =>
                {
                    db.gameObject.SetActive(false);
                    db.transform.GetChild(0).transform.gameObject.transform.localPosition = new Vector3(1260 , 0 , 0);
                    actioncallBack?.Invoke();
                });
            });

        }

        private void OnDisable()
        {
            DisableMM = false;
        }

        public void RestartGame()
        {
            Adcounter = 0;
            num = 0;
            DisableMM = false;
            Epopping = false;
            //GoogleAdMobController.THIS.ShowInterstitialAd(); //AdCallPosition
            SceneManager.LoadScene(3);
        }
        public void PlayMiniGame()
        {
            StartCoroutine(GetActive_RandomMiniGame());
        }


        public void LevelId(int i)
        {
            PlayerPrefs.GetInt("LevelID", i);
            Debug.Log("level id is " + PlayerPrefs.GetInt("LevelID", i));
        }


        public void PlayParticles()
        {
            confettiParticles.SetActive(true);
            Invoke("StopParticles", 3f);
        
        }

        public void StopParticles()
        {
            confettiParticles.SetActive(false);
        }



    }


  
}