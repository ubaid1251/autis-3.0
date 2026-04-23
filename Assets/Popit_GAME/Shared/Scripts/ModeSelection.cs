using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        //if (GMAdsManager.Instance)
        //    GMAdsManager.Instance.Show_Interstitial();
        //GMAdsManager.Instance.Log_FB_Event(sceneName+"_SELECTED");
        SceneManager.LoadScene(sceneName);
    }

    public void ButtonClickSound()
    {
        SoundManager.instance.PlayEffect_Instance(7);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
