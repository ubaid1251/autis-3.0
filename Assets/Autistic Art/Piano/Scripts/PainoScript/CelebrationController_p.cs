using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CelebrationController_p : MonoBehaviour {
    public GameObject characterAnim;
    public GameObject objOff;
    public AudioSource bgsound;
    public AudioSource supportingSound;
	IEnumerator Start () {
      
        if (bgsound) {
            bgsound.enabled = false;
        }
       StartCoroutine(ObjOn());
        yield return new WaitForSeconds(2.0f);

        if (PlayerPrefs.GetInt("Ftime") != 0)
        {
            if (PlayerPrefs.GetInt("isPurchased") == 0)
            {

                //if (IntitializeAdmobAds_CB._instance.HasAdmobInterstialAvaible())
                //{
                //    IntitializeAdmobAds_CB._instance.ShowAdmobInterstialAd();
                //}
            }
        }
        PlayerPrefs.SetInt("Ftime", 1);
    }
    IEnumerator ObjOn()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(.4f);
        characterAnim.SetActive(true);
    }
}
