using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoAudioObj : MonoBehaviour
{
    //public AudioClip newClip, OldClip;
    //  public Sprite PlayImg, PauseImg;
    public AudioSource LoopAudio, PianoAudio;
    public Sprite NewPlayImg, NewPauseImg;
    public Image Current;
    public int id = 0;

    public DancingPianoController controller;
    public clickLocker locker;

    public void AutoClick()
    {
        Debug.Log("Value: " + PlayerPrefs.GetInt("SelectedIndex"));
        if (locker && PlayerPrefs.GetInt("isPurchased") == 0)
        {
            if (PlayerPrefs.GetInt("SelectedIndex") > 1 && PlayerPrefs.GetInt("isPurchased") == 0)
            {
                locker.MoveToPurchase();
            }
        }
        else
        {
            if (id == 0)
            {
                Current.sprite = NewPauseImg;
                LoopAudio.enabled = false;
                if (PianoAudio.enabled == true)
                    PianoAudio.Play();
                id = 1;
                if (controller)
                {
                    controller.AutoPlay();
                }
            }
            else
            {
                Current.sprite = NewPlayImg;
                LoopAudio.enabled = true;
                PianoAudio.Stop();
                id = 0;
                if (controller)
                {
                    controller.StopAutoPlay();
                }
            }
        }
    }
}