using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnColorObjects : MonoBehaviour
{
    public static OnColorObjects ins;
    public Color[] ExtraA1,ExtraA2;
    public GameObject BorderA1, BorderA2, BorderA3,Finger;
    public List<Color> MyColors = new List<Color>();
    public List<Color> ChoiceColors = new List<Color>();
    private void OnEnable()
    {
        Finger.SetActive(true);
    }
    void Start()
    {
        ins = this;
        GameController.ins.ScratchColor();
        GameController.ins.ScratchCardObject.GetComponent<ScratchCard>().InputEnabled = true;
    }
    public void SelColorBorders(int num)
    {
        BorderA1.transform.GetComponent<Image>().color = GameController.ins.MyColors[GameController.CountSpriteColor];
        BorderA2.transform.GetComponent<Image>().color = RandmColors.ins.RandomColor1[GameController.CountSpriteColor];
        BorderA3.transform.GetComponent<Image>().color = RandmColors.ins.RandomColor2[GameController.CountSpriteColor];
    }
    public void RewardShape(GameObject obj)
    {
        obj.transform.parent.GetComponent<Button>().enabled = true;
        obj.SetActive(false);
    }

}
