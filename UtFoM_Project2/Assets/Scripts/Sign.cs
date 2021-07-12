using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
	InputController controls;

	public Signal contextOn;
	public Signal contextOff;
	public GameObject box;
	public Text dialogue;
	public string insert;
	public bool active;
	public bool obtained;
	public Animator animator;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player") && collision.isTrigger)
		{
			contextOn.Raise();
			active = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("Player") && collision.isTrigger) 
		{
			contextOff.Raise();
			active = false;
			box.SetActive(false);

			if(dialogue.text == insert)
			{
				collision.GetComponent<Inventory>().AddItem(GetComponentInChildren<Item>());
				GetComponentInChildren<Item>().gameObject.SetActive(false);
				animator.Play("item_holder_voip");
				obtained = true;

				//this.gameObject.SetActive(false);
			}
		}
	}

	void Awake()
	{
		controls = new InputController();
		controls.Enable();
	}

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        obtained = false;
    }

    // Update is called once per frame
    void Update()
    {
		if(controls.Player.ContextConfirm.triggered && active) 
		{
			box.SetActive(true);
			dialogue.text = insert;
		}

		if(obtained);
		{
			this.gameObject.SetActive(false);
		}
    }
}
