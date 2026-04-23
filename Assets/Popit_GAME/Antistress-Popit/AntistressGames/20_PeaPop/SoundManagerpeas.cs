using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

[RequireComponent(typeof(AudioSource))]
public class SoundManagerpeas : MonoBehaviour
{
   public AudioSource aSource;
    public List<Clip> clips;
    public static SoundManagerpeas Instance;
    [HideInInspector]
    internal bool IsPlaying { get { return aSource.isPlaying; } }

    public float Volume { get {
            return aSource.volume;
        } set
        {
            aSource.volume = value;
        }
    }

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

    }

    public void PlayOneShot(string name, float volume = 1)
    {

        //if (UI_SettingController.IsSoundOnn)
        //{
            Clip clip = GetClip(name);
            if (clip != null && clip.clip != null)
            {
                aSource.clip = clip.clip;
                aSource.PlayOneShot(aSource.clip, volume);
            }
       // }
    }

    public void PlayAudio(string name, bool loop = false,bool vibration = true)
    {
      //  if (UI_SettingController.IsSoundOnn)
      //  {
            Clip clip = GetClip(name);
            if (clip != null && clip.clip != null)
            {
                aSource.clip = clip.clip;
                aSource.loop = loop;
                aSource.Play();
            }
      //  }

        if (vibration) //&&  (UI_SettingController.IsHapticOnn))
        {
         //11   MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        }
    }

    public void PlayRandomAudio()
    {
        Clip clip = clips[Random.Range(0, clips.Count)];

        if(clip.clip != null )//&& (UI_SettingController.IsSoundOnn))
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
