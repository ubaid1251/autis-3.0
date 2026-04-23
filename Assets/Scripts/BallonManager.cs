using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class BallonManager : MonoBehaviour
{
    public static BallonManager Instance;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;

    private int currentScore = 0;
    private int bestScore = 0;
    public GameObject balloonPrefab;
    public Transform spawnParent;

    public float spawnInterval = 0.2f;
    public Vector2 spawnRangeX = new Vector2(-300f, 300f);
    public Vector2 spawnRangeY = new Vector2(-500f, 500f);
    public Sprite[] BallonSprites;
    public GameObject nextbtn,GameOverpopUp;
    public ParticleSystem BallonPop;
    private void Awake()
    {
            Instance = this;
            bestScore = PlayerPrefs.GetInt("BestScore", 0);
            UpdateScoreTexts();
            SoundManager.instance.PlayEffect_Instance(13);
    }
    private void Start()
    {
        InvokeRepeating(nameof(SpawnBalloon), 0f, spawnInterval);
        Invoke(nameof(NxtBtn), 5f);
        Invoke(nameof(StopBallon), 13f);
    }

    void SpawnBalloon()
    {
        int RandomBalon = Random.Range(0, BallonSprites.Length);
        GameObject newBalloon = Instantiate(balloonPrefab, spawnParent);
        RectTransform rt = newBalloon.GetComponent<RectTransform>();
        newBalloon.transform.GetComponent<Image>().sprite = BallonSprites[RandomBalon];
        rt.anchoredPosition = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y));  
    }
    void NxtBtn()
    {
        nextbtn.SetActive(true);
    }
    void StopBallon()
    {
        CancelInvoke("SpawnBalloon");
        Invoke(nameof(GameOverOn), 2f);
    }
    void GameOverOn()
    {
        nextbtn.GetComponent<DOTweenAnimation>().DOPlay();
        Invoke(nameof(NxtBtnClick),1);
    }

    public void PopBalloon(GameObject balloon)
    {
        BallonPop.transform.position = balloon.transform.position;
        BallonPop.gameObject.SetActive(true);
        BallonPop.Play();
        Destroy(balloon);
        currentScore++;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        UpdateScoreTexts();
    }

    private void UpdateScoreTexts()
    {
        currentScoreText.text = "Score: " + currentScore;
        bestScoreText.text = "Best Score: " + bestScore;
    }
    public void NxtBtnClick()
    {
        DOTween.KillAll(false);
        SoundManager.instance.StopAllSounds();
        SoundManager.instance.PlayEffect_Instance(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}