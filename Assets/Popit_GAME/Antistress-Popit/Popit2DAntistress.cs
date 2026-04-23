using System.Collections.Generic;
using UnityEngine;
public class Popit2DAntistress : MonoBehaviour
{
    public GameObject BG;
    public Transform PopitsParent;
    public Sprite GlowSpr;
    public List<Sprite> defaultSpr;
    public BoxCollider2D[] popits;
    private int Maxnum;
    [SerializeField]
    private List<int> epops = new List<int>();
    int count;
    int total;
    bool initialized;
    public ParticleSystem spikes;
    public GameObject clickEffect;
    private PopSounds PS;
    private bool once;
    private void OnEnable()
    {
        if (Popit2DManagerAntistress.Epopping)
            Popit2DManagerAntistress.INSTANCE.OnEpopReset += HomeBtnReset;
        if (PopitsParent == null)
            PopitsParent = transform;

        total = PopitsParent.childCount;
        popits = PopitsParent.GetComponentsInChildren<BoxCollider2D>();
        if (!once)
        {
            once = true;
            for (int i = 0; i < popits.Length; i++)
            {
                defaultSpr.Add(popits[i].transform.GetComponent<SpriteRenderer>().sprite);
            }
        }
        if (Popit2DManagerAntistress.Epopping)
        {
            GenerateRandomList();
        }
        print("Enable");
    }
    private void OnDisable()
    {
        if (Popit2DManagerAntistress.Epopping)
            Popit2DManagerAntistress.INSTANCE.OnEpopReset -= HomeBtnReset;
        for (int i = 0; i < popits.Length; i++)
        {
            popits[i].transform.GetComponentInChildren<PopSounds>().selectedPop = false;
            popits[i].transform.gameObject.SetActive(true);
        }
        for (int i = 0; i < popits.Length; i++)
        {
            popits[i].transform.GetComponent<SpriteRenderer>().sprite = defaultSpr[i];
        }
        popits = new BoxCollider2D[0];

    }
    private void Start()
    {
        EnablePopit();
    }
    public void EnablePopit()
    {
        Popit2DManagerAntistress.INSTANCE.fillAmountImage.fillAmount = 0;
        Popit2DManagerAntistress.INSTANCE.value = 1f / popits.Length;
        if (initialized)
        {

            //while (newList.Count < number && tmpList.Count > 0)
            //{
            //    int index = Random.Range(0, tmpList.Count);
            //    newList.Add(tmpList[index]);
            //    tmpList.RemoveAt(index);
            //}
            count = 0;
            for (int i = 0; i < popits.Length; i++)
            {
                print("//////////////" + i + "/////////////////////////");
                popits[i].gameObject.SetActive(true);
            }
        }
        print("Start");




        //if (Epopping)
        //{
        //    Maxnum = Random.Range(1, popits.Length);

        //}       
        initialized = true;
    }
    public void GenerateRandomList()
    {

        Maxnum = Random.Range(1 , popits.Length);
        print("MaxNum=>" + Maxnum);
        for (int i = 0; i < Maxnum; i++)
        {
            int numToAdd = Random.Range(0 , Maxnum);
            while (epops.Contains(numToAdd))
            {

                numToAdd = Random.Range(0 , Maxnum);
            }

            epops.Add(numToAdd);
        }
        for (int i = 0; i < popits.Length; i++)
        {
            popits[i].GetComponent<SpriteRenderer>().sprite = defaultSpr[i];
        }
        for (int i = 0; i < epops.Count; i++)
        {
            popits[epops[i]].GetComponent<SpriteRenderer>().sprite = GlowSpr;
            popits[epops[i]].GetComponent<PopSounds>().selectedPop = true;
        }

    }
    void Update()
    {
#if UNITY_EDITOR
        EditorInput();
#else
                MobileInput();
#endif
    }

    void MobileInput()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (Popit2DManagerAntistress.Epopping == false)
                    CheckHit(touch.position);
                if (Popit2DManagerAntistress.Epopping == true)
                    Check_E_Hit(touch.position);
            }
        }
    }

    void EditorInput()
    {

        if (Input.GetMouseButton(0))
        {
            if (Popit2DManagerAntistress.Epopping == false)
            {
                CheckHit(Input.mousePosition);
            }
            if (Popit2DManagerAntistress.Epopping)
            {
                Check_E_Hit(Input.mousePosition);
            }

        }

    }

    void CheckHit(Vector3 position)
    {
        position = Camera.main.ScreenToWorldPoint(new Vector3(position.x , position.y , -Camera.main.transform.position.z + transform.position.z));
        RaycastHit2D hit = Physics2D.Raycast(position , Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("obj"))
        {
            hit.transform.gameObject.SetActive(false);
            if (Popit2DManagerAntistress.INSTANCE.fillAmountImage.fillAmount < 1)
            {
                Popit2DManagerAntistress.INSTANCE.FilImageAmountCall();
            }
            count++;
            //int p = Random.Range(1 , 2);
            SoundManager.instance.PlayEffect_Instance(21);
            //MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.RigidImpact);
            ChecktAllPophit();

            //  GameObject temp = Instantiate(clickEffect, hit.transform.position, Quaternion.identity);
            //  Destroy(temp, 0.5f);
            /*spikes.transform.position = hit.point;
            spikes.Play();*/
        }

    }

    void Check_E_Hit(Vector3 pos)
    {
        pos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x , pos.y , -Camera.main.transform.position.z + transform.position.z));
        RaycastHit2D hit = Physics2D.Raycast(pos , Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("obj"))
        {
            PS = hit.transform.GetComponent<PopSounds>();


            if (PS.selectedPop)
            {
                count++;
                PS.setSound(PS.Popsound);
            }
            else
            {
                PS.setSound(PS.warningSound);
            }
            hit.transform.gameObject.SetActive(false);
            //   hit.transform.GetComponent<SpriteRenderer>().sprite=defaultSpr;
            //MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.RigidImpact);
            // ChecktAllPophit();
            CheckAll_E_hitPops();

            //  GameObject temp = Instantiate(clickEffect, hit.transform.position, Quaternion.identity);
            //  Destroy(temp, 0.5f);
            /*spikes.transform.position = hit.point;
            spikes.Play();*/
        }

    }

    void CheckAll_E_hitPops()
    {
        print("Count     :" + count + " List Size   :" + epops.Count);
        if (count >= epops.Count)
        {
            epops.Clear();
            for (int i = 0; i < popits.Length; i++)
            {
                popits[i].transform.gameObject.SetActive(false);
            }

            print("E Pops Done");
            if (Popit2DManagerAntistress.Adcounter > 2)
            {
                Popit2DManagerAntistress.Adcounter = 0;
                //GoogleAdMobController.THIS.ShowR1InterstitialAd(); //AdCallPosition
            }
            Invoke("ResetPop" , 1.0f);
        }

    }

    //Only Use for Epops Btn if HomeBtn used;
    public void HomeBtnReset()
    {

        count = epops.Count;
        epops.Clear();
        Maxnum = 0;
        count = 0;
        for (int i = 0; i < Popit2DManagerAntistress.INSTANCE.Levels.Length; i++)
        {
            Popit2DManagerAntistress.INSTANCE.Levels[i].SetActive(false);
        }

        Popit2DManagerAntistress.INSTANCE.EpopsSelectionUI.SetActive(true);
        //epops.Clear();

        //Invoke("ResetPop", 1.0f);
    }

    void ResetPop()
    {
        Popit2DManagerAntistress.Adcounter++;
        EnablePopit();
        GenerateRandomList();
        if (Popit2DManagerAntistress.Adcounter > 3)
        {
            Popit2DManagerAntistress.Adcounter = 0;
            //GoogleAdMobController.THIS.ShowR1InterstitialAd(); //AdCallPosition
        }
    }
    void ChecktAllPophit()
    {
        // if(transform.parent.gameObject!=null)
        {
            int childCount = transform.childCount;
            int counter = 0;
            for (int i = 0; i < childCount; i++)
            {
                if (!transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    counter++;
                    // Debug.Log(counter + " : suleman : " + childCount);
                }
            }

            if (counter >= childCount)
            {
                print("<<<< POP IT COMPLETED >>>>");
                spikes.Play();
                Popit2DManagerAntistress.INSTANCE.FileUserExpericenBar(3.6f , transform.gameObject.name);
                SoundManager.instance.PlayEffect_Instance(1);
                Invoke("EnablePopit" , 0.2f);
                //  Popit2DManager.INSTANCE.GetCallMiniGameOnCompletePopHit();
                counter = 0;
            }
        }
    }

}
