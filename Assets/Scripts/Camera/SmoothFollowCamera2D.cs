using UnityEngine;
using System.Collections;

public class SmoothFollowCamera2D : MonoBehaviour {

	public GameObject target;
    private Transform targetTransform;
	private Vector3 velocity = Vector3.zero;

    public GameObject stage;
    private SpriteRenderer stageSpriteRenderer;
    private MeshRenderer stageMeshRenderer;
    private Transform stageTransform;

    public float aspectRatio;

	public float smoothTime = 0.15f;

	public bool verticalMaxEnabled = false;
	public float verticalMax = 0f;
	public bool verticalMinEnabled = false;
	public float verticalMin = 0f;

	public bool horizontalMaxEnabled = false;
	public float horizontalMax = 0f;
	public bool horizontalMinEnabled = false;
	public float horizontalMin = 0f;

    void Start()
    {
        if (target)
        {
            targetTransform = target.GetComponent<Transform>();

            if (stage)
            {
                float vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
                float horzExtent;
                if (aspectRatio == 0f)
                {
                    horzExtent = vertExtent * Screen.width / Screen.height;
                }
                else
                {
                    horzExtent = vertExtent * aspectRatio;
                }

                Vector3 finalBounds;
                if (stage.GetComponent<MeshRenderer>() == null)
                {
                    stageSpriteRenderer = stage.GetComponent<SpriteRenderer>();
                    stageTransform = stage.GetComponent<Transform>();

                    finalBounds = Vector3.Scale(stageSpriteRenderer.sprite.bounds.size, stageTransform.localScale);

                    verticalMin = (float)((finalBounds.y / 2f) - vertExtent) * -1f;
                    verticalMax = (float)((finalBounds.y / 2f) - vertExtent);
                    horizontalMin = (float)((finalBounds.x / 2f) - horzExtent) * -1f;
                    horizontalMax = (float)((finalBounds.x / 2f) - horzExtent);
                }
                else
                {
                    stageMeshRenderer = stage.GetComponent<MeshRenderer>();
                    stageTransform = stage.GetComponent<Transform>();

                    finalBounds = Vector3.Scale(stageMeshRenderer.bounds.size, stageTransform.localScale);

                    verticalMin = (float)(((finalBounds.y / 2f) - vertExtent) * -1f) - stageMeshRenderer.bounds.extents.y;
                    verticalMax = (float)(((finalBounds.y / 2f) - vertExtent)) - stageMeshRenderer.bounds.extents.y;
                    horizontalMin = (float)(((finalBounds.x / 2f) - horzExtent) * -1f) + stageMeshRenderer.bounds.extents.x;
                    horizontalMax = (float)(((finalBounds.x / 2f) - horzExtent)) + stageMeshRenderer.bounds.extents.x;
                }
            }
        }       
    }

	void Update () {
		if (target) {
			Vector3 targetPosition = targetTransform.position;

			if (verticalMinEnabled && verticalMaxEnabled) {
				targetPosition.y = Mathf.Clamp(targetTransform.position.y, verticalMin, verticalMax);
			} else if (verticalMinEnabled) {
				targetPosition.y = Mathf.Clamp(targetTransform.position.y, verticalMin, targetTransform.position.y);
			} else if (verticalMaxEnabled) {
				targetPosition.y = Mathf.Clamp(targetTransform.position.y, targetTransform.position.y, verticalMax);
			}

			if (horizontalMinEnabled && horizontalMaxEnabled) {
				targetPosition.x = Mathf.Clamp(targetTransform.position.x, horizontalMin, horizontalMax);
			} else if (horizontalMinEnabled) {
				targetPosition.x = Mathf.Clamp(targetTransform.position.x, horizontalMin, targetTransform.position.x);
			} else if (horizontalMaxEnabled) {
				targetPosition.x = Mathf.Clamp(targetTransform.position.x, targetTransform.position.x, horizontalMax);
			}

			targetPosition.z = transform.position.z;

			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
		}
	}
}