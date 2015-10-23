using UnityEngine;
using System.Collections;

public class DepthBehaviour : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    // Awake Method
    void Awake() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Late update method
    void LateUpdate() {
        spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y * -1;
    }
}
