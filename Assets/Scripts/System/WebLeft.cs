using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WebLeft : MonoBehaviour {

    public int maxWebs;
    public Text textField;
    private int websLeft;

	// Use this for initialization
	void Start () {
        websLeft = maxWebs;
	}
	
	// Update is called once per frame
	void Update () {
        if (textField)
        {
            textField.text = websLeft + "/" + maxWebs;
        }
	}

    public void decreaseWeb()
    {
        websLeft--;
    }

    public bool isEmpty()
    {
        return websLeft == 0;
    }
}
