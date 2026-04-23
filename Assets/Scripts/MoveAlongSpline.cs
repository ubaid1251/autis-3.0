using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Splines;
//using MoreMountains.NiceVibrations;
using DG.Tweening;
public class MoveAlongSpline : MonoBehaviour
{
    public Camera MyCam;
    public float speed = 1f;
    public float DistancePercntge = 0f;
    public float SplineLength;
    public static int i = 0;
    public  static bool IsComplete, IsRestartOption, BtnPressed,restrt;
    public static int CountLine;
    public static MoveAlongSpline ins;
    public EventSystem Eve;
    public float lerpSpeed = 0.5f, delayTime;
    static int MinusShape = 0, ResetGame;
    public static int childNo = 0;
    void Start()
    {
        i = 0; 
        CountLine = 0; MinusShape = 0; childNo = 0; ResetGame = 0;
        BtnPressed = false;
        ins = this;
        PlayerPrefs.SetInt("VibrateShape", 0);
        PlayerPrefs.SetInt("ForLine", 0);
        PlayerPrefs.SetInt("PosSet", 0);
        DragDropSprite.ins.isSound = false;
        IsComplete = false; restrt = false;
        SetSplineLenght();
        GameController.ins.TapToDraw.SetActive(true);
        //StartCoroutine(StartCooldown());
        //SplineLength = 8;
    }
    public void SetSplineLenght()
    {
        SplineLength = GameController.ins.Spline[0].transform.GetChild(childNo).transform.GetComponent<SplineContainer>().CalculateLength();
    }
    void Update()
    {
        if (!Eve.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0) && IsComplete == false)
            {
               // OnSplime = true;
                GameController.ins.ScratchCardObject.SetActive(true);
                delayTime += speed * Time.deltaTime;
                SplimeAndLine();
                //StopCoroutine(StartCooldown());
                GameController.ins.TapToDraw.SetActive(false);
                if (PlayerPrefs.GetInt("PLayDrawON") == 1)
                {
                    //print("PLyyyy");
                    SoundManager.instance.PlayEffect_Loop(20);
                }
                
            }
        }
        if (!Eve.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonUp(0) && IsComplete == false)
            {
                //MyCam.orthographicSize = 5f;
                PlayerPrefs.SetInt("VibrateShape", 0);
                PlayerPrefs.SetInt("PosSet", 0);
                // OnSplime = false;
                SoundManager.instance.StopEffect(20);
                delayTime = 0;
               //  StartCoroutine(StartCooldown());
                GameController.ins.ScratchCardObject.GetComponent<ScratchCard>().InputEnabled = false;
                transform.DOMove(new Vector3(transform.position.x-0.5f, transform.position.y - 0.5f, transform.position.z), 0.2f).SetEase(Ease.Linear);
                if (CheckCollision.Colide)
                {
                    SoundManager.instance.PlayEffect_Instance(3);
                    CountLine++;
                 //   StopCoroutine(StartCooldown());
                    if (CheckCollision.ins.HaveObject==true)
                    {
                      //  print("inHave");
                        GameController.ins.DotedImgs[0].transform.GetChild(GameController.PartCountStart).gameObject.SetActive(true);
                        GameController.ins.DotedImgs[0].transform.GetChild(GameController.PartCountStart-1).gameObject.SetActive(false);
                    }
                    PlayerPrefs.SetInt("ForLine", 1);
                    GameController.ins.Circles[0].transform.GetChild(i).gameObject.SetActive(false);
                    if (PlayerPrefs.GetInt("ForLine") == 1)
                    {
                        PlayerPrefs.SetInt("ForLine", 0);
                        if (GameController.PartCountStart == GameController.ShapeTotalParts)
                        {
                         //   GameController.ins.ShapeSpritesAfter[0].transform.GetChild(GameController.PartCountStart - 1).gameObject.SetActive(true);
                        //    GameController.ins.ScratchImg.GetComponent<SpriteRenderer>().enabled = false;
                        }
                        else if (GameController.PartCountStart < GameController.ShapeTotalParts)
                        {
                           // GameController.ins.PlayParticle = false;
                            PlayerPrefs.SetInt("Playpar", 0);
                            //print("Particle = "+PlayerPrefs.GetInt("Playpar"));
                            GameController.ins.ScratchSketch();
                        }
                    }


                    if (GameController.ins.Positions != null && i < GameController.ShapeTotalRandPos)
                    {
                        CheckCollision.Colide = false;
                        transform.position = GameController.ins.Positions[0].transform.GetChild(i).transform.position;
                        i++;
                        DistancePercntge = 0f;
                        GameController.ins.Circles[0].transform.GetChild(i).gameObject.SetActive(true);

                    }
                    else
                    {
                        IsComplete = true;
                    }
                }
            }
        }
        if (IsComplete == true)
        {
            OtherBodyParts.ins.OnRemainingParts();
            transform.DOMove(new Vector3(-11f, -2.82f, 0f), 0.6f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    GameController.ins.CanvasParent.SetActive(true);
                });
        }
        if(SplineLength<=5)
        {
            speed = 1.3f;
        }
        else
        {
            speed = 2.5f;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (holdCoroutine != null)
            {
                StopCoroutine(holdCoroutine);
                holdCoroutine = null;
             //   SoundManager.instance.PlayEffect_Loop(9);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            holdCoroutine = StartCoroutine(StartCooldown());
            // Stop coroutine on mouse up
 
        }
    }
    private Coroutine holdCoroutine;
    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(5);
        if (IsComplete == false)
        {           
            GameController.ins.TapToDraw.SetActive(true);
        }
    }

    Vector3 CurrentPos;
    void SplimeAndLine()
    {
        //MyCam.orthographicSize = 3.5f;
        if (PlayerPrefs.GetInt("VibrateShape") == 0)
        {
          //  MMVibrationManager.Vibrate();
            PlayerPrefs.SetInt("VibrateShape", 1);
        }

        if (PlayerPrefs.GetInt("PosSet") == 0)
        {
           CurrentPos = GameController.ins.Spline[0].transform.GetChild(i).transform.GetComponent<SplineContainer>().EvaluatePosition(DistancePercntge);
            transform.DOMove(CurrentPos, 0.1f).SetEase(Ease.Linear)
            .OnComplete(() =>
               {
                   if (delayTime >= 0.25)
                   {
                 //      print("5Sec");
                       GameController.ins.ScratchCardObject.GetComponent<ScratchCard>().InputEnabled = true;
                       GameController.ins.ScratchCardObject.GetComponent<ScratchCard>().enabled = true;
                       ResetGame = 0;
                       PlayerPrefs.SetInt("PosSet", 2);
                   }
               });
        }
        else if (PlayerPrefs.GetInt("PosSet") == 2)
        {
                waitsplm();
        }
        //LineRendrer
        {
            //   Vector3 DirectionMy = NextPos - CurrentPos;
            //if (DistancePercntge <= 1f)
            //{
            //  //  transform.rotation = Quaternion.LookRotation(DirectionMy, transform.up);
            //}
            //if (DistancePercntge >= 1)
            //{
            //    GameController.ins.GameOverPanel.SetActive(true);
            //    GameController.ins.DrawingPen.SetActive(false);
            //    SoundManager.instance.PlayEffect_Instance(10);
            //    for (int i = 0; i < LinesList.Count - 1; i++)
            //    {
            //        LinesList[i].SetActive(false);

            //    }
            //}
            //if (PlayerPrefs.GetInt("ForLine") == 1)
            //{
            //    PlayerPrefs.SetInt("ForLine", 0);
            //    GameController.ins.ScratchSketch();
            //    //LinesList[i].SetActive(true);
            //}
        }
    }
    void waitsplm()
    {
        CurrentPos = GameController.ins.Spline[0].transform.GetChild(i).transform.GetComponent<SplineContainer>().EvaluatePosition(DistancePercntge);
        DistancePercntge += speed * Time.deltaTime / SplineLength;
        transform.position = CurrentPos;
        
    }

    void WaitDraw()
    {
        Invoke("SplimeAndLine", 1f);
    }
    void SplimeRestart1()
    {
        DistancePercntge = 0;
        Vector3 CurrentPosition;
        CurrentPosition = GameController.ins.Spline[0].transform.GetChild(childNo).transform.GetComponent<SplineContainer>().EvaluatePosition(DistancePercntge);
        transform.position = CurrentPosition;
        GameController.ins.ScratchCardObject.GetComponent<ScratchCardManager>().SpriteCard.GetComponent<SpriteRenderer>().enabled=false;

        if(MoveAlongSpline.childNo>0)
        {
            MoveAlongSpline.childNo--;
        }
        print(MoveAlongSpline.childNo);
        GameController.ins.ScratchSketch();
    }
    void SplimeRestart2()
    {
        DistancePercntge = 0;
        Vector3 CurrentPosition;
        if (MinusShape >= 1)
        {
            CurrentPosition = GameController.ins.Spline[0].transform.GetChild(childNo).transform.GetComponent<SplineContainer>().EvaluatePosition(DistancePercntge);
            GameController.ins.Spline[0].transform.GetChild(childNo).transform.gameObject.SetActive(false);
        }
        else
        {
            CurrentPosition = GameController.ins.Spline[0].transform.GetChild(childNo).transform.GetComponent<SplineContainer>().EvaluatePosition(DistancePercntge);
        }
        transform.position = CurrentPosition;
        GameController.ins.ScratchCardObject.GetComponent<ScratchCardManager>().SpriteCard.GetComponent<SpriteRenderer>().enabled = false;
        //print("Call 3");
        if (MoveAlongSpline.childNo > 0)
        {
            MoveAlongSpline.childNo--;
        }
        //  MoveAlongSpline.childNo--;
        //print(MoveAlongSpline.childNo+"Cildno-");
        GameController.ins.ScratchSketch();
        GameController.ins.Circles[0].transform.GetChild(i).gameObject.SetActive(false);
        i--;
        GameController.ins.Circles[0].transform.GetChild(i).gameObject.SetActive(true);
        GameController.ins.DotedImgs[0].transform.GetChild(GameController.PartCountStart - 1).gameObject.SetActive(true);
        GameController.ins.DotedImgs[0].transform.GetChild(GameController.PartCountStart).gameObject.SetActive(false);

        MinusShape = 0;
        ResetGame = 0;
    }
    public void Restart()
    {

        SoundManager.instance.PlayEffect_Instance(8);
       
        if (IsComplete == false)
        {
            restrt = true;
            BtnPressed = true;
            GameController.ins.CircleColliderOff();
           // print(ResetGame + " = ResetGame Ahead");
            if (ResetGame==0)
            {
                ScratchCard.IncreaseShape--;
                GameController.PartCountStart--;
             //   print(ResetGame + "ResetGame 0");
                SplimeRestart1();
                
            }

             if (ResetGame == 1)
            {
                MinusShape += 2;
                ScratchCard.IncreaseShape = ScratchCard.IncreaseShape - MinusShape;
                GameController.PartCountStart = GameController.PartCountStart - MinusShape;
               // print(ResetGame + "ResetGame 1");

                SplimeRestart2();
                
            }
            if (GameController.PartCountStart > 1)
            {
               // print(ResetGame + "gamecont");
                ResetGame++;
                if(ResetGame==2)
                {
                    ResetGame = 0;
                }
            }

       
        }

        if (GameController.ins.OnColorRestrt == true)
        {
         //   print("hiiiiiiiiiiiiiii");
            GameController.ins.ColoringPen.GetComponent<DragDropSprite>().enabled = false;
            GameController.ins.ColoringPen.transform.DOLocalMove(new Vector3(4.48f, -0.89f, 0), 0.5f).SetEase(Ease.Linear);
            GameController.ins.ColorsA.SetActive(true);
            ScratchCard.IncreaseColor--;
            GameController.ins.ScratchCardObject.GetComponent<ScratchCardManager>().SpriteCard.GetComponent<SpriteRenderer>().enabled = false;
            GameController.ins.ScratchColor();
        }
    }

}
