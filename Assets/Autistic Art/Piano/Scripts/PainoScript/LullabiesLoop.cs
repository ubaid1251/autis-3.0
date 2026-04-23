using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LullabiesLoop : MonoBehaviour
{
    public PianoController nextLullaby;
    public PianoController nextLullaby1Locked;
    public float time;
    private void OnEnable()
    {
       // StartCoroutine(GotoNext(time));
    }
    public void GotoNext()
    {
        //yield return new WaitForSeconds(timer);
        if ( gameObject.name.Contains("ch2")) {
            if(PlayerPrefs.GetInt("sleeping") != 1 && PlayerPrefs.GetInt("unlockall") != 1)
            nextLullaby1Locked.GotoNextLullaby();
        }
        else
            nextLullaby.GotoNextLullaby();
    }
}
