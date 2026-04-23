using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstrumentHandler : MonoBehaviour
{
    
    public List<GameObject> AllScene;
    //public List<GameObject> BGS;
    public int required;
    public Button autoplay;
    public static int SelectedIndex;
    public static InstrumentHandler instance;
    
    private void Start()
    {
        instance = this;
        if (gameObject.name == "All Scene")
        {
            /*for (int i = 0; i < AllScene.Count; i++)
            {
                AllScene[i].SetActive(false);
            }*/

            if (SceneManager.GetActiveScene().name == "Piano")
            {
                PianoTabSetting.Instance.SetColor(SelectedIndex);
                var x = Instantiate(AllScene[SelectedIndex], transform.GetChild(1).transform); //.SetActive(true);
                x.name = AllScene[SelectedIndex].name;
                x.SetActive(true);
               
            }
            else
            {
               print("bbb");
                var x = Instantiate(AllScene[SelectedIndex], transform); //.SetActive(true);
                x.name = AllScene[SelectedIndex].name;
                x.SetActive(true);
            }
        }
        else if (gameObject.name == "Sound Scene")
        {
            //if (ResCheck.instance.resType == ResType.tab)
            {
                Camera.main.orthographicSize = 4.8f;
                Camera.main.transform.position = new Vector3(0.0f, 1.18f, -10);
            }
            var x = Instantiate(AllScene[SelectedIndex], transform); //.SetActive(true);
            x.name = AllScene[SelectedIndex].name;
            x.SetActive(true);
        }

        if (gameObject.name == "All Scene" || gameObject.name == "Sound Scene")
        {
            string ss = SceneManager.GetActiveScene().name;
            ss.Replace(" ", "");
            string s = AllScene[SelectedIndex].name.Replace(" ", "");
            s.Replace("Clone", "");
           // InitializeFirebase_CB._Instance.LogFirebaseEvent(ss + "_" + s + "_Started");
        }
    }

    public void changeScene()
    {

        if (autoplay)
        {
            if (autoplay.GetComponent<AutoAudioObj>() != null)
            {
                autoplay.GetComponent<AutoAudioObj>().LoopAudio.volume = 0;
                if (autoplay.GetComponent<AutoAudioObj>().id == 1)
                {
                    autoplay.GetComponent<AutoAudioObj>().AutoClick();
                }
            }
        }

        string ss = SceneManager.GetActiveScene().name;
        ss.Replace(" ", "");
        string d = AllScene[required].name.Replace(" ", "");
        d.Replace("(Clone)", "");

        SelectedIndex = required;
        if (SceneManager.GetActiveScene().name == "SoundScene")
        {
            var x = Instantiate(AllScene[required],
                transform.parent.gameObject.GetComponentInParent<InstrumentHandler>().gameObject
                    .transform /*.GetChild(1).transform*/); //.SetActive(true);
            x.name = AllScene[required].name;
            string s = transform.parent.gameObject.GetComponentInParent<InstrumentHandler>().gameObject.transform
                .GetChild(1).gameObject.name;
            s.Replace(" ", "");
            //InitializeFirebase_CB._Instance.LogFirebaseEvent(ss + "_" + s + "_SwitchedTo_" + d);

            Destroy(transform.parent.gameObject.GetComponentInParent<InstrumentHandler>().gameObject.transform
                .GetChild(1).gameObject);
            x.SetActive(true);
        }
        else
        {
            PianoTabSetting.Instance.SetColor(required);
            if (SceneManager.GetActiveScene().name == "Piano")
            {
                //SelectedIndex = required;
                var x = Instantiate(AllScene[required],
                    transform.parent.gameObject.GetComponentInParent<InstrumentHandler>().gameObject.transform
                        .GetChild(1).transform);
                x.name = AllScene[required].name;
                string s = transform.parent.gameObject.GetComponentInParent<InstrumentHandler>().gameObject.transform
                    .GetChild(1).transform.GetChild(0).gameObject.name;
                s.Replace(" ", "");
                //InitializeFirebase_CB._Instance.LogFirebaseEvent(ss + "_" + s + "_SwitchedTo_" + d);
                Destroy(transform.parent.gameObject.GetComponentInParent<InstrumentHandler>().gameObject.transform
                    .GetChild(1).transform.GetChild(0).gameObject);
                x.SetActive(true);
            }
            else
            {
                var x = Instantiate(AllScene[required],
                    transform.parent.gameObject.GetComponentInParent<InstrumentHandler>().gameObject
                        .transform /*.GetChild(1).transform*/); //.SetActive(true);
                x.name = AllScene[required].name;
                string s = transform.parent.gameObject.GetComponentInParent<InstrumentHandler>().gameObject.transform
                    .GetChild(2).transform.gameObject.name;
                s.Replace(" ", "");
                //InitializeFirebase_CB._Instance.LogFirebaseEvent(ss + "_" + s + "_SwitchedTo_" + d);
                Destroy(transform.parent.gameObject.GetComponentInParent<InstrumentHandler>().gameObject.transform
                    .GetChild(2).gameObject);
                x.SetActive(true);
            }
        }
        SetPianoBg.ins.BgNew(SelectedIndex);
    }

    private void OnMouseDown()
    {
        PlayerPrefs.SetInt("SelectedIndex", required);
        PlayerPrefs.SetInt("ShowAd", 1);
        //soundref.instance.playTap();
        if (SceneManager.GetActiveScene().name == "SoundScene")
        {
            Debug.Log("Error MouseDown");
        }


        /*if (IntitializeAdmob.instance.IsInterAvailable())
        {
            Debug.Log("Called");
            if (PianoTabSetting.Instance)
            {
                if (PianoTabSetting.Instance.AdLoading)
                {
                    Debug.Log("Causing Issue");
                    PianoTabSetting.Instance.AdLoading.gameObject.SetActive(true);
                }                
            }
        }*/

        changeScene();
    }
}