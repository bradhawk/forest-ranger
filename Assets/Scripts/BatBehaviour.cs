using UnityEngine;
using System.Collections;

public class BatBehaviour : MonoBehaviour {

    public float speed = 1f;
    public bool verticalMove;
    public bool horizontalMove;
    private float xMove = 1f;
    private float yMove = 1f;
    private float xDir = 1f;
    private float yDir = 1f;
    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (verticalMove)
        {
            yMove = 1f * speed * yDir;
        }
        else
        {
            yMove = 0f;
        }

        if (horizontalMove)
        {
            xMove = 1f * speed * xDir;
        }
        else
        {
            xMove = 0f;
        }
        rigidBody.velocity = new Vector2(xMove, yMove);
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Terrain")
        {
            xDir *= -1f;
            yDir *= -1f;
        }
    }
}
