using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class LockedPuzzle : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("Purchased") == 1)
        {
            gameObject.SetActive(false);
        }
    }
    public void OnPointerDown()
    {
        DOTween.KillAll(false);
        PlayerPrefs.SetInt("FirstAnim", 0);
        SoundManager.instance.PlayEffect_Instance(7);
        PlayerPrefs.SetString("CameFrom", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("PurchasePanel_New");
    }
}

