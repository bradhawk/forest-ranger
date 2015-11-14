using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour {

    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    public VirtualButton virtualButton;
    public VirtualDirectionalControl virDirButton;

	// Use this for initialization
	void Start () {
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pauseGame()
    {
        Time.timeScale = 0f;
        virtualButton.isEnabled = false;
        virDirButton.isEnabled = false;
        pauseScreen.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        virtualButton.isEnabled = true;
        virDirButton.isEnabled = true;
        pauseScreen.SetActive(false);
    }
}
