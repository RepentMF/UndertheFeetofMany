using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

[System.Serializable]
public class RoomManager : GenericSingleton<RoomManager>
{
    public static RoomManager RM;
    public Hashtable TreasureTable;
    public Hashtable EnemyTable;
    public Hashtable PuzzleTable;
    private SceneManager sceneManager;
    public int index = 0;
    public bool sceneChange = false;

    // public void UpdateEntityInRoom(Hashtable table, string s, string comma)
    // {
    //     if (!table.ContainsKey(s.Substring(0, s.IndexOf(comma))))
    //     {
    //         table.Add(s.Substring(0, s.IndexOf(comma)), s.Substring(s.IndexOf(comma) + 2));
    //     }
    //     else if (table[s.Substring(0, s.IndexOf(comma))] != s.Substring(s.IndexOf(comma) + 2))
    //     {
    //         table.Remove(s.Substring(0, s.IndexOf(comma)));
    //         table.Add(s.Substring(0, s.IndexOf(comma)), s.Substring(s.IndexOf(comma) + 2));
    //     }

    //     foreach(string key in PuzzleTable.Keys)
    //     {
    //         Debug.Log(String.Format("{0}: {1}", key, PuzzleTable[key]));
    //     }
    //     Debug.Log("STOP");
    // }

    //Takes information from our data structure and sets up the current scene
    public void PlaceTreasureInRoom()
    {
        string[] strings = System.IO.File.ReadAllLines(@"treasures.txt");
        int found = 0;

        foreach (string s in strings)
        {
            //UpdateEntityInRoom(TreasureTable, s, ", ");
            for (int i = 0; i < FindObjectsOfType<SceneItem>(true).Length; i++)
            {
                if (s.Contains(FindObjectsOfType<SceneItem>(true)[i].ID + ", "))
                {
                    found = s.IndexOf(", ");
                    FindObjectsOfType<SceneItem>(true)[i].HasBeenPickedUp = bool.Parse(s.Substring(found + 2));
                }
            }
        }
    }

    public void PlaceEnemiesInRoom()
    {
        string[] strings = System.IO.File.ReadAllLines(@"enemies.txt");
        int found = 0;

        foreach (string s in strings)
        {
            //UpdateEntityInRoom(EnemyTable, s, ", ");
            for (int i = 0; i < FindObjectsOfType<EnemyInfo>(true).Length; i++)
            {
                if (s.Contains(FindObjectsOfType<EnemyInfo>(true)[i].ID + ", "))
                {
                    string[] words = s.Split(',');
                    for (int j = 0; j < words.Length; j++)
                    {
                        FindObjectsOfType<EnemyInfo>(true)[i].HasBeenDefeated = bool.Parse(words[1]);
                        FindObjectsOfType<EnemyInfo>(true)[i].DeathPlace.x = float.Parse(words[2].Substring(2));
                        FindObjectsOfType<EnemyInfo>(true)[i].DeathPlace.y = float.Parse(words[3]);

                        FindObjectsOfType<EnemyInfo>(true)[i].transform.position = new Vector3(float.Parse(words[2].Substring(2)), float.Parse(words[3]), FindObjectsOfType<EnemyInfo>(true)[i].transform.position.z);
                    }
                }
            }
        }
    }
    
    // public void PlacePuzzlesInRoom()
    // {
    //     string[] strings = System.IO.File.ReadAllLines(@"puzzles.txt");
    //     int found = 0;

    //     foreach (string s in strings)
    //     {
    //         //UpdateEntityInRoom(PuzzleTable, s, ", ");
    //         for (int i = 0; i < FindObjectsOfType<PuzzleManager>(true).Length; i++)
    //         {
    //             if (s.Contains(FindObjectsOfType<PuzzleManager>(true)[i].ID + ", "))
    //             {
    //                 found = s.IndexOf(", ");
    //                 FindObjectsOfType<PuzzleManager>(true)[i].PuzzleCompleted = bool.Parse(s.Substring(found + 2));
    //             }
    //         }
    //     }
    // }

    public void PlacePuzzlesInRoom()
    {
        string[] strings = System.IO.File.ReadAllLines(@"puzzles.json");

        foreach (string s in strings)
        {
            for (int i = 0; i < FindObjectsOfType<PuzzleManager>(true).Length; i++)
            {
                if (s.Contains(FindObjectsOfType<PuzzleManager>(true)[i].ID.ToString()))
                {
                    Debug.Log(s);
                    PuzzleInfo temp = JsonUtility.FromJson<PuzzleInfo>(s);
                    FindObjectsOfType<PuzzleManager>(true)[i].PuzzleCompleted = temp.PuzzleCompleted;
                    FindObjectsOfType<PuzzleManager>(true)[i].AlreadyComplete = temp.AlreadyComplete;
                    FindObjectsOfType<PuzzleManager>(true)[i].PlacedItem = temp.PlacedItem;
                }
            }
        }
    }

    //Takes information from the current scene and puts it in our data structure
    public void ObserveTreasureInRoom()
    {
        List<string> strings = new List<string>();

        for (int i = 0; i < FindObjectsOfType<SceneItem>(true).Length; i++)
        {
            //Grab item holder data
            string s = FindObjectsOfType<SceneItem>(true)[i].ID + ", " + FindObjectsOfType<SceneItem>(true)[i].HasBeenPickedUp;
            //Put item holder data in a list of strings
            strings.Add(s);
        }

        string[] stringsArray = System.IO.File.ReadAllLines(@"treasures.txt");
        List<string> stringsList = stringsArray.ToList();
        //Write the strings to a JSON file
        foreach (string s in stringsArray)
        {
            for (int i = 0; i < FindObjectsOfType<SceneItem>(true).Length; i++)
            {
                if (s.Contains(FindObjectsOfType<SceneItem>(true)[i].ID + ", "))
                {
                    stringsList.Remove(s);
                }
            }

            //UpdateEntityInRoom(TreasureTable, s, ", ");
        }

        System.IO.File.WriteAllLines(@"treasures.txt", stringsList);
        System.IO.File.AppendAllLines(@"treasures.txt", strings);
    }

    public void ObserveEnemiesInRoom()
    {
        List<string> strings = new List<string>();

        for (int i = 0; i < FindObjectsOfType<EnemyInfo>(true).Length; i++)
        {
            //Grab enemy info data
            string s = FindObjectsOfType<EnemyInfo>(true)[i].ID + ", " + FindObjectsOfType<EnemyInfo>(true)[i].HasBeenDefeated
             + ", " + FindObjectsOfType<EnemyInfo>(true)[i].DeathPlace;
            //Put item holder data in a list of strings
            strings.Add(s);
        }

        string[] stringsArray = System.IO.File.ReadAllLines(@"enemies.txt");
        List<string> stringsList = stringsArray.ToList();
        //Write the strings to a JSON file
        foreach (string s in stringsArray)
        {
            for (int i = 0; i < FindObjectsOfType<EnemyInfo>(true).Length; i++)
            {
                if (s.Contains(FindObjectsOfType<EnemyInfo>(true)[i].ID + ", "))
                {
                    stringsList.Remove(s);
                }
            }

            //UpdateEntityInRoom(TreasureTable, s, ", ");
        }

        System.IO.File.WriteAllLines(@"enemies.txt", stringsList);
        System.IO.File.AppendAllLines(@"enemies.txt", strings);
    }

    // public void ObservePuzzlesInRoom()
    // {
    //     // Make a setup list for setting up strings
    //     List<string> puzzleSetupStrings = new List<string>();

    //     // Look at all of the puzzles in the current scene
    //     for (int i = 0; i < FindObjectsOfType<PuzzleManager>(true).Length; i++)
    //     {
    //         //Grab puzzle data for their ID and their puzzle completed boolean
    //         string s = FindObjectsOfType<PuzzleManager>(true)[i].ID + ", " + FindObjectsOfType<PuzzleManager>(true)[i].PuzzleCompleted;
    //         //And put that puzzle data in a list of strings
    //         puzzleSetupStrings.Add(s);
    //     }

    //     // Grab save data that's in the game's save file and put it in a temporary string array
    //     string[] saveFileArray = System.IO.File.ReadAllLines(@"puzzles.txt");
    //     // And put that string array into a string list
    //     List<string> saveFileList = saveFileArray.ToList();
    //     // Iterate through the save data list
    //     foreach (string s in saveFileArray)
    //     {
    //     // Iterate through the current scene list
    //         for (int i = 0; i < puzzleSetupStrings.Count; i++)
    //         {
    //     // Compare the data from the two lists and remove it if the data exists in both lists
    //             if (s.Contains(FindObjectsOfType<PuzzleManager>(true)[i].ID + ", "))
    //             {
    //                 saveFileList.Remove(s);
    //             }
    //         }
    //     }

    //     // Clear the strings from the save file
    //     System.IO.File.WriteAllText(@"puzzles.txt", String.Empty);
    //     // Write the save data list to the save file
    //     System.IO.File.WriteAllLines(@"puzzles.txt", saveFileList);
    //     // Write the current scene data list to the save file
    //     System.IO.File.AppendAllLines(@"puzzles.txt", puzzleSetupStrings);
    // }

    public void ObservePuzzlesInRoom()
    {   
        // Look at all of the puzzles in the current scene
        List<PuzzleManager> currentScenePuzzles = FindObjectsOfType<PuzzleManager>(true).ToList();
        // Make a setup list for setting up strings
        List<string> puzzleSetupStrings = new List<string>();
        foreach (PuzzleManager puzzle in currentScenePuzzles)
        {
            puzzleSetupStrings.Add(JsonUtility.ToJson(puzzle));
        }

        // Grab save data that's in the game's save file and put it in a temporary string array
        string[] saveDataArray = System.IO.File.ReadAllLines(@"puzzles.json");
        // And put that string array into a string list
        List<string> saveDataStrings = saveDataArray.ToList();
        
        // Iterate through the save data list
        foreach (string s in saveDataArray)
        {
        // Iterate through the current scene list
            for (int i = 0; i < puzzleSetupStrings.Count; i++)
            {
        // Compare the data from the two lists and remove it if the data exists in both lists
                if (s.Contains(FindObjectsOfType<PuzzleManager>(true)[i].ID.ToString()))
                {
                    saveDataStrings.Remove(s);
                }
            }
        }

        // Clear the strings from the save file
        System.IO.File.WriteAllText(@"puzzles.json", String.Empty);
        // Write the save data list to the save file
        System.IO.File.WriteAllLines(@"puzzles.json", saveDataStrings);
        // Write the current scene data list to the save file
        System.IO.File.AppendAllLines(@"puzzles.json", puzzleSetupStrings);
    }
    
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
 
    void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (RM != null)
        {
            Destroy(this.gameObject);
            return;
        }

        RM = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
        TreasureTable = new Hashtable();
        EnemyTable = new Hashtable();
        PuzzleTable = new Hashtable();

        PlaceTreasureInRoom();
        PlaceEnemiesInRoom();
        PlacePuzzlesInRoom();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneChange)
        {
            sceneChange = false;
            PlaceTreasureInRoom();
            PlaceEnemiesInRoom();
            PlacePuzzlesInRoom();
        }
        else if (index != SceneManager.GetActiveScene().buildIndex)
        {
            index = SceneManager.GetActiveScene().buildIndex;
        }
    }
}