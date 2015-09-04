using UnityEngine;
using System.Collections;

public class GPI_Behavior_Push : MonoBehaviour {

	bool isDragging = false;
	Rigidbody2D body;
	int speed = 50000;
	Vector2 direction;

	void Start(){
		body = this.GetComponent<Rigidbody2D> ();
	}

	void Update(){
		if (isDragging) {
			body.AddForce(direction * speed);
		}
	}

	void OnCollisionEnter2D(Collision2D aCollision){

		PlayerStat playerStat = aCollision.gameObject.GetComponentInParent<PlayerStat> ();

		if (aCollision != null) {
			isDragging = true;
			direction = ((this.transform.position) - (aCollision.gameObject.transform.position));

			float x = (direction.x);
			float y = (direction.y);

			if(Mathf.Abs (x) > Mathf.Abs (y)){
				y = 0;
				x = x / Mathf.Abs(x);
			} else {
				x = 0;
				y = y / Mathf.Abs(y);
			}
			direction = new Vector2 (x,y);
		}
	}

	void OnCollisionExit2D (Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			isDragging = false;

			//If we want a harder stop
			//body.velocity = Vector2.zero;
		}
	}
}
