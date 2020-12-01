using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
	public Signal contextOn;
	public Signal contextOff;
	public GameObject box;
	public Text dialogue;
	public string insert;
	public bool active;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			contextOn.Raise();
			active = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) 
		{
			contextOff.Raise();
			active = false;
			box.SetActive(false);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonDown("context") && active) 
		{
			if (box.activeInHierarchy) 
			{
				box.SetActive(false);
			} 
			else 
			{
				box.SetActive(true);
				dialogue.text = insert;
			}
		}
    }
}
