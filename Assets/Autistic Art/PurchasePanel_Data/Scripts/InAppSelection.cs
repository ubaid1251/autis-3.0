using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InAppSelection : MonoBehaviour, IPointerDownHandler
{
    public InAppCalling_CB continueInApp;
    public int myIAPIndex = 0;
    public PanelSwipeDetector panel;


    public void OnPointerDown(PointerEventData eventData)
    {
        InitializeFirebase_CB.instance.LogFirebaseEvent("Inapp_no_"+myIAPIndex+"_selected");

        continueInApp.IAPIndex = myIAPIndex;
        for (int i = 0; i < panel.allSelected.Length; i++)
        {
            panel.allSelected[i].SetActive(false);
        }
        panel.allSelected[myIAPIndex].SetActive(true);
        SoundManager.instance.PlayEffect_Instance(7);

    }
}
