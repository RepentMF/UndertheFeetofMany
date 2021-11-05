using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

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
		string[] strings = System.IO.File.ReadAllLines(@"treasures.txt");
		int found = 0;

		foreach(string s in strings)
		{
			for(int i = 0; i < FindObjectsOfType<Sign>(true).Length; i++)
			{
				if(s.Contains(FindObjectsOfType<Sign>(true)[i].ID + ", "))
				{
					found = s.IndexOf(", ");
					FindObjectsOfType<Sign>(true)[i].obtained = bool.Parse(s.Substring(found + 2));
				}
			}	
		}
	}

	//Take treasure information from the current scene and put it in our data structure
	void GetTreasureInRoom()
	{
		List<string> strings = new List<string>();

		for(int i = 0; i < FindObjectsOfType<Sign>(true).Length; i++)
		{
			//Grab item holder data
			string s = FindObjectsOfType<Sign>(true)[i].ID + ", " + FindObjectsOfType<Sign>(true)[i].obtained;
			//Put item holder data in a list of strings
			strings.Add(s);
		}
		
		string[] stringsArray = System.IO.File.ReadAllLines(@"treasures.txt");
		List<string> stringsList = stringsArray.ToList();
		//Write the strings to a JSON file
		foreach(string s in stringsArray)
		{
			for(int i = 0; i < FindObjectsOfType<Sign>(true).Length; i++)
			{
				if(s.Contains(FindObjectsOfType<Sign>(true)[i].ID + ", "))
				{
					stringsList.Remove(s);
				}
			}
		}

		System.IO.File.WriteAllLines(@"treasures.txt", stringsList);
		System.IO.File.AppendAllLines(@"treasures.txt", strings);
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
    	if(sceneChange)
    	{
    		sceneChange = false;
    		GetTreasureInRoom();
    	}
    	else if(index != SceneManager.GetActiveScene().buildIndex)
    	{
     		index = SceneManager.GetActiveScene().buildIndex;
			SetTreasureInRoom();
    	}
    }
}