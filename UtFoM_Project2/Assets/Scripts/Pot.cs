using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
	private Animator animator;

	public void Smash()
	{
		animator.SetBool("smashed", true);
		StartCoroutine(breakCo ());
	}

	IEnumerator breakCo()
	{
		yield return new WaitForSeconds(.3f);
		this.gameObject.SetActive(false);
	}

    // Start is called before the first frame update
    void Start()
    {
		animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
