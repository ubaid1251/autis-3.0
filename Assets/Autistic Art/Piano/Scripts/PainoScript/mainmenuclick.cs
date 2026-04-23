using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenuclick : MonoBehaviour
{
    public static bool checkGo;
    private void Start()
    {
        if (checkGo)
        {
            Instantiate(Resources.Load<GameObject>("Canvas"));
            gameObject.SetActive(false);
        }
    }
    private void OnMouseUp()
    {
        Instantiate(Resources.Load<GameObject>("Canvas"));
        checkGo = true;
        Destroy(this.gameObject);
    }
}
