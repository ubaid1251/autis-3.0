using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SetChoices : MonoBehaviour
{
    public static SetChoices ins;
    public static int Count = 2; // Default value
    //public GameObject Gameplay;
    public TMP_Text counterNumber;
    private void Start()
    {
        //Count = 2;
        //if (PlayerPrefs.GetInt("FirstAnim") == 0)
        //{
        //    //PlayerPrefs.SetInt("FirstAnim", 1);
        //    //Count = 2;
        //    UpdateCounterText();
        //}
        //print("p1"+Count);
    }
    public void IncreaseCount()
    {
        //print("p2" + Count);
        SoundManager.instance.PlayEffect_Instance(4);
        if (Count < 5)
        {
            Count++;
            UpdateCounterText();
        }
    }

    public void DecreaseCount()
    {
        //print("p3" + Count);
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
}
