using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PopIt : MonoBehaviour
{
    public GameObject BGPlane,soundobj,SelectionUI,Tittle,Home,ThemeButton,Can;
    public GameObject[] Levels;
    public AudioClip[] sounds;
    public Material[] BGMats, PopMats;

    public GameObject[] ButtonUI;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("NoAds") == 1)
            RemoveAdIcon();
        //Debug.Log("sulemanm");
    }

    private void Awake()
    {
        
    }

    public int num;
    int MatAsign, ChildCount, Compare;
    

    void StartToPlay()
    {
        //PantraAdsManager.Instance.ShowBanner(PantraAdsManager.BannerSize.SMARTBANNER,PantraAdsManager.BannerPosition.BOTTOM);
        MatAsign = Random.Range(0, PopMats.Length);
        Levels[num].SetActive(true);
        if (Levels[num].GetComponent<OwnColorSelection>())
            Levels[num].GetComponent<OwnColorSelection>().SetRandomMaterial();
        else
            Levels[num].GetComponent<TwoSidedBlendShapes>().SetMaterial(PopMats[MatAsign]);
        //BGPlane.GetComponent<MeshRenderer>().material = BGMats[Random.Range(0, BGMats.Length)];
        /*Levels[num].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = PopMats[MatAsign];
        for (int i = 0; i < Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform.childCount; i++)
        {
            Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().material = PopMats[MatAsign];
            Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().material = PopMats[MatAsign];
            ChildCount++;
        }*/
    }

  

    int AdCount;
    public void ThemeChange()
    {
        if (AdCount < 3)
        {


            MatAsign = Random.Range(0, PopMats.Length);
            //Levels[num].SetActive(true);
            BGPlane.GetComponent<MeshRenderer>().material = BGMats[Random.Range(0, BGMats.Length)];
            if (Levels[num].GetComponent<OwnColorSelection>())
                Levels[num].GetComponent<OwnColorSelection>().SetRandomMaterial();
            else
                Levels[num].GetComponent<TwoSidedBlendShapes>().SetMaterial(PopMats[MatAsign]);
            //Levels[num].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = PopMats[MatAsign];
            /*for (int i = 0; i < Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform.childCount; i++)
            {
                Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().material = PopMats[MatAsign];
                Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().material = PopMats[MatAsign];

            }*/
            AdCount++;
        }
        else
        {
            Instantiate(Can);
            //CalmingGames.Show_Interstatial();

            //PantraAdsManager.Instance.ShowInterstitial(); 
            MatAsign = Random.Range(0, PopMats.Length);
            //Levels[num].SetActive(true);
            BGPlane.GetComponent<MeshRenderer>().material = BGMats[Random.Range(0, BGMats.Length)];
            if (Levels[num].GetComponent<OwnColorSelection>())
                Levels[num].GetComponent<OwnColorSelection>().SetRandomMaterial();
            else
            Levels[num].GetComponent<TwoSidedBlendShapes>().SetMaterial(PopMats[MatAsign]);
            AdCount =0;
        }
        
    }


    public void selectionButtons(int ButtonNum)
    {
        num = ButtonNum;
        StartCoroutine(AdsShow());
        
    }

    
    public void SelectRewardedButton(int buttonNum)
    {
        if (PlayerPrefs.GetInt("Rewarded" + buttonNum) == 1 || PlayerPrefs.GetInt("NoAds") == 1)
            selectionButtons(buttonNum);
        else
        {
            num = buttonNum;
            StartCoroutine(ShowRewardedAd());
            
        }
    }

    IEnumerator ShowRewardedAd()
    {
        Instantiate(Can);
        yield return new WaitForSeconds(1f);
        //PantraAdsManager.Instance.ShowRewardedVideo(() => {

        //    SelectionUI.SetActive(false);
        //    Tittle.SetActive(false);
        //    Home.SetActive(true);
        //    ThemeButton.SetActive(true);
        //    StartToPlay();
        //});
        yield return new WaitForSeconds(.5f);
        
    }
    IEnumerator AdsShow()
    {
        Instantiate(Can);
        
        yield return new WaitForSeconds(1f);
        if (PlayerPrefs.GetInt("NoAds") == 0)
            //PantraAdsManager.Instance.ShowInterstitial();
        yield return new WaitForSeconds(.5f);
        SelectionUI.SetActive(false);
        Tittle.SetActive(false);
        Home.SetActive(true);
        ThemeButton.SetActive(true);
        StartToPlay();
    }

    public void OnRewardComplete()
    {
        SelectionUI.SetActive(false);
        Tittle.SetActive(false);
        Home.SetActive(true);
        ThemeButton.SetActive(true);
        StartToPlay();
    }


    int HomeAd;
    public void HomeButtonFun()
    {
        if (HomeAd < 3 || PlayerPrefs.GetInt("NoAds") == 1)
        {
            SelectionUI.SetActive(true);
            Tittle.SetActive(true);
            Home.SetActive(false);
            ThemeButton.SetActive(false);
            ChildCount = 0;
            Rotation = 0;
            Compare = 0;
            Levels[num].GetComponent<TwoSidedBlendShapes>().Reset();

            for (int i = 0; i < Levels.Length; i++)
            {
                Levels[i].SetActive(false);
            }
            
                
            HomeAd++;
        }
        else
        {
            Instantiate(Can);
            
            //PantraAdsManager.Instance.ShowInterstitial();
            SelectionUI.SetActive(true);
            Tittle.SetActive(true);
            Home.SetActive(false);
            ThemeButton.SetActive(false);
            ChildCount = 0;
            Rotation = 0;
            Compare = 0;
            Levels[num].GetComponent<TwoSidedBlendShapes>().Reset();

            for (int i = 0; i < Levels.Length; i++)
            {
                Levels[i].SetActive(false);
            }
            
            HomeAd = 0;
        }
    }



    bool MouseClick,MovementStop;
    int Rotation;
    // Update is called once per frame
    void Update()
    {
/*#if UNITY_EDITOR
        EditorInput();
#else
        MobileInput();
#endif*/
    }

    void MobileInput()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                CheckHit(touch.position);
            }
        }
    }

    void EditorInput()
    {
        if (Input.GetMouseButton(0))
        {
            CheckHit(Input.mousePosition);
        }
    }

    void CheckHit(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "obj")
            {
                if (Compare < ChildCount - 1)
                {
                    //soundobj.GetComponent<AudioSource>().clip = sounds[Random.Range(0, sounds.Length)];
                    soundobj.GetComponent<AudioSource>().Play();
                    hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                    hit.collider.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);
                    Compare++;
                }
                else
                {
                    if (MovementStop == false)
                    {
                        soundobj.GetComponent<AudioSource>().Play();
                        hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                        hit.collider.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);
                        MovementStop = true;
                        StartCoroutine(RotateTray());
                    }
                }
            }
        }
    }

    IEnumerator RotateTray()
    {
        if (Rotation == 0)
        {
            Rotation = 1;
            Levels[num].transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 180, 0), 2f);
            for (int i = 0; i < Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).transform.childCount; i++)
            {
                Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);
            }
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).transform.childCount; i++)
            {
                Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = true;
                Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = false;
                Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);
            }
            Compare = 0;
            MovementStop = false;

        }
        else if (Rotation == 1)
        {
            Rotation = 0;
            Levels[num].transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 2f);
            for (int i = 0; i < Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform.childCount; i++)
            {
                Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);
            }
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform.childCount; i++)
            {
                Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = true;
                Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = false;
                Levels[num].gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);
            }
            Compare = 0;
            MovementStop = false;

        }

        yield return new WaitForSeconds(.1f);
    }

    public void DisableAd()
    {
        PlayerPrefs.SetInt("NoAds", 1);
        print("Ad Removed");
        RemoveAdIcon();
    }

    void RemoveAdIcon()
    {
        foreach(GameObject go in ButtonUI)
        {
            if (go.transform.childCount > 0)
                go.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
