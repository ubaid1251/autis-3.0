using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAppPurachasedCall : MonoBehaviour
{
    public bool IsRemoveAdsButton;
    public LockingScreenSlection[] lockingScreenSlection;

    public void RemoveAdsSucceeded()
    {
       // PantraAdsManager.Instance.HideBanner();
        PlayerPrefs.SetInt("NoAds", 1);
        if(lockingScreenSlection.Length>0)
        {
            foreach (LockingScreenSlection item in lockingScreenSlection)
            {
                item.OpenLockedItem_FromAdsORMarkete();
            }
        }
    }





}
