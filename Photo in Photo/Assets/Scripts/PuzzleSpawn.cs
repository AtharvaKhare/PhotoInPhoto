using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleSpawn : MonoBehaviour {

	public GameObject socket;
	public GameObject image;
	public float rows = 5;
	public float cols = 5;
	public float offsetX = 50;
	public float offsetY = -200;
	public Texture2D texture;


	public Button arrangeAll;
	public Button startGameButton;

	public GameObject jigsawOutlineLeft, jigsawOutlineRight, jigsawOutlineUp, jigsawOutlineDown;

	private float frameSize;
	private Camera currentCamera;
	private Sprite[] sprites;
	private float padding;


	void Awake()
	{
		
	}

	void Start () 
	{
		//startGameButton.onClick.AddListener (startGame);
		if (PersistantValues.currentTeam.jigsawPuzzleCompleted [PersistantValues.puzzleNumber]) {
			startGame ();
			arrange ();
		}
		else {
			startGame ();
		}
	}


	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
			SceneManager.LoadScene("MenuScreen", LoadSceneMode.Single); 	
		if(check())
			PersistantValues.currentTeam.jigsawPuzzleCompleted [PersistantValues.puzzleNumber] = true;
	}

	void startGame()
	{
		Destroy (startGameButton);
		currentCamera = GetComponent<Camera> ();
		frameSize = Screen.width - 2 * offsetX;
		padding = frameSize / rows;

		if (offsetY < 100) {
			offsetY = 100;

		}

		int count = 0, randomX , randomYIndex;
		int[] randomY = {(int) (padding + 25),(int) (offsetY + padding + frameSize + 50), (int) (offsetY + 2 * padding + frameSize + 50)} ;
		float limit = rows / 2 * padding, limitX = Screen.width - offsetX;

		Vector3 spawnSocketLocation;
		GameObject spawnedSocket;

		GameObject spawnedImage;
		Vector3 randomPosition;

		sprites = Resources.LoadAll<Sprite>(PersistantValues.puzzleName);
		if (sprites == null)
			Debug.Log ("Sprites not loaded!");


		randomPosition = currentCamera.ScreenToWorldPoint (new Vector3(0, 0, -10));
		spawnedImage = (GameObject) Instantiate(image, new Vector3(0,0,0) , Quaternion.identity);
		spawnedImage.GetComponent<SpriteRenderer>().sprite = sprites[0];


		Vector3 x2 = spawnedImage.GetComponent<SpriteRenderer> ().bounds.max;
		Vector3 x1 = spawnedImage.GetComponent<SpriteRenderer> ().bounds.min;
		Destroy (spawnedImage);

		Vector3 s1 = currentCamera.ScreenToWorldPoint (new Vector3 (0, 0, 10));
		Vector3 s2 = currentCamera.ScreenToWorldPoint (new Vector3 (padding, 0, 10));
		float size = (s2.x - s1.x) / (x2.x - x1.x);
		Vector2 sizeVector = new Vector2 (size, size);





		for (float j = limit; j + limit > 0.001; j -= padding)
			for (float i = offsetX + padding/2; i - limitX < 0.001; i += padding)
			{
				count += 1;

				spawnSocketLocation = currentCamera.ScreenToWorldPoint (new Vector3 (i , j + Screen.height / 2 - offsetY, 50));
				spawnedSocket = (GameObject) Instantiate (socket, spawnSocketLocation, Quaternion.identity);
				spawnedSocket.name = "Image" + count + "Socket";

				randomX = Random.Range (100, Screen.width - 100);
				randomYIndex = Random.Range (0, 3) ;
				randomPosition = currentCamera.ScreenToWorldPoint (new Vector3(randomX, randomY[randomYIndex], 10));
				spawnedImage = (GameObject) Instantiate(image, randomPosition , Quaternion.identity);
				spawnedImage.name = "Image" + count;
				spawnedImage.GetComponent<SpriteRenderer>().sprite = sprites[count - 1];
				spawnedImage.GetComponent<SpriteRenderer> ().transform.localScale = sizeVector;

			}

		//
		float sizeX = GameObject.Find("Image1").transform.localScale.x *100 * 5;
		float sizeY = GameObject.Find("Image1").transform.localScale.y *100 * 5;
		Vector3 jigsawOutlineLocation;
		Vector2 jigsawOutlineSize = new Vector2 (Screen.width / 80 , sizeY + 2 * Screen.height / 160);
		jigsawOutlineLeft.transform.localScale = jigsawOutlineSize;
		jigsawOutlineRight.transform.localScale= jigsawOutlineSize;

		jigsawOutlineLocation = GameObject.Find("Image11Socket").transform.position;
		jigsawOutlineLocation.x -= GameObject.Find ("Image11").transform.localScale.y / 2 + jigsawOutlineSize.x / 200;
		jigsawOutlineLeft.transform.position = jigsawOutlineLocation;

		jigsawOutlineLocation = GameObject.Find("Image15Socket").transform.position;
		jigsawOutlineLocation.x += GameObject.Find("Image15").transform.localScale.y / 2 + jigsawOutlineSize.x / 200;
		jigsawOutlineRight.transform.position = jigsawOutlineLocation;


		jigsawOutlineSize = new Vector2 (Screen.width / 80 , 5 * padding);
		jigsawOutlineSize.x = sizeX + 2 * Screen.width / 80;
		jigsawOutlineSize.y = Screen.height / 160;
		jigsawOutlineUp.transform.localScale = jigsawOutlineSize;
		jigsawOutlineDown.transform.localScale= jigsawOutlineSize;


		jigsawOutlineLocation = GameObject.Find("Image3Socket").transform.position;
		jigsawOutlineLocation.y += GameObject.Find("Image3").transform.localScale.y / 2 + jigsawOutlineSize.y / 200;
		jigsawOutlineUp.transform.position = jigsawOutlineLocation;

		jigsawOutlineLocation = GameObject.Find("Image23Socket").transform.position;
		jigsawOutlineLocation.y -= GameObject.Find("Image23").transform.localScale.y / 2 + jigsawOutlineSize.y / 200;
		jigsawOutlineDown.transform.position = jigsawOutlineLocation;


		//

		arrangeAll.onClick.AddListener (arrange);

	}

	void arrange()
	{
		GameObject socketSpawn1, socketSpawn2;
		for (int i = 1; i <= rows * cols; ++i) {
			socketSpawn1 = GameObject.Find ("Image" + i);
			socketSpawn2 = GameObject.Find ("Image" + i + "Socket");
			socketSpawn1.transform.position = socketSpawn2.transform.position;
		}
	}

	bool check()
	{
		GameObject imageObj, socketObj;
		for (int i = 1; i <= rows * cols; ++i) {
			imageObj = GameObject.Find ("Image" + i);
			socketObj = GameObject.Find ("Image" + i + "Socket");
			if (imageObj.transform.position != socketObj.transform.position)
				return false;
		}
		return true;
	}

	public void OnExitButtonClick()
	{
		SceneManager.LoadScene("MenuScreen", LoadSceneMode.Single);

	}

}
