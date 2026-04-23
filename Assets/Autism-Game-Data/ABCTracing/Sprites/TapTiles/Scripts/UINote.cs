using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UINote : MonoBehaviour, IPointerDownHandler
{
    double timeInstantiated;
    public float assignedTime;
    public ParticleSystem[] SupportingText;
    public GameObject particle;
    bool completed = false;
    public Sprite[] abc, tiles;
    RectTransform rect;
    void Start()
    {
        if (ResCheck.ResolutionType == ResType.iphone6)
        {
            transform.GetChild(1).GetComponent<TrailRenderer>().widthMultiplier = 1.8f;
        }
        if (ResCheck.ResolutionType == ResType.tab)
        {
            transform.GetChild(1).GetComponent<TrailRenderer>().widthMultiplier = 1.5f;
        }
        else
        {
            transform.GetChild(1).GetComponent<TrailRenderer>().widthMultiplier = 2.2f;
        }
        GetComponent<Image>().sprite = tiles[Random.Range(0, tiles.Length)];
        if (PlayerPrefs.GetInt("sfx") == 1)
        {
            GetComponent<AudioSource>().volume = 0;
            // transform.GetChild(0).GetComponent<AudioSource>().volume = 0;
        }
        rect = GetComponent<RectTransform>();
        // if (RhymeManager.instance.SelectedRhyme == (int)Rhymes.Alphabets)
        {
            int indx = SongManager.counter;
            transform.GetChild(0).GetComponent<Image>().sprite = abc[indx];
            transform.GetChild(0).GetComponent<Image>().SetNativeSize();
            SongManager.counter++;
            if (SongManager.counter >= 40)
            {
                SongManager.counter = 0;
            }
        }
        // else
        // {
        //     if (RhymeManager.instance.SelectOtherCards == 0)
        //     {
        //         int r = Random.Range(0, fruits.Length);
        //         GetComponent<Image>().sprite = fruits[r];
        //     }
        //     else if (RhymeManager.instance.SelectOtherCards == 1)
        //     {
        //         int r = Random.Range(0, animals.Length);
        //         GetComponent<Image>().sprite = animals[r];
        //     }
        //     else if (RhymeManager.instance.SelectOtherCards == 2)
        //     {
        //         int r = Random.Range(0, farm.Length);
        //         GetComponent<Image>().sprite = farm[r];
        //     }
        // }
        timeInstantiated = SongManager.GetAudioSourceTime();
    }
    private void OnEnable()
    {
        GetComponent<Image>().enabled = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!completed)
        {
            double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
            float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));
            if (t > 1)
            {
                completed = true;
                Destroy(gameObject);
            }
            else
            {
                rect.anchoredPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t);
                // if (PlayerPrefs.GetInt("IndiForPlay") == 0)
                // {
                //     if (rect.anchoredPosition.y > -1400 && rect.anchoredPosition.y < -1350)
                //     {
                //         Time.timeScale = 0;
                //         transform.GetChild(0).gameObject.SetActive(true);
                //         s = true;
                //         SongManager.Instance.audioSource.Pause();
                //
                //     }
                // }
            }
        }
    }
    public static bool s = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!s || transform.GetChild(0).gameObject.activeInHierarchy == true)
        {
            //Vibration.Vibrate(50);
            //float t = SongManager.Instance.fillAmount + 0.0125f;
            //SongManager.Instance.fillAmount = t;
            //SongManager.Instance.filler.DOFillAmount(t, .25f);
            s = false;
            completed = true;
            GetComponent<AudioSource>().Play();
            GetComponent<Image>().enabled = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            int ran = Random.Range(0, SupportingText.Length);
            particle.SetActive(true);
            // var p = Instantiate(SupportingText[ran], transform);
        }
        if (PlayerPrefs.GetInt("IndiForPlay") == 0 && transform.GetChild(0).gameObject.activeInHierarchy == true)
        {
            PlayerPrefs.SetInt("IndiForPlay", 1);
            SongManager.Instance.audioSource.Play();
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).GetComponent<AudioSource>().enabled = false;
            SongManager.Instance.GetComponent<AudioSource>().Play();
            Time.timeScale = 1;
        }
    }
}
