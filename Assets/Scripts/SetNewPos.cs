using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SetNewPos : MonoBehaviour
{
    Vector3 Pos;
    public GameObject posObj; // Reference to the object with the target position
    private Vector3 targetPos;
    private RectTransform rectTransform;

    public float duration = 0.25f; // Time it takes to move to the target position

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        targetPos = posObj.transform.GetComponent<RectTransform>().position;
        
        if (SceneManager.GetActiveScene().name == "Fruit" || SceneManager.GetActiveScene().name == "Vegatables")
        {
            //print("InSet Pos = " + SetFruits.ins.FruitCount);
            SetFruits.ins.FruitCount++;
        }
        else if (SceneManager.GetActiveScene().name == "Puzzle")
        {
            //print("InSet Pos = " + PuzzleMake.ins.CountPuzzlePart);
            PuzzleMake.ins.CountPuzzlePart++;
        }
        SoundManager.instance.PlayEffect_Instance(17);
        StartCoroutine(SmoothMove(targetPos, duration));
    }

    System.Collections.IEnumerator SmoothMove(Vector3 target, float time)
    {
        Vector3 start = rectTransform.position;
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            rectTransform.position = Vector3.Lerp(start, target, elapsed / time);
            yield return null; // Wait for the next frame
        }

        // Ensure the final position is exactly the target position
        rectTransform.position = target;
        


        if (SceneManager.GetActiveScene().name == "Fruit" || SceneManager.GetActiveScene().name == "Vegatables")
        {
            if (SetFruits.ins.FruitCount == SetNumber.Count)
            {
                //print("Drop");
                SetFruits.ins.FruitCount = 0;
                SetFruits.ins.Emoji.SetActive(true);
            }
        }
        else if (SceneManager.GetActiveScene().name == "Puzzle")
        {
            //print("In else");
            if (PuzzleMake.ins.TwoPiece == true && PuzzleMake.ins.CountPuzzlePart==2)
            {
                PuzzleMake.ins.EndImges[0].SetActive(true);
                PuzzleMake.ins.PuzzleImg[0].SetActive(false);
                Invoke("waitemoji", 0.5f);
             //   PuzzleMake.ins.Emoji.SetActive(true);
                PuzzleMake.ins.CountPuzzlePart = 0;
            }
            else if (PuzzleMake.ins.FourPiece == true && PuzzleMake.ins.CountPuzzlePart == 4)
            {
                PuzzleMake.ins.EndImges[1].SetActive(true);
                PuzzleMake.ins.PuzzleImg[1].SetActive(false);
                Invoke("waitemoji", 0.5f);
                PuzzleMake.ins.CountPuzzlePart = 0;
            }
            else if (PuzzleMake.ins.SixPiece == true && PuzzleMake.ins.CountPuzzlePart == 6)
            {
                PuzzleMake.ins.EndImges[2].SetActive(true);
                PuzzleMake.ins.PuzzleImg[2].SetActive(false);
                Invoke("waitemoji", 0.5f);
                PuzzleMake.ins.CountPuzzlePart = 0;
            }
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void waitemoji()
    {
        PuzzleMake.ins.Emoji.SetActive(true);
        PuzzleMake.ins.ConfetiPlay();
    }
}

