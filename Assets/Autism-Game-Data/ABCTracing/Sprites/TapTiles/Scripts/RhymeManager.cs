using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RhymeManager : MonoBehaviour
{
    public SongManager SongManager;
    public GameObject BackingTrack;
    //public GameObject FinalPar;
    public GameObject musicOn, musicOff;
    public RectTransform home, music,progression,banner;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            //IntitializeAdmob.Instance?.ShowBanner();
        }
        else
        {
            //home.DOAnchorPosY(-100, 0);
            //music.DOAnchorPosY(-280, 0);
            progression.DOAnchorPosY(-100, 0);
            banner.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("bgm") == 1)
        {
            musicOn.SetActive(false);
            musicOff.SetActive(true);
            BackingTrack.GetComponent<AudioSource>().volume = 0;
        }
        if (PlayerPrefs.GetInt("IndiForPlay") == 1)
            UINote.s = false;
        StartRhyme();
        // Invoke(nameof(playAd),BackingTrack.GetComponent<AudioSource>().clip.length/2);
    }
    public void StartRhyme()
    {
        //Invoke(nameof(activeNext), BackingTrack.GetComponent<AudioSource>().clip.length);
        BackingTrack.SetActive(true);
        SongManager.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (BackingTrack.GetComponent<AudioSource>().time >= BackingTrack.GetComponent<AudioSource>().clip.length && !completed)
        {
            InitializeFirebase_CB.instance.LogFirebaseEvent("ABCTilesCompleted");
            completed = true;
            activeNext();
        }
    }
    bool completed = false;
    void activeNext()
    {
        InitializeFirebase_CB.instance.LogFirebaseEvent("ABCTilesReloaded");
        PlayerPrefs.SetString("ReloadScene", "TapTiles");
        // FinalPar.GetComponent<LoadingHandler>().loadNextScene = true;
        // FinalPar.GetComponent<LoadingHandler>().showBannerEnd = false;
        // if (PlayerPrefs.GetInt("RemoveAds") == 0)
        //     FinalPar.SetActive(true);
        // else
        {
            DOTween.KillAll(false);
            SceneManager.LoadScene("TapTiles");
        }
    }

    // void playAd()
    // {
    //     if (PlayerPrefs.GetInt("RemoveAds") == 0)
    //     {
    //         Time.timeScale = 0;
    //         // FinalPar.GetComponent<LoadingHandler>().loadNextScene = false;
    //         // FinalPar.GetComponent<LoadingHandler>().showBannerEnd = true;
    //        // FinalPar.SetActive(true);
    //     }
    // }
}
