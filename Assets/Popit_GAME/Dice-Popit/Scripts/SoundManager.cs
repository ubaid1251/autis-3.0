//using MoreMountains.NiceVibrations;
using System.Collections.Generic;
using UnityEngine;

namespace DiceSpace
{
    [RequireComponent(typeof(AudioSource))]       //Dice


    public class SoundManager : MonoBehaviour
    {
        AudioSource aSource;
        public List<Clip> clips;
        public static SoundManager Instance;
        void Awake()
        {
            aSource = GetComponent<AudioSource>();
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1;
            if (PrivacyPolicy.GameStarted)
            {
                PrivacyPolicy.GameStarted = false;
                //ads   GoogleAdMobController.THIS.ShowAppOpenAd();
                //  print("Open App Dikha do naaaw");
            }
        }

        public void PlayOneShot(string name , float volume = 1)
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                Clip clip = GetClip(name);
                if (clip != null && clip.clip != null)
                {
                    aSource.clip = clip.clip;
                    aSource.PlayOneShot(aSource.clip , volume);
                }
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
                aSource.Play();
            }
        }
        public void PlayRandomFromRange(int min , int max)
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                Clip clip = clips[Random.Range(min , max <= clips.Count ? max : clips.Count)];

                if (clip.clip != null)
                {
                    aSource.clip = clip.clip;
                    aSource.PlayOneShot(clip.clip);
                }
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

        public void HapticSelection()
        {
            if (PlayerPrefs.GetInt("Vibration") == 0)
            {
                //MMVibrationManager.Haptic(HapticTypes.Selection);
            }
        }

        public void HapticSuccess()
        {
            if (PlayerPrefs.GetInt("Vibration") == 0)
            {
                //MMVibrationManager.Haptic(HapticTypes.Success);
            }
        }
        [System.Serializable]
        public class Clip
        {

            public string name;
            public AudioClip clip;
        }
    }
}