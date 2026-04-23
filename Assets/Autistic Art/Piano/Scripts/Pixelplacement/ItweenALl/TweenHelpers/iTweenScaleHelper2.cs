using UnityEngine;
using System.Collections;


public class iTweenScaleHelper2 : MonoBehaviour
{
	public enum ToTransform2
	{
		ScaleTo,
		ScaleFrom
	};
	public iTween2.EaseType EaseType = iTween2.EaseType.linear;
	public iTween2.LoopType loopType = iTween2.LoopType.none;
	public ToTransform2 toTransform = ToTransform2.ScaleTo;
	public Vector3 EndPoint;
	public float time = 1;
	public float delay = 0;


	void Start()
	{
		PlayTween();
	}
		

	public void PlayTween()
	{
		if (ToTransform2.ScaleFrom == toTransform)
		{
			iTween2.ScaleFrom(gameObject, iTween2.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time, "easetype", EaseType, "delay", delay, "looptype", loopType));
		}
		else
		{
			iTween2.ScaleTo(gameObject, iTween2.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time, "easetype", EaseType, "delay", delay, "looptype", loopType));
		}
	}

	void OnDisable()
	{
		//gameObject.transform.localScale = new Vector3 (0, 0, 0);
	}
}
