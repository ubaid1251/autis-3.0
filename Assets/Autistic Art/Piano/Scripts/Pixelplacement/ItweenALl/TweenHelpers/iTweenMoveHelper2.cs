using UnityEngine;
using System.Collections;

    
public class iTweenMoveHelper2 : MonoBehaviour
{
	public enum ToTransform2
	{
		MoveTo,
		MoveFrom
	};
    public iTween2.EaseType EaseType = iTween2.EaseType.linear;
    public iTween2.LoopType loopType = iTween2.LoopType.none;
    public ToTransform2 toTransform = ToTransform2.MoveTo;
    public Vector3 EndPoint;
    public float time = 1;
    public float delay = 0;
	public static bool playTweenForOnce = true;
	public bool playOnce = false;
	public bool islocal = false;

    void Start()
    {
		if (!playOnce) 
		{
			PlayTween ();
		}
    }

	void Update()
	{
		if (playTweenForOnce) 
		{
			PlayTweenForOnce ();
			playTweenForOnce = false;
		}
	}

	public IEnumerator PlayTweenForOnce()
	{
		if (ToTransform2.MoveFrom == toTransform)
		{
			iTween2.MoveFrom(gameObject, iTween2.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time,"islocal",islocal, "easetype", EaseType, "delay", delay));
			yield return new WaitForSeconds (time);
			iTween2.MoveFrom(gameObject, iTween2.Hash("x", gameObject.transform.position.x, "y", gameObject.transform.position.y,"islocal",islocal, "time", time, "easetype", EaseType, "delay", delay));
		}
		else
		{
			iTween2.MoveTo(gameObject, iTween2.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time,"islocal",islocal, "easetype", EaseType, "delay", delay));
			yield return new WaitForSeconds (time);
			iTween2.MoveTo(gameObject, iTween2.Hash("x", gameObject.transform.position.x, "y", gameObject.transform.position.y,"islocal",islocal, "time", time, "easetype", EaseType, "delay", delay));
		}
	}

    public void PlayTween()
    {
        if (ToTransform2.MoveFrom == toTransform)
        {
			iTween2.MoveFrom(gameObject, iTween2.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time,"islocal",islocal, "easetype", EaseType, "delay", delay, "looptype", loopType));
        }
        else
        {
			iTween2.MoveTo(gameObject, iTween2.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time,"islocal",islocal, "easetype", EaseType, "delay", delay, "looptype", loopType));
        }
    }
}
