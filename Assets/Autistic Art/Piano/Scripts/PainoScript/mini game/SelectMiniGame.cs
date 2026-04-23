using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMiniGame : MonoBehaviour
{
    public GameObject game,homebtn,minihomebt;
    public AudioSource bgSound;
    private void OnMouseDown()
    {
        transform.localPosition -= new Vector3(0.1f, 0.1f, 0.1f);
    }
    private void OnMouseUp()
    {
        if (homebtn) {
            if (minihomebt) {
                minihomebt.SetActive(true);
                homebtn.SetActive(false);
            }

        }
        bgSound.enabled = false;
        transform.localPosition += new Vector3(0.1f, 0.1f, 0.1f);
        game.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
