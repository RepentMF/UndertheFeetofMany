﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Testing git
public class RoomMove : MonoBehaviour
{
	public Vector2 cameraChange;
	public Vector3 playerChange;
	private CameraMovement cam;
	public bool needText;
	public string placeName;
	public GameObject text;
	public Text placeText;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			cam.minPosition += cameraChange;
			cam.maxPosition += cameraChange;
			other.transform.position += playerChange;
			if (needText)
			{
				StartCoroutine(placeNameCo());
			}
		}
	}

	private IEnumerator placeNameCo()
	{
		text.SetActive(true);
		placeText.text = placeName;
		yield return new WaitForSeconds(4f);
		text.SetActive(false);
	}

    // Start is called before the first frame update
    void Start()
    {
		cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
