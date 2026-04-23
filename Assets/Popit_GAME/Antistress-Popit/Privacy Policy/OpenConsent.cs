using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenConsent : MonoBehaviour
{
    public void OpenPrivacyPolicyLink()
    {
        Application.OpenURL("https://calminggamesantistress.blogspot.com/p/privacy.html");
    }


    public void StopGame()
    {
    
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
