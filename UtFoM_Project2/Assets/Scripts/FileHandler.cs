// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;

// public class FileHandler : MonoBehaviour
// {
//     public void SaveToJSON<T>(List<T> toSave, string fileName)
//     {
//     	string content = "";
//     	WriteFile(GetPath(fileName));
//     }

//     public void ReadFromJSON()
//     {

//     }

//     private string GetPath(string fileName)
//     {
//     	// return Application.persistentDataPath + "/" fileName;
//     }

//     private void WriteFile(string path, string content)
//     {
//     	FileStream fileStream = new FileStream(path, FileMode.Create);

//     	using (StreamWriter writer = new StreamWriter(fileStream))
//     	{
//     		writer.Write(content);
//     	}
//     }

//     private string ReadFile()
//     {
//     	return "";
//     }
// }
