using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    public GameObject[] Buttons;
    public Text CoinsText;
    int coins;
    public GameObject LoadingScreen;
    // Start is called before the first frame update
    void Start()
    {
        InitLevel();
        CoinsText.text = coins.ToString();

    }

    public void InitLevel()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            int sCoins = PlayerPrefs.GetInt("Coins" + (i + 1));
            coins += sCoins;
            Buttons[i].transform.GetChild(1).GetComponentInChildren<Text>().text = sCoins.ToString();

        }
    }

    public void CallSceneNumer(int numScene)
    {

        StartCoroutine(AdEnumerator(numScene));

    }

    IEnumerator AdEnumerator(int numScene)
    {
        GameObject go = Instantiate(LoadingScreen);
        go.GetComponent<Loading>().LoadLevel = numScene;
        yield return new WaitForSeconds(1);
        //  GoogleAdMobController.THIS.ShowInterstitialAd(); //AdCallPosition
        yield return new WaitForSeconds(2f);

    }

    public void Purchase(int index)
    {
        if (PlayerPrefs.GetInt("Coins") >= 1000 && PlayerPrefs.GetInt("Scene" + index) == 0)
        {
            PlayerPrefs.SetInt("Scene" + index , 1);
            coins -= 1000;
            PlayerPrefs.SetInt("Coins" , coins);
            Buttons[index].transform.GetChild(1).gameObject.SetActive(false);
            Buttons[index].transform.GetChild(2).gameObject.SetActive(true);
        }
    }

}
