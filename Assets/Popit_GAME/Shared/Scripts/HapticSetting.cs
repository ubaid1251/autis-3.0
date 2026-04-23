using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HapticSetting : MonoBehaviour
{

    public Sprite hapticOn;
    public Sprite hapticOff;

    public  bool hapticEnabled;
    public Button hapticButton;


    private void Start()
    {

        hapticEnabled = PlayerPrefs.GetInt("HapticEnabled") == 0 ? true : false;

        Debug.Log("is haptics in sound setting is " + hapticEnabled);

        if (hapticEnabled)
            hapticButton.image.sprite = hapticOn;
        else
            hapticButton.image.sprite = hapticOff;

    }

    public void ToggleHaptics()
    {

        if (hapticEnabled)
        {
            hapticButton.image.sprite = hapticOff;
            PlayerPrefs.SetInt("HapticEnabled", 1);
            hapticEnabled = false;
            Debug.Log(" haptics should be false/off  " + hapticEnabled);

        }
        else
        {
            hapticButton.image.sprite = hapticOn;
            PlayerPrefs.SetInt("HapticEnabled", 0);
            hapticEnabled =true;
            Debug.Log(" haptics should be true/on  " + hapticEnabled);


        }
         Dino.SoundManager.Instance.SetHapticFlag();

    }

    private void UpdateButtonImage()
    {
        if (hapticEnabled)
        {
            hapticButton.image.sprite = hapticOn;
   
            PlayerPrefs.SetInt("HapticEnabled", 1);
        }
        else
        {
            hapticButton.image.sprite = hapticOff;
            PlayerPrefs.SetInt("HapticEnabled", 0);
       

        }

    }

    private void SaveHapticSettings()
    {
       // PlayerPrefs.SetInt("HapticEnabled", hapticEnabled ? 1 : 0);
      //  PlayerPrefs.Save();
       
    }

    private void LoadHapticSettings()
    {
        int hapticEnabledInt = PlayerPrefs.GetInt("HapticEnabled", 1);
        hapticEnabled = hapticEnabledInt == 1;
    }


}
