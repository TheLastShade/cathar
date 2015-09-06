using UnityEngine;
using System.Collections;

public class GPI_Behavior_AutoMove : MonoBehaviour {

	public bool isHorizontal;
	public int speed;
	private Rigidbody2D body;

	void Start(){
		body = GetComponent<Rigidbody2D> ();
		AdjustSpeed ();
	}

	void OnCollisionEnter2D (Collision2D aCollision){
		speed = -speed;
		AdjustSpeed ();

	}


	void AdjustSpeed(){
		if (isHorizontal) {
			body.velocity = (new Vector2 (speed, 0));
			body.constraints = (RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation);
		} else {
			body.velocity = (new Vector2 (0,speed));
			body.constraints = (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation);
		}
	}

}
