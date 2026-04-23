using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FourPieces : MonoBehaviour
{
    public GameObject CompleteImg;
    public GameObject[] PieceOne, PieceTwo, PieceThree, PieceFour;
    public Sprite[] CompleteSprites;
    public Sprite[] PieceOneSprites, PieceTwoSprites, PieceThreeSprites, PieceFourSprites;
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
        PieceThree[0].GetComponent<Image>().sprite = PieceThreeSprites[RandomSpriteNum];
        PieceThree[1].GetComponent<Image>().sprite = PieceThreeSprites[RandomSpriteNum];
        PieceFour[0].GetComponent<Image>().sprite = PieceFourSprites[RandomSpriteNum];
        PieceFour[1].GetComponent<Image>().sprite = PieceFourSprites[RandomSpriteNum];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
