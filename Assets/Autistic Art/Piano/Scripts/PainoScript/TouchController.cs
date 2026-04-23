using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    GameObject hit1;
    bool isClicked1;
    //public List<SpineAnimHandler> Characters;
    public Animator characterAnim;
    //public string Animparameter;
    // public int maxAnim;
    public Transform clickParticle;
    int val;
    public bool Is_Play;
    public ParticleSystem play_part;
    void PlayParticles(Vector3 temp)
    {
        if (clickParticle.childCount > 0)
        {
            val = Random.Range(0, clickParticle.childCount);
            temp.z = 0;
            clickParticle.GetChild(val).transform.position = temp;
            clickParticle.GetChild(val).gameObject.SetActive(true);

        }
    }
    string tilename;
    private void Start()
    {
        clickParticle = GameObject.Find("MusicParticles").transform;
        Is_Play = true;
    }
    void PlayMusic(TilesController reftile)
    {
        if (reftile.audioSrc.enabled == true)
            reftile.audioSrc.Play();
        reftile.highlighter.SetActive(true);
        if (characterAnim)//if normal animation
        {
            if (tilename == reftile.name)
            {
                characterAnim.SetBool("Idle", true);
                characterAnim.SetBool("Happy", false);
            }
            else
            {
                if (Is_Play)
                {
                    characterAnim.SetBool("Idle", false);
                    characterAnim.SetBool("Happy", true);
                    Is_Play = false;
                }
            }
        }
        else//spine animation
        {
            if (tilename == reftile.name)
            {
                // for (int i = 0; i < Characters.Count; i++)
                // {
                //     Characters[i].setIdle();
                // }
            }
            else
            {
                if (Is_Play)
                {
                    // for (int i = 0; i < Characters.Count; i++)
                    // {
                    //     Characters[i].SetHappy();
                    // }
                    Is_Play = false;
                }
            }
        }
        tilename = reftile.name;
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    isClicked1 = true;
                    Vector3 temp = Camera.main.ScreenToWorldPoint(touch.position);

                    RaycastHit2D hit = Physics2D.Raycast(temp,Vector3.zero/*, 1000, LayerMask.NameToLayer("PostProcessing")*/);

                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.tag == "tile")
                        {
                            if (play_part != null)
                            {
                                play_part.Play();
                            }
                            if(clickParticle)
                            {
                                PlayParticles(temp);

                            }
                            hit1 = hit.collider.gameObject;
                            if (hit1.GetComponent<TilesController>())
                            {
                                Debug.Log("||Error|| " + hit.transform.gameObject.name);
                                PlayMusic(hit1.GetComponent<TilesController>());
                            }
                        }

                    }
                }

                if (touch.phase == TouchPhase.Moved)
                {

                    if (isClicked1)
                    {

                        Vector3 temp = Camera.main.ScreenToWorldPoint(touch.position);

                        RaycastHit2D hit = Physics2D.Raycast(temp, Vector3.zero/*, 1000, LayerMask.NameToLayer("PostProcessing")*/);
                        if (hit.collider != null)
                        {

                            if (hit.collider.gameObject.tag == "tile" && hit1 != hit.collider.gameObject)
                            {
                                if(clickParticle)
                                {
                                    PlayParticles(temp);
                                }
                                
                                if (play_part != null)
                                {
                                    play_part.Play();
                                }
                                if (hit1 == null)
                                {
                                    hit1 = hit.collider.gameObject;

                                }
                                if (hit1.GetComponent<TilesController>())
                                {

                                    hit1.GetComponent<TilesController>().highlighter.SetActive(false);
                                }

                                hit1 = hit.collider.gameObject;
                                if (hit1.GetComponent<TilesController>())
                                {
                                    PlayMusic(hit1.GetComponent<TilesController>());
                                }
                            }

                        }
                    }

                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (isClicked1)
                    {
                        if (hit1 && hit1.GetComponent<TilesController>())
                        {
                            hit1.GetComponent<TilesController>().highlighter.SetActive(false);
                            if (characterAnim)//animation
                            {
                                characterAnim.SetBool("Idle", true);
                                characterAnim.SetBool("Happy", false);
                            }
                            else//spine
                            {
                                // for (int i = 0; i < Characters.Count; i++)
                                // {
                                //     Characters[i].setIdle();
                                // }
                            }
                           
                            Is_Play = true;
                        }
                        isClicked1 = false;
                    }
                    
                }
                //if (characterAnim)//animation
                //{
                //    characterAnim.SetBool("Idle", true);
                //    characterAnim.SetBool("Happy", false);
                //}
            }
        }
    }
}
