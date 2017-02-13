using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour {

	public Image Logo;
	public Image CoverImage;

	public Button LoginButton, AboutButton, ActualLoginButton;

	public InputField UserIdInput;
	public InputField PasswordInput;


	public Transform EndMarkerCoverImage;
	public Transform EndMarkerLogo;
	public float speed = 100.0f;


	Vector3 temp0, temp1, temp2;
	Toast toast;

	private bool LoginButtonPressed = false, AboutButtonPressed = false, CoverScreenAnimationDone = false;
	private float ButtonWidth, ButtonHeight, ConstButtonWidth, ConstButtonHeight;

	void Awake()
	{

		toast = new Toast();
		UserIdInput.image.rectTransform.sizeDelta = new Vector3 (0, 0, 0);
		PasswordInput.image.rectTransform.sizeDelta = new Vector3 (0, 0, 0);
		ActualLoginButton.image.rectTransform.sizeDelta = new Vector3 (0, 0, 0);
		temp0 = new Vector3 ( Screen.width / 2, 5 * Screen.height / 8, 0);
		temp1 = new Vector3( Screen.width / 2,  Screen.height / 8, 0);
		Logo.rectTransform.position = temp0;


		temp2.x = ConstButtonWidth = ButtonWidth = LoginButton.image.rectTransform.rect.width;
		temp2.y = ConstButtonHeight = ButtonHeight = LoginButton.image.rectTransform.rect.height;
		temp2.z = 0;
		temp0 = CoverImage.rectTransform.position;
		temp1 = Logo.rectTransform.position;

	}

	void Start()
	{
		PersistantValues.Load ();
		toast.AndroidToast ("App by Atharva Khare");
	}


	public void LoginButtonOnClick()
	{
		LoginButtonPressed = true;
	}

	public void VerifyLogin()
	{
		PersistantValues.currentTeam = PersistantValues.TeamsDatabase [int.Parse(UserIdInput.text)];
		string password = Crypto.Md5Sum (PersistantValues.currentTeam.UserID + PersistantValues.currentTeam.Name);
		Debug.Log ("Password: " + password.Substring (0, 6));
		if (PasswordInput.text == password.Substring(0,6))
			LoginSuccessful ();
		else
			toast.AndroidToast ("Invalid User ID or Password!");


	}

	public void LoginSuccessful() 
	{
		SceneManager.LoadScene("MenuScreen", LoadSceneMode.Single);
	}

	void Update()
	{
		
		if (LoginButtonPressed) 
		{
			if (Logo.rectTransform.position.y < EndMarkerLogo.position.y) {
				temp1.y = temp1.y + speed/3;
				Logo.rectTransform.position = temp1;
			}
			if (CoverImage.rectTransform.position.y < EndMarkerCoverImage.position.y) {
				temp0.y = temp0.y + speed;
				CoverImage.rectTransform.position = temp0;
				speed = speed - 1;
			} else
				CoverScreenAnimationDone = true;

			if (CoverScreenAnimationDone ) {
				Vector3 temp3 = new Vector3(UserIdInput.image.rectTransform.rect.width ,UserIdInput.image.rectTransform.rect.height , 0);
				if (temp3.x < ConstButtonWidth)
					temp3.x += 100;
				if (temp3.y < ConstButtonHeight)
					temp3.y += 10;
				UserIdInput.image.rectTransform.sizeDelta = temp3;
				PasswordInput.image.rectTransform.sizeDelta = temp3;
				ActualLoginButton.image.rectTransform.sizeDelta = temp3;
				if (temp3.x >= ConstButtonWidth && temp3.y >= ConstButtonHeight)
					CoverScreenAnimationDone = false;
			}


			/*else {
				temp.y = EndMarker.position.y;
				CoverImage.rectTransform.position = temp;
			}*/
			if (ButtonWidth <= 0) {
				Destroy (LoginButton);
				Destroy (AboutButton);

			} 
			else {	
				ButtonWidth -= 50;
				LoginButton.image.rectTransform.sizeDelta = new Vector2 (ButtonWidth, ButtonHeight);
				AboutButton.image.rectTransform.sizeDelta = new Vector2 (ButtonWidth, ButtonHeight);
			}
		}
	}

}
