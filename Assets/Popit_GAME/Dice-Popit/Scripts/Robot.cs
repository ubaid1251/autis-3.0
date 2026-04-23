using System.Collections;
using UnityEngine;


public class Robot : PlayerBehaviour
{
    bool poping;
    // Update is called once per frame
    void Update()
    {
        if (c > 0 && !poping)
        {
            poping = true;
            StartCoroutine(PopEnumerator());
        }
    }

    IEnumerator PopEnumerator()
    {
        while (c > 0)
        {
            MyPopits[count].gameObject.SetActive(false);
            count++;
            DiceSpace.SoundManager.Instance.PlayRandomFromRange(0 , 4);
            c--;
            if (count == total)
            {
                OnCompleteCallback?.Invoke(true);
                poping = false;
            }
            else if (c == 0)
            {
                /*if (hasSix)
                    Throw();
                else*/
                OnCompleteCallback?.Invoke(false);
                poping = false;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    public override void Throw()
    {
        dice.Throw(Pop , true);
    }
}
