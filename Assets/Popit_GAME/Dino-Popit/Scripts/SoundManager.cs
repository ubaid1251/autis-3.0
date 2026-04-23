using System.Collections.Generic;
using UnityEngine;

namespace Dino
{
    [RequireComponent(typeof(AudioSource))]

    public class SoundManager : MonoBehaviour     //dino
    {
        AudioSource aSource;
        public List<Clip> clips;
        public List<Clip> uiClips;
        public static SoundManager Instance;
        public static bool isHaptic;
        //public AudioSource MusicController;
        void Awake()
        {
            aSource = GetComponent<AudioSource>();
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            if (PrivacyPolicy.GameStarted)
            {
                PrivacyPolicy.GameStarted = false;
                //GoogleAdMobController.THIS.ShowAppOpenAd(); //AdCallPosition
            }
            AudioListener.volume = PlayerPrefs.GetInt("SoundVolume") == 0 ? 1 : 0;


            isHaptic = PlayerPrefs.GetInt("HapticEnabled") == 0 ? true : false;

            Debug.Log("is haptics in sound manager is " + isHaptic);
            //if (GlobalVars.Instance.Sound)
            //{
            //    MusicController.Play();

            //}

        }
        //public void Music(bool play)
        //{
        //    if (play)
        //        MusicController.Play();
        //    else
        //        MusicController.Pause();
        //}

        public void SetHapticFlag() {
            isHaptic = PlayerPrefs.GetInt("HapticEnabled") == 0 ? true : false;
        }

        public void PlayOneShot(string name , float volume = 1)
        {

            Clip clip = uiClips.Find(c => c.name.Equals(name));
            //Clip clip = GetClip(name);
            if (clip != null && clip.clip != null)
            {
                aSource.clip = clip.clip;
                aSource.PlayOneShot(aSource.clip , volume);
            }
        }

        public void PlayAudio(string name , bool loop = false)
        {
            Clip clip = GetClip(name);
            if (clip != null && clip.clip != null)
            {
                aSource.clip = clip.clip;
                aSource.loop = loop;
                aSource.Play();
            }
        }

        public void PlayRandomAudio()
        {
            Clip clip = clips[Random.Range(0 , clips.Count)];

            if (clip.clip != null)
            {
                aSource.clip = clip.clip;
                aSource.PlayOneShot(clip.clip);
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
            return clips.Find(c => c.name.Equals(name));
        }
        [System.Serializable]
        public class Clip
        {

            public string name;
            public AudioClip clip;
        }

    }
}