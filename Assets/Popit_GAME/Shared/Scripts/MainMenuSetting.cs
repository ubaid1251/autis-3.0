using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuSetting : MonoBehaviour
{


    //private static bool is_AP_Shown;


    private void Start()
    {
        //if (!is_AP_Shown)
        //{
        //    //if (GMAdsManager.Instance)
        //    //    GMAdsManager.Instance.Show_App_Open();
        //    //is_AP_Shown = true;
        //}
        //if (GMAdsManager.Instance)
        //    GMAdsManager.Instance.Load_Interstitial();
    }

    private void OnEnable()
    {
        //if (GMAdsManager.Instance)
        //    GMAdsManager.Instance.LoadAndShow_SmallBanner_1();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
