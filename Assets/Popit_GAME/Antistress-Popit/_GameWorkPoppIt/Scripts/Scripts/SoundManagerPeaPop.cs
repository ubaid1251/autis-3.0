using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManagerPeaPop : MonoBehaviour
{
    AudioSource aSource;
    public List<Clip> clips;
    public List<Clip> uiClips;
    public static SoundManagerPeaPop Instance;
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
        Time.timeScale = 1;

        //if (PrivacyPolicy.GameStarted)
        //{
        //    PrivacyPolicy.GameStarted = false;
        //    GoogleAdMobController.THIS.ShowAppOpenAd();
        //}
    }

    public void PlayOneShot(string name, float volume = 1)
    {
        Clip clip = uiClips.Find(c => c.name.Equals(name));
        if(clip != null && clip.clip != null)
        {
            aSource.clip = clip.clip;
            aSource.PlayOneShot(aSource.clip, volume);
        }
    }

    public void PlayAudio(string name, bool loop = false)
    {
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
        Clip clip = clips[Random.Range(0, clips.Count)];

        if(clip.clip != null)
        {
            aSource.clip = clip.clip;
            aSource.Play();
        }
    }

    public void StopAudio()
    {
        aSource.Stop();
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
    public class Clip{

        public string name;
        public AudioClip clip;
    }
}
