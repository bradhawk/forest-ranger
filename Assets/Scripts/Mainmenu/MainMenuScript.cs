using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public GameObject splashScreen;
    public GameObject waitScreen;
    public GameObject mainScreen;
    Image[] arrImage;

	// Use this for initialization
	void Start () {
        arrImage = mainScreen.GetComponentsInChildren<Image>();
        splashScreen.SetActive(true);
	    waitScreen.SetActive(false);
        mainScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onPushSplashScreen()
    {
        StartCoroutine(showMainMenu());
    }

    IEnumerator showMainMenu()
    {
        waitScreen.SetActive(true);
        splashScreen.GetComponent<Image>().CrossFadeAlpha(0f, 0.5f, false);
        yield return new WaitForSeconds(0.5f);
        splashScreen.SetActive(false);
        yield return new WaitForSeconds(1f);
        mainScreen.SetActive(true);
        for (int i = 0; i < arrImage.Length; i++)
        {
            arrImage[i].CrossFadeAlpha(0f, 0f, false);
        }

        for (int i = 0; i < arrImage.Length; i++)
        {
            arrImage[i].CrossFadeAlpha(1f, 0.5f, false);
        }
        yield return new WaitForSeconds(0.5f);
        waitScreen.SetActive(false);
    }
}
