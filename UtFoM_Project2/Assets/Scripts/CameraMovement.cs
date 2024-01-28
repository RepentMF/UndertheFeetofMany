using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
	public Transform target;
	public float smoothing;
	public Vector2 maxPosition;
	public Vector2 minPosition;
	public Vector2 difference;

	void Awake()
	{
		target = GameObject.FindWithTag("P1").transform;
    	difference.x = maxPosition.x - minPosition.x;
    	difference.y = maxPosition.y - minPosition.y;
	}

    // Update is called once per frame
    void LateUpdate()
    {	
		bool startTransition = false;
		foreach (SceneTransition st in FindObjectsOfType<SceneTransition>())
		{
			if (st.startTransition)
			{
				target = this.gameObject.transform;
				target.transform.position = this.gameObject.transform.position;
				startTransition = true;
			}
		}
		
		if (!startTransition)
    	{
			target = GameObject.FindWithTag("P1").transform;
    	}

		if (transform.position != target.position && !startTransition)
		{
			Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
			targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
			targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
			transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
		}
	}
}
