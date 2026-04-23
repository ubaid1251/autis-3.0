using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PeaPopManager : MonoBehaviour
{


    public Sprite[] PeaSpriteArr;
    public Sprite[] PeaPresSpriteArr;


    public PeaData[] peaData;
    public GameObject peaPrefabs;
    public int peaCount;
    public bool pea1, pea2;
    public GameObject home, next;
    private void OnEnable()
    {
        //GoogleAdMobController.THIS.ShowR1InterstitialAd(); //AdCallPosition
    }
    void Update()
    {
#if UNITY_EDITOR
        EditorInput();
#else
        MobileInput();
#endif
    }


    void EditorInput()
    {
        if (Input.GetMouseButton(0))
        {
            CheckHit(Input.mousePosition);
        }
    }
    void MobileInput()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                CheckHit(touch.position);
            }
        }
    }

    private int count;

    void CheckHit(Vector3 position)
    {
        position = Camera.main.ScreenToWorldPoint(new Vector3(position.x , position.y , position.z));
        RaycastHit2D hit = Physics2D.Raycast(position , Vector2.zero);

        if (hit.collider != null && hit.collider.tag == "Pea")
        {

            if (hit.transform.GetComponent<Pea>())
            {
                Pea pea = hit.transform.GetComponent<Pea>();

                if (!pea.PeaPresFound)
                {
                    hit.transform.GetComponent<Collider2D>().enabled = false;
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = PeaSpriteArr[hit.transform.GetSiblingIndex()];
                    SoundManagerpeas.Instance.PlayOneShot("pop");
                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.Selection);
                    pea.PeaPresFound = true;

                    GameObject obj = Instantiate(peaPrefabs);
                    obj.transform.position = hit.transform.position;
                    Destroy(obj , 1f);
                    if (CheckAllPeaOut(pea))
                    {
                        StartCoroutine(DoMoveOutPeaOnComplete(peaData[pea.peaIndex]));
                    }
                }
            }

        }
    }


    bool CheckAllPeaOut(Pea pea_Par)
    {
        if (pea_Par.transform.parent.childCount > 0)
        {
            int counter = 0;
            for (int i = 0; i < pea_Par.transform.parent.childCount - 1; i++)
            {
                if (pea_Par.transform.parent.transform.GetChild(i).GetComponent<Pea>().PeaPresFound)
                {
                    counter++;
                    print(counter);
                }
            }

            if (counter >= pea_Par.transform.parent.childCount - 1)
            {
                return true;
            }
        }

        return false;

    }

    IEnumerator DoMoveOutPeaOnComplete(PeaData peaData)
    {
        yield return new WaitForSeconds(0.1f);
        peaData.PeaObj.transform.DOMove(peaData.nextOutPos , 0.5f).OnComplete(() =>
        {
            peaData.PeaObj.transform.position = peaData.comeStartPos;
            MoveInStarPlayPos(peaData);

        });
        if (peaData.peaNum == 1)
        {
            pea1 = true;
        }
        else
        {
            pea2 = true;
        }
        if (pea1 && pea2)
        {
            home.SetActive(true);
            //  next.SetActive(true);
        }
    }

    void MoveInStarPlayPos(PeaData peaData)
    {
        for (int i = 0; i < PeaPresSpriteArr.Length; i++)
        {
            peaData.PeaObj.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = PeaPresSpriteArr[i];
            peaData.PeaObj.transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
            peaData.PeaObj.transform.GetChild(i).GetComponent<Pea>().PeaPresFound = false;
        }
        count = 0;

        peaData.PeaObj.transform.DOMove(peaData.midPlayPosition , 0.5f);
    }


    [System.Serializable]
    public class PeaData
    {
        public GameObject PeaObj;
        public int peaNum;
        public Vector3 nextOutPos;
        public Vector3 comeStartPos;
        public Vector3 midPlayPosition;


    }



}

