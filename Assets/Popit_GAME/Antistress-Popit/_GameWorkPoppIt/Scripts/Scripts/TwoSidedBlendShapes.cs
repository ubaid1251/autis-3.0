using DG.Tweening;
using MoreMountains.NiceVibrations;
using System.Collections;
using UnityEngine;

public class TwoSidedBlendShapes : MonoBehaviour
{

    public Transform Rotatable;
    public Renderer Main;
    public GameObject FrontParent, BackParent;
    private SkinnedMeshRenderer[] Front, Back;
    int count = 0;
    int total;
    bool isFront = true;
    public bool isRotatable = true;
    public bool isRainbow;
    // Start is called before the first frame update
    void Start()
    {
        Front = FrontParent.GetComponentsInChildren<SkinnedMeshRenderer>();
        if (BackParent)
            Back = BackParent.GetComponentsInChildren<SkinnedMeshRenderer>();
        total = Front.Length;

        for (int i = 0; i < total; i++)
        {
            if (isRotatable)
            {
                Back[i].transform.GetComponent<Collider>().enabled = false;
                Back[i].SetBlendShapeWeight(0 , 0);
            }
            Front[i].transform.GetComponent<Collider>().enabled = true;
            Front[i].SetBlendShapeWeight(0 , 0);
        }
        if (Rotatable)
            Rotatable.transform.rotation = Quaternion.identity;
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
                    Invoke("Rotate" , 0.5f);
            }
        }
    }

    IEnumerator WaitBlend(SkinnedMeshRenderer blend)
    {
        for (int i = 0; i <= 100; i += 10)
        {
            blend.SetBlendShapeWeight(0 , i);
            yield return null;
        }
    }
    void Rotate()
    {
        if (isRotatable)
        {
            if (isFront)
            {
                for (int i = 0; i < total; i++)
                {
                    Back[i].GetComponent<Collider>().enabled = true;
                }

                Rotatable.DORotateQuaternion(Quaternion.Euler(new Vector3(0 , 180 , 0)) , 1).OnComplete(() =>
                {
                    for (int i = 0; i < total; i++)
                    {
                        Front[i].SetBlendShapeWeight(0 , 0);
                    }
                });
            }
            else
            {
                for (int i = 0; i < total; i++)
                {
                    Front[i].GetComponent<Collider>().enabled = true;
                }

                Rotatable.DORotateQuaternion(Quaternion.Euler(Vector3.zero) , 1).OnComplete(() =>
                {
                    for (int i = 0; i < total; i++)
                    {
                        Back[i].SetBlendShapeWeight(0 , 0);
                    }
                });
            }

            isFront = !isFront;
        }
        else
        {
            for (int i = 0; i < Front.Length; i++)
            {
                Front[i].SetBlendShapeWeight(0 , 0);
                Front[i].GetComponent<Collider>().enabled = true;
            }
        }
        count = 0;
    }

    public void SetMaterial(Material mat)
    {
        if (!isRainbow)
        {
            print("Mats");
            if (total == 0)
                Start();
            Main.material = mat;
            for (int i = 0; i < total; i++)
            {
                Front[i].material = mat;
                if (isRotatable)
                    Back[i].material = mat;
            }
        }
    }

    public void SetPopitMaterial(Material shapMat , Material popitMat)
    {
        if (total == 0)
            Start();
        Material[] mats = Main.materials;
        mats[0] = shapMat;
        Main.sharedMaterials = mats;
        for (int i = 0; i < total; i++)
        {
            Front[i].material = popitMat;
            if (isRotatable)
                Back[i].material = popitMat;
        }
    }

    internal void Reset()
    {
        for (int i = 0; i < Front.Length; i++)
        {
            Front[i].SetBlendShapeWeight(0 , 0);
            Front[i].GetComponent<Collider>().enabled = true;
        }

        if (isRotatable && BackParent)
        {
            for (int j = 0; j < total; j++)
            {
                Back[j].SetBlendShapeWeight(0 , 0);
                Back[j].GetComponent<Collider>().enabled = false;
            }
            Rotatable.rotation = Quaternion.identity;
        }


    }
}
