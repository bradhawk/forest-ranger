using UnityEngine;
using System.Collections;

public class VirtualDirectionalControl : MonoBehaviour {

	public Texture controlGraphics;
	public float relativeSize = 3f;
	public float relativeLeft = 20f;
	public float relativeBottom = 20f;

	private float size;
	private float topPos;
	private float leftPos;
	private float boxSize;
	private Vector2 pos;

	private float lastScreenWidth;
	private float lastScreenHeight;

	private Rect[] collisionRect = new Rect[9];
	private Rect imageRect;
	
	public const int DIRECTION_UP_LEFT = 1;
	public const int DIRECTION_UP = 2;
	public const int DIRECTION_UP_RIGHT = 3;
	public const int DIRECTION_LEFT = 4;
	public const int DIRECTION_MIDDLE = 5;
	public const int DIRECTION_RIGHT = 6;
	public const int DIRECTION_DOWN_LEFT = 7;
	public const int DIRECTION_DOWN = 8;
	public const int DIRECTION_DOWN_RIGHT = 9;

	// Use this for initialization
	void Start () {
		lastScreenHeight = Screen.height;
		lastScreenWidth = Screen.width;
		
		imageRect = new Rect ();
		for (int i = 0; i < collisionRect.Length; i++) {
			collisionRect [i] = new Rect ();
		}

		size = Screen.height / relativeSize;
		topPos = (Screen.height - size) - (Screen.width / relativeBottom);
		leftPos = Screen.width / relativeLeft;
		boxSize = size / 3f;
		int index = 0;
		for(int i = 0; i < 3; i++) {
			for(int j = 0; j < 3; j++) {
				float boxLeftPos = leftPos + (j * boxSize);
				float boxTopPos = topPos + (i * boxSize);
				collisionRect[index].Set (boxLeftPos, boxTopPos, boxSize, boxSize);
				index++;
			}
		}
		imageRect.Set (leftPos, topPos, size, size);
	}
	
	// Update is called once per frame
	void Update () {
		if (lastScreenHeight != Screen.height || lastScreenWidth != Screen.width) {
			lastScreenHeight = Screen.height;
			lastScreenWidth = Screen.width;
			// TODO: call when size changed
			size = Screen.height / relativeSize;
			topPos = (Screen.height - size) - (Screen.width / relativeBottom);
			leftPos = Screen.width / relativeLeft;
			boxSize = size / 3f;
			int index = 0;
			for(int i = 0; i < 3; i++) {
				for(int j = 0; j < 3; j++) {
					float boxLeftPos = leftPos + (j * boxSize);
					float boxTopPos = topPos + (i * boxSize);
					collisionRect[index].Set (boxLeftPos, boxTopPos, boxSize, boxSize);
					index++;
				}
			}
			imageRect.Set (leftPos, topPos, size, size);
		}

	}

	// Generate GUI On Screen
	void OnGUI() {
		if(!controlGraphics) {
			return;
		}
		GUI.DrawTexture (imageRect, controlGraphics, ScaleMode.StretchToFill, true);
	}

	// Get direction, compare with DIRECTION_ECT
	public int GetDirection() {
		if (Application.platform == RuntimePlatform.Android) {
			pos = GetTouchPosition();
		} else {
			pos = GetMouseClickPosition();
		}
		if (pos != Vector2.zero) {
			for (int i = 0; i < collisionRect.Length; i++) {
				if (collisionRect [i].Contains (pos)) {
					return i + 1;
				}
			}
		}
		return 5;
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
			if(imageRect.Contains(touchPos)) {
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
