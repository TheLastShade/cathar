using UnityEngine;
using System.Collections;

public class GPI_Behavior_AutoMove : MonoBehaviour {

	public bool isHorizontal;
	public int speed;
	private Rigidbody2D body;
	private Vector2 direction;

	void Start(){
		body = GetComponent<Rigidbody2D> ();
		AdjustSpeed ();
	}

	void OnCollisionEnter2D (Collision2D aCollision){
		direction = (this.transform.position - aCollision.transform.position);
		AdjustSpeed ();
	}

	void OnCollisionExit2D (Collision2D aCollision){
		//AdjustSpeed ();
	}


	void AdjustSpeed(){
		if (isHorizontal) {
			if (direction.x > 0 && speed < 0 || direction.x < 0 && speed > 0){
				speed = -speed;
			}

			
			body.velocity = (new Vector2 (speed, 0));
			body.constraints = (RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation);
			body.AddForce (direction);

		} else {
			if (direction.y > 0 && speed < 0 || direction.y < 0 && speed > 0){
				speed = -speed;
			}

			body.velocity = (new Vector2 (0,speed));
			body.constraints = (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation);
			body.AddForce (direction);

		}
	}

}
