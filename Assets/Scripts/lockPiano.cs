using UnityEngine;

public class lockPiano : MonoBehaviour
{
    public GameObject Lockimg;
    void Start()
    {
        
    }
    private void Awake()
    {
        //if (PlayerPrefs.GetInt("Piano") == 1)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().color =new Color(1,1,1,1);
            Lockimg.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
