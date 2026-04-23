using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class iTweenMoveHelper : MonoBehaviour
{
	private Vector3 intPos;
	
	public enum ToTransform
	{
		MoveTo,
		MoveFrom
	};
    public iTween.EaseType EaseType = iTween.EaseType.linear;
    public iTween.LoopType loopType = iTween.LoopType.none;
    public ToTransform toTransform = ToTransform.MoveTo;
    public Vector3 EndPoint;
    public float time = 1;
    public float delay = 0;
	public static bool playTweenForOnce = true;
	public bool playOnce = false;
	public bool islocal = false;

	private void OnEnable()
	{
		intPos = transform.position;
		
		if (!playOnce) 
		{
			PlayTween ();
		}
	}

	public bool ResetPos;
	private void OnDisable()
	{
		if (ResetPos)
		{
			transform.position = intPos;
		}
	}

	/*void Start()
    {
		if (!playOnce) 
		{
			PlayTween ();
		}
    }*/

    void Update()
    {
	    if (playTweenForOnce)
	    {
		    PlayTweenForOnce();
		    playTweenForOnce = false;
	    }
    }

    public IEnumerator PlayTweenForOnce()
	{
		if (ToTransform.MoveFrom == toTransform)
		{
			iTween.MoveFrom(gameObject, iTween.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time,"islocal",islocal, "easetype", EaseType, "delay", delay));
			yield return new WaitForSeconds (time);
			iTween.MoveFrom(gameObject, iTween.Hash("x", gameObject.transform.position.x, "y", gameObject.transform.position.y,"islocal",islocal, "time", time, "easetype", EaseType, "delay", delay));
		}
		else
		{
			iTween.MoveTo(gameObject, iTween.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time,"islocal",islocal, "easetype", EaseType, "delay", delay));
			yield return new WaitForSeconds (time);
			iTween.MoveTo(gameObject, iTween.Hash("x", gameObject.transform.position.x, "y", gameObject.transform.position.y,"islocal",islocal, "time", time, "easetype", EaseType, "delay", delay));
		}
	}

    public void PlayTween()
    {
        if (ToTransform.MoveFrom == toTransform)
        {
			iTween.MoveFrom(gameObject, iTween.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time,"islocal",islocal, "easetype", EaseType, "delay", delay, "looptype", loopType));
        }
        else
        {
			iTween.MoveTo(gameObject, iTween.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time,"islocal",islocal, "easetype", EaseType, "delay", delay, "looptype", loopType));
        }
    }
}
