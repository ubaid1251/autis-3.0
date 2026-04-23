using DG.Tweening;
//using Firebase.Analytics;//lock
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadscene : MonoBehaviour
{
    public static int levelloaded;
    public string scenename;
    public bool isselection, ispianoScene;
    public GameObject LoadCanvas;
    private void OnMouseDown()
    {
        if (!isselection)
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }
    public void OnCheckLoadscene()
    {
        PlayerPrefs.SetInt("Completed",  1);
        //PlayerPrefs.SetInt("RateCounter", PlayerPrefs.GetInt("RateCounter") + 1);
        print(PlayerPrefs.GetInt("RateCounter"));
        DOTween.KillAll(false);
        /*var v = PlayerPrefs.GetString("SelectedCategory"); ;
        string stringWithoutSpaces = Regex.Replace(SceneManager.GetActiveScene().name, @"\s+", "");
        Debug.Log("String without Spaces: " + stringWithoutSpaces);
        FirebaseAnalytics.LogEvent("HomeButtonTapped", new Parameter("CategoryName", v), new Parameter("ActivityName", stringWithoutSpaces));//lock*/
        OnMouseUp();
    }
    private void OnMouseUp()
    {
        if (!isselection)
        {
            //transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            if (ispianoScene)
                scenename = gameObject.name;

            Debug.Log("Test Value: " + PlayerPrefs.GetInt("MainSelectionTest"));
            if (PlayerPrefs.GetInt("MainSelectionTest") == 0)
            {
                if (SceneManager.GetActiveScene().name == "MiniGames")
                {
                   // LoadingHandler.backPressed = true;
                }

                SceneManager.LoadScene(scenename);
                //LoadingHandler._LoadsceneName = scenename;
                
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "MiniGames")
                {
                  //  LoadingHandler.backPressed = true;
                }
                
                if (SceneManager.GetActiveScene().name == "CountingScene")
                {
                    SceneManager.LoadScene(scenename);
                   // LoadingHandler._LoadsceneName = scenename;
                }
                else
                {
                    SceneManager.LoadScene("2Selection screen");
                }
            }

            /*if (IntitializeAdmob.instance.play_ad && PlayerPrefs.GetInt("RemoveAds") == 0 && IntitializeAdmob.instance.IsInterAvailable())
            {
                LoadingHome.instance.loader.SetActive(true);
                LoadingHome.instance.loader.GetComponent<Loading>().SceneName = "New_SubSelection";
                
            }

            else */if (SceneManager.GetActiveScene().name != "ABTestScene" &&
                PlayerPrefs.GetInt("ABSelectionScene") == 1)
            {
                SceneManager.LoadScene("ABTestScene");
            }

            else
            {
                if (LoadCanvas)
                {
                    LoadCanvas.SetActive(true);
                }
                //SceneManager.LoadScene("2Selection screen");
            }
        }
    }
    public void BAckTo(int val)
    {
        levelloaded = val;
    }
    public void Clicked()
    {
        if (PlayerPrefs.GetInt("MainSelectionTest") == 0)
        {
            /*if (IntitializeAdmob.instance.IsInterAvailable() && !RateNew.RateUsHandler.instance.CheckRateLoadingCondition() && 
                (SceneManager.GetActiveScene().name == "Piano") && SceneManager.GetActiveScene().name == "Instruments" && SceneManager.GetActiveScene().name == "Lullabies"
                && SceneManager.GetActiveScene().name == "PlayAlong" && SceneManager.GetActiveScene().name == "SoundScene")
            {
                LoadingHome.instance.loader.GetComponent<Loading>().SceneName = "2Selection screen";
                LoadingHome.instance.loader.SetActive(true);
            }
            else*/
                SceneManager.LoadScene("2Selection screen");
        }
        else
        {
            /*if (IntitializeAdmob.instance.IsInterAvailable() && !RateNew.RateUsHandler.instance.CheckRateLoadingCondition() && 
                (SceneManager.GetActiveScene().name == "Piano" && SceneManager.GetActiveScene().name == "Instruments" && SceneManager.GetActiveScene().name == "Lullabies"
                && SceneManager.GetActiveScene().name == "PlayAlong" && SceneManager.GetActiveScene().name == "SoundScene"))
            {
                LoadingHome.instance.loader.GetComponent<Loading>().SceneName = "2Selection screen";
                LoadingHome.instance.loader.SetActive(true);
            }
            else*/
            {
                /*if (IntitializeAdmob.instance.IsInterAvailable())
                {
                    LoadingHome.instance.loader.GetComponent<Loading>().SceneName = "2Selection screen";
                    LoadingHome.instance.loader.SetActive(true);
                }
                else*/
                    SceneManager.LoadScene("2Selection screen");
            }
        }
    }
    public void Clicked2(GameObject scene)
    {
        Instantiate(scene);
        transform.parent.parent.gameObject.SetActive(false);
    }
}
/*
using DG.Tweening;
using Firebase.Analytics;//lock
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadscene : MonoBehaviour
{
    public static int levelloaded;
    public string scenename;
    public bool isselection, ispianoScene;
    public GameObject LoadCanvas;
    private void OnMouseDown()
    {
        if (!isselection)
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }
    public void OnCheckLoadscene()
    {
        PlayerPrefs.SetInt("Completed",  1);
        PlayerPrefs.SetInt("RateCounter", PlayerPrefs.GetInt("RateCounter") + 1);
        print(PlayerPrefs.GetInt("RateCounter"));
        DOTween.KillAll(false);
        var v = PlayerPrefs.GetString("SelectedCategory"); ;
        string stringWithoutSpaces = Regex.Replace(SceneManager.GetActiveScene().name, @"\s+", "");
        Debug.Log("String without Spaces: " + stringWithoutSpaces);
        InitializeFirebase_CB._Instance.LogFirebaseEvent("HomeButtonTapped", new Parameter("CategoryName", v), new Parameter("ActivityName", stringWithoutSpaces));//lock
        OnMouseUp();
    }
    private void OnMouseUp()
    {
        if (!isselection)
        {
            //transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            if (ispianoScene)
                scenename = gameObject.name;

            Debug.Log("Test Value: " + PlayerPrefs.GetInt("MainSelectionTest"));
            if (PlayerPrefs.GetInt("MainSelectionTest") == 0)
            {
                if (SceneManager.GetActiveScene().name == "MiniGames")
                {
                    LoadingHandler.backPressed = true;
                }
                LoadingHandler._LoadsceneName = scenename;
                //LoadingHandler._LoadsceneName = "SelectionScreenNew";
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "MiniGames")
                {
                    LoadingHandler.backPressed = true;
                }
                LoadingHandler._LoadsceneName = "New_SubSelection";
            }
            if (IntitializeAdmob.instance.IsInterAvailable()&&PlayerPrefs.GetInt("RemoveAds")==0)
            {
                LoadingHome.instance.loader.SetActive(true);
                LoadingHome.instance.loader.GetComponent<Loading>().SceneName = "New_SubSelection";
            }
            else
            {
                SceneManager.LoadScene("New_SubSelection");
            }
        }
    }
    public void BAckTo(int val)
    {
        levelloaded = val;
    }
    public void Clicked()
    {
        if (PlayerPrefs.GetInt("MainSelectionTest") == 0)
        {
            SceneManager.LoadScene("New_SubSelection");
        }
        else
        {
            SceneManager.LoadScene("New_SubSelection");
        }
    }
    public void Clicked2(GameObject scene)
    {
        Instantiate(scene);
        transform.parent.parent.gameObject.SetActive(false);
    }
}
*/
