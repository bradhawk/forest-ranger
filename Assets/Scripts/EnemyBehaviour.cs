using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public EnemiesLeft enemiesLeft;
    public Transform kenaJaring;
    private bool kenaTangkap;

	// Use this for initialization
	void Start () {
        enemiesLeft.instantiateEnemy();
        kenaJaring = transform.FindChild("JaringKena");
        kenaJaring.gameObject.SetActive(false);
        kenaTangkap = false;
	}
	
	// Update is called once per frame
	void Update () {
        kenaJaring.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 10;
	}

    // Called when something collides
    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Projectile" && !kenaTangkap) 
        {
            enemiesLeft.decreaseEnemy();
            Destroy(coll.gameObject);
            kenaJaring.gameObject.SetActive(true);
            kenaTangkap = true;
        }
    }
}
