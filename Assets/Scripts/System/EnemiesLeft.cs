using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemiesLeft : MonoBehaviour {

    public int maxEnemies;
    public Text textField;
    private int enemiesLeft;

	// Use this for initialization
	void Start ()
    {
        enemiesLeft = maxEnemies;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (textField)
        {
            textField.text = enemiesLeft + "/" + maxEnemies;
        }	
	}

    public void decreaseEnemy()
    {
        enemiesLeft--;
    }

    public bool isEmpty()
    {
        return enemiesLeft == 0;
    }
}
