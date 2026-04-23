//using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fruitdetection : MonoBehaviour
{
    public GameObject[] toBefilled;
    public List<string> tags;
    private int spawnValue, tempSpawnValue;
    public Vector3[] position;
    public float spawnerSpeed;
    public static Fruitdetection Instance;
    public AudioSource audioSource;
    public GameObject _FinalAnim;
    public Animator Happy_Ch;
    public Animator Happy_Ch1;
    public float First;
    public float second;
    public GameObject round_btn;
    public bool Is_Setter;

    //public SpineCharacterController controller;
    private void Start()
    {
       // audioSource = activeGameMini.intsnace.pop;
        //StartCoroutine(Spawner);
        //if (MultiRez.Res_Value == 1.6f && Is_Setter)
        {
            Debug.Log("value");
            round_btn.transform.localPosition = new Vector3(0f, First, 0f);
        }
        //if (MultiRez.Res_Value < 1.35 && Is_Setter)
        //{
        //    round_btn.transform.localPosition = new Vector3(0f, second, 0f);
        //}
    }


    private void Awake()
    {
        Instance = this;
    }
    //private void OnValidate()
    //{
    //    audioSource = GameObject.Find("Poppp").GetComponent<AudioSource>();
    //}
    public IEnumerator Spawner
    {
        get
        {
            yield return new WaitForSeconds(spawnerSpeed);
            spawnValue = Random.Range(0, tags.Count);
            switch (tags[spawnValue])
            {
                case "apple":
                    SpawnNewObject(toBefilled[0], "apple", position, Quaternion.identity);
                    break;
                case "lemon":
                    SpawnNewObject(toBefilled[1], "lemon", position, Quaternion.identity);
                    break;
                case "orange":
                    SpawnNewObject(toBefilled[2], "orange", position, Quaternion.identity);
                    break;
                case "grapes":
                    SpawnNewObject(toBefilled[3], "grapes", position, Quaternion.identity);
                    break;
                case "pineapple":
                    SpawnNewObject(toBefilled[4], "pineapple", position, Quaternion.identity);
                    break;
            }
            if (tags.Count > 0)
                StartCoroutine(Spawner);
            else
            {
                // Debug.LogError("Game Won!\n Switch Scene");
                //activeGameMini.intsnace.OnCheckLoadNewScreen();
                // activeGameMini.intsnace.NextLevelBtn();
               // activeGameMini.intsnace.OnCheckLoadCompletion();
            }
        }
    }

    private void SpawnNewObject(GameObject obj, string tag, Vector3[] position, Quaternion quaternion)
    {
        if (obj.transform.localPosition.y < 0)
        {
            GameObject spawn = PoolingFurits.Instance.SpawnfromPool(tag, position[Random.Range(0, position.Length)], quaternion);
            //            print(obj.transform.localPosition.y);
        }
        else
        {
            tags.Remove(tag);
        }
    }
    public AudioClip[] Happy_Clip;
    int get_touch;
    public int Delay_Time;
    public bool Play_Effect;
    public void OnCheckPlayAudio()
    {
        get_touch++;
        Debug.Log("Hello Buddy");
        if (get_touch == Delay_Time && Play_Effect)
        {
            Debug.Log("Hello Buddy if");
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            if (audio.enabled == true)
                audio.PlayOneShot(Happy_Clip[Random.Range(0, Happy_Clip.Length)]);
            get_touch = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "orange")
        {
            Debug.Log("Cause Errors");
        }
        switch (collision.gameObject.tag)
        {
            case "apple":
                FillBar(collision.gameObject, position[0], 0);
                PlaySound();
                OnCheckPlayAudio();
                break;
            case "lemon":
                FillBar(collision.gameObject, position[1], 1);
                PlaySound();
                OnCheckPlayAudio();
                break;
            case "orange":
                FillBar(collision.gameObject, position[1], 2);
                PlaySound();
                OnCheckPlayAudio();
                break;
            case "grapes":
                FillBar(collision.gameObject, position[2], 3);
                PlaySound();
                OnCheckPlayAudio();
                break;
            case "pineapple":
                FillBar(collision.gameObject, position[2], 4);
                PlaySound();
                OnCheckPlayAudio();
                break;
        }
    }
    public GameObject[] tick;
    
    IEnumerator waitForParticles(GameObject s)
    {
        yield return new WaitForSeconds(0.25f);
        if(s.GetComponent<MeshRenderer>())
        {
            s.GetComponent<MeshRenderer>().enabled = true;
        }
        s.SetActive(false);
    }
    public bool happy = false;
    public float tofill;
    public void FillBar(GameObject detector, Vector3 position, int index)
    {
        ////if (controller)
        {
           // controller.PlayHappy();
        }
        if (Happy_Ch != null)
        {
            Happy_Ch.SetTrigger("Happy");
        }
        //detector.SetActive(false);
        if (detector.transform.childCount > 0)
            detector.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        if (detector.GetComponent<SkinnedMeshRenderer>())
            detector.GetComponent<SkinnedMeshRenderer>().enabled = false;
        else if (detector.GetComponent<MeshRenderer>())
            detector.GetComponent<MeshRenderer>().enabled = false;
        //else if(detector.transform.childCount>=2)
        if (detector.GetComponent<Collider2D>())
            detector.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(waitForParticles(detector));
        //detector.transform.localPosition = position;
        if (toBefilled[index].transform.localPosition.y < 0)
        {
            toBefilled[index].transform.localPosition += new Vector3(0, tofill, 0);
            Debug.Log("position plus   "+ toBefilled[index].transform.localPosition.y);

            if (toBefilled[index].transform.localPosition.y >= 0f)
            {
                if (Happy_Ch1 != null)
                {
                    Happy_Ch1.SetTrigger("Happy");
                }
                if (tick[index])
                    tick[index].SetActive(true);
                if (toBefilled[index].transform.parent.transform.childCount >= 2)
                    toBefilled[index].transform.parent.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        else
        {
            //if (Happy_Ch1 != null)
            //{
            //    Happy_Ch1.SetTrigger("Happy");
            //}
            if (toBefilled[index].transform.parent.transform.childCount >= 2)
                toBefilled[index].transform.parent.transform.GetChild(1).gameObject.SetActive(true);
            if (tick[index])
                tick[index].SetActive(true);
            //  Debug.Log("Complete");
        }
    }

    private void PlaySound()
    {
        if (audioSource.enabled==true)
            audioSource.Play();
    }
    public GameObject AnimEnd;
    public GameObject GameRef;
    private void SelectionScreen()
    {
        AnimEnd.SetActive(true);
        //GameRef.SetActive(false);
        //SceneManager.LoadScene("Selection screen");
    }

}