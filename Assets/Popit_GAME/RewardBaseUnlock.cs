using System;
using UnityEngine;

namespace GameWork.Scripts
{
    public class RewardBaseUnlock : MonoBehaviour
    {
        public string modeId;

        public int id;

        public bool isManualIdSelect;
        public bool isLocked;
        public GameObject lockedObj;
        public Vector3 position;
        public Vector3 scale;


        const string prefkey = "locked";
        GameObject spawnADPrefab;

        private void Start()
        {
          
            if (!isManualIdSelect)
            {
               id = transform.GetSiblingIndex();

            }
            string key= prefkey + modeId + id;

            if(PlayerPrefs.HasKey(key))
                isLocked = PlayerPrefs.GetInt(key) == 0 ? true : false;

            //Debug.Log(isLocked);


            if (isLocked)
            {
                spawnADPrefab = Instantiate(lockedObj,transform.position,Quaternion.identity);               
                spawnADPrefab.transform.SetParent(transform);
                //spawnADPrefab.transform.localScale = Vector3.one;
                spawnADPrefab.transform.localScale = scale;
                spawnADPrefab.transform.localPosition = position;
              
            }
           

        }
       
        //private void Update()
        //{

        //    if(!PlayerPrefs.HasKey(prefkey + modeId + id))
        //        PlayerPrefs.SetInt(prefkey + id,lockedFound);
        //    else
        //        lockedFound = PlayerPrefs.GetInt(prefkey + id);

        //    lockedObj.gameObject.SetActive(lockedFound != 1);
        //}

        public void UnlockedLevel()
        {
            PlayerPrefs.SetInt(prefkey + modeId+id,1);
            isLocked = false;
            spawnADPrefab.gameObject.SetActive(false);
        }
    }
}
