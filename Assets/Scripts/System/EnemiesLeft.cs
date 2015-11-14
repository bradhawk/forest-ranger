using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemiesLeft : MonoBehaviour {

    public Text textField;
    private int enemiesLeft;
    private int maxEnemies;
    
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (textField)
        {
            textField.text = enemiesLeft + "/" + maxEnemies;
        }	
	}

    public void instantiateEnemy()
    {
        maxEnemies++;
        enemiesLeft = maxEnemies;
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
