using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform MyPopitsParent;
    protected BoxCollider2D[] MyPopits;
    protected string MyPopitTag;
    protected Action<bool> OnCompleteCallback;
    public Dice dice;
    protected int count;
    protected int c;
    protected int total;
    protected bool hasSix;
 

    public PlayerBehaviour InitBehaviour(Transform popitsParent , Dice d , string popitTag)
    {
        MyPopitsParent = popitsParent;
        MyPopits = popitsParent.GetComponentsInChildren<BoxCollider2D>(true);
        MyPopitTag = popitTag;
        dice = d;
        count = 0;
        c = 0;
        print(name + " " + dice.name);
        for (int i = 0; i < MyPopits.Length; i++)
        {
            MyPopits[i].transform.tag = popitTag;
            MyPopits[i].enabled = true;
            MyPopits[i].gameObject.SetActive(true);
        }
        total = MyPopits.Length;
        return this;
    }

    public void OnComplete(Action<bool> callback)
    {
        OnCompleteCallback = callback;
    }
    public void RemoveListner()
    {
        OnCompleteCallback = null;

    }
    public virtual void Throw()
    {
        hasSix = false;
        DiceSpace.SoundManager.Instance.PlayOneShot("turn");

        if (Dino.SoundManager.isHaptic)
        {
            DiceSpace.SoundManager.Instance.HapticSuccess();
            Debug.Log("in dice 4 game vibrations are " + Dino.SoundManager.isHaptic);
        }
        dice.Throw(Pop);
    }

    protected void Pop(int num)
    {

        if (num > (total - count))
        {
            OnCompleteCallback?.Invoke(false);
            return;
        }
        c = num;
        if (c == 6)
            hasSix = true;
    }
}
