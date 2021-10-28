using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
	private SceneManager sceneManager;
	public List<List<bool>> listHolder;
	public List<bool> itemHolders0;

    // Start is called before the first frame update
    void Start()
    {
    	Debug.Log("this is being logged");
    }

    // Update is called once per frame
    void Update()
    {
    	// idk what i'm doing save me
    	// for(int i = 0; i < 38; i++)
    	// {
    	// 	if(SceneManager.GetActiveScene().buildIndex == i)
    	// 	{

    	// 	}
    	// }
    }
}