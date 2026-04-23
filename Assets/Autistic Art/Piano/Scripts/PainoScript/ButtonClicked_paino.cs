using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonClicked_paino : MonoBehaviour {
	public GameObject TragetObject = null;
	public  string SendMessageToTraget;
	public int IntParameter = -1;
	public string StringParameter="None";
	public GameObject IndicatorOff;
	public Vector3 scale=new Vector3(1,1,1);
	Vector3 scaleintVal;
	// Update is called once per frame
	void Update () {
        if (GetComponent<BoxCollider2D>())
        {
            if (Utility.getTouched_Phase2D(0) == TouchPhase.Began)
            {
                if (Utility.isClicked_2D(GetComponent<BoxCollider2D>()))
                {
                    if(SceneManager.GetActiveScene().name=="sleeping scene"&&gameObject.name!= "lock banner")
                    {
                        //LullabiesSelection.LullabiisNum = IntParameter;
                    }
                    scaleintVal = gameObject.transform.localScale;
                    Invoke("ScaleBack", .1f);
                    transform.localScale = scale;
                    if (IndicatorOff != null)
                    {
                        IndicatorOff.SetActive(false);
                    }
                    if (TragetObject != null)
                    {
                        if (IntParameter != -1)
                        {
                            TragetObject.SendMessage(SendMessageToTraget, IntParameter);
                        }
                        else if (StringParameter != "None")
                        {
                            TragetObject.SendMessage(SendMessageToTraget, StringParameter);
                        }
                        else
                        {
                            TragetObject.SendMessage(SendMessageToTraget);
                        }
                    }
                }
            }
        }
        if (GetComponent<CircleCollider2D>()) {
            if (Utility.getTouched_Phase2D(0) == TouchPhase.Began)
            {
                if (Utility.isClicked_2D(GetComponent<CircleCollider2D>()))
                {
                    scaleintVal = gameObject.transform.localScale;
                    Invoke("ScaleBack", .1f);
                    transform.localScale = scale;
                    if (IndicatorOff != null)
                    {
                        IndicatorOff.SetActive(false);
                    }
                    if (TragetObject != null)
                    {
                        if (IntParameter != -1)
                        {
                            TragetObject.SendMessage(SendMessageToTraget, IntParameter);
                        }
                        else if (StringParameter != "None")
                        {
                            TragetObject.SendMessage(SendMessageToTraget, StringParameter);
                        }
                        else
                        {
                            TragetObject.SendMessage(SendMessageToTraget);
                        }
                    }
                }
            }

        }

	}
	void ScaleBack(){
		gameObject.transform.localScale = scaleintVal;
	}
}
