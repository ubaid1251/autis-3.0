using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float num = 4;
    public Text txt;
    private int i = 0;
  //  public GameObject compensatePanel;
    public GameObject panel;
 //   public GameObject rewardedPanel;

    private void Start()
    {
       // txt = GetComponent<Text>();
    }

    private void OnEnable()
    {
        num = 3.45f;
        i = 0;
      //  GoogleAdMobController.THIS.RequestAndLoadRewardedAd();
    }

    private void Update()
    {
       
        if (num >= 0.5f)
        {
            txt.text = num.ToString("0");
            num -= Time.deltaTime;
        }
        else
        {
           
           CALLFunction();
        }
    }

    private void CALLFunction()
    {
        if (i == 0)
        {
            panel.gameObject.SetActive(false);
            i++;
        }
    }
}
