using GameWork.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{

    public GameObject /*MainScreen,*/ ModeScreen, BoardScreen, PlayScreen;
    public GameObject CurrentScreen;
    public Animation TransitionScreen;
 

    public void ShowMainScreen()
    {
        if (CurrentScreen)
            CurrentScreen.SetActive(false);
        CurrentScreen = /*MainScreen*/ModeScreen;    // change to mode screen
        CurrentScreen.SetActive(true);
        TransitionScreen.Play();
    }

    public void ShowModeScreen()
    {
        if (CurrentScreen)
            CurrentScreen.SetActive(false);
        CurrentScreen = ModeScreen;
        CurrentScreen.SetActive(true);
        TransitionScreen.Play();
    }

    public void ShowBoardScreen()
    {
        //if (CurrentScreen)
        //    CurrentScreen.SetActive(false);

        ModeScreen.SetActive(false);


        CurrentScreen = BoardScreen;
        CurrentScreen.SetActive(true);
        TransitionScreen.Play();
    }

    public void ShowPlayScreen()
    {
        var selectedObj = EventSystem.current.currentSelectedGameObject;
        var rewardBase = selectedObj.transform.GetComponent<RewardBaseUnlock>();

        if (rewardBase.isLocked)
        {
            //if (GMAdsManager.Instance)
            //    GMAdsManager.Instance.Show_Rewarded(rewardBase.UnlockedLevel);
        }
        else
        {
            if (CurrentScreen)
                CurrentScreen.SetActive(false);
            CurrentScreen = PlayScreen;   //uncomment later
            CurrentScreen.SetActive(true);
            TransitionScreen.Play();

        }
       
    }

    public void ShowTransitionScreen()
    {
        TransitionScreen.Play();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
