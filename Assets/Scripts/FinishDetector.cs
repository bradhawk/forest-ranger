using UnityEngine;
using System.Collections;

public class FinishDetector : MonoBehaviour {

    public EnemiesLeft enemiesLeft;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && enemiesLeft.isEmpty())
        {
            // TODO: Finish! back to menu. And complete.
        }
    }
}
