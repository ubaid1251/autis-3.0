using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpCharacter : MonoBehaviour
{
    public GameObject hand;
    //SkeletonAnimation anim;
    private void Start()
    {
      //  anim=GetComponent<SkeletonAnimation>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))//&&anim.AnimationState.GetCurrent(0).Animation.Name!="Jump")
        {
            // anim.AnimationState.GetCurrent(0).Complete -= walking;
            // anim.AnimationState.SetAnimation(0, "Jump", false).Complete += walking;
            //GetComponent<Animator>().SetTrigger("Happy");
            if (hand)
                hand.SetActive(false);
        }   
    }
    // public void walking(TrackEntry tk)
    // {
    //     anim.AnimationState.SetAnimation(0, "Move", true);
    // }
}
