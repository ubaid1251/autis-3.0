using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
//using MoreMountains.NiceVibrations;
public class pointrdown : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerPrefs.SetInt("PLayDrawON", 1);
        gameObject.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
