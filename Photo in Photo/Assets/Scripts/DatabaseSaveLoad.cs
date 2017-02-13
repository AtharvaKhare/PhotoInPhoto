using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class DatabaseSaveLoad : MonoBehaviour {
	public TeamInfo currentTeam;
	public List<TeamInfo> TeamsDatabase;

	public string puzzleName;

	public void Save()
	{
		if (File.Exists (Application.persistentDataPath + "/cache.txt"))
		{
			BinaryFormatter loadF = new BinaryFormatter();
			FileStream loadFile = File.Open(Application.persistentDataPath + "/cache.txt", FileMode.Open);
			TeamsDatabase = (List<TeamInfo>) loadF.Deserialize(loadFile);
			loadFile.Close ();
		}

		BinaryFormatter saveF = new BinaryFormatter();
		FileStream saveFile = File.Create(Application.persistentDataPath + "/cache.txt");
		TeamInfo saveTeam = new TeamInfo ();
		saveTeam.UserID = 0;
		saveTeam.Name = "Atharva Khare";
		saveTeam.jigsawPuzzleName1 = "Puzzle1";
		saveTeam.jigsawPuzzleName2 = "Puzzle2"; 
		saveTeam.jigsawPuzzleName3 = "Puzzle3";
		saveTeam.fourPicPuzzleName1 = saveTeam.fourPicPuzzleName2 = saveTeam.fourPicPuzzleName3= "Puzzle4";
		saveTeam.riddle1 = saveTeam.riddle2 = saveTeam.riddle3 = "Puzzle5";
		TeamsDatabase.Add (saveTeam);
		TeamInfo saveTeam1 = new TeamInfo ();
		saveTeam1.UserID = 1;
		saveTeam1.Name = "TEMP";
		TeamsDatabase.Add (saveTeam1);
		saveF.Serialize (saveFile, TeamsDatabase);
		saveFile.Close ();
	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/cache.txt"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/cache.txt", FileMode.Open);
			TeamsDatabase = (List<TeamInfo>) bf.Deserialize(file);
			file.Close ();
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
