using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Called when something collides
    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Projectile") {
            print("aw!");
            Destroy(gameObject);
        }
    }
}
