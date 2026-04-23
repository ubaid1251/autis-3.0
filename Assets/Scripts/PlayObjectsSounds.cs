using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayObjectsSounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void PLayAnimalSounds(string name)
    {
        if(name== "cat")
        {
            SoundManager.instance.PlayAnimalEffect_Complete(0);
        }
        else if (name == "dog")
        {
            SoundManager.instance.PlayAnimalEffect_Complete(2);
        }
        else if (name == "elephant")
        {
            SoundManager.instance.PlayAnimalEffect_Complete(3);
        }
        else if (name == "hen")
        {
            SoundManager.instance.PlayAnimalEffect_Complete(5);
        }
        else if (name == "frog")
        {
            SoundManager.instance.PlayAnimalEffect_Complete(4);
        }
        else if (name == "monkey")
        {
            SoundManager.instance.PlayAnimalEffect_Complete(7);
        }
    }
}
