using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Vector2 difference;
	public Transform target;
	public float smoothing;
	public float oldSmoothing;
	public Vector2 camChange;
	public Vector2 maxPosition;
	public Vector2 minPosition;

	void CamChange()
	{
		if(target.position.x > maxPosition.x || target.position.y > maxPosition.y)
		{
			maxPosition += camChange;
			minPosition += camChange;
		}
		if(target.position.x < minPosition.x || target.position.y < minPosition.y)
		{
			maxPosition -= camChange;
			minPosition -= camChange;
		}
	}

	void Awake()
	{
		target = GameObject.FindWithTag("P1").transform;
	}

    // Start is called before the first frame update
    void Start()
    {
		//transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
		oldSmoothing = smoothing;
		CamChange();
    }

	private Vector3 RoundPosition(Vector3 position)
	{
		float xOffset = position.x % .0625f;

		if(xOffset != 0)
		{
			position.x -= xOffset;
		}

		float yOffset = position.y % .0625f;

		if(yOffset != 0)
		{
			position.y -= yOffset;
		}

		return position;
	}

    // Update is called once per frame
    void LateUpdate()
    {
    	if(target == null)
    	{
			target = GameObject.FindWithTag("P1").transform;
    	}
    	//CamChange();
    	difference.x = maxPosition.x - target.position.x;
    	difference.y = maxPosition.y - target.position.y;

		if(transform.position != target.position)
		{
			Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
			targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
			targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
			transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
		}
	}
}
