using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class LoadingMobileCase : MonoBehaviour
{
    public GameObject Load, FilImg;
    public Sprite[] loadimg;
    public int sceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("loadReload");
        StartCoroutine("BarFil");


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

        }
        else
        {
            Load.GetComponent<Image>().sprite = loadimg[0];
            yield return new WaitForSeconds(.3f);
            num = 1;
            StartCoroutine("loadReload");

        }
    }

    IEnumerator BarFil()
    {
        while (Filing < 1f)
        {
            Filing += 0.5f * Time.deltaTime;
            yield return null;
            //StartCoroutine("BarFil");

        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        FilImg.GetComponent<Image>().fillAmount = Filing;
    }
}
