using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {

	public float moveSpeed = 10f;
	public GameObject bulletObject;
	public float bulletDuration = 2f;
	public float bulletSpeed = 10f;
	public float freezeTime = 2f;
	public VirtualDirectionalControl dirControl;
	public VirtualButton actionButton;

	private float savedFreezeTime;
	private bool isFiring;

	private Rigidbody2D mainRigid;
	private Vector2 moveDir;
    
    private int direction;
    private bool isWalking;

    private Animator animator;

	// Use this for initialization
	void Start () {
		mainRigid = gameObject.GetComponent<Rigidbody2D>();
		SpriteRenderer bulletSprite = bulletObject.GetComponent<SpriteRenderer>();

		moveDir = new Vector2 ();
		bulletSprite.enabled = false;
		isFiring = false;
		savedFreezeTime = freezeTime;

        direction = 2;
        isWalking = false;
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isFiring) {
			freezeTime -= Time.deltaTime;
			if(freezeTime <= 0.0f) {
				isFiring = false;
				freezeTime = savedFreezeTime;
			}
		}
	}

	void FixedUpdate() {
		if (!isFiring) {
			switch (dirControl.GetDirection ()) {
			case VirtualDirectionalControl.DIRECTION_UP:
				moveDir.x = 0;
				moveDir.y = 1;
                direction = 2;
                isWalking = true;
                
				break;
			case VirtualDirectionalControl.DIRECTION_DOWN:
				moveDir.x = 0;
				moveDir.y = -1;
                direction = 8;
                isWalking = true;
				break;
			case VirtualDirectionalControl.DIRECTION_LEFT:
				moveDir.x = -1;
				moveDir.y = 0;
                direction = 4;
                isWalking = true;
				break;
			case VirtualDirectionalControl.DIRECTION_RIGHT:
				moveDir.x = 1;
				moveDir.y = 0;
                direction = 6;
                isWalking = true;
				break;
			default:
				moveDir.x = 0;
				moveDir.y = 0;
                isWalking = false;
				break;
			}
		} else {
			moveDir.x = 0;
			moveDir.y = 0;
            isWalking = false;
		}
		Move (moveDir);

		if(actionButton.GetPressedDown()) {
			FireBullet();
		}

        animator.SetInteger("Direction", direction);
        animator.SetBool("isWalking", isWalking);
	}

	// call this method to move this gameObject with Vector2 parameter
	void Move(Vector2 movement) {
		mainRigid.velocity = new Vector2 (movement.x * moveSpeed, movement.y * moveSpeed);
		//float speed = Mathf.Abs (movement.x) + Mathf.Abs (movement.y);
		//float angle = Mathf.Atan2 (movement.y, movement.x) * Mathf.Rad2Deg;

		/*if (speed > 0.0f) {
			float turnSpeed = 5;
			gameObject.transform.rotation = Quaternion.Slerp (gameObject.transform.rotation,
			                                                  Quaternion.Euler (0, 0, angle), 
			                                                  turnSpeed * Time.deltaTime);
		}*/
	}

	// call this method to spawn bullet in the player, 
	// change bullet duration if you want longer duration of the bullet
	void FireBullet() {
		isFiring = true;
		GameObject bObject = Instantiate(bulletObject, new Vector3(gameObject.transform.position.x, 
		                                                           gameObject.transform.position.y, 
		                                                           gameObject.transform.position.z),
		                                 Quaternion.identity) as GameObject;
		Rigidbody2D bRigid = bObject.GetComponent<Rigidbody2D>();
		Physics2D.IgnoreCollision (gameObject.GetComponent<Collider2D> (), bObject.GetComponent<Collider2D> ());
		SpriteRenderer bRender = bObject.GetComponent<SpriteRenderer>();
		float rotation = gameObject.transform.rotation.eulerAngles.z;
		float x = Mathf.Cos(rotation * Mathf.Deg2Rad);
		float y = Mathf.Sin(rotation * Mathf.Deg2Rad);
		bObject.transform.rotation = Quaternion.AngleAxis (rotation, new Vector3 (0, 0, 1));
		bRigid.velocity = new Vector2 (x * bulletSpeed, y * bulletSpeed);
		bRender.enabled = true;
		Destroy(bObject, bulletDuration);

	}
}