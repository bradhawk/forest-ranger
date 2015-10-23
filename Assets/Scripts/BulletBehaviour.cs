using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

	private Rigidbody2D bRigid;
	public float slowdownValue = 0.5f;

	// Use this for initialization
	void Start () {
		bRigid = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 velocity = bRigid.velocity;
		bRigid.velocity = new Vector2 (velocity.x * slowdownValue, velocity.y * slowdownValue);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//bRigid.velocity = new Vector2 (0f, 0f);
	}
}
