using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using MoreMountains.NiceVibrations;
using UnityEngine.SceneManagement;

public class BubleWrap : MonoBehaviour
{
    public GameObject BubleActivit, BubleSound;
    
    // Start is called before the first frame update
    void Start()
    {
        //Activities = 1;
        //Advertisements.Instance.HideBanner();



    }








    int TotalBubles;
    bool bubledown;
    
    // Update is called once per frame
    void Update()
    {
        
            if(Input.GetMouseButtonDown(0))
            {
                bubledown = true;
            }
            if(Input.GetMouseButtonUp(0))
            {
                bubledown = false;
            }
            if (bubledown == true)
            {
                Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(inputRay, out hit))
                {
                    if (hit.collider.tag == "obj")
                    {
                        //MMVibrationManager.Haptic(HapticTypes.Selection);
                       // MMVibrationManager.Haptic(HapticTypes.);
                        BubleSound.GetComponent<AudioSource>().Play();
                        hit.collider.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0,100);
                        hit.collider.gameObject.GetComponent<BoxCollider>().enabled =false ;
                        TotalBubles++;
                        //if(!BubleSound.GetComponent<AudioSource>().isPlaying)
                        //{
                        //}
                        if(TotalBubles==44)
                        {
                            for(int i=0;i<BubleActivit.transform.childCount;i++)
                            {
                                BubleActivit.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0,0);
                                BubleActivit.transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled =true;
                                TotalBubles = 0;
                            }

                       
                        }
                    
                }
                }
            }
        

        

    }
}
