
using UnityEngine;
using DG.Tweening;
using System.Collections;


public class MemoryTiles : MonoBehaviour
{
    public int tileId;

    public static MemoryTiles lastClick_Memorytile;

    public MemoryManager MM;

    private void OnMouseDown()
    {
        if(MemoryTiles.lastClick_Memorytile == null)
        {
            MM.AS.clip = MM.AC[0];
            MM.AS.Play();
            MemoryTiles.lastClick_Memorytile = this;
            FilpTileFaceUp();
        }
        else
        {
            FilpTileFaceUp(()=> { 
            
                if (lastClick_Memorytile != this)
                {
                    if (lastClick_Memorytile.tileId == tileId)
                    {
                        lastClick_Memorytile.GetComponent<Collider>().enabled = false;
                        GetComponent<Collider>().enabled = false;
                        lastClick_Memorytile = null;
                        MemoryManager.counterCheck++;                       
                        StartCoroutine("ResetAllMermorytileDithDelay", 1);
                    }
                    else
                    {
                        
                        FilpTileFaceDown();
                       lastClick_Memorytile.FilpTileFaceDown();
                       lastClick_Memorytile = null;
                        MM.AS.clip = MM.AC[1];
                        MM.AS.Play();
                    }
                }
            
            });
        }
    }

    public void FilpTileFaceUp(System.Action action=null)
    {
        transform.DOLocalRotate(new Vector3(-90, 180, 180f), 0.5f).OnComplete(()=> {

            action?.Invoke();
        });
    }

    
    public void FilpTileFaceDown()
    {
        
        transform.DOLocalRotate(new Vector3(-90, 180, 0), 0.5f).OnComplete(()=> {

            GetComponent<BoxCollider>().enabled = true;
            
        });

    }

    IEnumerator ResetAllMermorytileDithDelay()
    {
        yield return new WaitForSeconds(1f);
        MemoryManager.count++;
        if (MemoryManager.count>=9)
        {
            OneCycleDone();
        }
        else
        {
            MM.AS.clip = MM.AC[2];
            MM.AS.Play();
        }
        print("Matched");        
        MemoryManager.Instance.ResetTile();
        StopCoroutine(ResetAllMermorytileDithDelay());
    }
    void OneCycleDone()
    {
        MM.AS.clip = MM.AC[3];
        MM.AS.Play();
        print("Enable UI, Play Completion Sound");
        MemoryManager.count = 0;
        StartCoroutine("SceneDone");
    }
    IEnumerator SceneDone()
    {
        yield return new WaitForSeconds(0.5f);
        UIManager_Sallu.INSTANCE.OpenDialogeAnimation(MM.completionDB);
        MM.oldBg.SetActive(true);
        MM.myObj.SetActive(false);
        StopCoroutine(SceneDone());
      //  MM.completionDB.SetActive(true);
    }
}

