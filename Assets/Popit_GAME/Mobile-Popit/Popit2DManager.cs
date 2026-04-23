using GameWork.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MobileCase
{
    public class Popit2DManager : MonoBehaviour
    {

        public GameObject CurrentBG, SelectionUI, Gamepanel, Can;
        public GameObject[] Levels;
        public GameObject[] ButtonUI;
        int levelPlayed;
        int coins;
        public Text CoinsText;                   //mobile
        int AdCount;
        int HomeAd;
        public static int level;

        public static Popit2DManager Instance;
        public GameObject confettiParticles;

        private void Awake()
        {
            Instance = this;

        }

        // Start is called before the first frame update
        void Start()
        {
            InitLevel();
        }


        public void InitLevel()
        {
            for (int i = 0; i < ButtonUI.Length; i++)
            {
                int sCoins = PlayerPrefs.GetInt("Coins" + i);
                coins += sCoins;
                if (ButtonUI[i].transform.GetComponentInChildren<Text>())
                {
                    ButtonUI[i].transform.GetChild(1).GetComponentInChildren<Text>().text = sCoins.ToString();
                }

            }

            if (CoinsText)
                CoinsText.text = coins.ToString();

        }

        void StartToPlay()
        {
            //PantraAdsManager.Instance.ShowBanner();

            Levels[level].SetActive(true);
            if (CurrentBG)
                CurrentBG.SetActive(false);

            CurrentBG = Levels[level].GetComponentInChildren<Popit2D>().BG;
            CurrentBG.SetActive(true);
            MobileCaseManger.Instance.ShowCoins(level);
        }

        public void selectionButtons(int ButtonNum)
        {

            var selectedObj = EventSystem.current.currentSelectedGameObject;
            var rewardBase = selectedObj.transform.parent.GetComponent<RewardBaseUnlock>();


            if (rewardBase.isLocked)
            {
                //if (GMAdsManager.Instance)
                //    GMAdsManager.Instance.ShowBothRewarded(rewardBase.UnlockedLevel);
            }

            else
            {


                LockingScreenSlection lockScreenScrip = EventSystem.current.currentSelectedGameObject.GetComponent<LockingScreenSlection>();
                if (lockScreenScrip == null)
                    return;

                if (lockScreenScrip.IsUnlockedItemTrue)
                {
                    level = ButtonNum;
                    StartCoroutine(AdsShow());
                }
                else
                {
                    // watch ad to  unlocked 
                    if (!lockScreenScrip.IsInAppPurchased)
                    {
                        //GoogleAdMobController.THIS.ShowRewardedAd(() =>
                        //{
                        //    lockScreenScrip.OpenLockedItem_FromAdsORMarkete(); //AdCallPosition
                        //    //load level
                        //    OnRewardComplete();
                        //});
                    }
                }
            }
        }

        public void OnRewardComplete()
        {
            SelectionUI.SetActive(false);
            Gamepanel.SetActive(true);
            StartToPlay();
        }

        IEnumerator AdsShow()
        {
            Instantiate(Can);

            yield return new WaitForSeconds(1f);
            //if (levelPlayed > 0)
            //GoogleAdMobController.THIS.ShowInterstitialAd(); //AdCallPosition

            levelPlayed++;
            yield return new WaitForSeconds(.5f);
            SelectionUI.SetActive(false);
            Gamepanel.SetActive(true);
            StartToPlay();
        }

        public void ResetPopitsShowads()
        {
            //if (AdCount < 0)
            //{
            //    AdCount++;
            //}
            //else
            //{
            //  Instantiate(Can);
            // GoogleAdMobController.THIS.ShowInterstitialAd(); //AdCallPosition
            AdCount = 0;
            // }
            // Levels[level].GetComponentInChildren<Popit2D>().EnablePopit();
        }


        public void HomeButtonFun()
        {
            if (/*HomeAd < 3 || */PlayerPrefs.GetInt("NoAds") == 1)
            {
                SelectionUI.SetActive(true);
                Gamepanel.SetActive(false);
                InitLevel();
                Levels[level].GetComponentInChildren<Popit2D>().EnablePopit();
                for (int i = 0; i < Levels.Length; i++)
                {
                    Levels[i].SetActive(false);
                }
                HomeAd++;
            }
            else
            {
                Instantiate(Can);
                //GoogleAdMobController.THIS.ShowInterstitialAd(); //AdCallPosition
                SelectionUI.SetActive(true);
                Gamepanel.SetActive(false);
                InitLevel();
                Levels[level].GetComponentInChildren<Popit2D>().EnablePopit();

                for (int i = 0; i < Levels.Length; i++)
                {
                    Levels[i].SetActive(false);
                }

                HomeAd = 0;
            }
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

        /*
         IEnumerator ShowRewardedAd()
         {
             Instantiate(Can);
             yield return new WaitForSeconds(1f);
             PantraAdsManager.Instance.ShowRewardedAd((success,error) => 
             {
                 if (success)
                 {
                     SelectionUI.SetActive(false);
                     Gamepanel.SetActive(true);
                     StartToPlay();
                 }
             });
             yield return new WaitForSeconds(.5f);

         }

         public void SelectRewardedButton(int buttonNum)
         {
             if (PlayerPrefs.GetInt("Rewarded" + buttonNum) == 1 || PlayerPrefs.GetInt("NoAds") == 1)
                 selectionButtons(buttonNum);
             else
             {
                 level = buttonNum;
                 StartCoroutine(ShowRewardedAd());

             }
         }
         public void DisableAd()
         {
             PlayerPrefs.SetInt("NoAds", 1);
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
     */





    }


}