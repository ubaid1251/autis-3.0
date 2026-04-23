using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MobilePopit : MonoBehaviour
{
    public Renderer Main;
    public GameObject FrontParent;
    private SkinnedMeshRenderer[] Front;
    int count = 0;
    int total;
    List<SkinnedMeshRenderer> popits;
    // Start is called before the first frame update
    void Start()
    {
        Front = FrontParent.GetComponentsInChildren<SkinnedMeshRenderer>();

        total = Front.Length;
        popits = Front.ToList();
    }

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
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
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
        if (Physics.Raycast(ray , out hit))
        {
            if (hit.collider.CompareTag("obj"))
            {
                hit.collider.enabled = false;
                count++;
                SoundManager.instance.PlayEffect_Instance(21);

                //MMVibrationManager.TransientHaptic(0.3f, 0.3f);
                MobileCaseManger.Instance.AddCoin(hit.point);
                StartCoroutine(WaitBlend(hit.transform.GetComponent<SkinnedMeshRenderer>()));
                if (count == total)
                    StartCoroutine(ResetBlends());
            }
        }
    }

    IEnumerator WaitBlend(SkinnedMeshRenderer blend)
    {
        for (int i = 0; i <= 100; i += 20)
        {
            blend.SetBlendShapeWeight(0 , i);
            yield return null;
        }
    }

    private IEnumerator ResetBlends()
    {

        var rand = new System.Random();
        var shuffled = popits.OrderBy(p => rand.Next());
        int i = 0;
        yield return new WaitForSeconds(0.5f);
        foreach (SkinnedMeshRenderer blend in shuffled)
        {
            if (i % 5 == 0)
            {
                SoundManager.instance.PlayEffect_Instance(21);
                yield return null;
            }
            StartCoroutine(WaitReset(blend));
        }

        count = 0;
    }

    private IEnumerator WaitReset(SkinnedMeshRenderer blend)
    {
        for (int i = 100; i >= 0; i -= 10)
        {
            blend.SetBlendShapeWeight(0 , i);
            yield return null;
        }
        blend.GetComponent<Collider>().enabled = true;
    }
}
