using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShapButton : MonoBehaviour
{
    public GameObject[] Buttons;
    int current;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void SwitchButton()
    {
        Buttons[current].gameObject.SetActive(false);
        current++;

        if (current >= Buttons.Length)
            current = 0;
        Buttons[current].gameObject.SetActive(true);
    }

    
}
