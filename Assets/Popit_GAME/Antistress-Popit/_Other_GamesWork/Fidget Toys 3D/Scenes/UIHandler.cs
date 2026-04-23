using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public GameObject can;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void HomeButton()
    {
        //SceneManager.LoadScene("Selection");
        StartCoroutine("MoveHome");
    }

    IEnumerator MoveHome()
    {
        Instantiate(can);
        yield return new WaitForSeconds(2f);
        //VieAds.AdmobInterstertial();
        //Advertisements.Instance.ShowInterstitial();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Selection");
        //PlayerPrefs.SetInt("Ads", 0);
        //if (PlayerPrefs.GetInt("Ads") >= 3)
        //{
        //    Instantiate(can);
        //    yield return new WaitForSeconds(2f);
        //    //VieAds.AdmobInterstertial();
        //    Advertisements.Instance.ShowInterstitial();
        //    yield return new WaitForSeconds(1f);
        //    SceneManager.LoadScene("Selection");
        //    PlayerPrefs.SetInt("Ads", 0);

        //}
        //else
        //{
        //    SceneManager.LoadScene("Selection");
        //    PlayerPrefs.SetInt("Ads", PlayerPrefs.GetInt("Ads") + 1);
        //}
        //Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
    }
    public void ReloadButton()
    {
        StartCoroutine("MoveReplay");

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator MoveReplay()
    {
        Instantiate(can);
        yield return new WaitForSeconds(2f);
        //VieAds.AdmobInterstertial();
       // Advertisements.Instance.ShowInterstitial();
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("Ads", 0);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    //    if (PlayerPrefs.GetInt("Ads") >= 3)
    //    {
    //        Instantiate(can);
    //        yield return new WaitForSeconds(2f);
    //        //VieAds.AdmobInterstertial();
    //        Advertisements.Instance.ShowInterstitial();
    //        yield return new WaitForSeconds(1f);
    //        PlayerPrefs.SetInt("Ads", 0);

    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    }
    //    else
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //        PlayerPrefs.SetInt("Ads", PlayerPrefs.GetInt("Ads") + 1);

    //    }
    //    //Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
