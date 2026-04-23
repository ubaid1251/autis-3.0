using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLockAndFree : MonoBehaviour
{
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("isPurchased") == 1)
        {
            if (gameObject.name == "Lock" || gameObject.name == "free")
            {
                gameObject.SetActive(false);
            }
        }
    }
}
