using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TwoPieces : MonoBehaviour
{
    public GameObject CompleteImg;
    public GameObject[] PieceOne, PieceTwo;
    public Sprite[] CompleteSprites;
    public Sprite[] PieceOneSprites, PieceTwoSprites;
    int RandomSpriteNum;
    void Start()
    {
        if (PlayerPrefs.GetInt("FirstPuzzle") == 1)
        {
            if (PlayerPrefs.GetInt("Purchased") == 1)
            {
                RandomSpriteNum = Random.Range(0, CompleteSprites.Length);
            }
            else
            {
                RandomSpriteNum = Random.Range(0, 6);
            }
        }


        else if (PlayerPrefs.GetInt("FirstPuzzle") == 0)
        {
            PlayerPrefs.SetInt("FirstPuzzle", 1);
            RandomSpriteNum = PlayerPrefs.GetInt("MyPuzzle");
        }
        CompleteImg.GetComponent<Image>().sprite = CompleteSprites[RandomSpriteNum];
        PieceOne[0].GetComponent<Image>().sprite = PieceOneSprites[RandomSpriteNum];
        PieceOne[1].GetComponent<Image>().sprite = PieceOneSprites[RandomSpriteNum];
        PieceTwo[0].GetComponent<Image>().sprite = PieceTwoSprites[RandomSpriteNum];
        PieceTwo[1].GetComponent<Image>().sprite = PieceTwoSprites[RandomSpriteNum];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
