using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine;
public class makeLine : MonoBehaviour
{
    public static makeLine ins;

    LineRenderer line;
    Vector3 PreviosPos;
    public GameObject obj;

    [SerializeField]
    float MinDistance = 0.1f;

    [SerializeField,Range(0,1)]
    float width;
    public static bool IsPlay;
    void Start()
    {
        ins = this;
        IsPlay = true;
        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        PreviosPos = transform.position;
        line.startWidth = line.endWidth = width;
    }

    // Update is called once per frame
    void Update()
    {
        if( (IsPlay == true))
        {
            // Vector3 CurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 CurrentPos = obj.transform.position;
            CurrentPos.z = 0f;

            if (Vector3.Distance(CurrentPos, PreviosPos) > MinDistance)
            {
                if (PreviosPos == transform.position)
                {
                    line.SetPosition(0, CurrentPos);
                }
                else
                {
                    line.positionCount++;
                    line.SetPosition(line.positionCount-1,CurrentPos);
                }

                PreviosPos = CurrentPos;
            }

        }

    }
}
