using UnityEngine;

public class RectTransformCopy : MonoBehaviour
{
    public GameObject GameObject1; // First GameObject
    public GameObject GameObject2; // Second GameObject

    void Start()
    {
        int randomNum = Random.Range(0, 3);
        if (randomNum == 0 || randomNum == 1)
        {
            print("noShuffle");
        }
        else
        {
            print("Shuffled");
            ShufflePositions(GameObject1, GameObject2);
        }
    }

    void ShufflePositions(GameObject obj1, GameObject obj2)
    {
        if (obj1 == null || obj2 == null)
        {
          //  Debug.LogError("One or both GameObjects are not assigned!");
            return;
        }

        RectTransform rect1 = obj1.GetComponent<RectTransform>();
        RectTransform rect2 = obj2.GetComponent<RectTransform>();

        if (rect1 == null || rect2 == null)
        {
           // Debug.LogError("Both GameObjects must have RectTransform components!");
            return;
        }

        // Swap positions
        Vector3 tempPosition = rect1.anchoredPosition;
        rect1.anchoredPosition = rect2.anchoredPosition;
        rect2.anchoredPosition = tempPosition;

        // Optionally, you can also swap the sizes and anchors if needed
        Vector2 tempSizeDelta = rect1.sizeDelta;
        rect1.sizeDelta = rect2.sizeDelta;
        rect2.sizeDelta = tempSizeDelta;

        Vector2 tempAnchorMin = rect1.anchorMin;
        rect1.anchorMin = rect2.anchorMin;
        rect2.anchorMin = tempAnchorMin;

        Vector2 tempAnchorMax = rect1.anchorMax;
        rect1.anchorMax = rect2.anchorMax;
        rect2.anchorMax = tempAnchorMax;

        //Debug.Log("Positions and RectTransforms shuffled successfully.");
    }
}