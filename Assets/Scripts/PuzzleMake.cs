using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMake : MonoBehaviour
{
    public bool TwoPiece=false, FourPiece=false, SixPiece = false, IsCollide = true;
    public GameObject[] Modes,EndImges,PuzzleImg,AllModePics;
    public GameObject Emoji;
    public static PuzzleMake ins;
    public int CountPuzzlePart = 0;
    public ParticleSystem Confeti;
    void Start()
    {
        ins = this;
        if (SetPuzzleCount.Count == 2)
        {
            TwoPiece = true;
        }
        else if (SetPuzzleCount.Count == 4)
        {
            FourPiece = true;
        }
        else if (SetPuzzleCount.Count == 6)
        {
            SixPiece = true;
        }
        if (TwoPiece==true)
        {
            Modes[0].SetActive(true);
        }
        else if (FourPiece == true)
        {
            Modes[1].SetActive(true);
        }
        else if (SixPiece == true)
        {
            Modes[2].SetActive(true);
        }
    }

    // Update is called once per frame
    public void ConfetiPlay()
    {
        SoundManager.instance.PlayEffect_Instance(0);
        Confeti.gameObject.SetActive(true);
        Confeti.Play();
    }
    public void OffColiiders()
    {
        IsCollide = false;
        //Invoke("OnColiiders", 0.2f);
    }
    public void OnColiiders()
    {
        IsCollide = true;
        //for (int i = 0; i < AllModePics.Length; i++)
        //{
        //    AllModePics[i].GetComponent<BoxCollider2D>().enabled = true;
        //}
    }
}
