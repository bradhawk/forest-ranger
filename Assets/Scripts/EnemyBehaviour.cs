using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public EnemiesLeft enemiesLeft;

	// Use this for initialization
	void Start () {
        enemiesLeft.instantiateEnemy();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Called when something collides
    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Projectile") {
            enemiesLeft.decreaseEnemy();
            Destroy(gameObject);
        }
    }
}
