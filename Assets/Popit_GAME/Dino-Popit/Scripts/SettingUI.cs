using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public Toggle Sound;
    public Toggle Vibration;
    // Start is called before the first frame update
    void Start()
    {
        Sound.isOn = GlobalVars.Instance.Sound;
        Vibration.isOn = GlobalVars.Instance.Vibration;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleSound(bool value)
    {
        GlobalVars.Instance.Sound = value;
        SoundManager.instance.PlayEffect_Instance(5);
    }

    public void ToggleVibration(bool value)
    {
        GlobalVars.Instance.Vibration = value;
    }
}
