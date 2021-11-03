using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
	public static RoomManager RM;

	private SceneManager sceneManager;
	public List<List<bool>> treasureListHolder = new List<List<bool>>(38);
	public List<bool> treasureBoolHolder;
	public int index = 0;
	public bool sceneChange = false;

	//Take treasure information from the data of our data structure and set up the current scene
	void SetTreasureInRoom()
	{
		for(int i = 0; i < FindObjectsOfType<Sign>(true).Length; i++)
		{
			//Debug.Log(FindObjectsOfType<Sign>().Length);
			if(FindObjectsOfType<Sign>() != null)
			{
				//Debug.Log(i);
				FindObjectsOfType<Sign>()[i].obtained = treasureBoolHolder[i];
			}
		}
	}

	//Take treasure information from the current scene and put it in our data structure
	void GetTreasureInRoom()
	{
		List<bool> boolHolder = new List<bool>(FindObjectsOfType<Sign>().Length);

		for(int i = 0; i < FindObjectsOfType<Sign>().Length; i++)
		{
			boolHolder.Add(FindObjectsOfType<Sign>()[i].obtained);
		}

		treasureListHolder.Add(boolHolder);
	}

    // Start is called before the first frame update
    void Start()
    {
    	if(RM != null)
    	{
    		Destroy(this.gameObject);
    		return;
    	}

    	RM = this;
    	GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    	//index = SceneManager.GetActiveScene().buildIndex;
    	if(sceneChange)
    	{
    		GetTreasureInRoom();
    		sceneChange = false;
    	}
    	else if(index != SceneManager.GetActiveScene().buildIndex)
    	{
    		//Debug.Log(treasureListHolder[index]);
    		index = SceneManager.GetActiveScene().buildIndex;
			//treasureBoolHolder = treasureListHolder[index];
    		SetTreasureInRoom();
    	}
    }
}