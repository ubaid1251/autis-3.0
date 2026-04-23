using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopSounds : MonoBehaviour
{
    public AudioSource PopsoundManager;
    public AudioClip Popsound, warningSound;
    public bool selectedPop;

    public void setSound(AudioClip clipEffect)
    {
        PopsoundManager.PlayOneShot(clipEffect);
    }
    private void OnDisable()
    {
        selectedPop = false;
    }

}
