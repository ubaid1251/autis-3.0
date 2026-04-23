using UnityEngine;

public class Player : PlayerBehaviour
{

    Ray ray;
    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        if (c > 0)
        {
#if UNITY_EDITOR
            Keyboard();
#else
            Mobile();
#endif
        }
    }

    void Keyboard()
    {
        if (Input.GetMouseButton(0))
        {
            CheckHit(Input.mousePosition);
        }
    }

    void Mobile()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                CheckHit(Input.touches[i].position);
            }
        }
    }

    void CheckHit(Vector3 position)
    {
        position = Camera.main.ScreenToWorldPoint(new Vector3(position.x , position.y , -Camera.main.transform.position.z + transform.position.z));
        RaycastHit2D hit = Physics2D.Raycast(position , Vector2.zero);

        if (hit.collider != null && hit.collider.tag == MyPopitTag)
        {
            hit.transform.gameObject.SetActive(false);
            count++;
            if (Dino.SoundManager.isHaptic)
            {
                DiceSpace.SoundManager.Instance.HapticSelection();
                Debug.Log("in dice 3 game vibrations are " + Dino.SoundManager.isHaptic);

            }
            DiceSpace.SoundManager.Instance.PlayRandomFromRange(0 , 4);
            c--;
            if (count == total)
            {
                OnCompleteCallback?.Invoke(true);
            }
            else if (c == 0)
            {
                /*if (hasSix)
                    Throw();
                else*/
                OnCompleteCallback?.Invoke(false);

            }
            //SoundManager.Instance.PlayAudio("pop");
            // MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.RigidImpact);
            /*spikes.transform.position = hit.point;
            spikes.Play();*/
        }

    }
}
