using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class SetNumber : MonoBehaviour
{
    public static SetNumber ins;
    public static int Count = 2; // Default value
    public GameObject Gameplay;
    public TMP_Text counterNumber;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {

        if (PlayerPrefs.GetInt("FirstAnim") == 1)
        {
            Gameplay.SetActive(true);
            gameObject.SetActive(false);
        }


       else if (PlayerPrefs.GetInt("FirstAnim")==0)
        {
            PlayerPrefs.SetInt("FirstAnim", 1);
            Count = 2;
            UpdateCounterText();
            PlayerPrefs.SetInt("IsText", 0);
        }
    }
    int limitnum;
    public void IncreaseCount()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        if (SceneManager.GetActiveScene().name == "Fruit" || SceneManager.GetActiveScene().name == "Vegatables" || SceneManager.GetActiveScene().name == "Objects")
        {
            limitnum=6;
        }
        else
        {
            limitnum = 4;
        }
        if (Count < limitnum)
        {
            Count++;
            UpdateCounterText();
        }
    }

    public void DecreaseCount()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        if (Count > 1)
        {
            Count--;
            UpdateCounterText();
        }
    }

    private void UpdateCounterText()
    {
        counterNumber.text = Count.ToString();
    }
    public void YesBTN()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        PlayerPrefs.SetInt("IsText", 1);
    }
    public void NoBTN()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        PlayerPrefs.SetInt("IsText", 0);
    }
    public void Playbtn()
    {
        SoundManager.instance.PlayEffect_Instance(5);
        //Debug.Log("AnimalCount set to: " + Count);
        Gameplay.SetActive(true);
        gameObject.SetActive(false);
    }
}