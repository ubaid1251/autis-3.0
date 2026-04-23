using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCamColor : MonoBehaviour
{
    public GameObject Bg;
    public Color[] BgColors;
    int Colornum;
    void Start()
    {
        Colornum = PlayerPrefs.GetInt("BGColorSet");
        Bg.GetComponent<Camera>().backgroundColor = BgColors[Colornum];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
