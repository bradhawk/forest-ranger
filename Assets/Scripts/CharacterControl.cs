using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {

	public float moveSpeed = 10f;
	public GameObject leftJaring;
    public GameObject rightJaring;
    public GameObject topJaring;
    public GameObject bottomJaring;
	public float bulletDuration = 2f;
	public float bulletSpeed = 10f;
	public float freezeTime = 2f;
    public float cooldownTime = 2f;
	public VirtualDirectionalControl dirControl;
	public VirtualButton actionButton;

	private float savedFreezeTime;
	private bool isFiring;

    private float savedCooldownTime;
    private bool canCast;

	private Rigidbody2D mainRigid;
	private Vector2 moveDir;
    
    private int direction;
    private bool isWalking;

    private Animator animator;

	// Use this for initialization
	void Start () {
		mainRigid = gameObject.GetComponent<Rigidbody2D>();
        SpriteRenderer lRender = leftJaring.GetComponent<SpriteRenderer>();
        SpriteRenderer rRender = rightJaring.GetComponent<SpriteRenderer>();
        SpriteRenderer tRender = topJaring.GetComponent<SpriteRenderer>();
        SpriteRenderer bRender = bottomJaring.GetComponent<SpriteRenderer>();
        lRender.enabled = false;
        rRender.enabled = false;
        tRender.enabled = false;
        bRender.enabled = false;
        moveDir = new Vector2 ();
		isFiring = false;
        canCast = true;
		savedFreezeTime = freezeTime;
        savedCooldownTime = cooldownTime;

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
        if (!canCast)
        {
            cooldownTime -= Time.deltaTime;
            if (cooldownTime <= 0.0f)
            {
                canCast = true;
                cooldownTime = savedCooldownTime;
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

		if(actionButton.GetPressedDown() && canCast) {
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
        canCast = false;
        GameObject projObject;
        SpriteRenderer projRender;
        if (direction == 2)
        {
            projObject = Instantiate(topJaring, new Vector3(gameObject.transform.position.x,
                                                            gameObject.transform.position.y,
                                                            gameObject.transform.position.z),
                                     Quaternion.identity) as GameObject;
            projRender = projObject.GetComponent<SpriteRenderer>();
            projObject.transform.position = projObject.transform.position + new Vector3(0f, projRender.bounds.extents.y / 2f, 0f);
            Rigidbody2D bRigid = projObject.GetComponent<Rigidbody2D>();
            bRigid.velocity = new Vector2(0 * bulletSpeed, 1 * bulletSpeed);
        }
        else if (direction == 4)
        {
            projObject = Instantiate(leftJaring, new Vector3(gameObject.transform.position.x,
                                                             gameObject.transform.position.y,
                                                             gameObject.transform.position.z),
                                     Quaternion.identity) as GameObject;
            projRender = projObject.GetComponent<SpriteRenderer>();
            projObject.transform.position = projObject.transform.position - new Vector3(projRender.bounds.extents.x / 2f, 0f, 0f);
            Rigidbody2D bRigid = projObject.GetComponent<Rigidbody2D>();
            bRigid.velocity = new Vector2(-1 * bulletSpeed, 0 * bulletSpeed);
        }
        else if (direction == 6)
        {
            projObject = Instantiate(rightJaring, new Vector3(gameObject.transform.position.x,
                                                              gameObject.transform.position.y,
                                                              gameObject.transform.position.z),
                                     Quaternion.identity) as GameObject;
            projRender = projObject.GetComponent<SpriteRenderer>();
            projObject.transform.position = projObject.transform.position + new Vector3(projRender.bounds.extents.x / 2f, 0f, 0f);
            Rigidbody2D bRigid = projObject.GetComponent<Rigidbody2D>();
            bRigid.velocity = new Vector2(1 * bulletSpeed, 0 * bulletSpeed);
        }
        else
        {
            projObject = Instantiate(bottomJaring, new Vector3(gameObject.transform.position.x,
                                                               gameObject.transform.position.y,
                                                               gameObject.transform.position.z),
                                     Quaternion.identity) as GameObject;
            projRender = projObject.GetComponent<SpriteRenderer>();
            projObject.transform.position = projObject.transform.position - new Vector3(0f, projRender.bounds.extents.y / 2f, 0f);
            Rigidbody2D bRigid = projObject.GetComponent<Rigidbody2D>();
            bRigid.velocity = new Vector2(0 * bulletSpeed, -1 * bulletSpeed);
        }
        projRender.enabled = true;
        Destroy(projObject, bulletDuration);
	}
}