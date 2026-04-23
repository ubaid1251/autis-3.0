using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressPianolock : MonoBehaviour
{
    private Collider2D myCol;
    private void Awake()
    {
        myCol = GetComponent<Collider2D>();
        //if (PlayerPrefs.GetInt("Piano") == 1)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        myCol.enabled = false;
        SoundManager.instance.PlayEffect_Instance(7);
        PlayerPrefs.SetString("CameFrom", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("PurchasePanel_New");
    }

}
