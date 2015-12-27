using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour {

    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject finishScreen;

    public VirtualButton virtualButton;
    public VirtualDirectionalControl virDirButton;
	
	public TimeLeft timeLeft;
	
	private bool isGameOver;

	// Use this for initialization
	void Start () {
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        finishScreen.SetActive(false);
		isGameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeLeft.isFinished)
		{
			gameOver();
		}
	}

    public void finishGame()
    {
        Time.timeScale = 0f;
        finishScreen.SetActive(true);
        virtualButton.isEnabled = false;
        virDirButton.isEnabled = false;
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

    public void restartLevel()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
    }
	
	public void gameOver()
	{
		if(!isGameOver)
		{
			gameOverScreen.SetActive(true);
			virtualButton.isEnabled = false;
			virDirButton.isEnabled = false;
		}
	}
}
