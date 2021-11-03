using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class RoomManager : MonoBehaviour
{
	public static RoomManager RM;

	private SceneManager sceneManager;
	public List<List<bool>> treasureListHolder = new List<List<bool>>();
	public List<bool> treasureBoolHolder;
	public int index = 0;
	public bool sceneChange = false;

	//Take treasure information from the data of our data structure and set up the current scene
	void SetTreasureInRoom()
	{
		string json = JsonUtility.ToJson(treasureBoolHolder);
		Debug.Log(json);
		//Debug.Break();
		for(int i = 0; i < FindObjectsOfType<Sign>(true).Length; i++)
		{
			//Debug.Log(FindObjectsOfType<Sign>().Length);
			if(FindObjectsOfType<Sign>(true) != null)
			{
				FindObjectsOfType<Sign>(true)[i].obtained = treasureBoolHolder[i];
			}
		}
	}

	//Take treasure information from the current scene and put it in our data structure
	void GetTreasureInRoom()
	{
		List<bool> boolHolder = new List<bool>(FindObjectsOfType<Sign>(true).Length);

		for(int i = 0; i < FindObjectsOfType<Sign>(true).Length; i++)
		{
			boolHolder.Add(FindObjectsOfType<Sign>(true)[i].obtained);
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
    		GetTreasureInRoom();

    		index = SceneManager.GetActiveScene().buildIndex;
			treasureBoolHolder = treasureListHolder[index];
    		SetTreasureInRoom();
    	}
    }
}