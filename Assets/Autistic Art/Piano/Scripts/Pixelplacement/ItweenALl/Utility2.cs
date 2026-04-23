using UnityEngine;
using System.Collections;

public class Utility2 : MonoBehaviour
{

    private static bool status;
    private static TouchPhase touchPhase;

    private static Vector2 previousPositionVector2;
    private static long previousTimeInMilliSeconds;

    private static Vector2 vector2D_getPositonVector2;

    private static Vector2 vector2D_getDeltaPositonVector2;

    // --------------------------------------------------------------------
    // METHOD FOR GENERIC INPUT TO RUN CODE ON UNITY EDITOR AND UNDITY ANDROID
    // --------------------------------------------------------------------


    public static Vector3 touchPosition()
    {
        Vector3 touchPosition3;
#if UNITY_EDITOR
        touchPosition3 = Input.mousePosition;

#elif UNITY_ANDROID || UNITY_IOS
		touchPosition3 = Input.GetTouch(0).position;

#endif

        return touchPosition3;

    }


    public static bool isClicked_2D(Collider2D collider2D)
    {
        status = false;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchPosition3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPosition2 = new Vector2(touchPosition3.x, touchPosition3.y);
            if (collider2D == Physics2D.OverlapPoint(touchPosition2))
                status = true;
            else
                status = false;

        }
#elif UNITY_ANDROID || UNITY_IOS
   if (Input.touchCount > 0){
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
		{

		Vector3 touchPosition3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 touchPosition2 = new Vector2 (touchPosition3.x,touchPosition3.y);
		if (collider2D == Physics2D.OverlapPoint(touchPosition2))
		status = true;
		else 
		status = false;
        }
		}
#endif

        return status;
    }

    public static bool isTouched()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0) || Input.GetMouseButtonDown(0))
        {
            return true;
        }
        else
            return false;

#elif UNITY_ANDROID || UNITY_IOS
 if (Input.touchCount > 0){
		if (Input.touchCount == 1)
		{
		return true;
		}
        }
		else 
		return false;
#endif
        return false;

    }

    public static Vector2 getPositionVector2()
    {

#if UNITY_EDITOR
        if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0) || Input.GetMouseButtonDown(0))
        {
            Vector3 touchPosition3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            vector2D_getPositonVector2 = new Vector2(touchPosition3.x, touchPosition3.y);
        }
        //		else 
        //			vector2D = null;

#elif UNITY_ANDROID || UNITY_IOS
   if (Input.touchCount > 0){
		if (Input.touchCount == 1)
		{
		Vector3 touchPosition3 = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		vector2D_getPositonVector2 = new Vector2(touchPosition3.x, touchPosition3.y);
		}
        }

#endif

        return vector2D_getPositonVector2;

    }

    public static Vector2 getDeltaPositionVector2()
    {


#if UNITY_EDITOR

        Vector2 currentPositionVector2 = Input.mousePosition;

        long currentTimeInMilliSeconds = (long)(Time.time * 1000);
        int timeDifference = (int)(currentTimeInMilliSeconds - previousTimeInMilliSeconds);


        // Not updated for 3 second clear all values
        if (timeDifference > 3000)
        {
            previousTimeInMilliSeconds = 0;
            previousPositionVector2.Set(0, 0);
        }
        else
        {
            previousTimeInMilliSeconds = currentTimeInMilliSeconds;
        }



        if (previousPositionVector2.Equals(currentPositionVector2))
        {
            vector2D_getDeltaPositonVector2.Set(0, 0);
        }
        else
        {
            vector2D_getDeltaPositonVector2 = currentPositionVector2 - previousPositionVector2;
            previousPositionVector2 = currentPositionVector2;
        }

        print("Time Difference in MilliSeconds  :: " + timeDifference);
        print("Current Mouse Positon :: " + currentPositionVector2);
        print("Current Mouse Delta Positon :: " + vector2D_getDeltaPositonVector2);

#elif UNITY_ANDROID || UNITY_IOS
		//Input.touches[0].getDeltaPositionVector2();

		Touch touch  = Input.touches[0];		
		//vector2D = touch.deltaPosition;
#endif

        return vector2D_getDeltaPositonVector2;
    }

    public static TouchPhase getTouched_Phase2D(int touchedIndex)
    {

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(touchedIndex))
        {
            //print("Began");
            touchPhase = TouchPhase.Began;
        }
        else if (Input.GetMouseButtonUp(touchedIndex))
        {
            //			print("Ended");
            touchPhase = TouchPhase.Ended;
        }
        else if (Input.GetMouseButton(touchedIndex))
        {
            //			print("Moved");

            touchPhase = TouchPhase.Moved;
        }
#elif UNITY_ANDROID || UNITY_IOS
		touchPhase = Input.GetTouch(touchedIndex).phase;
#endif

        return touchPhase;

    }

    // --------------------------------------------------------------------
    // GET COLLIDER2D 
    // RETURN COLLIDER ON THE SAME LAYER 
    // --------------------------------------------------------------------

    public static Collider2D getCollider2DFromOverlapArea(Collider2D pCollider2D, LayerMask layerToDetect)
    {
        Vector2 pointA = new Vector2(pCollider2D.transform.position.x - pCollider2D.bounds.extents.x,
            pCollider2D.transform.position.y - pCollider2D.bounds.extents.y);
        Vector2 pointB = new Vector2(pCollider2D.transform.position.x + pCollider2D.bounds.extents.x, pCollider2D.transform.position.y + pCollider2D.bounds.extents.y);

        return Physics2D.OverlapArea(pointA, pointB, layerToDetect);
    }

    public static void SoundPollInitCall()
    {
#if UNITY_EDITOR

        Debug.Log("in editor");
#elif UNITY_ANDROID
		//		 if (AppController.IsSound)
		//               {
		//               SoundPoolInit.activity.Call<int>("playSound", 0, false);
		//                }

#endif
    }
    public static bool isClicked_2Dlayer(Collider2D collider2D, LayerMask layer)
    {
        status = false;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchPosition3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPosition2 = new Vector2(touchPosition3.x, touchPosition3.y);
            if (collider2D == Physics2D.OverlapPoint(touchPosition2, layer))
            {

                status = true;
            }
            else
                status = false;

        }
#elif UNITY_ANDROID
		   if (Input.touchCount > 0)
       {
		    if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
		    {

		    Vector3 touchPosition3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		    Vector2 touchPosition2 = new Vector2 (touchPosition3.x,touchPosition3.y);
		    if (collider2D == Physics2D.OverlapPoint(touchPosition2, layer) )
		    status = true;
		    else 
		    status = false;
		    }
		}
#endif

        return status;
    }
}


























