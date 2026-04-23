using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBgColor : MonoBehaviour
{
    public GameObject Bg;
    public Color[] BgColors;
    int Colornum;
    void Start()
    {
        Colornum = PlayerPrefs.GetInt("BGColorSet");
        Bg.GetComponent<Image>().color = BgColors[Colornum];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
