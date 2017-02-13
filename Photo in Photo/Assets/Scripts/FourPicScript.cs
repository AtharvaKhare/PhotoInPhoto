using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FourPicScript : MonoBehaviour {

	GameObject FourPicA, FourPicB, FourPicC, FourPicD;
	public GameObject image;
	Camera currentCamera;
	Sprite[] PicA, PicB, PicC, PicD;
	// Use this for initialization
	void Start () {


		currentCamera = GetComponent<Camera> ();


		int imageWidth = Screen.width / 2;
		Vector3 position = currentCamera.ScreenToWorldPoint(new Vector3(imageWidth / 2, Screen.height / 2 + imageWidth / 2, 10));
		FourPicA = Instantiate (image, position, Quaternion.identity);
		position = currentCamera.ScreenToWorldPoint(new Vector3(3 * imageWidth / 2, Screen.height / 2 + imageWidth / 2, 10));
		FourPicB = Instantiate (image, position, Quaternion.identity);
		position = currentCamera.ScreenToWorldPoint(new Vector3(Screen.width / 4, Screen.height / 2 - imageWidth / 2, 10));
		FourPicC = Instantiate (image, position, Quaternion.identity);
		position = currentCamera.ScreenToWorldPoint(new Vector3(3 * Screen.width / 4, Screen.height / 2 - imageWidth / 2, 10));
		FourPicD = Instantiate (image, position, Quaternion.identity);
		PicA = Resources.LoadAll<Sprite> ("FourPicSprites/" + PersistantValues.puzzleName + "A");
		PicB = Resources.LoadAll<Sprite> ("FourPicSprites/" + PersistantValues.puzzleName + "B");
		PicC = Resources.LoadAll<Sprite> ("FourPicSprites/" + PersistantValues.puzzleName+ "C");
		PicD = Resources.LoadAll<Sprite> ("FourPicSprites/" + PersistantValues.puzzleName + "D");
		Debug.Log (PersistantValues.puzzleName);
		FourPicA.GetComponent<SpriteRenderer> ().sprite = PicA [0];
		FourPicB.GetComponent<SpriteRenderer> ().sprite = PicB [0];
		FourPicC.GetComponent<SpriteRenderer> ().sprite = PicC [0];
		FourPicD.GetComponent<SpriteRenderer> ().sprite = PicD [0];

		Vector3 s1 = currentCamera.ScreenToWorldPoint (new Vector3 (0, 0, 10));
		Vector3 s2 = currentCamera.ScreenToWorldPoint (new Vector3 (imageWidth, 0, 10));

		Vector3 x2 = FourPicA.GetComponent<SpriteRenderer> ().bounds.max;
		Vector3 x1 = FourPicA.GetComponent<SpriteRenderer> ().bounds.min;

		float size = (s2.x - s1.x) / (x2.x - x1.x);
		Vector2 sizeVector = new Vector2 (size, size);

		FourPicA.GetComponent<SpriteRenderer> ().transform.localScale = sizeVector;
		FourPicB.GetComponent<SpriteRenderer> ().transform.localScale = sizeVector;
		FourPicC.GetComponent<SpriteRenderer> ().transform.localScale = sizeVector;
		FourPicD.GetComponent<SpriteRenderer> ().transform.localScale = sizeVector;
		
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
