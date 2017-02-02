using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuScreenUI : MonoBehaviour {


	public Image LoginImage;
	public Button LoginButton;

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

	public void LoginSuccessful()
	{
		SceneManager.LoadScene("TestScene0", LoadSceneMode.Single);
	}
}
