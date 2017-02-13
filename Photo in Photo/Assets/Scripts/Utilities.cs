using System.Collections;
using UnityEngine;
using System.Collections.Generic; 

[System.Serializable]
public class TeamInfo
{
	public int UserID;
	public string Name;
	public string jigsawPuzzleName1, jigsawPuzzleName2, jigsawPuzzleName3;
	public string fourPicPuzzleName1, fourPicPuzzleName2, fourPicPuzzleName3;
	public string riddle1, riddle2, riddle3;
	public bool[] jigsawPuzzleCompleted;

}

public static class PersistantValues
{
	public static TeamInfo currentTeam;
	public static List<TeamInfo> TeamsDatabase;

	public static string puzzleName;
	public static int puzzleNumber;
	public static TeamInfo[] saveTeam;
	public static void Load()
	{
		saveTeam = new TeamInfo[1000];
		TeamsDatabase = new List<TeamInfo>();

		saveTeam[0] = new TeamInfo ();
		saveTeam[0].UserID = 0;
		saveTeam[0].Name = "Atharva Khare";
		saveTeam[0].jigsawPuzzleName1 = "Puzzle1";
		saveTeam[0].jigsawPuzzleName2 = "Puzzle2"; 
		saveTeam[0].jigsawPuzzleName3 = "Puzzle3";
		saveTeam[0].fourPicPuzzleName1 = saveTeam[0].fourPicPuzzleName2 = saveTeam[0].fourPicPuzzleName3= "Puzzle0";
		saveTeam[0].riddle1 = saveTeam[0].riddle2 = saveTeam[0].riddle3 = "Puzzle1";
		TeamsDatabase.Add (saveTeam[0]);
		for (int i = 1; i <= 100; ++i) {
			saveTeam[i] = new TeamInfo ();
			saveTeam[i].UserID = i;
			saveTeam[i].Name = "Dummy Team:" + i;
			TeamsDatabase.Add (saveTeam[i]);
		}

		int jiggy1 = 1, jiggy2 = 1, jiggy3 = 1;
		int four1 = 1;

		for(int i = 1; i <=100; ++i)
		{
			if (jiggy1 >= 3)
				jiggy1 = 1;
			if (jiggy2 >= 3)
				jiggy2 = 1;
			if (jiggy3 >= 3)
				jiggy3 = 1;
			if(four1 >= 2)
				four1 = 1;
			TeamsDatabase [i].jigsawPuzzleName1 = "JigsawSprites/Short Distance/Puzzle" + jiggy1;
			TeamsDatabase [i].jigsawPuzzleName2 = "JigsawSprites/Long Distance/Puzzle" + jiggy2;
			TeamsDatabase [i].jigsawPuzzleName3 = "JigsawSprites/Hard/Puzzle" + jiggy3;
			TeamsDatabase [i].fourPicPuzzleName1 = "FourPic" + four1;
			TeamsDatabase [i].fourPicPuzzleName2 = "FourPic" + (four1 + 1);
			TeamsDatabase [i].jigsawPuzzleCompleted = new bool[3];
			TeamsDatabase [i].jigsawPuzzleCompleted [0] = TeamsDatabase [i].jigsawPuzzleCompleted [1] = TeamsDatabase [i].jigsawPuzzleCompleted [2] = false;
			TeamsDatabase [i].riddle1 = "23 x 23";
			TeamsDatabase [i].riddle2 = "I Have 3 eyes \nBut i can’t see.";
			TeamsDatabase [i].riddle3 = "You hate my first three letters on any day\nYou wish to the rest of me.";
			++jiggy1;
			++jiggy2;
			++jiggy3;
			++four1;
		}
		string line = "";
		for (int i = 0; i <= 100; ++i) {
			string stk = "";
			stk = Crypto.Md5Sum (TeamsDatabase[i].UserID + TeamsDatabase[i].Name);
			stk = stk.Substring (0, 6);
			line += i + "\tTeam Number " + ": " + stk + "\n";
			stk = "";
			stk = Crypto.Md5Sum (TeamsDatabase[i].UserID + TeamsDatabase[i].Name + TeamsDatabase[i].jigsawPuzzleName1);
			stk = stk.Substring (0, 6);
			line += "\tPassword Jigsaw 1: " + stk + "\n";
			stk = "";
			stk = Crypto.Md5Sum (TeamsDatabase[i].UserID + TeamsDatabase[i].Name + TeamsDatabase[i].jigsawPuzzleName2);
			stk = stk.Substring (0, 6);
			line += "\tPassword Jigsaw 2: " + stk + "\n";
			stk = "";
			stk = Crypto.Md5Sum (TeamsDatabase[i].UserID + TeamsDatabase[i].Name + TeamsDatabase[i].jigsawPuzzleName3);
			stk = stk.Substring (0, 6);
			line += "\tPassword Jigsaw 3: " + stk + "\n";
			stk = "";
			stk = Crypto.Md5Sum (TeamsDatabase[i].UserID + TeamsDatabase[i].Name + TeamsDatabase[i].fourPicPuzzleName1);
			stk = stk.Substring (0, 6);
			line += "\tPassword Four Pic 1: " + stk + "\n";
			stk = "";
			stk = Crypto.Md5Sum (TeamsDatabase[i].UserID + TeamsDatabase[i].Name + TeamsDatabase[i].fourPicPuzzleName2);
			stk = stk.Substring (0, 6);
			line += "\tPassword Four Pic 2: " + stk + "\n";
			stk = "";
			stk = Crypto.Md5Sum (TeamsDatabase[i].UserID + TeamsDatabase[i].Name + TeamsDatabase[i].riddle1);
			stk = stk.Substring (0, 6);
			line += "\tPassword Riddle: " + stk + "\n\n";
			line += "-------------------------------\n\n";
			Debug.Log (i);
		}
		System.IO.File.WriteAllText(Application.persistentDataPath + "/passwords.txt", line);	
	}
}



public static class Crypto
{

	public static string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}

	public static string Sha1Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.SHA1CryptoServiceProvider sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
		byte[] hashBytes = sha1.ComputeHash (bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}



}

public class Toast
{
	AndroidJavaObject currentActivity;
	string toastString;


	public void AndroidToast (string toastString)
	{
		if (Application.platform == RuntimePlatform.Android) {
			showToastOnUiThread (toastString);
		}
	}

	void showToastOnUiThread(string toastString){
		AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

		currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		this.toastString = toastString;

		currentActivity.Call ("runOnUiThread", new AndroidJavaRunnable (showToast));
	}

	void showToast(){
		AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
		AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
		AndroidJavaObject javaString=new AndroidJavaObject("java.lang.String",toastString);
		AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject> ("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
		toast.Call ("show");
	}

}