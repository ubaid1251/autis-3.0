using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySound_Paino : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource player;
    public GameObject obj;
    public Animator AnimPlay;
    public string animName;
    private void OnMouseDown()
    {
        //if (player.isPlaying&&player.clip!=clip)
        //{
        //    player.Stop();
        //    player.clip = clip;
        //    player.PlayOneShot(clip);
        //}
        //if (!player.isPlaying)
        //{
        //    player.clip = clip;
        //    player.PlayOneShot(clip);
        //}
        if (SceneManager.GetActiveScene().name != "2sound scene")
        {
            if (AnimPlay)
            {
                if (!AnimPlay.GetCurrentAnimatorStateInfo(0).IsName(animName))
                    AnimPlay.Play(animName);
            }
            if (obj)
                obj.SetActive(true);
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().clip = clip;
                GetComponent<AudioSource>().Play();
            }
            if (transform.childCount > 0)
                transform.GetChild(0).gameObject.SetActive(true);
        }

    }
    public void OnChekcGetDown()
    {
        if (AnimPlay)
        {
            if (!AnimPlay.GetCurrentAnimatorStateInfo(0).IsName(animName))
                AnimPlay.Play(animName);
        }
        if (obj)
            obj.SetActive(true);
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().Play();
        }
        if (transform.childCount > 0)
            transform.GetChild(0).gameObject.SetActive(true);
    }
    public void OnCheckGetUp()
    {
        if (transform.childCount > 0)
            transform.GetChild(0).gameObject.SetActive(false);
    }
    private void OnMouseUp()
    {
        if (SceneManager.GetActiveScene().name != "2sound scene")
        {
            if (transform.childCount > 0)
                transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
