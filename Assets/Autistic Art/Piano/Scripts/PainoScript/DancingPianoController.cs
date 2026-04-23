
using System.Collections.Generic;
using UnityEngine;

public class DancingPianoController : MonoBehaviour
{
    public Animator anim, anim2;
    public GameObject bgMove;
    private GameObject currentTile, currentTile1;
    public GameObject soundeffect;
    public AudioClip clip, NewClip;
    private bool isClicked1, isClicked2;
    public static bool checkstop, sound;
    RaycastHit2D raycastHit2D, raycastHit2D1;
    public Transform clickParticle;
    int val;
    //public DancingPianoController2 DancingPianoController2;
    //public Animator[] Dog_Truck;
    //public Animator[] Monkey;
    //public bool Monkey_Set;
    //public string[] animName;
    public bool _play;
    //public GameObject monkey_happyCh;
    //public GameObject monkey_IdleCh;

    //public List<SkeletonAnimation> BG;
    public Animator[] otherObjects;

    //public string[] animName, BGAnim;

    public AutoAudioObj autoAudioObj;
    public bool animatedBG = false;

    public AudioClip[] CharactersSound;
    public bool CharactersVoices;
    //public string[] idle_anim;
    private AudioSource mySource;
    private void Start()
    {
        mySource = GetComponent<AudioSource>();
        //Monkey_Set = true;
    }
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
    void Update()
    {
        if (Play_Effect1 == true)
        {
            if (Play_Effect)
            {
                get_touch += Time.deltaTime;
                if (get_touch > 4 && Play_Effect)
                {
                    OnCheckPlayAudio();
                    get_touch = 0;
                }
            }
        }
        if (checkstop && anim2)
        {
            if (anim2.speed > 0)
                anim2.speed -= 0.4f * Time.deltaTime;
            else
                checkstop = false;
        }
        if (sound)
        {
            if (soundeffect.GetComponent<AudioSource>().volume > 0)
                soundeffect.GetComponent<AudioSource>().volume -= 0.5f * Time.deltaTime;
            else
            {
                soundeffect.GetComponent<AudioSource>().Stop();
                sound = false;
            }
        }
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Play_Effect1 = true;
                    Vector3 temp = Camera.main.ScreenToWorldPoint(touch.position);
                    raycastHit2D = Physics2D.Raycast(temp, Vector2.zero);
                    if (raycastHit2D.collider != null)
                    {
                        if (raycastHit2D.collider.gameObject.tag == "tile")
                        {
                            currentTile = raycastHit2D.collider.gameObject;
                            if (currentTile && currentTile.transform.childCount > 0)
                                currentTile.transform.GetChild(0).gameObject.SetActive(true);
                            PlayParticles(temp);
                            if (anim != null)
                                anim.SetInteger("anim", 0);
                            if (bgMove != null)
                            {
                                bgMove.GetComponent<iTweenMoveHelper>().enabled = true;
                                iTween.Resume(bgMove);
                            }
                            PianoTabSetting.Instance.SetAnim(true);
                            // if (BG.Count > 0)
                            // {
                            //     if (CharactersVoices)
                            //     {
                            //         BG[0].AnimationState.Event += HandleEvent;
                            //     }
                            //     SetBGAnimation();
                            // }
                           // if (otherObjects != null)
                            {
                               // SetOthersToAnimation();
                            }
                            if (anim2)
                            {
                                anim2.enabled = true;
                                anim2.speed = 1;
                            }
                            isClicked1 = true;
                            if (anim != null)
                                anim.speed = 1;
                            sound = false;
                            soundeffect.GetComponent<AudioSource>().volume = 1;
                            if (!soundeffect.GetComponent<AudioSource>().isPlaying)
                            {
                                if (NewClip != null)
                                {
                                    soundeffect.GetComponent<AudioSource>().clip = NewClip;
                                }
                                else
                                {
                                    soundeffect.GetComponent<AudioSource>().clip = clip;
                                }
                                if (soundeffect.GetComponent<AudioSource>().enabled == true)
                                    soundeffect.GetComponent<AudioSource>().Play();
                            }
                            checkstop = false;
                        }
                    }
                }
                if (touch.phase == TouchPhase.Moved)
                {

                    if (isClicked1)
                    {
                        Play_Effect1 = true;
                        Vector3 temp = Camera.main.ScreenToWorldPoint(touch.position);
                        raycastHit2D = Physics2D.Raycast(temp, Vector2.zero);
                        if (raycastHit2D.collider != null)
                        {
                            if (raycastHit2D.collider.tag.Contains("tile") && currentTile != raycastHit2D.collider.gameObject)
                            {
                                if (currentTile != raycastHit2D.collider.gameObject && currentTile.transform.childCount > 0)
                                    currentTile.transform.GetChild(0).gameObject.SetActive(false);
                                PlayParticles(temp);
                                if (anim != null)
                                {
                                    anim.SetInteger("anim", 0);
                                }
                                if (anim2)
                                {
                                    anim2.enabled = true;
                                    anim2.speed = 1;
                                }
                                if (anim != null)
                                    anim.speed = 1;
                                isClicked1 = true;
                                if (anim != null)
                                    anim.speed = 1;

                                currentTile = raycastHit2D.collider.gameObject;
                                if (currentTile.transform.childCount > 0)
                                    currentTile.transform.GetChild(0).gameObject.SetActive(true);
                            }
                        }
                    }
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    Play_Effect1 = false;
                    get_touch = 0;
                    if (isClicked1 && play == false)
                    {
                        if (currentTile && currentTile.transform.childCount > 0)
                        {
                            currentTile.transform.GetChild(0).gameObject.SetActive(false);
                            currentTile = null;
                        }
                        isClicked1 = false;
                        if (bgMove != null)
                        {
                            iTween.Pause(bgMove);
                        }
                        PianoTabSetting.Instance.SetAnim(false);
                        // if (BG != null)
                        // {
                        //     if (autoAudioObj)
                        //     {
                        //         if (autoAudioObj.id == 0)
                        //         {
                        //             SetBGIdle();
                        //         }
                        //     }
                        //     else
                        //     {
                        //         SetBGIdle();
                        //     }
                        // }
                        // if (otherObjects != null)
                        // {
                        //     if (autoAudioObj)
                        //     {
                        //         if (autoAudioObj.id == 0)
                        //         {
                        //             SetOthersToIdle();
                        //         }
                        //     }
                        // }
                        
                    }
                    if (!isClicked1 && !isClicked2 && play == false)
                    {
                        if (anim != null)
                            anim.SetInteger("anim", 1);
                        if (anim2)
                        {
                            checkstop = true;
                        }
                        sound = true;
                        if (anim2)
                            anim2.speed = 0;
                    }
                }
            }
        }
    }
    bool play = false;
    public void AnimPlay()
    {
        if (play == false)
        {
            
            play = true;
            if (anim != null)
                anim.SetInteger("anim", 0);
            if (anim != null)
                anim.speed = 1;
            // if (DancingPianoController2 != null)
            // {
            //     if (anim != null)
            //         DancingPianoController2.anim.SetInteger("anim", 0);
            //     if (anim != null)
            //         DancingPianoController2.anim.speed = 1;
            //     if (DancingPianoController2.anim1)
            //     {
            //         DancingPianoController2.anim1.SetInteger("anim", 0);
            //         DancingPianoController2.anim1.speed = 1;
            //     }
            //     if (DancingPianoController2.anim1)
            //     {
            //         DancingPianoController2.anim3.SetInteger("anim", 0);
            //         DancingPianoController2.anim3.speed = 1;
            //     }
            //     DancingPianoController2.bg_move = true;
            //     DancingPianoController2.Is_Move = true;
            // }
            if (anim2)
            {
                anim2.enabled = true;
                anim2.speed = 1;
            }
            if (bgMove != null)
            {
                bgMove.GetComponent<iTweenMoveHelper>().enabled = true;
                iTween.Resume(bgMove);
                // if (DancingPianoController2 != null)
                // {
                //     DancingPianoController2.bg_move = true;
                // }
            }
        }
        else
        {
            play = false;
            if (anim2)
            {
                checkstop = true;
                anim2.speed = 0;
            }

            if (bgMove != null)
            {
                iTween.Pause(bgMove);
                // if (DancingPianoController2 != null)
                // {
                //     DancingPianoController2.bg_move = false;
                // }
            }
            // if (DancingPianoController2 != null)
            // {
            //     if (anim != null)
            //         DancingPianoController2.anim.SetInteger("anim", 1);
            //     if (DancingPianoController2.anim1)
            //         DancingPianoController2.anim1.SetInteger("anim", 1);
            //     if (DancingPianoController2.anim3)
            //         DancingPianoController2.anim3.SetInteger("anim", 1);
            //
            //     DancingPianoController2.bg_move = false;
            //     DancingPianoController2.Is_Move = false;
            // }
        }
    }

    public AudioClip[] Happy_Clip;
    float get_touch;
    public float Delay_Time;
    public bool Play_Effect;
    public bool Play_Effect1 = false;
    void OnCheckPlayAudio()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        if (audio.enabled == true)
            audio.PlayOneShot(Happy_Clip[Random.Range(0, Happy_Clip.Length)]);
    }


    public void SetBGIdle()
    {
        // for (int i = 0; i < BG.Count; i++)
        // {
        //     if (animatedBG)
        //     {
        //             BG[i].AnimationState.SetAnimation(0, BGAnim[1], true);
        //     }
        //     else
        //     {
        //         BG[i].AnimationState.TimeScale = 0;
        //     }            
        // }

    }
    public void HandleEvent()//Spine.TrackEntry trackEntry, Spine.Event e)
    {
        for (int i = 0; i < CharactersSound.Length; i++)
        {
            //if (e.Data.Name == CharactersSound[i].name)
            {
                /*if (mySource.isPlaying)
                {
                    mySource.Stop();
                }*/
                if (mySource.enabled == true&&!mySource.isPlaying)
                    mySource.PlayOneShot(CharactersSound[i]);
                break;
            }
        }

    }

    public void SetOthersToIdle()
    {
        
    }

    public void SetOthersToAnimation()
    {
        Debug.Log("Error||| ");
        // if (otherObjects.Length > 0)
        // {
        //     Debug.Log("Anim State: " + otherObjects[0].state.GetCurrent(0));
        //     if (otherObjects[0].state.GetCurrent(0) != null)
        //     {
        //         Debug.Log("Anim If: " + otherObjects[0].state.GetCurrent(0));
        //         if (otherObjects[0].state.GetCurrent(0).Animation.Name != animName[0])
        //         {
        //             foreach (SkeletonAnimation anim in otherObjects)
        //             {
        //                 anim.AnimationState.TimeScale = 1;
        //                 Debug.Log("Name: " + anim.name + " || " + anim.timeScale);
        //                 anim.AnimationState.SetAnimation(0, animName[0], true);
        //             }
        //         }
        //         else
        //         {
        //             foreach (SkeletonAnimation anim in otherObjects)
        //             {
        //                 anim.AnimationState.TimeScale = 1;
        //                 //Debug.Log("Name: " + anim.name + " || " + anim.timeScale);
        //                 //anim.AnimationState.SetAnimation(0, animName[0], true);
        //             }
        //         }
        //     }
        //     else
        //     {
        //         foreach (SkeletonAnimation anim in otherObjects)
        //         {
        //             anim.AnimationState.TimeScale = 1;
        //             Debug.Log("Name: " + anim.name + " || " + anim.timeScale);
        //
        //             anim.AnimationState.SetAnimation(0, animName[0], true);
        //         }
        //     }
        //
        // }
    }

    public void SetBGAnimation()
    {
       
        // if (BG[0].state.GetCurrent(0) != null)
        // {
        //     if (BG[0].state.GetCurrent(0).Animation.Name != BGAnim[0])
        //     {
        //         for (int i = 0; i < BG.Count; i++)
        //         {
        //             BG[i].AnimationState.SetAnimation(0, BGAnim[0], true);
        //             
        //         }
        //     }
        //     else
        //     {
        //         for (int i = 0; i < BG.Count; i++)
        //         {
        //             BG[i].AnimationState.TimeScale = 1;
        //             
        //         }
        //     }
        // }
        // else
        // {
        //     for (int i = 0; i < BG.Count; i++)
        //     {
        //         BG[i].AnimationState.SetAnimation(0, BGAnim[0], true);
        //     }
        // }

    }


    public void AutoPlay()
    {
        // if (BG != null)
        // {
        //     SetBGAnimation();
        // }
        // if (otherObjects != null)
        // {
        //     SetOthersToAnimation();
        // }
    }

    public void StopAutoPlay()
    {
        // if (BG != null)
        // {
        //     SetBGIdle();
        // }
        // if (otherObjects != null)
        // {
        //     SetOthersToIdle();
        // }
    }

}
