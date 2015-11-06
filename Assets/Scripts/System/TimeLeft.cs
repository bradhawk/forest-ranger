using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeLeft : MonoBehaviour {

    public float maxTimeInSecs;
    public Text textFeild;
    private float currentTimeInSecs;

	// Use this for initialization
	void Start () {
        currentTimeInSecs = maxTimeInSecs;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentTimeInSecs > 0)
        {
            currentTimeInSecs -= Time.deltaTime;
            if (textFeild)
            {
                int currentTime = (int)currentTimeInSecs;
                int currentSecs = currentTime % 60;
                string secs;
                if (currentSecs < 10)
                {
                    secs = "0" + currentSecs;
                }
                else
                {
                    secs = "" + currentSecs;
                }
                currentTime /= 60;
                textFeild.text = currentTime + ":" + secs;
            }
        }
	}
}
