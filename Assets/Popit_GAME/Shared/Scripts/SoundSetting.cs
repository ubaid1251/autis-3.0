using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public Sprite soundOn; 
    public Sprite soundOff;
    public Button soundButton;
    private int sound;

    private void Start()
    {

        sound /*AudioListener.volume*/ = PlayerPrefs.GetInt("SoundVolume") == 0 ? 1 : 0;
        AudioListener.volume = sound;
        if (AudioListener.volume == 1)
            soundButton.image.sprite = soundOn;
        else
            soundButton.image.sprite = soundOff;

    }

    public void ToggleSound()
    {
        //if (AudioListener.volume == 1)
        //{
        //    AudioListener.volume = 0;
            
        //}
        //else
        //{
        //    AudioListener.volume = 1;
         
        //}


        //UpdateButtonImage();
        //SaveSoundSettings();



        if (AudioListener.volume == 1)
        {
            soundButton.image.sprite = soundOff;
            PlayerPrefs.SetInt("SoundVolume", 1);
            AudioListener.volume = 0;
        }
        else
        {
            soundButton.image.sprite = soundOn;         
            PlayerPrefs.SetInt("SoundVolume", 0);
            AudioListener.volume = 1;


        }

    }

    private void UpdateButtonImage()
    {
        //if (AudioListener.volume == 1)
        //{
        //    GetComponent<Image>().sprite = soundOn;

        //}
        //if (AudioListener.volume == 0)
        //{
        //    GetComponent<Image>().sprite = soundOff;

        //}


        if (AudioListener.volume == 1)
        {
            soundButton.image.sprite = soundOn;
            PlayerPrefs.SetInt("SoundVolume", 1);
        }
        else
        {
            soundButton.image.sprite = soundOff;
            PlayerPrefs.SetInt("SoundVolume", 0);

        }

    }


    private void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("SoundVolume", AudioListener.volume);
        PlayerPrefs.Save();
    }

    private void LoadSoundSettings()
    {
        float volume = PlayerPrefs.GetFloat("SoundVolume", 1);
        AudioListener.volume = volume;


    }



}
