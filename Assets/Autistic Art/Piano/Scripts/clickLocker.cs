using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class clickLocker : MonoBehaviour,IPointerDownHandler
{
    public bool isEndScene;
    public bool isMiniGame, IsCanvasPart = false;
    public bool Lullabies;

    public GameObject LullabiesScene;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("isPurchased") == 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (IsCanvasPart)
            {
                Collider2D[] allColliders2D = FindObjectsOfType<Collider2D>();
                foreach (Collider2D collider in allColliders2D)
                {
                    collider.enabled = false;
                }
            }            
        }
    }

    private void OnMouseDown()
    {
        //  check.SetActive(true);

        
        if (!isEndScene)
        {
            if (PlayerPrefs.GetInt("MainSelectionTest") == 0)
            {
                PlayerPrefs.SetString("CameFrom", "SelectionScreenWithCards");
            }
            else
            {
                if (isMiniGame)
                {
                    PlayerPrefs.SetString("CameFrom", "MiniGames");
                }
                else
                {
                    PlayerPrefs.SetString("CameFrom", SceneManager.GetActiveScene().name);
                }
            }

            //PanelSwipeDetector.isLockerSelected = true;
            SceneManager.LoadScene("PurchasePanel_New");
        }
        
    }
    
    
    public void MoveToPurchase()
    {
        //  check.SetActive(true);

        
        if (!isEndScene)
        {
            if (PlayerPrefs.GetInt("MainSelectionTest") == 0)
            {
                PlayerPrefs.SetString("CameFrom", "SelectionScreenWithCards");
            }
            else
            {
                if (isMiniGame)
                {
                    PlayerPrefs.SetString("CameFrom", "MiniGames");
                }
                else
                {
                    PlayerPrefs.SetString("CameFrom", SceneManager.GetActiveScene().name);
                }
            }

            //PanelSwipeDetector.isLockerSelected = true;
            SceneManager.LoadScene("PurchasePanel_New");
        }
        
    }


    public void CallInter()
    {
        if (PlayerPrefs.GetInt("MainSelectionTest") == 0)
        {
            PlayerPrefs.SetString("CameFrom", "SelectionScreenWithCards");
        }
        else
        {
            PlayerPrefs.SetString("CameFrom", "SelectionScreenWithCards");
        }
        //PanelSwipeDetector.isLockerSelected = true;
        SceneManager.LoadScene("PurchasePanel_New");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Causing Error: " + isEndScene);
        Debug.Log("Lullabies Error: " + Lullabies);

        if (!isEndScene && !Lullabies)
        {
            if (PlayerPrefs.GetInt("MainSelectionTest") == 0)
            {
                PlayerPrefs.SetString("CameFrom", "SelectionScreenWithCards");
            }
            else
            {
                if (isMiniGame)
                {
                    PlayerPrefs.SetString("CameFrom", "Mini games");
                }
                else
                {
                    PlayerPrefs.SetString("CameFrom", SceneManager.GetActiveScene().name);
                }
            }
            //PanelSwipeDetector.isLockerSelected = true;
            SceneManager.LoadScene("PurchasePanel_New");
        }
        else if (Lullabies)
        {
            if (LullabiesScene)
            {
                LullabiesScene.SetActive(true);
                Camera.main.gameObject.GetComponent<AudioSource>().enabled = false;
            }
        }
    }
}