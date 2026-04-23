using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{


    private void Start()
    {
        Invoke("ChangeScene",4f);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
