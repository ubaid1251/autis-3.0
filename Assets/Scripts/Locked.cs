using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Locked : MonoBehaviour
{
    public string myPref;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("Purchased") == 1|| PlayerPrefs.GetInt(myPref) == 1)
        {
            gameObject.SetActive(false);
        }
    }
    public void OnPointerDown()
    {
        SoundManager.instance.PlayEffect_Instance(7);

        PlayerPrefs.SetString("CameFrom", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("PurchasePanel_New");
    }
}
