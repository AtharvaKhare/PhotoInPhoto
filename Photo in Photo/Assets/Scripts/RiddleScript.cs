using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RiddleScript : MonoBehaviour {

	public Text display;

	// Use this for initialization
	void Start () {
		if(PersistantValues.puzzleNumber == 6)
			display.text = PersistantValues.currentTeam.riddle1;
		else if(PersistantValues.puzzleNumber == 7)
			display.text = PersistantValues.currentTeam.riddle2;
		else if (PersistantValues.puzzleNumber == 8)
			display.text = PersistantValues.currentTeam.riddle3;
		else
			display.text = "Error. Please contact desk at Cultural Center.";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			SceneManager.LoadScene("MenuScreen", LoadSceneMode.Single); 

	}

	public void OnExit()
	{
		SceneManager.LoadScene ("MenuScreen", LoadSceneMode.Single); 
	}
}
