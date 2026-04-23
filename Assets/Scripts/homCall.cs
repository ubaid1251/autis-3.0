using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class homCall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   

    }
    public void preshomePuzle()
    {
        PlayerPrefs.SetInt("FirstPuzzle", 0);
    }
    // Update is called once per frame
    public void Home()
    {
        DOTween.KillAll(false);
        SoundManager.instance.StopAllAnimalSounds();
        SoundManager.instance.StopAllBodyPartsSounds();
        SoundManager.instance.StopAllFruitSounds();
        SoundManager.instance.StopAllVegetableSounds();
        SoundManager.instance.StopAllShapeSounds();
        SoundManager.instance.PlayEffect_Instance(4);
        PlayerPrefs.SetInt("Completed", 1);
        PlayerPrefs.SetInt("RateCounter", PlayerPrefs.GetInt("RateCounter") + 1);
        PlayerPrefs.SetInt("FirstAnim", 0);
        SceneManager.LoadScene("Selection");
    }
}
