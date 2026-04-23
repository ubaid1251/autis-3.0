using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClosePanel : MonoBehaviour
{
   
    public GameObject SoundPanel;
    private bool isEnabled;

    private void Start()
    {
       
        SoundPanel.SetActive(false);
        isEnabled = false;
    }

    public void ToggleObject()
    {
        isEnabled = !isEnabled;
        SoundPanel.SetActive(isEnabled);
    }
}
