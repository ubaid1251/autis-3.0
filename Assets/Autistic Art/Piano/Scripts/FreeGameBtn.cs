using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
//using Firebase.Analytics;//lock

public class FreeGameBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public string New_String, OtherString;
    public GameObject Loading;
    public GameObject[] Off_Object;
    public bool Is_Done;
    public static bool on = false;

    IEnumerator Start()
    {
        if (PlayerPrefs.GetInt("FreeGamesBtns") != 1)
        {
            if (Is_Done && SelectScene.FreeGame_bool == true)
            {
                foreach (var item in Off_Object)
                {
                    item.SetActive(false);
                }
            }
            yield return new WaitForSeconds(3.0f);
            if (SelectScene.FreeGame_bool == true)
            {
                on = true;
                gameObject.SetActive(true);
                gameObject.transform.DOScale(0.3f, 0.5f);
            }
        }
        else if (PlayerPrefs.GetInt("FreeGamesBtns") == 1 && on)
        {
            if (SelectScene.FreeGame_bool == true)
            {
                gameObject.SetActive(true);
                gameObject.transform.DOScale(0.3f, 0f);
            }
        }
        else
        {
            yield return new WaitForSeconds(3.0f);
            if (SelectScene.FreeGame_bool == true)
            {
                on = true;
                gameObject.SetActive(true);
                gameObject.transform.DOScale(0.3f, 0.5f);
            }
        }
    }

    public void OnCheckLoadStirn()
    {
        on = false;
        if (Loading)
        {
            Loading.SetActive(true);
        }
        if (PlayerPrefs.GetInt("FreeGamesBtns") != 1)
        {
            //LoadingHandler._LoadsceneName = New_String;
        }
        else
        {
            //string stringWithoutSpaces = Regex.Replace(SceneManager.GetActiveScene().name, @"\s+", "");
            string stringWithoutSpaces = SceneManager.GetActiveScene().name.Replace(" ", "");
            Debug.Log("String without Spaces: " + stringWithoutSpaces);
            //FirebaseAnalytics.LogEvent("ActivitySwitched", new Parameter("CategoryName", "FreeGames"), new Parameter("ActivityName", stringWithoutSpaces));//lock
           // LoadingHandler._LoadsceneName = OtherString;
        }
    }
}
