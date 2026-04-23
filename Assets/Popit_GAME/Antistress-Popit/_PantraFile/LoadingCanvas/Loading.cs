using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Loading : MonoBehaviour
{
    public GameObject Load, FilImg;
    public Sprite[] loadimg;
    public int LoadLevel;// = " ";
    public static System.Action loadingEndCallBack;
    public bool isMM;
    public GameObject DisableCanvas;
    // Start is called before the first frame update
    private void Awake()
    {
        //  DontDestroyOnLoad(this.gameObject);
    }
    void OnEnable()
    {
        if (!isMM)
        {
            DisableCanvas.SetActive(false);
            // GoogleAdMobController.THIS.ShowInterstitialAd();
        }


    }
    float Filing;
    int num;
    IEnumerator loadReload()
    {

        if (num < loadimg.Length)
        {
            Load.GetComponent<Image>().sprite = loadimg[num];
            yield return new WaitForSeconds(.3f);
            num++;
            StartCoroutine("loadReload");
        }
        else
        {
            Load.GetComponent<Image>().sprite = loadimg[0];
            yield return new WaitForSeconds(.3f);
            num = 1;
            StartCoroutine("loadReload");

        }

    }

    public void BarFil()
    {
        if (!isMM)
        {
            // DisableCanvas.SetActive(false);
            //GoogleAdMobController.THIS.ShowInterstitialAd(); //AdCallPosition
        }
        StartCoroutine("ChangeScene");


    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(LoadLevel);
        StopCoroutine(ChangeScene());
    }
    // Update is called once per frame
    void Update()
    {
        FilImg.GetComponent<Image>().fillAmount = Filing;
    }
}
