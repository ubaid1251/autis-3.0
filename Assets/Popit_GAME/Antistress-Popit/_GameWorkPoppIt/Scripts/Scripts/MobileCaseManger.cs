using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MobileCaseManger : MonoBehaviour
{
    public static MobileCaseManger Instance;
    public Text coinsText;
    int coin;
    public GameObject AnimatedText;
    private List<GameObject> AnimatedTexts = new List<GameObject>();
    private List<GameObject> ActiveTexts = new List<GameObject>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            coin = PlayerPrefs.GetInt("Coins" + SceneManager.GetActiveScene().buildIndex, 0);
            coinsText.text = coin.ToString();
        }
        else DestroyImmediate(gameObject);
    }
   public void Home()
    {
        SceneManager.LoadScene(0);
    }
    public void ShowCoins(int level)
    {
        coin = PlayerPrefs.GetInt("Coins" + (level), 0);
        coinsText.text = coin.ToString();
    }


    public void AddCoin(Vector3 position,int level = -1,int c = 1)
    {
        coin+=c;
        if (level == -1)
            level = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Coins" + level, coin);
        coinsText.text = coin.ToString();
    }

    
}
