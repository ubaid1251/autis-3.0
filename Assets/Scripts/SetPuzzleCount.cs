using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SetPuzzleCount : MonoBehaviour
{
    public static SetPuzzleCount ins;
    public static int Count = 2; // Default value
    public GameObject Gameplay,GotoGame;
    public TMP_Text counterNumber;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstAnim") == 1)
        {
            GotoGame.SetActive(true);
            gameObject.SetActive(false);
        }


        else if (PlayerPrefs.GetInt("FirstAnim") == 0)
        {
            PlayerPrefs.SetInt("FirstAnim", 1);
            UpdateCounterText();
        }
    }

    public void IncreaseCount()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        if (Count < 6)
        {
            Count += 2;
            UpdateCounterText();
        }
    }

    public void DecreaseCount()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        if (Count > 2)
        {
            Count -= 2;
            UpdateCounterText();
        }
    }

    private void UpdateCounterText()
    {
        counterNumber.text = Count.ToString();
    }
    public void Playbtn()
    {
        SoundManager.instance.PlayEffect_Instance(5);
        //Debug.Log("PuzzleCount set to: " + Count);
        Gameplay.SetActive(true);
        gameObject.SetActive(false);
    }
}
