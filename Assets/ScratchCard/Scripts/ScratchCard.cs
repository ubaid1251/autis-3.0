using ScratchCardAsset.Core;
using ScratchCardAsset.Core.Data;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
//using MoreMountains.NiceVibrations;
using DG.Tweening;

[System.Serializable]
public class MultipleTexuterWorking
{
    public GameObject SourceCard;
    //   public Material[] materials;

    public float brushScale;
    public bool isEraseMode;
    public int Limit;
    public Transform PaintedPos;
}
public class ScratchCard : MonoBehaviour
{
    public enum Quality
    {
        Low = 4,
        Medium = 2,
        High = 1
    }

    public enum ScratchMode
    {
        Erase,
        Restore
    }
    //public MultipleTexuterWorking Multi;

    public Camera MainCamera;
    public Transform Surface;
    public Quality RenderTextureQuality = Quality.High;
    public Material Eraser;
    public Material Progress;
    public Material ScratchSurface;
    public RenderTexture RenderTexture;
    //  public Vector2 BrushScale = Vector2.one;
    public Vector2 BrushScale;

    public bool InputEnabled = true;
    public bool InShapeMode = false, InColorMode = false;
    private ScratchMode _mode = ScratchMode.Restore;
    // public ScratchMode mode2 = ScratchMode.Erase;

    public ScratchMode Mode
    {
        get { return _mode; }
        set
        {
            _mode = value;
            var blendOp = _mode == ScratchMode.Erase ? (int)BlendOp.Add : (int)BlendOp.ReverseSubtract;
            Eraser.SetInt(BlendOpShaderParam, blendOp);

        }
    }

    public bool IsScratching
    {
        get
        {
            return cardInput.IsScratching;
        }
    }

    public bool IsScratched
    {
        get
        {
            if (cardRenderer != null)
            {
                return cardRenderer.IsScratched;
            }
            return false;
        }
        private set
        {
            cardRenderer.IsScratched = value;
        }
    }

    private ScratchCardRenderer cardRenderer;
    private ScratchCardInput cardInput;
    private Triangle triangle;
    private SpriteRenderer surfaceSpriteRenderer;
    private MeshFilter surfaceMeshFilter;
    private Renderer surfaceRenderer;
    private RectTransform surfaceRectTransform;
    private Vector2 boundsSize;
    private Vector2 imageSize;
    private bool isCanvasOverlay;
    private bool isFirstFrame = true;
    private int lastFrameId;

    private const string BlendOpShaderParam = "_BlendOpValue";
    public static ScratchCard ins;
    public EraseProgress EraseProgress;

    public ScratchCardManager ScratchCardManager;
    //private string ToggleKey = "Toggle";
    //private string BrushKey = "Brush";
    //private string ScaleKey = "Scale";

    // public Text ProgressText;
    public float progressEditor;

    public Transform PointedPos;
    public bool isOnTouch;
    public List<MultipleTexuterWorking> multipleTexuterWorkings = new List<MultipleTexuterWorking>();
    public List<MultipleTexuterWorking> ColorMultipleTextures = new List<MultipleTexuterWorking>();
    public static int IncreaseShape, IncreaseColor;

    [Header("ObjectWich is Required To Scale")]
    public Transform ParentScale;
    private float storedScale;
    public Material material;
    bool isstart;
    public bool start = false;
    int LimitForCollider;
    #region MonoBehaviour Methods


    void Start()
    {

        ins = this;
        IncreaseShape = 0; IncreaseColor = 0;
        // btnIncreaseFun = NailPaint.setNo;
        //MultiTexWorkingCaller(0);
        ///btnIncreaseFun++;
        Init();
        //btnIncreaseFun = NailPaint.NailPaintTool+1;
        //print("NailPaintTool Start " + NailPaint.NailPaintTool);
        //print("START--btnIncreaseFun ==>>>>>" + btnIncreaseFun);
    }

    public void ChangeTroughBtn()
    {
        InShapeMode = true;
        InColorMode = false;
        GameController.PartCountStart++;
        //print("Part Count Start = " + GameController.PartCountStart);
        //print("IncreaseShape start = " + IncreaseShape);
        Invoke("waitSprite", 0.5f);
        if (IncreaseShape >= 1)
        {
            MoveAlongSpline.childNo++;
            //print(" MoveAlongSpline.childNo =" + MoveAlongSpline.childNo);
            MoveAlongSpline.ins.SetSplineLenght();
            //   ScratchCardManager.SpriteCard.GetComponent<SpriteRenderer>().enabled = false;
        }
        IncreaseShape++;
        MultiTexWorkingCaller(IncreaseShape - 1);
    }

    public void ColorObjectChangeTroughBtn()
    {
        InShapeMode = false;
        InColorMode = true;
        IncreaseColor++;
        if (GameController.CountSpriteColor >= 1)
        {
            //GameController.ins.ColorsObject[0].transform.GetChild(GameController.CountSpriteColor - 1).gameObject.SetActive(true);
            //  ScratchCardManager.SpriteCard.GetComponent<SpriteRenderer>().enabled = false;
        }
        //   GameController.ins.ScratchImg.transform.position = GameController.ins.ColorsObject[0].transform.GetChild(GameController.CountSpriteColor).transform.position;
        MultiColorWorkingCaller(IncreaseColor - 1);
    }
    void waitSprite()
    {
        ScratchCardManager.SpriteCard.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void MultiTexWorkingCaller(int index)
    {
        // ScratchCardManager.EraseTextureScale *= multipleTexuterWorkings[index].brushScale;
        ScratchCardManager.EraseTextureScale = new Vector2(multipleTexuterWorkings[index].brushScale, multipleTexuterWorkings[index].brushScale);
        LimitForCollider = multipleTexuterWorkings[index].Limit;
        MainCamera = null;
        Surface = null;
        Eraser = null;
        Progress = null;
        ScratchSurface = null;
        RenderTexture = null;

        // Sprite MySprite = multipleTexuterWorkings[index].SourceCard.GetComponent<Image>().sprite;
        // ScratchCardManager.SpriteCard0[index].SetActive(true);
        Sprite MySprite = multipleTexuterWorkings[index].SourceCard.GetComponent<SpriteRenderer>().sprite;
        material.mainTexture = MySprite.texture;
        //ScratchCardManager.ImageCard = multipleTexuterWorkings[index].SourceCard;
        //ScratchCardManager.ImageCard.GetComponent<SpriteRenderer>().material = material;

        ScratchCardManager.SpriteCard = multipleTexuterWorkings[index].SourceCard;
        ScratchCardManager.SpriteCard.GetComponent<SpriteRenderer>().material = material;
        ScratchCardManager.ScratchSurfaceSprite = MySprite;
        PointedPos = multipleTexuterWorkings[index].PaintedPos;
        ScratchCardManager.Init();
        EraseProgress.InitCalled();
        Init();
        if (multipleTexuterWorkings[index].isEraseMode)
        {
            Mode = ScratchMode.Erase;
        }
        else
        {
            Mode = ScratchMode.Restore;
        }
        isstart = true;
        start = true;

        ScratchCardManager.SpriteCard.SetActive(true);
    }
    public void MultiColorWorkingCaller(int index)
    {
        ScratchCardManager.EraseTextureScale = new Vector2(ColorMultipleTextures[index].brushScale, ColorMultipleTextures[index].brushScale);
        LimitForCollider = ColorMultipleTextures[index].Limit;
        //ScratchCardManager.EraseTextureScale *= ColorMultipleTextures[index].brushScale;
        MainCamera = null;
        Surface = null;
        Eraser = null;
        Progress = null;
        ScratchSurface = null;
        RenderTexture = null;

        // Sprite MySprite = multipleTexuterWorkings[index].SourceCard.GetComponent<Image>().sprite;
        // ScratchCardManager.SpriteCard0[index].SetActive(true);
        Sprite MySprite = ColorMultipleTextures[index].SourceCard.GetComponent<SpriteRenderer>().sprite;
        material.mainTexture = MySprite.texture;
        //ScratchCardManager.ImageCard = ColorMultipleTextures[index].SourceCard;
        //ScratchCardManager.ImageCard.GetComponent<SpriteRenderer>().material = material;
        ScratchCardManager.SpriteCard = ColorMultipleTextures[index].SourceCard;
        ScratchCardManager.SpriteCard.GetComponent<SpriteRenderer>().material = material;
        ScratchCardManager.ScratchSurfaceSprite = MySprite;
        PointedPos = ColorMultipleTextures[index].PaintedPos;
        ScratchCardManager.Init();
        EraseProgress.InitCalled();
        Init();
        if (ColorMultipleTextures[index].isEraseMode)
        {
            Mode = ScratchMode.Erase;
        }
        else
        {
            Mode = ScratchMode.Restore;
        }
        isstart = true;
        start = true;

        ScratchCardManager.SpriteCard.SetActive(true);
    }

    void OnDestroy()
    {
        if (RenderTexture != null && RenderTexture.IsCreated())
        {
            RenderTexture.Release();
            Destroy(RenderTexture);
        }
        if (cardRenderer != null)
            cardRenderer.Release();
    }

    void Update()
    {
        if (isstart)
        {


            if (ParentScale.localScale.x != storedScale && Surface != null)
            {
                storedScale = ParentScale.localScale.x;
                GetScratchBounds();
            }

            if (lastFrameId == Time.frameCount)
                return;

            cardInput.Update();
            if (isFirstFrame)
            {
                cardRenderer.FillRenderTextureWithColor(Color.clear);
                //   print("Helo update?");

                isFirstFrame = false;
            }
            if (cardInput.IsScratching)
            {
                cardInput.Scratch();
            }
            else
            {
                cardRenderer.IsScratched = false;
            }
            lastFrameId = Time.frameCount;

        }


    }

    #endregion

    #region Initializaion

    internal void Init()
    {
        GetScratchBounds();
        InitVariables();
        InitTriangle();
        Invoke("fill", .3f);
        EraseProgress.ResetProgress();
        Application.targetFrameRate = 60;
        EraseProgress.OnProgress += OnEraseProgress;
        EraseProgress.OnCompleted += CompleteProgress;
    }



    private void CompleteProgress(float progress)
    {

        if (Mode == ScratchMode.Restore)
        {
            ClearInstantly();
        }
        else
        {
            //print("isToolFunCompleteCount ELSE ");
            FillInstantly();
        }
        if (InShapeMode == true)
        {
            GameController.Countshape = IncreaseShape;
            // SoundManager.instance.PlayEffect_Instance(3);
            // GameController.CompletePartCount=IncreaseShape-1;
            //if (GameController.Countshape == 1)
            //{
            //    //ChangeTroughBtn();
            //    //GameController.ins.ShapeSpritesAfter[0].transform.GetChild(GameController.Countshape - 1).gameObject.SetActive(true);
            //    //ScratchCardManager.SpriteCard.GetComponent<SpriteRenderer>().enabled = false;
            //    //     GameController.ins.Shape[0].transform.GetChild(GameController.CountSprite).gameObject.SetActive(true);
            //    //    GameController.ins.Tick.SetActive(true);
            //    //    GameController.ins.Pen.GetComponent<DragDropSprite>().enabled = false;
            //    //    GameController.ins.Pen.transform.DOLocalMove(new Vector3(4.14f, -1.21f, 0), 0.5f).SetEase(Ease.Linear);
            //}


            //  print("IncreaseShapeComplete =  " + IncreaseShape);
            // print("CompleteFace " + GameController.Countshape);
            //}

        }
        if (InColorMode == true)
        {
            GameController.CountSpriteColor = IncreaseColor;
            // SoundManager.instance.PlayEffect_Instance(3);

            if (GameController.CountSpriteColor > 0)
            {
                GameController.ins.OnColorRestrt = false;
                SoundManager.instance.StopEffect(20);
                // GameController.ins.ShapeSpritesAfter[0].transform.GetChild(GameController.CountSpriteColor - 1).gameObject.SetActive(true);
                //GameController.ins.ColorsObject[0].transform.GetChild(GameController.CountSpriteColor-1).gameObject.SetActive(true);
                //  ScratchCardManager.SpriteCard.GetComponent<SpriteRenderer>().enabled = false;
                GameController.ins.Tick.SetActive(true);
                GameController.ins.ColoringPen.GetComponent<DragDropSprite>().enabled = false;
                GameController.ins.ColoringPen.transform.DOLocalMove(new Vector3(10.1f, -2.29f, 0), 0.5f).SetEase(Ease.Linear);
                DragDropSprite.ins.isSound = false;
                GameController.ins.PlayParticleForColorParts();
                DragDropSprite.ins.finger.SetActive(false);
                //.OnComplete(() =>
                // {

                // });
            }

            if (GameController.CountSpriteColor == GameController.ColorTotalParts)
            {
                //print("in if " + GameController.CountSpriteColor);
                GameController.ins.Tick.SetActive(false);
                GameController.ins.NextBtn.SetActive(true);
                if (PlayerPrefs.GetInt("RemoveAds") == 0)
                {
//                    IntitializeAdmob.instance.HideBanner();//remove later
                }
                DragDropSprite.ins.finger.SetActive(false);
                GameController.ins.LevelCompleteParticles();
                //GameController.ins.PlayParticleForColorParts();
            }
            //   print("IncreaseColor " + IncreaseColor);
            // print("CompleteFace " + CleanFace.CompleteFace);
        }


    }


    public void OnEraseProgress(float progress)
    {
        //  var value = Mathf.Round(progress * 100f);
        var value = Mathf.Round((1 - progress) * 100f);
        // ProgressText.text = string.Format("Progress: {0}%", value);
        progressEditor = value;
        if (value >= LimitForCollider && GameController.ins.GameStart==true)
        {           
            GameController.ins.Circles[0].transform.GetChild(MoveAlongSpline.i).GetComponent<BoxCollider>().enabled = true;
        }
        // print(value+"= val");
        // UpdateProgress(value);
        if (InColorMode && value>=90)
        {
            //ColorObjectChangeTroughBtn();
        }
    }
    public void UpdateProgress(float value)
    {
        float convertedValue = 1f - (value / 100f);

        // Clamp the converted value between 0 and 1
        float clampedValue = Mathf.Clamp01(convertedValue);
        //  print(clampedValue);
        //  EyeLash.ins.ProgressBar.transform.GetChild(0).GetComponent<Image>().fillAmount = clampedValue;
    }
    void fill()
    {
        if (Mode == ScratchMode.Restore)
        {
            FillInstantly();
            EraseProgress.UpdateProgress();

        }
        else
        {
            ClearInstantly();
            EraseProgress.ResetProgress();
            EraseProgress.UpdateProgress();
        }

        //   ScratchCardManager.ImageCard.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

        ScratchCardManager.SpriteCard.gameObject.SetActive(true);
        //ScratchCardManager.SpriteCard.transform.position = Multi.SourceCard.transform.position;
        //ScratchCardManager.SpriteCard0[CountSprite].GetComponent<SpriteRenderer>().enabled = true;
        //ScratchCardManager.SpriteCard0[CountSprite].gameObject.SetActive(true);
        //ScratchCardManager.ImageCard.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    /// <summary>
    /// Saves scratch card renderer component bounds
    /// </summary>
    private void GetScratchBounds()
    {


        surfaceRenderer = Surface.GetComponent<Renderer>();
        if (surfaceRenderer != null)
        {
            var sourceTexture = surfaceRenderer.sharedMaterial.mainTexture;
            imageSize = new Vector2(sourceTexture.width, sourceTexture.height);
            surfaceMeshFilter = Surface.GetComponent<MeshFilter>();
            if (surfaceMeshFilter != null)
            {
                boundsSize = surfaceMeshFilter != null && !MainCamera.orthographic ? surfaceMeshFilter.sharedMesh.bounds.size : surfaceRenderer.bounds.size;
            }
            else
            {
                surfaceSpriteRenderer = Surface.GetComponent<SpriteRenderer>();
                boundsSize = surfaceSpriteRenderer != null && !MainCamera.orthographic ? surfaceSpriteRenderer.sprite.bounds.size : surfaceRenderer.bounds.size;
            }
        }
        else
        {
            surfaceRectTransform = Surface.GetComponent<RectTransform>();
            if (surfaceRectTransform != null)
            {
                var rect = surfaceRectTransform.rect;
                imageSize = new Vector2(rect.width, rect.height);
                boundsSize = MainCamera.orthographic ? Vector2.Scale(rect.size, surfaceRectTransform.lossyScale) : imageSize;
                var canvas = Surface.GetComponentInParent<Canvas>();
                if (canvas != null)
                {
                    isCanvasOverlay = canvas.renderMode == RenderMode.ScreenSpaceOverlay;
                }
            }
            else
            {
                Debug.LogError("Can't find Renderer or RectTransform Component!");
            }
        }
    }

    private void InitVariables()
    {
        cardInput = new ScratchCardInput(this);
        cardInput.OnScratchStart -= OnScratchStart;
        cardInput.OnScratchStart += OnScratchStart;
        cardInput.OnScratchHole -= OnScratchHole;
        cardInput.OnScratchHole += OnScratchHole;
        cardInput.OnScratchLine -= OnScratchLine;
        cardInput.OnScratchLine += OnScratchLine;
        cardInput.OnScratch -= GetScratchPosition;
        cardInput.OnScratch += GetScratchPosition;
        if (cardRenderer != null)
        {
            cardRenderer.Release();
        }
        cardRenderer = new ScratchCardRenderer(this);
        cardRenderer.SetImageSize(imageSize);
        cardRenderer.CreateRenderTexture();
    }

    private void InitTriangle()
    {
        //bottom left
        var position0 = new Vector3(-boundsSize.x / 2f, -boundsSize.y / 2f, 0);
        var uv0 = Vector2.zero;
        //upper left
        var position1 = new Vector3(-boundsSize.x / 2f, boundsSize.y / 2f, 0);
        var uv1 = Vector2.up;
        //upper right
        var position2 = new Vector3(boundsSize.x / 2f, boundsSize.y / 2f, 0);
        var uv2 = Vector2.one;
        triangle = new Triangle(position0, position1, position2, uv0, uv1, uv2);
    }

    #endregion

    private void OnScratchStart()
    {
        cardRenderer.IsScratched = false;
    }

    private void OnScratchHole(Vector2 position)
    {
        cardRenderer.ScratchHole(position);
    }

    private void OnScratchLine(Vector2 start, Vector2 end)
    {
        cardRenderer.ScratchLine(start, end);
    }

    private Vector2 GetScratchPosition(Vector2 position)
    {
        var scratchPosition = Vector2.zero;
        if (MainCamera.orthographic || isCanvasOverlay)
        {
            var clickPosition = isCanvasOverlay ? (Vector3)position : MainCamera.ScreenToWorldPoint(position);
            var lossyScale = Surface.lossyScale;
            var clickLocalPosition = Vector2.Scale(Surface.InverseTransformPoint(clickPosition), lossyScale) + boundsSize / 2f;
            var pixelsPerInch = new Vector2(imageSize.x / boundsSize.x / lossyScale.x, imageSize.y / boundsSize.y / lossyScale.y);
            scratchPosition = Vector2.Scale(Vector2.Scale(clickLocalPosition, lossyScale), pixelsPerInch);
        }
        else
        {
            var plane = new Plane(Surface.forward, Surface.position);
            var ray = MainCamera.ScreenPointToRay(position);
            float enter;
            if (plane.Raycast(ray, out enter))
            {
                var point = ray.GetPoint(enter);
                var pointLocal = Surface.InverseTransformPoint(point);
                var uv = triangle.GetUV(pointLocal);
                scratchPosition = new Vector2(uv.x * imageSize.x, uv.y * imageSize.y);
            }
        }
        return scratchPosition;
    }

    #region Public Methods

    /// <summary>
    /// Fills RenderTexture with white color (100% scratched surface)
    /// </summary>
    public void FillInstantly()
    {
        //   print("aua");
        cardRenderer.FillRenderTextureWithColor(Color.white);
        IsScratched = true;
    }

    /// <summary>
    /// Fills RenderTexture with clear color (0% scratched surface)
    /// </summary>
    public void ClearInstantly()
    {
        cardRenderer.FillRenderTextureWithColor(Color.clear);
        ///   print("TOOL Clear" + FacialScreenModel.ins.isToolFunCompleteCount + 1);

        IsScratched = true;
    }

    /// <summary>
    /// Clears scratched surface in next frame
    /// </summary>
    public void Clear()
    {
        isFirstFrame = true;
        IsScratched = true;
    }

    /// <summary>
    /// Recreates RenderTexture and clears it in next frame
    /// </summary>
    public void ResetRenderTexture()
    {
        cardRenderer.CreateRenderTexture();
        isFirstFrame = true;
        IsScratched = true;
    }

    /// <summary>
    /// Scratches hole
    /// </summary>
    /// <param name="position"></param>
    public void ScratchHole(Vector2 position)
    {
        cardRenderer.ScratchHole(position);
        IsScratched = true;
    }

    /// <summary>
    /// Scratches line
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="endPosition"></param>
    public void ScratchLine(Vector2 startPosition, Vector2 endPosition)
    {
        cardRenderer.ScratchLine(startPosition, endPosition);
        IsScratched = true;
    }

    /// <summary>
    /// Returns scratch texture
    /// </summary>
    /// <returns></returns>
    public Texture2D GetScratchTexture()
    {
        var previousRenderTexture = RenderTexture.active;
        var texture2D = new Texture2D(RenderTexture.width, RenderTexture.height, TextureFormat.ARGB32, false);
        RenderTexture.active = RenderTexture;
        texture2D.ReadPixels(new Rect(0, 0, texture2D.width, texture2D.height), 0, 0, false);
        texture2D.Apply();
        RenderTexture.active = previousRenderTexture;
        return texture2D;
    }

    /// <summary>
    /// Set new ScratchTexture
    /// </summary>
    /// <param name="texture"></param>
    public void SetScratchTexture(Texture2D texture)
    {
        Init();
        ClearInstantly();
        Graphics.Blit(texture, RenderTexture);
        IsScratched = true;
    }

    #endregion
}