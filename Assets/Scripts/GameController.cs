using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
public class GameController : MonoBehaviour
{
    public static GameController ins;
    public GameObject[]  CurrentShape;
    [HideInInspector]
    public List<GameObject> Spline = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> Circles = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> Positions = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> ShapeParts = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> DotedImgs = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> ColorsParts = new List<GameObject>();
    public Color[] MyColors;
    public GameObject BarText;
  //  public List<Color> MyColors;
    public Sprite[] AllShapeSprites;
    public string[] AllShapeNames;
    public Vector3[] PenStartPosition;
    public static int ShapeTotalParts, ColorTotalParts, ShapeTotalRandPos,TotalCircles,ShapeActiveNo,ForLvl;
    public static int Countshape, CountSpriteColor,ChildColor, PartCountStart;
    public GameObject Tick, ColorsA, ColorsB, ColoringPen, DrawingPen, CanvasParent, GameOverPanel, ScratchImg
        , NextBtn, SelectionPanel;//LoadingCanvas;
    public GameObject BarImage, TapToDraw;
    public GameObject ScratchCardObject;
    public ParticleSystem[] ParticleShapes;
    public bool PlayParticle = false,OnColorRestrt = false, GameStart = false, PlayColorParticle = false;
    public GameObject ActiveShape;
    public TMP_Text Lvl;
    public ParticleSystem ConfetiPar;
    private void Awake()
    {
        GameStart = false; PlayParticle = false; OnColorRestrt = false; PlayColorParticle = false;

        //if (PlayerPrefs.GetInt("OngameStart") == 0)
        //{
        //    PlayerPrefs.SetInt("OngameStart", 1);
        //}
    }
    void Start()
    {
        ins = this;
        if (PlayerPrefs.GetInt("ShapeIsChosse")==1)
        {
            SelectionPanel.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("ShapeIsChosse") == 0)
        {
            SelectionPanel.SetActive(true);
        }
        PlayerPrefs.SetInt("Playpar", 0);
        PlayerPrefs.SetInt("ShapeIsChosse", 1);
//        IntitializeAdmob.instance.ShowBanner();
        //AdsManager.Instance.ShowBanner();
        //  ShapeActiveNo = 0; ////  Set the number which shape number u want to on 
        ShapeActiveNo = PlayerPrefs.GetInt("ShapeNumber");
        PlayerPrefs.SetInt(SelectShape.ins.AllShapes[ShapeActiveNo].name, 1);
        //print(ShapeActiveNo);
        ForLvl = ShapeActiveNo + 1;
        Lvl.text = "Level " + ForLvl;
        CountSpriteColor = 0; 
        Countshape = 0; 
        ChildColor = 0; 
        PartCountStart = 0;
        DragDropSprite.ins.isSound = false;
        //Invoke("InitializeShape",1f);
        InitializeShape();
    }
    void InitializeShape()
    {
  
        ActiveShape = Instantiate(CurrentShape[ShapeActiveNo]);
        // int randomText = Random.Range(0, BarText.Length);
        BarText.GetComponent<TMP_Text>().text = "DRAW " + AllShapeNames[ShapeActiveNo];
        BarText.SetActive(true);
        //CircleColliderOff();
        SetShapeElements();
        CountShapeParts();
        CountShapeRaNPositions();
        DrawingPen.transform.DOLocalMove(PenStartPosition[ShapeActiveNo], 0.8f).SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            DrawingPen.GetComponent<MoveAlongSpline>().enabled = true;
        });

        Invoke("CircleColliderOff", 0.5f);
        GameStart = true;
    }
    void SetShapeElements()
    {
        Spline.Add(ActiveShape.transform.GetChild(0).gameObject);
        Circles.Add(ActiveShape.transform.GetChild(1).gameObject);
        Positions.Add(ActiveShape.transform.GetChild(2).gameObject);
        ShapeParts.Add(ActiveShape.transform.GetChild(3).GetChild(0).gameObject);
        DotedImgs.Add(ActiveShape.transform.GetChild(3).GetChild(1).gameObject);
        ColorsParts.Add(ActiveShape.transform.GetChild(3).GetChild(2).gameObject);
        ActiveShape.SetActive(true);
        BarImage.GetComponent<Image>().sprite = AllShapeSprites[ShapeActiveNo];
        BarImage.GetComponent<Image>().SetNativeSize();
        BarImage.SetActive(true);
    }
    void CountShapeRaNPositions()
    {

        if (Positions.Count > 0)
        {
            Transform currentTransform = Positions[0].transform;
            int childCount = currentTransform.childCount;
            //Debug.Log("Total Random Positions = : " + childCount);
            ShapeTotalRandPos = childCount;
        }
    }
    int[] limitValues ;
    void CountShapeParts()
    {
        //SetLimits();
        if (ShapeParts.Count > 0)
        {
            Transform firstShapeTransform = ShapeParts[0].transform;
            int childCount = firstShapeTransform.childCount;
            //Debug.Log("Total Parts Count of ShapeParts =  " + childCount);
            ShapeTotalParts = childCount;
        }
        for (int AddTextureComponent = 0; AddTextureComponent < ShapeTotalParts; AddTextureComponent++)
        {
            MultipleTexuterWorking newTexuterWorking = new MultipleTexuterWorking();
            ScratchCardObject.GetComponent<ScratchCard>().multipleTexuterWorkings.Add(newTexuterWorking);
            newTexuterWorking.SourceCard = ShapeParts[0].transform.GetChild(AddTextureComponent).gameObject;
            newTexuterWorking.brushScale = 0.8f;
            newTexuterWorking.PaintedPos = ScratchCardObject.GetComponent<ScratchCard>().PointedPos;
            newTexuterWorking.Limit = 30;
            //newTexuterWorking.Limit = limitValues[AddTextureComponent % limitValues.Length];
        }
        Invoke("ScratchSketch", .3f);
        CountColorParts();
    }


    void CountColorParts()
    {
        if (ColorsParts.Count > 0)
        {
            Transform firstShapeTransform = ColorsParts[0].transform;
            int childCount = firstShapeTransform.childCount;
            //Debug.Log("Total Parts Count of ColorParts = " + childCount);
            ColorTotalParts = childCount;
            MyColors = new Color[ColorTotalParts];
        }
        for (int AddColorComponent = 0; AddColorComponent < ColorTotalParts; AddColorComponent++)
        {
            MultipleTexuterWorking newColorWorking = new MultipleTexuterWorking();
            ScratchCardObject.GetComponent<ScratchCard>().ColorMultipleTextures.Add(newColorWorking);
            newColorWorking.SourceCard = ColorsParts[0].transform.GetChild(AddColorComponent).gameObject;
            newColorWorking.brushScale = 0.9f;
            newColorWorking.Limit = 100;
            newColorWorking.PaintedPos = ColoringPen.transform.GetChild(0).transform;

            MyColors[AddColorComponent] = ColorsParts[0].transform.GetChild(AddColorComponent).GetComponent<SpriteRenderer>().color;
        }
        for (int AddColorComponent = 0; AddColorComponent < ColorTotalParts; AddColorComponent++)
        {
            ColorsParts[0].transform.GetChild(AddColorComponent).GetComponent<SpriteRenderer>().color = new Color(0.529f, 0.529f, 0.529f, 1);
        }
    }



    void SetLimits()
    {
        if(ShapeActiveNo==0)
        {
            limitValues = new int[] { 28, 39, 33 , 44 };
        }
        else if (ShapeActiveNo == 1)
        {
            limitValues = new int[] { 47, 39, 33, 44, 26 };
        }
        else if (ShapeActiveNo == 2)
        {
            limitValues = new int[] { 47, 39, 33, 44, 26 };
        }
    }
    public void CircleColliderOff()
    {
        for (int i = 0; i < ShapeTotalParts; i++)
        {
            Circles[0].transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void RandomParticles(GameObject Posobj)
    {
        if (PlayParticle == true)
        {
            int randomNum = Random.Range(0, ParticleShapes.Length);
            for (int i = 0; i < ParticleShapes.Length; i++)
            {
                ParticleShapes[i].gameObject.SetActive(false);
            }
        //    print("randomNum" + randomNum);
            ParticleShapes[randomNum].transform.position = Posobj.transform.position;
            ParticleShapes[randomNum].gameObject.SetActive(true);
            

            ParticleShapes[randomNum].Play();
            //SoundManager.instance.SpecialPlayEffect_Instance(randomNum);
            //PlayerPrefs.SetInt("Playpar", 0);
            Invoke("offbool", 1f);
        }
        else
        {

        }
    }
    void offbool()
    {
        PlayParticle = false;
        PlayerPrefs.SetInt("Playpar", 0);
    }
    public void OffParticles()
    {
        for (int i = 0; i < ParticleShapes.Length; i++)
        {
            ParticleShapes[i].gameObject.SetActive(false);
        }
    }
    public void PlayParticleForColorParts()
    {
        if (PlayColorParticle)
        {
            PlayColorParticle = false;
            int randomNum = Random.Range(0, ParticleShapes.Length);
            for (int i = 0; i < ParticleShapes.Length; i++)
            {
                ParticleShapes[i].gameObject.SetActive(false);
            }
         //   print("randomNum " + randomNum + 1);
            ParticleShapes[randomNum].transform.position = new Vector3(0, 0, 0);
            ParticleShapes[randomNum].gameObject.SetActive(true);


            ParticleShapes[randomNum].Play();
            //SoundManager.instance.SpecialPlayEffect_Instance(randomNum);
        }
    }
    public void ScratchSketch()
    {
        //   Debug.Log("Total Shape Parts Count: " + ShapeTotalParts);
   
        ScratchCardObject.GetComponent<ScratchCard>().ChangeTroughBtn();
        //MoveAlongSpline.ins.SetSplineLenght();
    }
    public void ScratchColor()
    {
        //Debug.Log("Total Shape Parts Count: " + ShapeTotalParts);
        ScratchCardObject.GetComponent<ScratchCard>().ColorObjectChangeTroughBtn();
        OnColorObjects.ins.SelColorBorders(CountSpriteColor);
       //if(CountSpriteColor==0)
       // {
        ColorsA.SetActive(true);
       //     ColorsB.SetActive(false);
       // }
       // else if (CountSpriteColor == 1)
       // {
       //     ColorsA.SetActive(false);
       //     ColorsB.SetActive(true);
       // }
      //  print(CountSpriteColor);
    }

    public void SetColor(GameObject Colorobj)
    {
        SoundManager.instance.PlayEffect_Instance(4);
        DragDropSprite.ins.isSound = true;
        PlayColorParticle = true;
        OnColorRestrt = true;
        SpriteRenderer MyColoringObj;
        MyColoringObj = ScratchCardObject.GetComponent<ScratchCardManager>().SpriteCard.GetComponent<SpriteRenderer>();
        DragDropSprite.ins.referenceSpriteRenderer = MyColoringObj;
        MyColoringObj.color = Colorobj.transform.GetComponent<Image>().color;
        ColoringPen.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = MyColoringObj.color;
        FadeInOut.ins.FadingObjStop();
        ColoringPen.transform.DOLocalMove(MyColoringObj.transform.position, 0.8f).SetEase(Ease.Linear)
        .OnComplete(() =>
         {
             GameController.ins.ColoringPen.GetComponent<DragDropSprite>().enabled = true;
             MyColoringObj.enabled = true;
         });

    }
    public void tick()
    {
        SoundManager.instance.PlayEffect_Instance(4);

        ChildColor++;
        if(ChildColor<=CountSpriteColor)
        {
          //  ColorsA.SetActive(true);
            ScratchColor();
        }
        else if (ChildColor > CountSpriteColor)
        {
            SceneManager.LoadSceneAsync("ColorGame");
        }
    }
    public void LoadRestart()
    {
        SoundManager.instance.PlayEffect_Instance(4);
        SceneManager.LoadScene("ColorGame");
    }
    public void OffPens()
    {
        DrawingPen.SetActive(false);
        ColoringPen.SetActive(false);
    }
    public void LoadNext()
    {
        PlayerPrefs.SetInt("PLayDrawON", 0);
        DOTween.KillAll();
        SoundManager.instance.PlayEffect_Instance(4);
        
        OffPens();
        if (PlayerPrefs.GetInt("PlayedAllShapes") == 1)
        {
            int randomIndex = Random.Range(0, CurrentShape.Length);
            PlayerPrefs.SetInt("ShapeNumber", randomIndex);
        }
        else
        {
            if ((PlayerPrefs.GetInt("ShapeNumber") == 30 && PlayerPrefs.GetInt("Purchased") == 1) ||
                (PlayerPrefs.GetInt("ShapeNumber") == 3 && PlayerPrefs.GetInt("Purchased") == 0))
            {
                PlayerPrefs.SetInt("ShapeNumber", 0);
                //PlayerPrefs.SetInt("PlayedAllShapes", 1);
            }
            else
            {
                PlayerPrefs.SetInt("ShapeNumber", PlayerPrefs.GetInt("ShapeNumber") + 1);
                PlayerPrefs.SetInt("BuyShape", PlayerPrefs.GetInt("BuyShape") + 1);
            }
        }
        if (PlayerPrefs.GetInt("BuyShape") % 4 ==0 && PlayerPrefs.GetInt("Purchased") == 0)
        {
            PlayerPrefs.SetString("CameFrom", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("PurchasePanel_New");
        }
        else
        {
            SceneManager.LoadScene("ColorGame");
        }
        //wait1();
        //LoadingCanvas.SetActive(true);
        ////Invoke("wait1", 2.5f);
    }
    public void ReloadBtn()
    {
        PlayerPrefs.SetInt("PLayDrawON", 0);
        DOTween.KillAll();
        SoundManager.instance.PlayEffect_Instance(4);
      //  IntitializeAdmob.instance.ShowInterstitialAd();
        OffPens();
        wait1();
        //LoadingCanvas.SetActive(true);
        //Invoke("wait1", 2.5f);
    }

    public void HomeBtn()
    {
        PlayerPrefs.SetInt("PLayDrawON", 0);
        PlayerPrefs.SetInt("ShapeIsChosse", 0);
        DOTween.KillAll();
        SoundManager.instance.PlayEffect_Instance(4);
       //        IntitializeAdmob.instance.ShowInterstitialAd();
        OffPens();
        //LoadingCanvas.SetActive(true);
        //Invoke("wait1", 2.5f);
        wait1();
    }
    public void wait1()
    {
        SceneManager.LoadScene("ColorGame");
    }
    public void Playsound()
    {
        SoundManager.instance.PlayEffect_Instance(4);
    }
    public void DragSoundOn()
    {
        DragDropSprite.ins.isSound = true;
    }
    public void DragSoundOff()
    {
        DragDropSprite.ins.isSound = false;

    }
    void AnimScale()
    {
        Vector3 originalScale = ActiveShape.transform.localScale;
        Vector3 increasedScale = originalScale + new Vector3(0.05f, 0.05f, 0.05f);

        Sequence scaleSequence = DOTween.Sequence();
        scaleSequence.Append(ActiveShape.transform.DOScale(increasedScale, 0.5f).SetEase(Ease.Linear));
        //scaleSequence.AppendInterval(0.001f); // Optional pause at max scale
        scaleSequence.Append(ActiveShape.transform.DOScale(originalScale, 0.5f).SetEase(Ease.Linear));
    }
    public void LevelCompleteParticles()
    {
        SoundManager.instance.PlayEffect_Instance(0);
        AnimScale();
        ConfetiPar.gameObject.SetActive(true);
        ConfetiPar.Play();
    }

    void Update()
    {
      //  print(Pen.transform.position);
    }
}
