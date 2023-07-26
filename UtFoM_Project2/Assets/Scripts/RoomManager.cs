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

    public void UpdateEntityInRoom(Hashtable table, string s, string comma)
    {
        if (!table.ContainsKey(s.Substring(0, s.IndexOf(comma))))
        {
            table.Add(s.Substring(0, s.IndexOf(comma)), s.Substring(s.IndexOf(comma) + 2));
        }
        else if (table[s.Substring(0, s.IndexOf(comma))] != s.Substring(s.IndexOf(comma) + 2))
        {
            table.Remove(s.Substring(0, s.IndexOf(comma)));
            table.Add(s.Substring(0, s.IndexOf(comma)), s.Substring(s.IndexOf(comma) + 2));
        }
    }

    //Takes information from our data structure and sets up the current scene
    public void PlaceTreasureInRoom()
    {
        string[] strings = System.IO.File.ReadAllLines(@"treasures.txt");
        int found = 0;

        foreach (string s in strings)
        {
            UpdateEntityInRoom(TreasureTable, s, ", ");
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
            UpdateEntityInRoom(EnemyTable, s, ", ");
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
                    }
                }
            }
        }
    }
    
    public void PlacePuzzlesInRoom()
    {
        string[] strings = System.IO.File.ReadAllLines(@"puzzles.txt");
        int found = 0;

        foreach (string s in strings)
        {
            UpdateEntityInRoom(PuzzleTable, s, ", ");
            for (int i = 0; i < FindObjectsOfType<PuzzleManager>(true).Length; i++)
            {
                if (s.Contains(FindObjectsOfType<PuzzleManager>(true)[i].ID + ", "))
                {
                    found = s.IndexOf(", ");
                    FindObjectsOfType<PuzzleManager>(true)[i].PuzzleCompleted = bool.Parse(s.Substring(found + 2));
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

            UpdateEntityInRoom(TreasureTable, s, ", ");
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

            UpdateEntityInRoom(TreasureTable, s, ", ");
        }

        System.IO.File.WriteAllLines(@"enemies.txt", stringsList);
        System.IO.File.AppendAllLines(@"enemies.txt", strings);
    }

    public void ObservePuzzlesInRoom()
    {
        List<string> strings = new List<string>();

        for (int i = 0; i < FindObjectsOfType<PuzzleManager>(true).Length; i++)
        {
            //Grab puzzle data
            string s = FindObjectsOfType<PuzzleManager>(true)[i].ID + ", " + FindObjectsOfType<PuzzleManager>(true)[i].PuzzleCompleted;
            //Put puzzle data in a list of strings
            strings.Add(s);
        }

        string[] stringsArray = System.IO.File.ReadAllLines(@"puzzles.txt");
        List<string> stringsList = stringsArray.ToList();
        //Write the strings to a JSON file
        foreach (string s in stringsArray)
        {
            for (int i = 0; i < FindObjectsOfType<PuzzleManager>(true).Length; i++)
            {
                if (s.Contains(FindObjectsOfType<PuzzleManager>(true)[i].ID + ", "))
                {
                    stringsList.Remove(s);
                }
            }

            UpdateEntityInRoom(PuzzleTable, s, ", ");
        }

        System.IO.File.WriteAllLines(@"puzzles.txt", stringsList);
        System.IO.File.AppendAllLines(@"puzzles.txt", strings);
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