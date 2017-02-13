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


	public ParticleSystem placedAnimation;


	ParticleSystem lowerAnimation;
	ParticleSystem upperAnimation;
	ParticleSystem rightAnimation;
	ParticleSystem leftAnimation;


	void Update () 
	{
		//If piece is picked up
		if (pieceStatus == 1)
		{
			Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector2 objectPosition = Camera.main.ScreenToWorldPoint (mousePosition);
			transform.position = objectPosition;
		}

		if (lowerAnimation && !lowerAnimation.IsAlive()) {
			Destroy (lowerAnimation);
			Destroy (upperAnimation);
			Destroy (rightAnimation);
			Destroy (leftAnimation);
		}



	}

	void OnTriggerStay2D(Collider2D socket)
	{
		if (socket.gameObject.name == gameObject.name + "Socket") 
		{
			if (!Input.GetMouseButton (0)) {
				transform.position = socket.gameObject.transform.position;
				pieceStatus = 2;
				GetComponent<Renderer> ().sortingOrder = 0;
				ParticleSystem.ShapeModule tempShapeModule;
				Vector3 temp1 = GetComponent<SpriteRenderer> ().bounds.max;
				Vector3 temp0 = GetComponent<SpriteRenderer> ().bounds.min;
				float offset = (temp1.x - temp0.x)/2;
				temp0.x = temp0.x + offset;
				lowerAnimation = Instantiate (placedAnimation, temp0, Quaternion.Euler(0, 0, 180));
				lowerAnimation.name = gameObject.name + "lowerAnimation";
				tempShapeModule = lowerAnimation.GetComponent<ParticleSystem> ().shape;
				tempShapeModule.radius = offset;

				temp0.y = temp0.y + 2 * offset;
				upperAnimation = Instantiate (placedAnimation, temp0, Quaternion.identity);
				upperAnimation.name = gameObject.name + "upperAnimation";
				tempShapeModule = upperAnimation.GetComponent<ParticleSystem> ().shape;
				tempShapeModule.radius =  offset;

				temp1.y = temp1.y - offset;
				rightAnimation = Instantiate (placedAnimation, temp1, Quaternion.Euler(0, 0, 270));
				rightAnimation.name = gameObject.name + "rightAnimation";
				tempShapeModule = rightAnimation.GetComponent<ParticleSystem> ().shape;
				tempShapeModule.radius = offset;

				temp1.x = temp1.x - 2 * offset;
				leftAnimation = Instantiate (placedAnimation, temp1, Quaternion.Euler(0, 0, 90));
				leftAnimation.name = gameObject.name + "leftAnimation";
				tempShapeModule = leftAnimation.GetComponent<ParticleSystem> ().shape;
				tempShapeModule.radius = offset;



				Destroy (GetComponent<BoxCollider2D>());
				Destroy (socket.GetComponent<BoxCollider2D>());
				Destroy (GetComponent<Rigidbody2D>());
			}
		}
	}

	void OnMouseDrag()
	{
		
		if (pieceStatus == 0) {
			//Piece is picked up
			pieceStatus = 1;
			GetComponent<Renderer> ().sortingOrder = 10;
		} 
	}

	void OnMouseUp()
	{
		if (pieceStatus == 1) {
			pieceStatus = 0;
			GetComponent<Renderer> ().sortingOrder = 0;
		}
	}
}
