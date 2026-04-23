using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnColorSelection : MonoBehaviour
{
    public Material[] shapMats;
    public Material[] popitMats;
    int matIndex;
    int prevMatIndex = -1;
    public void SetMaterial()
    {

    }

    public void SetRandomMaterial()
    {
       for(int i = 0; i< 10; i++)
        {
            matIndex = Random.Range(0, shapMats.Length);
            if(matIndex != prevMatIndex)
            {
                prevMatIndex = matIndex;
                break;
            }
        }
        GetComponent<TwoSidedBlendShapes>().SetMaterial(shapMats[matIndex]);

    }

   
}
