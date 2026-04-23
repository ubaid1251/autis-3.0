using Antistress;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapes : MonoBehaviour
{

    public SkinnedMeshRenderer[] Blends;
    int total;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        total = Blends.Length;
        print("Total");
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        HandleEditorInput();
#else
        HandleMobileInput();
#endif
    }
    void HandleEditorInput()
    {
        if (Input.GetMouseButton(0))
        {
            HandleInput(Input.mousePosition);
        }
    }
    void HandleMobileInput()
    {
        if(Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
            {
                HandleInput(touch.position);
            }
        }
    }

    Ray ray;
    RaycastHit hit;
    void HandleInput(Vector3 position)
    {
        ray = Camera.main.ScreenPointToRay(position);
        if(Physics.Raycast(ray,out hit))
        {
            if (hit.collider.CompareTag("Blend"))
            {
                hit.collider.enabled = false;
                count++;
                SoundManager.instance.PlayEffect_Instance(21);
                StartCoroutine(WaitBlend(hit.transform.GetComponent<SkinnedMeshRenderer>()));
                if (count == total)
                    StartCoroutine("ResetDefault");
            }
        }
    }

    IEnumerator WaitBlend(SkinnedMeshRenderer blend)
    {
        for(int i = 0; i<=100; i += 10)
        {
            blend.SetBlendShapeWeight(0, i);
            yield return null;
        }
    }

    IEnumerator ResetDefault()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < Blends.Length; i++)
        {
            Blends[i].SetBlendShapeWeight(0, 0);
            Blends[i].GetComponent<Collider>().enabled = true;
        }
        count = 0;
        StopCoroutine(ResetDefault());
    }
    
}
