using MoreMountains.NiceVibrations;
using UnityEngine;

public class ButtonWalaMobile : MonoBehaviour
{
    public float buttonYPosition;
    public float pressButtonYposition;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        //#if UNITY_EDITOR
        HandleEditorInput();
        //#else
        //HandleMobileInput();
        //#endif
    }


    void HandleEditorInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (hit.transform != null)
            {
                hit.transform.localPosition = hit.transform.localPosition - hit.transform.forward * buttonYPosition;
                hit.collider.enabled = true;
            }
        }
    }
    void HandleMobileInput()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                HandleInput(touch.position);
            }
        }
    }

    Ray ray;
    RaycastHit hit;
    void HandleInput(Vector3 position)
    {
        ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray , out hit))
        {
            if (hit.collider.CompareTag("button"))
            {
                hit.collider.enabled = false;
                hit.transform.localPosition += hit.transform.forward * buttonYPosition;
                //if (Antistress.SoundManager.Instance)
                //    Antistress.SoundManager.Instance.PlayAudio("beep"); //AdCallPosition
                //MMVibrationManager.TransientHaptic(0.3f, 0.3f);
                MobileCaseManger.Instance.AddCoin(hit.point);
            }
        }
    }
}
