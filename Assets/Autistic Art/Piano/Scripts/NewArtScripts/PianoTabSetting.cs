using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PianoTabSetting : MonoBehaviour
{
    public static PianoTabSetting Instance;
    public List<Animator> ch;
    public Color[] BGColors;

    public GameObject AdLoading;
    
    private void Awake()
    {
        if (ResCheck.ResolutionType == ResType.tab)
        {
            if (SceneManager.GetActiveScene().name == "Piano")
            {
                Camera.main.orthographicSize = 4.8f;
                Camera.main.transform.position = new Vector3(0.05f,1.18f,-10);
                
            }
        }
        Instance = this;
    }
    public void SetAnim(bool play)
    {
        print("call");
        if (play)
        {
            print("call1"+ch.Count);
            for (int i = 0; i < ch.Count; i++)
            {
                print("call2");
                ch[i].Play("dance");
            }
        }
        else
        {
            for (int i = 0; i < ch.Count; i++)
            {
                ch[i].Play("idle");
            }
        }
    }
    public void SetColor(int index)
    {
        Camera.main.backgroundColor = BGColors[index];
    }

}
