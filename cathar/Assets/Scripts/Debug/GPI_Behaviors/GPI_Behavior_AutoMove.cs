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

	void Update(){
		print (body.velocity.magnitude);
	}

	void OnCollisionEnter2D (Collision2D aCollision){
		speed = -speed;
		AdjustSpeed ();

	}


	void AdjustSpeed(){
		if (isHorizontal) {
			body.velocity = (new Vector2 (speed, 0));
		} else {
			body.velocity = (new Vector2 (0,speed));
		}
	}

}
