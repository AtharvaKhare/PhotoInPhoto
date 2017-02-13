using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

	public GameObject ImagePrefab;
	private Camera currentCamera;
	private Sprite[] sprite1, sprite2;

	GameObject[] Background1 = new GameObject[12];
	GameObject[] Background2 = new GameObject[12];
	float life = 1;
	bool transition1 = true, transition2 = false;
	int[] tracker;
	int imageToChange;
	GameObject currentImage;
	int direction;
	float speed;
	float delay;
	void Awake()
	{
		currentCamera = GetComponent<Camera> ();
		sprite1 = Resources.LoadAll<Sprite>("UI/Background1");
		sprite2 = Resources.LoadAll<Sprite>("UI/Background2");
		if (sprite1 == null || sprite2 == null)
			Debug.Log ("Sprites not loaded!");
		tracker = new int[12];
		for (int i = 0; i < 12; ++i)
			tracker[i] = 0;
		imageToChange = Random.Range (0, 12);
		tracker [imageToChange] = 1;
		transition1 = true;
		transition2 = false;
		direction = 1;
		speed = 2;
		delay = 2;
	}

	// Use this for initialization
	void Start () {
		Vector3 location;
		location = currentCamera.ScreenToWorldPoint (new Vector3 (0, 0, -10));
		GameObject test = Instantiate (ImagePrefab, location, Quaternion.identity);
		test.GetComponent<SpriteRenderer>().sprite = sprite1[0];


		Vector3 x2 = test.GetComponent<SpriteRenderer> ().bounds.max;
		Vector3 x1 = test.GetComponent<SpriteRenderer> ().bounds.min;
		Destroy (test);

		Vector3 s1 = currentCamera.ScreenToWorldPoint (new Vector3 (0, 0, 10));
		Vector3 s2 = currentCamera.ScreenToWorldPoint (new Vector3 (Screen.width / 3, Screen.height / 4, 10));
		float size = (s2.x - s1.x) / (x2.x - x1.x);
		Vector2 sizeVector = new Vector2 (size, size);

		int count = 0;
		for (float j = 7 * Screen.height / 8; j >=  Screen.height / 8 - 1; j -= Screen.height / 4) {
			for (float i = Screen.width / 6; i <= 5 * Screen.width / 6 + 1; i += Screen.width / 3) {
				location = currentCamera.ScreenToWorldPoint (new Vector3 (i, j, 10));
				Background1 [count] = Instantiate (ImagePrefab, location, Quaternion.identity);
				Background1 [count].GetComponent<SpriteRenderer>().sprite = sprite1[count];
				Background1 [count].GetComponent<SpriteRenderer> ().transform.localScale = sizeVector;

				Background2 [count] = Instantiate (ImagePrefab, location, Quaternion.identity);
				Background2 [count].GetComponent<SpriteRenderer>().sprite = sprite2[count];
				Background2 [count].GetComponent<SpriteRenderer> ().transform.localScale = sizeVector;

				++count;
			}
		}
		currentImage = Background1 [imageToChange];
	}
	
	// Update is called once per frame
	void Update () {
		if (delay >= 0) {
			delay -= Time.deltaTime;
		} else {
			Color t = currentImage.GetComponent<SpriteRenderer> ().color;
			t.a = life;
			currentImage.GetComponent<SpriteRenderer> ().color = t;

			if (life >= 0) {
				tracker [imageToChange] = 0;
				life -= Time.deltaTime * speed * direction;
			}

			if (life <= 0) {
				tracker [imageToChange] = 1;
				life -= Time.deltaTime * speed * direction;
			}
			if (transition1 == true) {
				//0 to 1
				if (Background1 [imageToChange].GetComponent<SpriteRenderer> ().color.a <= 0) {
					while (tracker [imageToChange] == 1)
						imageToChange = Random.Range (0, 12);
					//tracker [imageToChange] = 1;
					currentImage = Background1 [imageToChange];
					life = 1;
				}
				int flag = 0;
				for (int i = 0; i < 12; ++i) {
					if (tracker [i] == 0) {
						flag = 1;
						break;
					}
				}
				Debug.Log (flag);
				if (flag == 0) {
					Debug.Log ("Set");
					transition1 = false;
					transition2 = true;
					direction = -1;
					delay = 0;
				} else {
					transition2 = false;
					transition1 = true;
				}
			}
			if (transition2 == true) {
				//1 to 0
				//Debug.Log("Hello");
				if (Background1 [imageToChange].GetComponent<SpriteRenderer> ().color.a >= 1) {
					while (tracker [imageToChange] == 0)
						imageToChange = Random.Range (0, 12);
					//tracker [imageToChange] = 0;
					currentImage = Background1 [imageToChange];
					life = 0;
				}
				int flag = 0;
				for (int i = 0; i < 12; ++i) {
					if (tracker [i] == 1) {
						flag = 1;
						break;
					}
				}
				if (flag == 0) {
					Debug.Log ("Set");
					transition2 = false;
					transition1 = true;
					direction = 1;
					delay = 0;
				} else {
					transition1 = false;
					transition2 = true;
				}
			}

		}
		
	}
}
