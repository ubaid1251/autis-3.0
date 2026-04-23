// DanielLochner.Assets.SimpleScrollSnap;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadsceneCustom : MonoBehaviour
{
    public string scenename, TestSecne;
    public bool IsLocked = true, ApplyTest;
    public static int Save_Scene;
    public bool isWorldInstrument;

    private void OnEnable()
    { 
        if (ApplyTest)
        {
            if (PlayerPrefs.GetInt("MainSelectionTest") == 1)
            {
                InstrumentHandler.SelectedIndex = 0;
                scenename = TestSecne;
            }
        }
        
    }


    public void Clicked()
    {
        if (IsLocked)
        {
            PlayerPrefs.SetInt("LockedScene", 1);
        }
        else
        {
            PlayerPrefs.SetInt("LockedScene", 0);
        }

        PlayerPrefs.SetInt("SelectedMode", 0);
        PlayerPrefs.SetInt("CameFSubSel", 1);
        PlayerPrefs.SetInt("CountT", 0);
        var v = EventSystem.current.currentSelectedGameObject;
        Debug.Log("Event System Causing error: " + v.name);
        if (isWorldInstrument && PlayerPrefs.GetInt("MainSelectionTest") == 1)
        {
            PlayerPrefs.SetInt("LockedScene", 0);
        }
        
        if (PlayerPrefs.GetInt("ABSelectionScene") == 1 &&
            (v.name == "Home" || v.name == "Home (1)" || v.name == "HomeBtn"))
        {
            Debug.Log("Causing Error");
            scenename = "ABTestScene";
        }

        /*if (IntitializeAdmob.instance.play_ad && PlayerPrefs.GetInt("RemoveAds") == 0 &&
            (v.name == "Home" || v.name == "Home (1)" || v.name == "HomeBtn") && IntitializeAdmob.instance.IsInterAvailable() && !RateNew.RateUsHandler.instance.CheckRateLoadingCondition())
        {
            Debug.Log("Getting Called");
            LoadingHome.instance.loader.GetComponent<Loading>().SceneName = scenename;
            LoadingHome.instance.loader.GetComponent<Loading>().moveToScene = true;
            LoadingHome.instance.loader.SetActive(true);
        }
        else
        {
            Debug.Log("Getting Called");
            DOTween.KillAll(false);
            SceneManager.LoadScene(scenename);
        }*/
        
        /*if (IntitializeAdmob.instance.play_ad && PlayerPrefs.GetInt("RemoveAds") == 0 &&
            !(v.name == "Home" || v.name == "Home (1)" || v.name == "HomeBtn" || v.name == "Back") && IntitializeAdmob.instance.IsInterAvailable() && !RateNew.RateUsHandler.instance.CheckRateLoadingCondition())
        {
            LoadingHome.instance.loader.GetComponent<Loading>().SceneName = scenename;
            LoadingHome.instance.loader.GetComponent<Loading>().moveToScene = true;
            LoadingHome.instance.loader.SetActive(true);
        }
        else*/ /*if(IntitializeAdmob.instance.play_ad && PlayerPrefs.GetInt("RemoveAds") == 0 &&
                (v.name == "Home" || v.name == "Home (1)" || v.name == "HomeBtn" || v.name == "Back") && IntitializeAdmob.instance.IsInterAvailable())

        {
            LoadingHome.instance.loader.GetComponent<Loading>().SceneName = scenename;
            LoadingHome.instance.loader.GetComponent<Loading>().moveToScene = true;
            LoadingHome.instance.loader.SetActive(true);
        }
        else*/
        {
            Debug.Log("Loading Scene issue");
            DOTween.KillAll(false);
            if (MainSelection)
            {
                StartCoroutine(waitForLoad());
            }
            else
            {
                SceneManager.LoadScene(scenename);
            }
        }
        
    }

    IEnumerator waitForLoad()
    {
        if (PlayerPrefs.GetFloat("ClipDuration") > 0)
        {
            yield return new WaitForSeconds(PlayerPrefs.GetFloat("ClipDuration"));
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }
        SceneManager.LoadScene(scenename);
    }

    public bool MainSelection;
    
    
    public void OnCheckCustomValue(int id)
    {
       // RandPop._SceneNum = id;
    }

    public void OnCheckLoadScene(int index)
    {
        if (IsLocked)
        {
            PlayerPrefs.SetInt("LockedScene", 1);
        }
        else
        {
            PlayerPrefs.SetInt("LockedScene", 0);
        }

        PlayerPrefs.SetInt("CameFSubSel", 1);
        PlayerPrefs.SetInt("CountT", 0);
       // activeGameMini.activeObj = index;
        if (index == 4)
        {
            PlayerPrefs.SetInt("play", 1);
        }

        if (index == 7)
        {
            PlayerPrefs.SetInt("play1", 1);
            PlayerPrefs.SetInt("play", 1);
        }

        if (index == 9)
        {
            PlayerPrefs.SetInt("play2", 1);
            PlayerPrefs.SetInt("play1", 1);
            PlayerPrefs.SetInt("play", 1);
        }

        if (index == 11)
        {
            PlayerPrefs.SetInt("play3", 1);
            PlayerPrefs.SetInt("play2", 1);
            PlayerPrefs.SetInt("play1", 1);
            PlayerPrefs.SetInt("play", 1);
        }

        Save_Scene = index;

        //Fruitdetection.flag = false;
        var v = EventSystem.current.currentSelectedGameObject;
        /*if (IntitializeAdmob.instance.play_ad && PlayerPrefs.GetInt("RemoveAds") == 0 &&
            !(v.name == "Home" || v.name == "Home (1)" || v.name == "HomeBtn" || v.name == "Back") && IntitializeAdmob.instance.IsInterAvailable() && !RateNew.RateUsHandler.instance.CheckRateLoadingCondition())
        {
            LoadingHome.instance.loader.GetComponent<Loading>().SceneName = scenename;
            LoadingHome.instance.loader.GetComponent<Loading>().moveToScene = true;
            LoadingHome.instance.loader.SetActive(true);
        }
        else*/ /*if(IntitializeAdmob.instance.play_ad && PlayerPrefs.GetInt("RemoveAds") == 0 &&
                    (v.name == "Home" || v.name == "Home (1)" || v.name == "HomeBtn" || v.name == "Back") && IntitializeAdmob.instance.IsInterAvailable())

        {
            LoadingHome.instance.loader.GetComponent<Loading>().SceneName = scenename;
            LoadingHome.instance.loader.GetComponent<Loading>().moveToScene = true;
            LoadingHome.instance.loader.SetActive(true);
        }
        else*/
        {
            DOTween.KillAll(false);
            SceneManager.LoadScene(scenename);
        }
    }

    public void Clicked11()
    {
        var v = EventSystem.current.currentSelectedGameObject;
        if (SceneManager.GetActiveScene().name == "ABCTracingLine" &&
            PlayerPrefs.GetString("ABC_Type") == "Sub_Selection_Small_ABC")
        {
            Debug.Log("Home abc");
            scenename = "Sub_Selection_Small_ABC";
        }
        else if (SceneManager.GetActiveScene().name == "ABCTracingLine" &&
                 PlayerPrefs.GetString("ABC_Type") == "Sub_Selection")
        {
            scenename = "Sub_Selection";
        }

        /*if (IntitializeAdmob.instance.play_ad && PlayerPrefs.GetInt("RemoveAds") == 0 &&
            (v.name == "Home" || v.name == "Home (1)" || v.name == "HomeBtn" || v.name == "Back") && IntitializeAdmob.instance.IsInterAvailable() && !RateNew.RateUsHandler.instance.CheckRateLoadingCondition())
        {
            LoadingHome.instance.loader.GetComponent<Loading>().SceneName = scenename;
            LoadingHome.instance.loader.GetComponent<Loading>().moveToScene = true;
            LoadingHome.instance.loader.SetActive(true);
        }
        else
        {
            DOTween.KillAll(false);
            SceneManager.LoadScene(scenename);
        }*/
        /*if (IntitializeAdmob.instance.play_ad && PlayerPrefs.GetInt("RemoveAds") == 0 &&
            !(v.name == "Home" || v.name == "Home (1)" || v.name == "HomeBtn" || v.name == "Back") && IntitializeAdmob.instance.IsInterAvailable() && !RateNew.RateUsHandler.instance.CheckRateLoadingCondition())
        {
            LoadingHome.instance.loader.GetComponent<Loading>().SceneName = scenename;
            LoadingHome.instance.loader.GetComponent<Loading>().moveToScene = true;
            LoadingHome.instance.loader.SetActive(true);
        }
        else*/ /*if(IntitializeAdmob.instance.play_ad && PlayerPrefs.GetInt("RemoveAds") == 0 &&
                (v.name == "Home" || v.name == "Home (1)" || v.name == "HomeBtn" || v.name == "Back") && IntitializeAdmob.instance.IsInterAvailable())

        {
            LoadingHome.instance.loader.GetComponent<Loading>().SceneName = scenename;
            LoadingHome.instance.loader.GetComponent<Loading>().moveToScene = true;
            LoadingHome.instance.loader.SetActive(true);
        }
        else*/
        {
            DOTween.KillAll(false);
            SceneManager.LoadScene(scenename);
        }
    }

    public void LoadScene()
    {
        var v = EventSystem.current.currentSelectedGameObject;
        /*if (IntitializeAdmob.instance.play_ad && PlayerPrefs.GetInt("RemoveAds") == 0 &&
            (v.name == "Home" || v.name == "Home (1)" || v.name == "HomeBtn" || v.name == "Back") && IntitializeAdmob.instance.IsInterAvailable() && !RateNew.RateUsHandler.instance.CheckRateLoadingCondition())
        {
            LoadingHome.instance.loader.GetComponent<Loading>().SceneName = scenename;
            LoadingHome.instance.loader.GetComponent<Loading>().moveToScene = true;
            LoadingHome.instance.loader.SetActive(true);
        }
        else
        {
            DOTween.KillAll(false);
            SceneManager.LoadScene(scenename);
        }*/
        /*if (IntitializeAdmob.instance.play_ad && PlayerPrefs.GetInt("RemoveAds") == 0 &&
            !(v.name == "Home" || v.name == "Home (1)" || v.name == "HomeBtn" || v.name == "Back") && IntitializeAdmob.instance.IsInterAvailable() && !RateNew.RateUsHandler.instance.CheckRateLoadingCondition())
        {
            LoadingHome.instance.loader.GetComponent<Loading>().SceneName = scenename;
            LoadingHome.instance.loader.GetComponent<Loading>().moveToScene = true;
            LoadingHome.instance.loader.SetActive(true);
        }
        else*/ /*if(IntitializeAdmob.instance.play_ad && PlayerPrefs.GetInt("RemoveAds") == 0 &&
                (v.name == "Home" || v.name == "Home (1)" || v.name == "HomeBtn" || v.name == "Back") && IntitializeAdmob.instance.IsInterAvailable())

        {
            LoadingHome.instance.loader.GetComponent<Loading>().SceneName = scenename;
            LoadingHome.instance.loader.GetComponent<Loading>().moveToScene = true;
            LoadingHome.instance.loader.SetActive(true);
        }
        else*/
        {
            DOTween.KillAll(false);
            SceneManager.LoadScene(scenename);
        }
    }
}