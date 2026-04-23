
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class NewFidgetNut : MonoBehaviour
{
    public GameObject MainBox, bubleobj,onoff,uperbt,btsound,onoffsound,upersound,RuberPhusSound;
    // Start is called before the first frame update
    void Start()
    {
        

    }

   

    void UperButtonFun()
    {
        // uperbt.GetComponent<DOTweenAnimation>().DOPlay();
            upersound.GetComponent<AudioSource>().Play();

            uperbt.GetComponent<DOTweenAnimation>().DORestart();
            //yield return new WaitForSeconds(.1f);
       
    }



    bool onoffcheck;
    void OnOffButton()
    {
        if(onoffcheck)
        {
            onoffsound.GetComponent<AudioSource>().Play();

            onoff.transform.localRotation = Quaternion.Euler(0, 20, 0f);
            onoffcheck = false;
        }
        else
        {
            onoffsound.GetComponent<AudioSource>().Play();

            onoff.transform.localRotation = Quaternion.Euler(0, -20, 0f);
            onoffcheck = true;
        }
    }


    bool buble;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {

                if (hit.collider.tag == "obj")
                {

                    hit.collider.gameObject.transform.DOLocalMove(new Vector3(hit.collider.transform.localPosition.x, hit.collider.transform.localPosition.y, -6.5f), .1f).SetEase(Ease.Linear);

                    hit.collider.transform.DOLocalMove(new Vector3(hit.collider.transform.localPosition.x, hit.collider.transform.localPosition.y, -6.916285f), .1f).SetEase(Ease.Linear).SetDelay(.1f);
                    btsound.GetComponent<AudioSource>().Play();
                }

                if(hit.collider.tag=="OnOff")
                {
                    OnOffButton();
                }
                if (hit.collider.tag == "pong")
                {
                    UperButtonFun();
                }
                if(hit.collider.tag == "buble")
                {
                    hit.collider.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);
                    RuberPhusSound.GetComponent<AudioSource>().Play();
                    buble = true;
                }

            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            if(buble)
            {
                buble = false;
                bubleobj.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0,0);
            }
        }
    }

}
