using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonPop : MonoBehaviour
{
    public int index;
    private void Start()
    {
        if(PlayerPrefs.GetInt("sfx") == 1)
        {
            GetComponent<AudioSource>().enabled = false;
        }
    }
    private void OnMouseDown()
    {
        if (Fruitdetection.Instance != null)
        {
            Fruitdetection.Instance.FillBar(gameObject, new Vector3(0, 0, 0), index);
            Fruitdetection.Instance.OnCheckPlayAudio();
            //gameObject.GetComponent<MeshRenderer>().enabled = true;
            if (GetComponent<AudioSource>())
            {
                if (GetComponent<AudioSource>().enabled == true)
                    GetComponent<AudioSource>().Play();
            }
            GameObject hand = GameObject.Find("hand");
            if (hand)
                hand.SetActive(false);
        }
    }

}
