using System.Collections;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using Melanchall.DryWetMidi.Devices;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
// using Unity.VisualScripting;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public Lane[] lanes;
    public float songDelayInSeconds;
    public double marginOfError; // in seconds

    public int inputDelayInMilliseconds;
    public static int counter = 0;

    public string fileLocation;
    public float noteTime;
    public float noteSpawnY;
    public float noteTapY;
    public AudioSource audioSource;
    public Image filler;
    private void Awake()
    {
        counter = 0;
    }

    private void Update()
    {
        float fillAmount = audioSource.time / audioSource.clip.length;
        filler.fillAmount = fillAmount;
    }
    public float noteDespawnY
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY);
        }
    }
    public void Home()
    {
        PlayerPrefs.SetInt("Completed", 1);
        PlayerPrefs.SetInt("RateCounter", PlayerPrefs.GetInt("RateCounter") + 1);
        InitializeFirebase_CB.instance.LogFirebaseEvent("TileABC_Switched_ByHome");
        DOTween.KillAll(false);
        // if (PlayerPrefs.GetInt("RemoveAds") == 0)
        // {
        //     PlayerPrefs.SetString("ReloadScene", "MainSelection");
        //     loading.GetComponent<LoadingHandler>().showBannerEnd = false;
        //     loading.GetComponent<LoadingHandler>().loadNextScene = true;
        //     loading.SetActive(true);
        // }
        // else
        {
            // if (RateUsHandler.Instance.rate.activeInHierarchy)
            // {
            //     RateUsHandler.Instance.Cross();
            // }

            //if (InappPanel.Instance.removeAdsPanel.activeInHierarchy)
            //{
            //    InappPanel.Instance.RemoveInAppPanel();
            //}
            SceneManager.LoadScene("Selection");
        }
    }
    public static MidiFile midiFile;
    public static Playback Playback;
    void Start()
    {
        Instance = this;
#if UNITY_ANDROID&&!UNITY_EDITOR
        StartCoroutine(ReadFromWebsite());
#endif
#if UNITY_EDITOR|| UNITY_IOS
        ReadFromFile();
#endif
    }

    private IEnumerator ReadFromWebsite()
    {
        string loaction = "jar:file://" + Application.dataPath + "!/assets/" + fileLocation;
        using (var www = new WWW(loaction))
        {
            yield return www;

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError("Error loading MIDI file: " + www.error);
                yield break;
            }
            using (var stream = new MemoryStream(www.bytes))
            {
                midiFile = MidiFile.Read(stream);
                GetDataFromMidi();
            }
        }
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
        Playback = midiFile.GetPlayback();
        Playback.Stop();
    }
    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Note[notes.Count];
        notes.CopyTo(array, 0);
        foreach (var lane in lanes) lane.SetTimeStamps(array);
    }
    public void StartSong()
    {
        audioSource.Play();
    }
    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

}
