using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsLocked_New : MonoBehaviour
{

    public GameObject lockedObject, buyPanel, parentControl;
    public Canvas InAppCanvas;
    //public static IsLocked instance;
    GameObject HomeButton;
    void Awake()
    {
        if (PlayerPrefs.GetInt("unlockall") == 1)
        {
            if (lockedObject)
            {
                lockedObject.SetActive(false);
                Debug.Log("Call");
            }
        }else
        {
            if (lockedObject)
            {
                lockedObject.SetActive(true);
                Debug.Log("Call12");
            }
        }
    }
    public bool isPurchased()
    {
        if (PlayerPrefs.GetInt("unlockall") == 1)
        {
            return true;

        }
        return false;
    }
    public void SetAcitveOn(GameObject obj)
    {
       // soundref.instance.PlaySnd(soundref.instance.click);

        obj.SetActive(true);
        HomeButton.SetActive(false);
    }
    public void SetAcitveOff(GameObject obj)

    {
        //soundref.instance.PlaySnd(soundref.instance.click);

        obj.SetActive(false);
        HomeButton.SetActive(true);
    }

    public void BuyInApp(int val)
    {
       // soundref.instance.PlaySnd(soundref.instance.click);
    }
    public void UnlockAllGame()
    {
        if (buyPanel)
        {
            buyPanel.SetActive(false);
            HomeButton.SetActive(false);
            HomeButton.SetActive(true);
        }
        PlayerPrefs.SetInt("unlockall", 1);
        if (lockedObject)
        {
            lockedObject.SetActive(false);
        }
       // if (activeGameMini.intsnace)
        {
           // activeGameMini.intsnace.UnlockMiniGame();
        }
    }
    public void ActiveParentalControl(int val)
    {
        //soundref.instance.PlaySnd(soundref.instance.click);
        //AdditionParentalPanel.inappval = val;
    }
}
