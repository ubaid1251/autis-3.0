using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PianoSubObj : MonoBehaviour
{
    public GameObject LevelNextBtn;
    void Start()
    {
        if (LevelNextBtn)
        {
            LevelNextBtn.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.4f).SetDelay(3f);
        }
    }
}
