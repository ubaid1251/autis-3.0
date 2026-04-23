using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SelectionMenu : MonoBehaviour
{
    public GameObject bg;
    public Color[] BgColors;
    public GameObject[] Colors,LockColors;
    public GameObject pnel,parental;
    public GameObject ProfilerBtns;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("Purchased") == 1)
        {
            for (int i = 0; i < LockColors.Length; i++)
            {
                LockColors[i].GetComponent<Button>().enabled = true;
            }
        }
        if (ResCheck.ResolutionType == ResType.tab)
        {
            ProfilerBtns.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
    void Start()
    {
        PlayerPrefs.SetInt("FirstAnim", 0);
        PlayerPrefs.SetInt("FirstPuzzle", 0);
        PlayerPrefs.SetInt("countAnimal", 0);
        PlayerPrefs.SetInt("countShape", 0);
        PlayerPrefs.SetInt("countScene", 0);
        PlayerPrefs.SetInt("countObjScene", 0);
        PlayerPrefs.SetInt("StartMatch", 0);
        PlayerPrefs.SetInt("DestMatch", 2);
        PlayerPrefs.SetInt("RemoveAds", 1);
        PlayerPrefs.SetInt("BuyShape", 0);

        if (RateUsHandler.Instance.CheckRateCondition())
        {
            RateUsHandler.Instance.rate.SetActive(true);
        }
    }
    public void RestorePurchase()
    {
        SoundManager.instance.PlayEffect_Instance(4);

        unityInAppPurchase_CB.instance.RestorePurchases();
    }

    // Update is called once per frame
    public void GotoScene(string num)
    {
        InitializeFirebase_CB.instance.LogFirebaseEvent(num+"_SceneLoaded");
        SoundManager.instance.PlayEffect_Instance(5);
        SceneManager.LoadScene(num);
    }
    public void SetColor(int Colrnum)
    {
        InitializeFirebase_CB.instance.LogFirebaseEvent(Colrnum + "_BackgroundColor");
        SoundManager.instance.PlayEffect_Instance(4);
        pnel.SetActive(false);
        for (int i = 0; i < Colors.Length; i++)
        {
            Colors[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        PlayerPrefs.SetInt("BGColorSet", Colrnum);
        bg.GetComponent<Image>().color = BgColors[Colrnum];
        Colors[Colrnum].transform.GetChild(0).gameObject.SetActive(true);
    }
    public void OpenColorPanel()
    {
        SoundManager.instance.PlayEffect_Instance(7);
        int numSelectd;
        numSelectd = PlayerPrefs.GetInt("BGColorSet");
        Colors[numSelectd].transform.GetChild(0).gameObject.SetActive(true);
    }
    public void Playsound()
    {
        SoundManager.instance.PlayEffect_Instance(7);
    }

    public void panelOff()
    {
        PlayerPrefs.SetInt("FirstTimeGamePlay", 1);
        pnel.SetActive(false);
    }
    public void ShowParental()
    {
        parental.SetActive(true);
    }
}
