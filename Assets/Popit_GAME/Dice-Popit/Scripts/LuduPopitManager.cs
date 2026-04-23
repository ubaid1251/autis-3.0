using GameWork.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LuduPopitManager : MonoBehaviour
{
    public PlayerBehaviour p1, p2, AI;
    public Dice d1;
    public GameObject[] Popits;
    public GameObject HomePanel;
    public GameObject GamePanel;
    public GameObject CompletePanel;
    public Text winText;
    public Text p1Text, p2Text;
    private PlayerBehaviour PB1, PB2;
    private GameObject CurrentTray;
    public GameObject Player1Image, Player2Image;
    private int currentBoard;
    private bool isSinglePlayer;
    public GameObject Tutorial;
    bool showingtutorial;
    // Start is called before the first frame update
    void Start()
    {
        Player2Image.SetActive(false);
        //AdmobCalling._instance.showBanner();
        //AdmobCalling._instance.LoadInterstitial(); //AdCallingPosition
    }

    public void IsSinglePlayer(bool value)
    {
        isSinglePlayer = value;
    }
    public void PlayVsPlayer()
    {

        Play(p1 , p2);
        p1.name = "Player 1";
        p2.name = "Player 2";
    }

    public void PlayVsRobot()
    {
        Play(p1 , AI);
        p1.name = "You";
        p2.name = "Robot";
    }
    void ShowGamePanel()
    {
        //HomePanel.SetActive(false);
        GamePanel.SetActive(true);
        CompletePanel.SetActive(false);
        FindObjectOfType<UIManager>().ShowMainScreen();
    }
    public void Play(PlayerBehaviour pp1 , PlayerBehaviour pp2)
    {
        ShowGamePanel();

        CurrentTray.SetActive(true);
        d1.gameObject.SetActive(true);
        pp1.gameObject.SetActive(true);
        pp2.gameObject.SetActive(true);
        PB1 = pp1;
        PB2 = pp2;
        Player1Image.SetActive(true);
        Player2Image.SetActive(false);
        PB1.InitBehaviour(CurrentTray.transform.GetChild(0) , d1 , "P1").OnComplete((win) =>
        {
            if (win)
            {
                Debug.Log("Player won");
                //if(GMAdsManager.Instance)
                //GMAdsManager.Instance.Show_Interstitial();
                //Debug.Log("Ad showed");
                GameComplete(PB1);
            }

            else
            {
                Debug.Log("player turn completed");
                Player2();
            }

        });
        //PB1.name = "Player 1";
        PB2.InitBehaviour(CurrentTray.transform.GetChild(1) , d1 , "P2").OnComplete((win) =>
        {
            if (win)
            {
                Debug.Log("robot won");
                //if (GMAdsManager.Instance)
                //    GMAdsManager.Instance.Show_Interstitial();
                //Debug.Log("Ad showed");
                GameComplete(PB2);
            }
            else
            {
                Debug.Log("robot turn completed");
                Player1();
            }
        });

        Tutorial.SetActive(true);
        showingtutorial = true;
        p1Text.text = PB1.name;
        p2Text.text = PB2.name;
        Player1();
    }
    void Player1()
    {
        Player1Image.SetActive(true);
        Player2Image.SetActive(false);
        PB1.Throw();
    }

    void Player2()
    {
        Player1Image.SetActive(false);
        Player2Image.SetActive(true);
        PB2.Throw();
    }
    public void Home()
    {
        Invoke("ShowAd" , 1.0f);
        Debug.Log("this is interstitial add ");
        PB1.RemoveListner();
        PB2.RemoveListner();
        PB1.gameObject.SetActive(false);
        PB2.gameObject.SetActive(false);
        CurrentTray.SetActive(false);
        d1.gameObject.SetActive(false);
        GamePanel.SetActive(false);
        CompletePanel.SetActive(false);
        FindObjectOfType<UIManager>().ShowMainScreen();
        StopAllCoroutines();
        //AdmobCalling._instance.ShowInterstitial(); //AdCallingPosition
    }

    public void SelectRandomTray()
    {
        SelectTray(Random.Range(0 , Popits.Length));
    }
    public void SelectTray(int index)
    {
        var selectedObj = EventSystem.current.currentSelectedGameObject;
        var rewardBase = selectedObj.transform.GetComponent<RewardBaseUnlock>();

        if (rewardBase.isLocked)
        {
            //if (GMAdsManager.Instance)
                //GMAdsManager.Instance.Show_Rewarded(rewardBase.UnlockedLevel);
        }
        else
        {

            Invoke("ShowAd", 1.0f);
            if (CurrentTray)
                CurrentTray.SetActive(false);
            CurrentTray = Popits[index];
            if (isSinglePlayer)
                PlayVsRobot();
            else
                PlayVsPlayer();
            //   GoogleAdMobController.THIS.ShowInterstitialAd();

        }

    }
    public void Replay()
    {
        Invoke("ShowAd" , 1.0f);
        Debug.Log("this is interstitial add ");
        PB1.RemoveListner();
        PB2.RemoveListner();
        Play(PB1 , PB2);
        CompletePanel.SetActive(false);
        GamePanel.SetActive(true);
        HomePanel.SetActive(false);
        
        StopAllCoroutines();
        // AdmobCalling._instance.ShowInterstitial(); //AdCallingPosition

    }
    void GameComplete(PlayerBehaviour p)
    {
        DiceSpace.SoundManager.Instance.PlayOneShot("win");


        if (Dino.SoundManager.isHaptic)
        {
            DiceSpace.SoundManager.Instance.HapticSuccess();
            Debug.Log("in dice 2 game vibrations are " + Dino.SoundManager.isHaptic);
        }

        winText.text = p.name.ToUpper();
        GamePanel.SetActive(true);
        CompletePanel.SetActive(true);
        d1.gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if (showingtutorial && Input.GetMouseButtonDown(0))
        {
            showingtutorial = false;
            Tutorial.SetActive(false);
        }
    }
    public void ShowAd()
    {
        //ads GoogleAdMobController.THIS.ShowInterstitialAd();
        Debug.Log("this is interstitial add ");
    }
}
