using UnityEngine;
using System.Collections;


public class iTweenMoveHelper1 : MonoBehaviour
{
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
    public Vector3 intPos;
    void OnEnable()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
       // intPos = transform.localPosition;
    }
    float get_touch;
    bool is_done = true;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            get_touch = 0;
            is_done = true;
        }
        else
        {
            get_touch += Time.deltaTime;
            //  Debug.Log(get_touch);
            if (get_touch >= 5)
            {
                if (is_done == true)
                {
                    get_touch = 0;
                    if (playTweenForOnce)
                    {
                        PlayTweenForOnce();
                        playTweenForOnce = false;
                    }
                    if (!playOnce)
                    {
                        StartCoroutine(PlayTween());
                       // PlayTween();
                    }
                    is_done = false;
                }
            }
        }

    }
    private void OnDisable()
    {
      //  transform.localPosition = intPos;

    }
    public IEnumerator PlayTweenForOnce()
    {
        if (ToTransform.MoveFrom == toTransform)
        {
            iTween.MoveFrom(gameObject, iTween.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time, "islocal", islocal, "easetype", EaseType, "delay", delay));
            yield return new WaitForSeconds(time);
            iTween.MoveFrom(gameObject, iTween.Hash("x", gameObject.transform.position.x, "y", gameObject.transform.position.y, "islocal", islocal, "time", time, "easetype", EaseType, "delay", delay));
        }
        else
        {
            iTween.MoveTo(gameObject, iTween.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time, "islocal", islocal, "easetype", EaseType, "delay", delay));
            yield return new WaitForSeconds(time);
            iTween.MoveTo(gameObject, iTween.Hash("x", gameObject.transform.position.x, "y", gameObject.transform.position.y, "islocal", islocal, "time", time, "easetype", EaseType, "delay", delay));
        }
    }

    IEnumerator PlayTween()
    {
        if (PlayerPrefs.GetInt("GamePlayValue") == 1)
        {
            yield return new WaitForSeconds(1.0f);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            if (ToTransform.MoveFrom == toTransform)
            {
                iTween.MoveFrom(gameObject, iTween.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time, "islocal", islocal, "easetype", EaseType, "delay", delay, "looptype", loopType));
            }
            else
            {
                iTween.MoveTo(gameObject, iTween.Hash("x", EndPoint.x, "y", EndPoint.y, "time", time, "islocal", islocal, "easetype", EaseType, "delay", delay, "looptype", loopType));
            }
            is_done = true;
            yield return new WaitForSeconds(2.0f);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
