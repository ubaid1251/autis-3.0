using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Piano
{
    public class GameController : MonoBehaviour
    {
        public static string temp;
        public static GameObject tempholder, tempsleepSceneObj;
        public GameObject defaultObj, myObj;

        private void Start()
        {
            if (temp == null)
            {
                //print("Value of temp " + temp);
                temp = defaultObj.name;
            }
            else
            {
                GameObject obj = GameObject.Find(temp);
                if (obj)
                    if (obj.name == temp)
                    {
                        print("object found! " + temp);
                        //  obj.GetComponent<GameController>().myObj.SetActive(true);
                    }
            }
            //print(temp);
        }

        private void OnMouseDown()
        {
            //print("button clicked! "+gameObject.name);

            if (temp != myObj.name)
            {
                temp = myObj.name;
            }

            SceneManager.LoadScene("2dance scene");
        }
    }
}