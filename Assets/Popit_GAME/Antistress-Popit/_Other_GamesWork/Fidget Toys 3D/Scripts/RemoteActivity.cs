using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RemoteActivity : MonoBehaviour
{
    public GameObject onoff, btsound, RuberPhusSound, onoffsound;
    // Start is called before the first frame update
    void Start()
    {

    }
    bool onoffcheck;
    void OnOffButton()
    {
        if (onoffcheck)
        {
            onoffsound.GetComponent<AudioSource>().Play();

            onoff.transform.localRotation = Quaternion.Euler(10,0, 0f);
            onoffcheck = false;
        }
        else
        {
            onoffsound.GetComponent<AudioSource>().Play();

            onoff.transform.localRotation = Quaternion.Euler(-10, 0, 0f);
            onoffcheck = true;
        }
    }
    bool buble;
    public GameObject bubleobj,pong;
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

                    hit.collider.gameObject.transform.DOLocalMove(new Vector3(hit.collider.transform.localPosition.x, 0.87f, hit.collider.transform.localPosition.z), .1f).SetEase(Ease.Linear);

                    hit.collider.transform.DOLocalMove(new Vector3(hit.collider.transform.localPosition.x, 1.141389f, hit.collider.transform.localPosition.z), .1f).SetEase(Ease.Linear).SetDelay(.1f);
                    btsound.GetComponent<AudioSource>().Play();
                }

                if (hit.collider.tag == "OnOff")
                {
                    OnOffButton();
                }

                if (hit.collider.tag == "buble")
                {
                    //bubleobj = hit.collider.gameObject;
                    bubleobj.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);
                    RuberPhusSound.GetComponent<AudioSource>().Play();
                    buble = true;
                }
                if (hit.collider.tag == "pong")
                {
                    //pong = hit.collider.gameObject;

                    pong.transform.DOScale(new Vector3(1.1f,1.1f,1.1f),.1f);
                    //RuberPhusSound.GetComponent<AudioSource>().Play();
                    buble = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (buble)
            {
                buble = false;
                bubleobj.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);
                pong.transform.DOScale(new Vector3(1f, 1f, 1f), .1f);

            }
        }

    }
}
