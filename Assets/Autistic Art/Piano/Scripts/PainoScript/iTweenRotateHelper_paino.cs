using UnityEngine;
using System.Collections;

public class iTweenRotateHelper_paino : MonoBehaviour
{
    public enum ToTransform
    {
        RotateTo,
        RotateFrom
    };

    public iTween.EaseType EaseType = iTween.EaseType.linear;
    public iTween.LoopType loopType = iTween.LoopType.none;
    public ToTransform toTransform = ToTransform.RotateTo;
    public Vector3 EndingRotation;
    public float time = 1;
    public float delay = 0;
    Vector3 intialpos;

    void Start()
    {
        intialpos = transform.rotation.eulerAngles;
        PlayTween();
    }

    void PlayTween()
    {
        if (ToTransform.RotateFrom == toTransform)
        {
            iTween.RotateFrom(gameObject, iTween.Hash("x", EndingRotation.x, "y", EndingRotation.y, "z", EndingRotation.z, "time", time, "easetype", EaseType, "delay", delay, "looptype", loopType));
        }
        else
        {
            iTween.RotateTo(gameObject, iTween.Hash("x", EndingRotation.x, "y", EndingRotation.y, "z", EndingRotation.z, "time", time, "easetype", EaseType, "delay", delay, "looptype", loopType));
        }
    }
    private void OnDisable()
    {
       
        transform.eulerAngles = intialpos;
    }
    public void ResetObj()
    {

        transform.eulerAngles = intialpos;
    }
}
