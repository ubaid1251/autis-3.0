using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationQuit : MonoBehaviour
{
    public GameObject exitPanel;

    public static ApplicationQuit applicationQuitInstance;


    private void Awake()
    {
            applicationQuitInstance = this;
            DontDestroyOnLoad(gameObject);
     
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
           exitPanel.SetActive(true);

        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("application off");
    }

    public void PlayAgainGame()
    {
        exitPanel.SetActive(false);
    }

}
