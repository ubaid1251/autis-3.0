using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class FindShape : MonoBehaviour
{
    //public GameObject ShapeInstantiateObj; // Prefab to instantiate
    public GameObject ShapeParent, SoundBtn, Emoji, LostEmoji, BallonCanvas, Gameplay;         // Parent object for fruits
    //public static int SetNumber.Count = 2;     // Number of fruits to create
    public Sprite[] ShapeSprites, ShapeSprites2;          // Array of fruit sprites
    public float delay = 0.0001f;
    private int playShapeCount,soundNum;
    public List<GameObject> AllShapes;
    public bool Ballon = false,TextShow=false;
    public static FindShape ins;
    public TMP_Text FindShapeName;
    public string[] ShapesNames;
    public GameObject NameBar;
    public ParticleSystem Confeti;
    void Start()
    {
        ins = this;
        ShuffleChildren();
        InstantiateFruits();
        playShapeCount = PlayerPrefs.GetInt("countShape");
        if (PlayerPrefs.GetInt("IsText") == 1)
        {
            TextShow = true;
        }
    }
    public void ConfetiPlay()
    {
        SoundManager.instance.PlayEffect_Instance(0);
        Confeti.gameObject.SetActive(true);
        Confeti.Play();
    }
    public void testng()
    {
        if (SetNumber.Count < 4)
        {
            PlayerPrefs.SetInt("countShape", PlayerPrefs.GetInt("countShape") + 1);
            if (playShapeCount >= 2)
            {
                SetNumber.Count++;
                PlayerPrefs.SetInt("countShape", 0);
            }
        }
        SceneManager.LoadScene(0);
    }
    List<Sprite> availableSprites;
    void InstantiateFruits()
    {
        int randomShape = Random.Range(0, 3);
        if(randomShape==2)
        {
            availableSprites = new List<Sprite>(ShapeSprites2);
        }
        else
        {
            availableSprites = new List<Sprite>(ShapeSprites);
        }

        // Instantiate the specified number of fruits
        for (int i = 0; i < SetNumber.Count; i++)
        {
            int randomIndex = Random.Range(0, availableSprites.Count);
            Sprite selectedSprite = availableSprites[randomIndex];
            availableSprites.RemoveAt(randomIndex);
            AllShapes[i].GetComponent<Image>().sprite = selectedSprite;
            AllShapes[i].GetComponent<Image>().sprite = selectedSprite;
            AllShapes[i].SetActive(true);
            if(i==0)
            {
                soundNum = randomIndex;
                //print(soundNum);
            }
        }
        OrignalPosOfArrayInPlaytime();
        StartCoroutine(OnFruits());
    }
    IEnumerator OnFruits()
    {
        yield return new WaitForSeconds(0.3f);
        if (TextShow)
        {
            NameBar.GetComponent<RectTransform>()
                   .DOAnchorPosX(292f, 0.5f) // Use a pixel-based position suitable for your layout
                   .SetEase(Ease.Linear);
            FindShapeName.text = ShapesNames[soundNum].ToString();
        }
        PlayAnswerSound();
        for (int i = 0; i < SetNumber.Count; i++)
        {
            //SoundManager.instance.PlayEffect_Instance(8);
            AllShapes[i].GetComponent<Image>().enabled = true;
            AllShapes[i].GetComponent<DOTweenAnimation>().DOPlay();
            yield return new WaitForSeconds(delay);
          //  AllShapes[i].transform.GetChild(0).GetComponent<Button>().enabled = true;

        }
         yield return new WaitForSeconds(1f);
        for (int i = 0; i < SetNumber.Count; i++)
        {
      
            AllShapes[i].transform.GetChild(0).GetComponent<Button>().enabled = true;

        }
         yield return new WaitForSeconds(0.7f);
        SoundBtn.GetComponent<Button>().interactable = true;
    }
    public static void ShuffleArray(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            // Swap the elements
            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
    void ShuffleChildren()
    {
        // Get the child objects
        int childCount = SetNumber.Count;
        if (childCount <= 1)
        {
            Debug.LogWarning("Not enough children to shuffle!");
            return;
        }

        // Store children in a list
        Transform[] children = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            children[i] = ShapeParent.transform.GetChild(i);
        }

        // Shuffle the array of children
        for (int i = children.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            // Swap positions in the array
            Transform temp = children[i];
            children[i] = children[randomIndex];
            children[randomIndex] = temp;
        }

        // Reassign the shuffled children to the parent
        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetSiblingIndex(i);
        }

    }
    public void PlayAnswerSound()
    {
        SoundManager.instance.PlayShapeEffect_Instance(soundNum);
        Invoke("waitBtn", 1.5f);
    }
    void waitBtn()
    {
        SoundBtn.GetComponent<Button>().interactable = true;
    }
    void OrignalPosOfArrayInPlaytime()
    {
        int childCount = ShapeParent.transform.childCount;

        // Ensure AllShapes list is initialized
        if (AllShapes == null || AllShapes.Count != childCount)
        {
            AllShapes = new List<GameObject>(childCount);
        }
        else
        {
            AllShapes.Clear();
        }

        // Populate the AllShapes list with the children of ShapeParent
        for (int i = 0; i < childCount; i++)
        {
            AllShapes.Add(ShapeParent.transform.GetChild(i).gameObject);
        }

    }
  public void RightBtn()
    {
        SoundManager.instance.StopAllShapeSounds();
        if (TextShow)
        {
            NameBar.transform.DOMoveX(-15.6f, 0.5f).SetEase(Ease.Linear);
        }
        SoundManager.instance.PlayEffect_Instance(7);
        if (SetNumber.Count < 4)
        {
            PlayerPrefs.SetInt("countShape", PlayerPrefs.GetInt("countShape") + 1);
            if (playShapeCount >= 2)
            {
                // Reset the play count and increase SetNumber.Count
                SetNumber.Count++;
                PlayerPrefs.SetInt("countShape", 0);
                Ballon = true;
            }
        }
        else if (SetNumber.Count == 4)
        {
            PlayerPrefs.SetInt("countShape", PlayerPrefs.GetInt("countShape") + 1);
            if (playShapeCount >= 2)
            {
                PlayerPrefs.SetInt("countShape", 0);
                Ballon = true;
            }
        }
        for (int i = 0; i < AllShapes.Count; i++)
        {
            AllShapes[i].transform.GetChild(0).GetComponent<Button>().enabled = false;
        }
        SoundBtn.GetComponent<Button>().interactable = false;
        ConfetiPlay();
        Emoji.SetActive(true);
        SoundBtn.SetActive(false);
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
    public void WrongBtn()
    {
        SoundManager.instance.StopAllShapeSounds();
        SoundManager.instance.PlayEffect_Instance(7);
        for (int i = 0; i < AllShapes.Count; i++)
        {
            AllShapes[i].transform.GetChild(0).GetComponent<Button>().enabled = false;
        }
        SoundBtn.GetComponent<Button>().interactable = false;
        LostEmoji.SetActive(true);

    }
    private void Update()
    {
        //print(NameBar.transform.position.x);
    }
}
