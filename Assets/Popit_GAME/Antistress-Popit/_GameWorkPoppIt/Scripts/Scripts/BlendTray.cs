using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTray : MonoBehaviour
{
    public Transform Rotatable;
    public GameObject Front, Back;
    int count;
    int total;

    private void Start()
    {
        total = Front.GetComponentsInChildren<SkinnedMeshRenderer>().Length;
        for(int i = 0; i< total; i++)
        {

        }
    }
    
}
