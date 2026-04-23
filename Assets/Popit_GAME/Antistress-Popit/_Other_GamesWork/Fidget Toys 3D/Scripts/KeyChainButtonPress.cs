using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KeyChainButtonPress : MonoBehaviour
{
    public GameObject btsound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

                    hit.collider.transform.DOLocalMove(new Vector3( hit.collider.transform.localPosition.x, 0.11f, hit.collider.transform.localPosition.z), .1f).SetEase(Ease.Linear);

                    hit.collider.transform.DOLocalMove(new Vector3(hit.collider.transform.localPosition.x, 0.63f, hit.collider.transform.localPosition.z), .1f).SetEase(Ease.Linear).SetDelay(.1f);
                    btsound.GetComponent<AudioSource>().Play();
                }
            }
        }

    }
}
