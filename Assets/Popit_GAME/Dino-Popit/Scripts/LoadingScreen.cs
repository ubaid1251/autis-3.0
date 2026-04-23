using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public GameObject LoadingObject;
    public Image progressbar;
    Action OnCompleteCallback;
    Action<float> OnUpdateProgressbar;
    float fill;
    // Start is called before the first frame update
    void Start()
    {
        LoadingObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public LoadingScreen ShowLoadingScreen(float speed=0.5f)
    {
        LoadingObject.SetActive(true);
        StartCoroutine(FillProgressbar(speed));
        return this;
    }

    public LoadingScreen OnUpdate(Action<float> callback)
    {
        OnUpdateProgressbar = callback;
        return this;
    }

    public LoadingScreen OnComplete(Action callback)
    {
        OnCompleteCallback = callback;
        return this;
    }
    IEnumerator FillProgressbar(float speed)
    {

        fill = 0;
        progressbar.fillAmount = fill;
        while (fill < 1)
        {
            fill += Time.deltaTime * speed;
            progressbar.fillAmount = fill;
            OnUpdateProgressbar?.Invoke(fill);
            yield return null;
        }
        OnCompleteCallback?.Invoke();
        LoadingObject.SetActive(false);
        OnCompleteCallback = null;
        OnUpdateProgressbar = null;
    }
}
