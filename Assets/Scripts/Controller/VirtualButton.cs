using UnityEngine;
using System.Collections;

public class VirtualButton : MonoBehaviour {

	public Texture buttonGraphics;
	public float relativeSize = 6f;
	public float relativeRight = 20f;
	public float relativeBottom = 20f;
    public bool isEnabled = true;

	private float size;
	private float topPos;
	private float leftPos;
	
	private float lastScreenWidth;
	private float lastScreenHeight;

	private Rect buttonRect;
	private Vector2 pos;

	private bool isPressed = false;

	// Use this for initialization
	void Start () {
		lastScreenHeight = Screen.height;
		lastScreenWidth = Screen.width;

		buttonRect = new Rect ();

		size = Screen.height / relativeSize;
		topPos = (Screen.height - size) - (Screen.width / relativeBottom);
		leftPos = (Screen.width - size) - (Screen.width / relativeRight);
		buttonRect.Set (leftPos, topPos, size, size);
	}
	
	// Update is called once per frame
	void Update () {
        if (isEnabled)
        {
            if (lastScreenHeight != Screen.height || lastScreenWidth != Screen.width)
            {
                lastScreenHeight = Screen.height;
                lastScreenWidth = Screen.width;
                //TODO: updated when screen size changed
                size = Screen.height / relativeSize;
                topPos = (Screen.height - size) - (Screen.width / relativeBottom);
                leftPos = (Screen.width - size) - (Screen.width / relativeRight);
                buttonRect.Set(leftPos, topPos, size, size);
            }
            if (isPressed && !GetPressed())
            {
                isPressed = false;
            }
        }
	}

	void OnGUI() {
        if (!buttonGraphics || !isEnabled)
        {
			return;
		}
		GUI.DrawTexture (buttonRect, buttonGraphics, ScaleMode.StretchToFill, true);
	}

	public bool GetPressed() {
		if (Application.platform == RuntimePlatform.Android) {
			pos = GetTouchPosition();
		} else {
			pos = GetMouseClickPosition();
		}

		if (pos != Vector2.zero) {
			if (buttonRect.Contains (pos)) {
				return true;
			}
		}
		return false;
	}

	public bool GetPressedDown() {
		if (!isPressed) {
			if(GetPressed()) {
				isPressed = true;
				return true;
			}
		}
		return false;
	}

	// for mobile user, get touch position
	Vector2 GetTouchPosition() {
		int touchCount = Input.touchCount;
		Touch checkTouch;
		Vector2 touchPos = new Vector2();
		for (int i = 0; i < touchCount; i++) {
			checkTouch = Input.GetTouch(i);
			touchPos.x = checkTouch.position.x;
			touchPos.y = Screen.height - checkTouch.position.y;
			if(buttonRect.Contains(touchPos)) {
				return touchPos;
			}
		}
		return Vector2.zero;
	}
	
	// for debugging purpose, get mouse on click
	Vector2 GetMouseClickPosition() {
		if (Input.GetMouseButton (0)) {
			Vector2 mousePos = new Vector2();
			mousePos.x = Input.mousePosition.x;
			mousePos.y = Screen.height - Input.mousePosition.y;
			return mousePos;
		}
		return Vector2.zero;
	}
}
