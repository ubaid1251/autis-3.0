using UnityEngine;

namespace Rainbow
{
    public class GlobalVars : MonoBehaviour
    {
        public static GlobalVars Instance;
        public int TotalCoins
        {
            get
            {
                return PlayerPrefs.GetInt("Coins" , 0);
            }
            set
            {
                PlayerPrefs.SetInt("Coins" , value);
            }
        }

        public int GetLevelCoins(string level)
        {
            return PlayerPrefs.GetInt("Coins" + level);
        }

        public void SetLevelCoins(string level , int value)
        {
            PlayerPrefs.SetInt("Coins" + level , value);
        }


        public bool Sound
        {
            get
            {
                return PlayerPrefs.GetInt("Sound" , 0) == 0;
            }
            set
            {

                PlayerPrefs.SetInt("Sound" , value ? 0 : 1);
            }
        }

        public bool Vibration
        {
            get
            {
                return PlayerPrefs.GetInt("Vibration" , 0) == 0;
            }
            set
            {

                PlayerPrefs.SetInt("Vibration" , value ? 0 : 1);
            }
        }

        public bool Rated
        {
            get
            {
                return PlayerPrefs.GetInt("Rated" , 0) == 0;
            }
            set
            {

                PlayerPrefs.SetInt("Rated" , value ? 0 : 1);
            }
        }

        public int RateCount
        {
            get
            {
                return PlayerPrefs.GetInt("RateCount");
            }
            set
            {
                PlayerPrefs.SetInt("RateCount" , value);
            }
        }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
    }
}