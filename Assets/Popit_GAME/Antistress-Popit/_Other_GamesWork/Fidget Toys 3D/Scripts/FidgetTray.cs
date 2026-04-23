using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FidgetTray : MonoBehaviour
{
    public GameObject MainTray,Front,Back,sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator Rotatetray()
    {
        yield return new WaitForSeconds(.5f);
        if (traypos == true)
        {
            Back.SetActive(true);
            Front.SetActive(false);
            //traypos = true;
            for (int i = 0; i < Back.transform.childCount; i++)
            {
                Back.transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = true;
                Back.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);
            }
        }
        else
        {
            Back.SetActive(false);
            Front.SetActive(true);
            //traypos = false;
            for (int i = 0; i < Front.transform.childCount; i++)
            {
                Front.transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = true;
                Front.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);
            }
        }

    }



    int count;
    public bool traypos,PressDwon;
    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            PressDwon = true;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (traypos == false)
                {


                    if (hit.collider.tag == "buble")
                    {
                        if (count < 23)
                        {
                            sound.GetComponent<AudioSource>().Play();
                            hit.collider.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);
                            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                            count++;

                        }
                        else
                        {
                            count = 0;
                            traypos = true;
                            MainTray.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 180, 0), 2f);
                            StartCoroutine("Rotatetray");

                            //Back.SetActive(true);
                            //Front.SetActive(false);
                            //for (int i=0;i<Back.transform.childCount;i++)
                            //{
                            //    Back.transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = true;
                            //    Back.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0,0);
                            //}
                        }
                    }
                }
                else
                {
                    if (hit.collider.tag == "buble")
                    {
                        if (count < 23)
                        {
                            count++;
                            sound.GetComponent<AudioSource>().Play();
                            hit.collider.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);
                            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;

                        }
                        else
                        {
                            count = 0;
                            traypos = false;
                            MainTray.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 2f);
                            StartCoroutine("Rotatetray");

                            //Back.SetActive(false);
                            //Front.SetActive(true);
                            //for (int i = 0; i < Front.transform.childCount; i++)
                            //{
                            //    Front.transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = true;
                            //    Front.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);
                            //}
                        }
                    }
                }

            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            PressDwon = false;
        }

        if(PressDwon)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (traypos == false)
                {


                    if (hit.collider.tag == "buble")
                    {
                        if (count < 23)
                        {
                            count++;
                            sound.GetComponent<AudioSource>().Play();
                            hit.collider.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);
                            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;

                        }
                        else
                        {
                            count = 0;
                            traypos = true;
                            PressDwon = false;
                            MainTray.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 180, 0), 2f);
                            hit.collider.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);
                            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                            StartCoroutine("Rotatetray");

                        }
                    }
                }
                else
                {
                    if (hit.collider.tag == "buble")
                    {
                        if (count < 23)
                        {
                            count++;
                            sound.GetComponent<AudioSource>().Play();
                            hit.collider.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);
                            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;

                        }
                        else
                        {
                            count = 0;
                            traypos = false;
                            PressDwon = false;
                            hit.collider.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);
                            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                            MainTray.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 2f);
                            StartCoroutine("Rotatetray");
                        }
                    }
                }

            }
        }






    }
}
