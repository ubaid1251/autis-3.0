using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public string AnimName;

  public void AnimReseter() {
        GetComponent<Animator>().SetInteger(AnimName, -1);
  }
}
