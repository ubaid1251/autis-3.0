using UnityEngine.UI;
using UnityEngine;

public class cois : MonoBehaviour
{
    [HideInInspector]
    public Transform targetPosTransForm;
    [HideInInspector]
    public bool IsMovECoins;
    [SerializeField] float speed = 0.3f;
    float timer;

    private void Start()
    {
        transform.localScale = Vector3.one;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsMovECoins)
        {
            if (timer < 1)
            {
                timer += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(transform.position, targetPosTransForm.position, timer);

               
                if (Vector3.Distance(transform.position, targetPosTransForm.position) <= 0.01f)
                {
                  // GetComponent<AudioSource>().Play();
                    GetComponent<Image>().enabled = false;
                    Destroy(this.gameObject,1f);
                    IsMovECoins = false;
                    timer = 0;
                }
            }
        }
    }
    
}
