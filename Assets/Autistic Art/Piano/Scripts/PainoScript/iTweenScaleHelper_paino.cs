using UnityEngine;
using System.Collections;


public class iTweenScaleHelper_paino : MonoBehaviour
{
	public enum ToTransform
	{
		ScaleTo,
		ScaleFrom
	};
	public iTween.EaseType EaseType = iTween.EaseType.linear;
	public iTween.LoopType loopType = iTween.LoopType.none;
	public ToTransform toTransform = ToTransform.ScaleTo;
	public Vector3 EndPoint;
	public float time = 1;
	public float delay = 0;


	void OnEnable()
	{
		PlayTween();
	}
		

	public void PlayTween()
	{
		if (ToTransform.ScaleFrom == toTransform)
		{
			iTween.ScaleFrom(gameObject, iTween.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time, "easetype", EaseType, "delay", delay, "looptype", loopType));
		}
		else
		{
			iTween.ScaleTo(gameObject, iTween.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time, "easetype", EaseType, "delay", delay, "looptype", loopType));
		}
	}

	void OnDisable()
	{
		gameObject.transform.localScale = new Vector3 (0, 0, 1);
	}
}
