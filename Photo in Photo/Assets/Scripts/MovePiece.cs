using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePiece : MonoBehaviour {

	public short pieceStatus = 0;
	// pieceStatus = 0 -> idle
	// pieceStatus = 1 -> pickedUp
	// pieceStatus = 2 -> locked
	public float mousePosX;
	public float mousePosY;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () 
	{

		//If piece is picked up
		if (pieceStatus == 1)
		{
			Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector2 objectPosition = Camera.main.ScreenToWorldPoint (mousePosition);
			transform.position = objectPosition;
		}

	}

	void OnTriggerStay2D(Collider2D socket)
	{
		if (socket.gameObject.name == gameObject.name + "Socket") 
		{
			if (!Input.GetMouseButton (0)) {
				transform.position = socket.gameObject.transform.position;
				pieceStatus = 2;
				Destroy (GetComponent<BoxCollider2D>());
			}
		}
	}

	void OnMouseDrag()
	{
		if (pieceStatus == 0) {
			//Piece is picked up
			pieceStatus = 1;
		}
	}

	void OnMouseUp()
	{
		if (pieceStatus == 1) {
			//Piece is dropped
			pieceStatus = 0;
		}
	}
}
