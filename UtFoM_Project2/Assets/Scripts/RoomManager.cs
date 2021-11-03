using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
	private SceneManager sceneManager;
	public List<List<bool>> listHolder = new List<List<bool>>(38);
	public List<bool> treasureHolder;
	public int index;

	void SetTreasureInRoom(List<bool> list, int sceneNum)
	{
		for(int i = 0; i < FindObjectsOfType<Sign>(true).Length; i++)
		{
			FindObjectsOfType<Sign>()[i].obtained = treasureHolder[i];
		}
	}

	void GetTreasureInRoom()
	{
		Sign[] test1 = FindObjectsOfType<Sign>();
		List<bool> boolHolder = new List<bool>(test1.Length); 
		for(int i = 0; i < test1.Length; i++)
		{
			boolHolder.Add(test1[i].obtained);
		}

		listHolder.Add(boolHolder);
	}

    // Start is called before the first frame update
    void Start()
    {
    	//GetRoom();
    	// boolHolder2 = new List<bool>(3);
    	// boolHolder2.Add(false);
    	// boolHolder2.Add(true);
    	// boolHolder2.Add(true);
    	// listHolder.Add(boolHolder2);
    	if(index != SceneManager.GetActiveScene().buildIndex)
    	{
    		index = SceneManager.GetActiveScene().buildIndex;
    		SetTreasureInRoom(listHolder[index], index);
    	}

    	foreach(List<bool> list in listHolder)
    	{
    		foreach(bool boolean in list)
    		{
    			Debug.Log(boolean);
    		}
    	}

    	SetTreasureInRoom(treasureHolder, SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
    	
    }
}