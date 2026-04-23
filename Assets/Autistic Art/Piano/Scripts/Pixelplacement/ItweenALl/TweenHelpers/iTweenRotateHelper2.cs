using UnityEngine;
using System.Collections;

public class iTweenRotateHelper2 : MonoBehaviour
{
    public enum ToTransform2
    {
        RotateTo,
        RotateFrom
    };

    public iTween2.EaseType EaseType = iTween2.EaseType.linear;
    public iTween2.LoopType loopType = iTween2.LoopType.none;
    public ToTransform2 toTransform = ToTransform2.RotateTo;
    public Vector3 EndingRotation;
    public float time = 1;
    public float delay = 0;


    void Start()
    {
        PlayTween();
    }

    void PlayTween()
    {
        if (ToTransform2.RotateFrom == toTransform)
        {
            iTween2.RotateFrom(gameObject, iTween2.Hash("x", EndingRotation.x, "y", EndingRotation.y, "z", EndingRotation.z, "time", time, "easetype", EaseType, "delay", delay, "looptype", loopType));
        }
        else
        {
            iTween2.RotateTo(gameObject, iTween2.Hash("x", EndingRotation.x, "y", EndingRotation.y, "z", EndingRotation.z, "time", time, "easetype", EaseType, "delay", delay, "looptype", loopType));
        }
    }
}
