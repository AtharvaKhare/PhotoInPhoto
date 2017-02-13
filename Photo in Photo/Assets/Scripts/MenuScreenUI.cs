using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuScreenUI : MonoBehaviour {

	public Text displayPassword;

	public Image LoginImage;
	public Button LoginButton;

	public InputField PasswordInput;

	private bool LoginImageAnimationComplete = false;
	private float LoginImageWidth, LoginImageHeight, ConstLoginImageWidth, ConstLoginImageHeight;


	Vector3 temp;

	void Awake()
	{
		ConstLoginImageWidth = LoginImageWidth = LoginImage.rectTransform.rect.width;
		ConstLoginImageHeight = LoginImageHeight = LoginImage.rectTransform.rect.height;
		temp = new Vector3(0,0,0);
		LoginImage.rectTransform.sizeDelta = temp;
	}


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!LoginImageAnimationComplete) {
			Vector3 temp3 = new Vector3(LoginImage.rectTransform.rect.width ,LoginImage.rectTransform.rect.height , 0);
			if (temp3.x < ConstLoginImageWidth)
				temp3.x += 100;
			if (temp3.y < ConstLoginImageHeight)
				temp3.y += 100;
			LoginImage.rectTransform.sizeDelta = temp3;
			if (temp3.x >= ConstLoginImageWidth && temp3.y >= ConstLoginImageHeight)
				LoginImageAnimationComplete = true;
		}
	}

	public void LoginButtonClick()
	{
		string password = PasswordInput.text;
		PersistantValues.puzzleName = "";
		string stk;
		stk = Crypto.Md5Sum (PersistantValues.currentTeam.UserID + PersistantValues.currentTeam.Name + PersistantValues.currentTeam.jigsawPuzzleName1);
		stk = stk.Substring (0, 6);
		Toast toast2 = new Toast ();
		displayPassword.text = "Jigsaw 1: " + stk;
		if (stk == password) {
			PersistantValues.puzzleName = PersistantValues.currentTeam.jigsawPuzzleName1;
			PersistantValues.puzzleNumber = 0;
		}
		
		stk = Crypto.Md5Sum (PersistantValues.currentTeam.UserID + PersistantValues.currentTeam.Name + PersistantValues.currentTeam.jigsawPuzzleName2);
		stk = stk.Substring (0, 6);
		displayPassword.text += "\nJigsaw 2: " + stk;
		if (stk == password) {
			PersistantValues.puzzleName = PersistantValues.currentTeam.jigsawPuzzleName2;
			PersistantValues.puzzleNumber = 1;
		}


		
		stk = Crypto.Md5Sum (PersistantValues.currentTeam.UserID + PersistantValues.currentTeam.Name + PersistantValues.currentTeam.jigsawPuzzleName3);
		stk = stk.Substring (0, 6);
		displayPassword.text += "\nJigsaw 3: " + stk;
		if (stk == password) {
			PersistantValues.puzzleName = PersistantValues.currentTeam.jigsawPuzzleName3;

			PersistantValues.puzzleNumber = 2;
		}


		stk = Crypto.Md5Sum (PersistantValues.currentTeam.UserID + PersistantValues.currentTeam.Name + PersistantValues.currentTeam.riddle1);
		stk = stk.Substring (0, 6);
		displayPassword.text += "\nRiddle 1: " + stk;
		if (stk == password) {
			PersistantValues.puzzleName = PersistantValues.currentTeam.riddle1;
			PersistantValues.puzzleNumber = 6;
		}

		stk = Crypto.Md5Sum (PersistantValues.currentTeam.UserID + PersistantValues.currentTeam.Name + PersistantValues.currentTeam.riddle2);
		stk = stk.Substring (0, 6);
		displayPassword.text += "\nRiddle 2: " + stk;
		if (stk == password) {
			PersistantValues.puzzleName = PersistantValues.currentTeam.riddle2;
			PersistantValues.puzzleNumber = 7;
		}

		stk = Crypto.Md5Sum (PersistantValues.currentTeam.UserID + PersistantValues.currentTeam.Name + PersistantValues.currentTeam.riddle3);
		stk = stk.Substring (0, 6);
		displayPassword.text += "\nRiddle 3: " + stk;
		if (stk == password) {
			PersistantValues.puzzleName = PersistantValues.currentTeam.riddle3;
			PersistantValues.puzzleNumber = 8;
		}

		stk = Crypto.Md5Sum (PersistantValues.currentTeam.UserID + PersistantValues.currentTeam.Name + PersistantValues.currentTeam.fourPicPuzzleName1);
		stk = stk.Substring (0, 6);
		displayPassword.text += "\nFourPic 1: " + stk;
		if (stk == password) {
			PersistantValues.puzzleName = PersistantValues.currentTeam.fourPicPuzzleName1;
			PersistantValues.puzzleNumber = 3;
		}
		
		stk = Crypto.Md5Sum (PersistantValues.currentTeam.UserID + PersistantValues.currentTeam.Name + PersistantValues.currentTeam.fourPicPuzzleName2);
		stk = stk.Substring (0, 6);
		displayPassword.text += "\nFourPic 2: " + stk;
		if (stk == password){
			PersistantValues.puzzleName = PersistantValues.currentTeam.fourPicPuzzleName2;
			PersistantValues.puzzleNumber = 4;
		}

		stk = Crypto.Md5Sum (PersistantValues.currentTeam.UserID + PersistantValues.currentTeam.Name + PersistantValues.currentTeam.fourPicPuzzleName3);
		stk = stk.Substring (0, 6);
		displayPassword.text += "\nFourPic 3: " + stk;
		if (stk == password){
			PersistantValues.puzzleName = PersistantValues.currentTeam.fourPicPuzzleName3;
			PersistantValues.puzzleNumber = 5;
		}
			
		if (PersistantValues.puzzleName == "") {
			Toast toast = new Toast ();
			toast.AndroidToast ("Invalid password");

		} else {
			if(PersistantValues.puzzleNumber >=0 && PersistantValues.puzzleNumber <=2)
				SceneManager.LoadScene("TestScene0", LoadSceneMode.Single);
			if (PersistantValues.puzzleNumber >= 3 && PersistantValues.puzzleNumber <= 5)
				SceneManager.LoadScene ("FourPic", LoadSceneMode.Single);
			if (PersistantValues.puzzleNumber >= 6 && PersistantValues.puzzleNumber <= 8)
				SceneManager.LoadScene ("RiddleScene", LoadSceneMode.Single);
			else
				Debug.Log ("Scene error");
		}
	}
}
