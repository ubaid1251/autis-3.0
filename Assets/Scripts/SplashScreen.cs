using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     Invoke(nameof(moveToSelection),1);   
    }
void moveToSelection()
{
    SceneManager.LoadScene("Selection");
}
}
