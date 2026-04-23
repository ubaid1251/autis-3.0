using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager_sallu : MonoBehaviour
{
    public  AudioSource aSource;
    public List<Clip> clips;
    [HideInInspector]
    public List<AudioClip> popoPitClipslist;
    public static SoundManager_sallu Instance;
    void Awake()
    {
        aSource = GetComponent<AudioSource>();
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            DestroyImmediate(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Time.timeScale != 1)
        Time.timeScale = 1;

    }

    public void PlayOneShot(string name, float volume = 1)
    {
        //if (!UI_SettingController.IsSoundOnn)
        //    return;
        Clip clip = GetClip(name);
        if(clip != null && clip.clip != null)
        {
            aSource.clip = clip.clip;
            aSource.PlayOneShot(aSource.clip, volume);
        }
    }

    public void PlayAudio(string name, bool loop = false)
    {
        //if (!UI_SettingController.IsSoundOnn)
        //    return;
        aSource.enabled = true;
        Clip clip = GetClip(name);
        if (clip != null && clip.clip != null)
        {
            //aSource.clip = clip.clip;
            aSource.loop = loop;
            aSource.PlayOneShot(clip.clip);
        }
    }

    public void PlayRandomAudio()
    {
        //if (!UI_SettingController.IsSoundOnn)
        //    return;

        AudioClip clip = popoPitClipslist[Random.Range(0, popoPitClipslist.Count-1)];

        if(clip != null)
        {
            aSource.clip = clip;
            aSource.Play();
        }
    }
    public void PlayIndexWiseAudio(int index)
    {
        //if (!UI_SettingController.IsSoundOnn)
        //    return;
        // Debug.Log(index);
        AudioClip clip = clips[index].clip;

        if (clip != null)
        {
            aSource.clip = clip;
            aSource.Play();
        }
    }
    public void StopAudio()
    {
        aSource.Stop();
        aSource.enabled = false;

    }

    public void PauseAudio()
    {
        aSource.Pause();
    }
   
    Clip GetClip(string name)
    {
        return  clips.Find(c => c.name.Equals(name));
    }
   
    [System.Serializable]
    public class Clip
    {

        public string name;
        public AudioClip clip;
    }

   
}
