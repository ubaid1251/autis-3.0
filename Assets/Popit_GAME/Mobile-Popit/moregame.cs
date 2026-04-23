using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class moregame : MonoBehaviour
{
    public string rainbogameLink;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(MoreGameLink);
    }

    // Update is called once per frame
    public void MoreGameLink()
    {
        Application.OpenURL("market://details?id=com.cg.rainbowpopit.bubblepopsup.fidgettoys");
    }
}
