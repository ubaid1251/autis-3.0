using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using Dino;
public class Dice : MonoBehaviour
{
    Action<int> OnTrhowCallback;
    Collider dice;
    public Vector3[] Faces;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        dice = GetComponent<Collider>();
        anim = GetComponent<Animator>();
        dice.enabled = false;
        //AdmobCalling._instance.LoadInterstitial(); //AdCallingPosition
        StartCoroutine(AdCalling());

    }
    IEnumerator AdCalling()
    {
        yield return new WaitForSeconds(150f);
        // AdmobCalling._instance.ShowInterstitial(); //AdCallingPosition
        // AdmobCalling._instance.LoadInterstitial(); //AdCallingPosition
        StartCoroutine(AdCalling());
    }
    // Update is called once per frame
    void Update()
    {
        /*if(dice.enabled && Input.GetKeyDown(KeyCode.Space))
        {
            *//*dice.enabled = false;
            int Rand = UnityEngine.Random.Range(1, 7);
            OnTrhowCallback?.Invoke(Rand);
            print(Rand);*//*
        }*/
    }
    private void OnDisable()
    {
        StopCoroutine(AdCalling());
    }
    public void Throw(Action<int> callback , bool robot = false)
    {

        OnTrhowCallback = callback;
        if (!robot)
            dice.enabled = true;
        else
        {
            Invoke("RobotThrow" , 1f);
        }
    }

    void RobotThrow()
    {
        /*int Rand = UnityEngine.Random.Range(1, 7);
        OnTrhowCallback?.Invoke(Rand);
        print("Robot: " + Rand);*/
        DiceSpace.SoundManager.Instance.PlayOneShot("roll");

        if (Dino.SoundManager.isHaptic)
        {
            DiceSpace.SoundManager.Instance.HapticSelection();
            Debug.Log("in dice game vibrations are " + Dino.SoundManager.isHaptic);
        }

        anim.enabled = true;
        anim.ForceStateNormalizedTime(0);
        anim.Play("roll");
        dice.enabled = false;
    }

    public void Rolled()
    {
        anim.enabled = false;
        //yield return null;
        int Rand = UnityEngine.Random.Range(1 , 7);

        transform.GetChild(0).DOLocalRotateQuaternion(Quaternion.Euler(Faces[Rand - 1]) , 0.2f).OnComplete(() =>
        {
            OnTrhowCallback?.Invoke(Rand);
        });
        //transform.GetChild(0).localRotation = Quaternion.Euler(Faces[Rand - 1]);

        //StartCoroutine(ShowCallback());

    }
    public IEnumerator ShowCallback()
    {
        anim.enabled = false;
        yield return null;
        int Rand = UnityEngine.Random.Range(1 , 7);
        OnTrhowCallback?.Invoke(Rand);
        transform.GetChild(0).localRotation = Quaternion.Euler(Faces[Rand - 1]);
        print(Rand);
    }
    private void OnMouseDown()
    {
        dice.enabled = false;
        RobotThrow();
    }
}
