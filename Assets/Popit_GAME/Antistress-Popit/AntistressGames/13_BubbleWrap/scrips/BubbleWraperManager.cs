using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BubbleWraperManager : MonoBehaviour
{
    public Sprite[] bubblePopAnimatedSprites;
    public Sprite[] bubblePopAnimatedHealtySprites;
    public SpriteRenderer[] bubbleSprArr;
    public Text hitcounterText;
    public GameObject next, home;
    public int hitcounter = 0;
    int counterclear = 0;
    // Update is called once per frame
    private void OnEnable()
    {
        // GoogleAdMobController.THIS.ShowR2InterstitialAd(); //AdCallPosition
    }
    void Update()
    {
#if UNITY_EDITOR
        EditorInput();
#else
                MobileInput();
#endif
        if (Input.GetMouseButtonUp(0))
        {
            if (counterclear >= bubbleSprArr.Length)
            {
                counterclear = 0;
                StartCoroutine(ButtonPressRealseDownAnimation());
            }
        }
    }

    void MobileInput()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                CheckHit(touch.position);
            }
        }
    }

    void EditorInput()
    {
        if (Input.GetMouseButton(0))
        {
            CheckHit(Input.mousePosition);
        }
    }

    void CheckHit(Vector3 position)
    {
        position = Camera.main.ScreenToWorldPoint(new Vector3(position.x , position.y , -Camera.main.transform.position.z));
        RaycastHit2D hit = Physics2D.Raycast(position , Vector2.zero);
        if (hit == null)
            return;

        if (hit.collider != null && hit.transform.gameObject.name == "Bolla")
        {
            StartCoroutine(ButtonPressDownAnimation(hit.transform.gameObject));
            counterclear++;
            return;
        }
    }

    IEnumerator ButtonPressDownAnimation(GameObject hitOutObj)
    {
        int counter = 0;
        if (!SoundManager_sallu.Instance.aSource.isPlaying)
            SoundManager_sallu.Instance.PlayIndexWiseAudio(0);

        //if (UI_SettingController.Instance)
        //{
        //    UI_SettingController.Instance.PlayNiceVibration();
        //}



        hitOutObj.GetComponent<Collider2D>().enabled = false;
        while (counter < bubblePopAnimatedSprites.Length)
        {
            yield return new WaitForSeconds(0.02f);
            hitOutObj.GetComponent<SpriteRenderer>().sprite = bubblePopAnimatedSprites[counter];
            counter++;
            // print(counter);
        }
        hitcounter++;
        if (hitcounter == 72)
        {
            home.SetActive(true);
            //  next.SetActive(true);
        }
        hitcounterText.text = hitcounter.ToString();
    }

    IEnumerator ButtonPressRealseDownAnimation()
    {
        int counter = 0;
        while (counter < bubbleSprArr.Length)
        {
            yield return new WaitForSeconds(0.02f);
            if (!SoundManager_sallu.Instance.aSource.isPlaying)
                SoundManager_sallu.Instance.PlayIndexWiseAudio(1);

            bubbleSprArr[counter].GetComponent<Collider2D>().enabled = true;
            bubbleSprArr[counter].sprite = bubblePopAnimatedHealtySprites[Random.Range(0 , bubblePopAnimatedHealtySprites.Length)];
            counter++;
        }
    }


}
