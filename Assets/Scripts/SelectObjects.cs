using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectObjects : MonoBehaviour
{
    public GameObject ObjInstantiateObj; // Prefab to instantiate
    public GameObject ObjParent, ObjShadowParent, Emoji, WrongEmoji, BallonCanvas, Gameplay;   // Parent object for objects
    public Sprite[] ObjectsSprites; // Array of object sprites
    public float delay = 0.0001f;
    private int ObjPlayCount;
    public List<GameObject> AllObjects;
    public GameObject[] ChoicesObj;
    public TMP_Text AnswerNum;
    public TMP_Text[] Choices;
    private int numberOfChoices;
    public bool Ballon = false;
    public static SelectObjects ins;
    void Start()
    {
        ins = this;
        // Ensure numberOfChoices does not exceed available UI elements
        numberOfChoices = Mathf.Min(SetChoices.Count, Choices.Length, ChoicesObj.Length);
        //print("Number of Choices: " + numberOfChoices);

        CheckGap();
        AddRandom();

        ObjPlayCount = PlayerPrefs.GetInt("countObjScene");
        //print("ObjPlayCount= " + ObjPlayCount);
    }

    void AddRandom()
    {
        int correctAnswer = SetNumber.Count;
        AnswerNum.text = correctAnswer.ToString();

        HashSet<int> usedChoices = new HashSet<int>();
        usedChoices.Add(correctAnswer); // So it won't appear in the choices

        for (int i = 0; i < numberOfChoices; i++)
        {
            int choice;
            int attempts = 0;

            do
            {
                choice = Random.Range(correctAnswer - 2, correctAnswer + 5);
                attempts++;
            }
            while ((usedChoices.Contains(choice) || choice < 1) && attempts < 20);

            usedChoices.Add(choice);

            if (i < Choices.Length)
            {
                Choices[i].text = choice.ToString();
            }
        }
    }

    // Helper method to check for duplicate values
    private bool IsDuplicate(int number, TMP_Text[] choices)
    {
        foreach (var choice in choices)
        {
            if (choice != null && choice.text == number.ToString())
            {
                return true;
            }
        }
        return false;
    }

    public void RightAns()
    {
        SoundManager.instance.PlayEffect_Instance(7);
        if (SetNumber.Count < 6)
        {
            PlayerPrefs.SetInt("countObjScene", PlayerPrefs.GetInt("countObjScene") + 1);
            if (ObjPlayCount >= 2)
            {
                SetNumber.Count++;
                PlayerPrefs.SetInt("countObjScene", 0);
                Ballon = true;
            }
        }
        else if (SetNumber.Count == 6)
        {
            PlayerPrefs.SetInt("countObjScene", PlayerPrefs.GetInt("countObjScene") + 1);
            if (ObjPlayCount >= 2)
            {
                PlayerPrefs.SetInt("countObjScene", 0);
                Ballon = true;
            }
        }
        ObjShadowParent.SetActive(false);
        Emoji.SetActive(true);
    }
    public void ForBallon()
    {
        if (Ballon)
        {
            Ballon = false;
            BallonCanvas.SetActive(true);
            Gameplay.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void WrongAns()
    {
        SoundManager.instance.PlayEffect_Instance(7);
        WrongEmoji.SetActive(true);
    }

    void CheckGap()
    {
        InstantiateObjects();
    }

    void InstantiateObjects()
    {
        if (ObjParent == null)
        {
            Debug.LogError("ObjParent is not assigned!");
            return;
        }

        if (ObjInstantiateObj == null)
        {
            Debug.LogError("ObjInstantiateObj is not assigned!");
            return;
        }

        if (ObjectsSprites.Length < SetNumber.Count)
        {
            Debug.LogError("Not enough sprites in ObjectsSprites to assign unique images!");
            return;
        }

        List<Sprite> availableSprites = new List<Sprite>(ObjectsSprites);
        int randomIndex = Random.Range(0, availableSprites.Count);

        for (int i = 0; i < SetNumber.Count; i++)
        {
            GameObject obj = Instantiate(ObjInstantiateObj, ObjParent.transform);
            Sprite selectedSprite = availableSprites[randomIndex];

            obj.GetComponent<Image>().sprite = selectedSprite;
            obj.name = $"Object_{i + 1}";
            AllObjects.Add(obj);
        }

        StartCoroutine(OnObjects());
    }

    IEnumerator OnObjects()
    {
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < AllObjects.Count; i++)
        {
            SoundManager.instance.PlayEffect_Instance(8);
            AllObjects[i].GetComponent<Image>().enabled = true;
            AllObjects[i].GetComponent<DOTweenAnimation>().DOPlay();
            yield return new WaitForSeconds(delay);
        }

        ShuffleChildren();

        for (int i = 0; i < numberOfChoices; i++)
        {
            if (ChoicesObj[i] != null)
            {
                ChoicesObj[i].SetActive(true);
            }
        }
    }

    public static void ShuffleArray(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    void ShuffleChildren()
    {
        int childCount = ObjShadowParent.transform.childCount;
        if (childCount <= 1)
        {
            Debug.LogWarning("Not enough children to shuffle!");
            return;
        }

        Transform[] children = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            children[i] = ObjShadowParent.transform.GetChild(i);
        }

        for (int i = children.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = children[i];
            children[i] = children[randomIndex];
            children[randomIndex] = temp;
        }

        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }

    void OrignalPosOfArrayInPlaytime()
    {
        int childCount = ObjShadowParent.transform.childCount;
        ChoicesObj = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            ChoicesObj[i] = ObjShadowParent.transform.GetChild(i).gameObject;
        }
    }
}
