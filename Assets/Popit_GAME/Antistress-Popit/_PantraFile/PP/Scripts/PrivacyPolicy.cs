using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PrivacyPolicy : MonoBehaviour
{
    public GameObject logo,parentObj;
    public static bool GameStarted;
    private void Start()
    {
        Time.timeScale = 1;
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("privacyAccepted") > 0)
        {
            parentObj.SetActive(false);
            logo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            StartCoroutine("PlayGameNow");
        }
        else
        {
            parentObj.SetActive(true);
           // logo.SetActive(false);
        }
        GameStarted = true;
    }
    // Start is called before the first frame update
    public void PrivacyAccepted()
    {
        PlayerPrefs.SetInt("privacyAccepted", 1);
        SceneManager.LoadScene(1);
    }
    public void PrivacyRejected()
    {
        Application.Quit();
    }
    public void GoTOPrivacyLink()
    {
        print("Visit Privacy Blog Spot");
        Application.OpenURL("https://calminggamesantistress.blogspot.com/p/privacy.html");
    }

    IEnumerator PlayGameNow()
    {
        yield return new WaitForSeconds(1.0f);
      //  LoadingData.SceneToLoad = sceneName;
        SceneManager.LoadScene(1);
    }
}
