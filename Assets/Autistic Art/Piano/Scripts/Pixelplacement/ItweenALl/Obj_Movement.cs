using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Movement : MonoBehaviour {
	public float x,y,z,time,delay;
	public float waitTime;
	public Vector3 f_scale;
	//	public string loopVal,easetype;
	public bool islocal;
	public enum looptype {loop,pingPong,none};
	public enum TransForm {move,rotateTo,rotateBy,scale};
	public enum easetype {easeInBack,easeInOutBack,linear};
	public looptype LoopType;
	public easetype EaseType;
	public TransForm transforms;
	void OnEnable()
	{

		StartCoroutine ("startWork");
	}
	// Use this for initialization
	IEnumerator startWork ()
	{
		
		switch (transforms) 
		{
		case TransForm.move:
			yield return new WaitForSeconds (waitTime);
			iTween2.MoveTo (gameObject, iTween2.Hash ("x", x, "y", y, "time", time,"delay",delay, "islocal", islocal, "easetype", ""+EaseType, "looptype",""+LoopType));
			break;
		case TransForm.rotateTo:
			yield return new WaitForSeconds (waitTime);
			iTween2.RotateTo (gameObject, iTween2.Hash ("z", z, "time", time,"delay",delay, "easetype", "" + EaseType, "looptype", "" + LoopType));
			break;
		case TransForm.rotateBy:
			yield return new WaitForSeconds (waitTime);
			iTween2.RotateBy (gameObject, iTween2.Hash ("z", z, "speed", time, "delay", delay, "islocal", islocal, "easetype", ""+EaseType, "looptype", ""+LoopType));
			break;
		case TransForm.scale:
			yield return new WaitForSeconds (waitTime);
			iTween2.ScaleTo (gameObject, iTween2.Hash ("scale", f_scale, "time", time, "delay", delay, "islocal", islocal, "easetype", "" + EaseType, "looptype", "" + LoopType));
			break;

		}

	}

	// Update is called once per frame
	void Update () 
	{

	}
}
