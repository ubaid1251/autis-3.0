using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] musicBtnSprite;
    public GameObject[] soundBtnSprite,Vibrate;
    public static Setting ins;
    private void Awake()
    {
        //if (ins == null)
        //{
            ins = this;
        //}
        //else if (ins != this)
        //{
        //    Destroy(gameObject);
        //}
        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    { 
        SoundManager.instance.CheckOnStart();
        if (PlayerPrefs.GetInt("IsStart") == 0)
        {
            PlayerPrefs.SetInt("Vibrate", 1);
            PlayerPrefs.SetInt("IsStart", 1);
        //    print("in");

        }
        if (PlayerPrefs.GetInt("Vibrate") == 1)
        {
       //     print("in2");
            Vibrate[0].SetActive(true);
            Vibrate[1].SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Vibrate") == 0)
        {
        //    print("in3");
            Vibrate[0].SetActive(false);
            Vibrate[1].SetActive(true);
        }
    }
    public void playSound()
    {
        SoundManager.instance.PlayEffect_Instance(4);
    }
    public void ForSound()
    {
        // SoundManager.instance.PlayEffect_Instance(4);
        SoundManager.instance.SoundSettingBtn_OnClick();
    }
    public void ForMusic()
    {
        // SoundManager.instance.PlayEffect_Instance(4);
        SoundManager.instance.MusicSettingBtn_OnClick();
    }
    public void VibrateOf()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        PlayerPrefs.SetInt("Vibrate", 0);
    }
    public void VibrateOn()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        PlayerPrefs.SetInt("Vibrate", 1);
    }
    public void PP()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        Application.OpenURL("https://pages.flycricket.io/ib-studios/privacy.html");
    }
    public void Term()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        Application.OpenURL("https://pages.flycricket.io/ib-studios/terms.html");
    }
    void Update()
    {
        
    }
}
