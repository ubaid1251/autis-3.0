using System.Collections;
using System.Linq;
using UnityEngine;

namespace MobileCase
{
    public class Popit2D : MonoBehaviour
    {
        public GameObject BG;
        public Transform PopitsParent;    

        private BoxCollider2D[] popits;
        int count;
        int total;
        bool initialized;

        #region UnityLifeCycle

        //public ParticleSystem spikes;
        private void OnEnable()
        {
            EnablePopit();
        }

        private void Start()
        {
            total = PopitsParent.childCount;
            popits = PopitsParent.GetComponentsInChildren<BoxCollider2D>();
            initialized = true;
        }

        private void Update()
        {
#if UNITY_EDITOR
            EditorInput();
#else
                MobileInput();
#endif
        }


        #endregion

        void MobileInput()
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    CheckHit(touch.position);
                }
            }
        }

        void EditorInput()
        {
            if (Input.GetMouseButton(0))
            {
                CheckHit(Input.mousePosition);
            }
        }

        void CheckHit(Vector3 position)
        {
            position = Camera.main.ScreenToWorldPoint(new Vector3(position.x , position.y , -Camera.main.transform.position.z));
            RaycastHit2D hit = Physics2D.Raycast(position , Vector2.zero);

            if (hit.collider == null)
                return;

            //  Debug.Log(hit.collider.transform.gameObject.name + " : " + count + " : " + popits.Length);

            if (hit.collider != null && hit.collider.tag == "obj" && count < popits.Length)
            {
                hit.transform.gameObject.SetActive(false);
                count++;
                SoundManager.Instance.PlayRandomAudio();

                ////mobile
                //MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.Selection);

                if (Dino.SoundManager.isHaptic)
                {
                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.Selection);
                    Debug.Log("in mobile game vibrations are " + Dino.SoundManager.isHaptic);
                }

                if (MobileCaseManger.Instance)
                    MobileCaseManger.Instance.AddCoin(hit.point , Popit2DManager.level);
                /*spikes.transform.position = hit.point;
                spikes.Play();*/

                if (count == total)
                {
                    Popit2DManager.Instance.PlayParticles();
                    SoundManager.Instance.PlayOneShot("PopitLevelUp");

                    Invoke("ShowInterstitialAD", 1f);

                    StartCoroutine(ResetBlends());
                }
            }

        }


        public void EnablePopit()
        {
            if (initialized)
            {
                for (int i = 0; i < popits.Length; i++)
                {
                    popits[i].gameObject.SetActive(true);
                }
                count = 0;
            }
        }

        private IEnumerator ResetBlends()
        {
            print("Reset Blends");
            Popit2DManager.Instance.ResetPopitsShowads();
            var rand = new System.Random();
            var shuffled = popits.OrderBy(p => rand.Next());
            int i = 1;
            int count = popits.Length / 5;
            yield return new WaitForSeconds(0.5f);
            foreach (Collider2D popits in shuffled)
            {
                if (i % count == 0)
                {
                    SoundManager.Instance.PlayRandomAudio();
                    yield return new WaitForSeconds(0.1f);
                }
                i++;
                popits.gameObject.SetActive(true);
            }

            print("Reset");
            this.count = 0;
        }



        public void ShowInterstitialAD()
        {
            //if (GMAdsManager.Instance)
            //    GMAdsManager.Instance.Show_Interstitial();
            //Debug.Log("Ad showed");
        }
    }
}