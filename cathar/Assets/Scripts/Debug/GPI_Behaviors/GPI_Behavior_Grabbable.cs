using UnityEngine;
using System.Collections;

public class GPI_Behavior_Grabbable : MonoBehaviour {

	bool isBeingDragged = false;
	Rigidbody2D body;
	Vector2 direction;
	bool isActioning = false;
	PlayerAction playerAction;
	Rigidbody2D playerBody;
	SpringJoint2D springJoint;

	void Start(){
		body = this.GetComponent<Rigidbody2D> ();
		body.isKinematic = true;
	}

	void Update(){

		if (playerAction != null) {

			//Check if that button is held
			isActioning = playerAction.getIsActioning ();

			if (!isActioning){
				if (springJoint != null){
					Destroy (springJoint);
					isBeingDragged = false;
					body.isKinematic = true;
					this.transform.position = new Vector3 (transform.position.x, transform.position.y, 30);
				}


			}

			
			if (isBeingDragged) {

				//TODO: Consider making a springjoint instead
				if (springJoint == null) {
					this.transform.position = new Vector3 (transform.position.x, transform.position.y, 15);
					springJoint = gameObject.AddComponent<SpringJoint2D> () as SpringJoint2D;
					springJoint.connectedBody = playerBody;
					springJoint.distance = 0;
					//springJoint.dampingRatio = 1000;
					springJoint.frequency = 10;
					springJoint.anchor = new Vector2 (0,-0.25f);
					body.isKinematic = false;
				}
			}	

		} else {

			//Find the player!
			playerAction = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponentInParent<PlayerAction> ();
			playerBody = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponentInParent<Rigidbody2D>();
		}

	}


	void OnCollisionStay2D (Collision2D aCollision){
		PlayerStat playerStat = aCollision.gameObject.GetComponentInParent<PlayerStat> ();
		
		if (playerStat != null) {
			if (playerAction != null) {
				isActioning = playerAction.getIsActioning ();
				if (isActioning) {
					isBeingDragged = true;
				} 
			}
		}
	}


	void OnCollisionEnter2D(Collision2D aCollision){

		PlayerStat playerStat = aCollision.gameObject.GetComponentInParent<PlayerStat> ();

		if (playerStat != null) {

			direction = ((this.transform.position) - (aCollision.gameObject.transform.position));

			float x = (direction.x);
			float y = (direction.y);
			float x_abs = Mathf.Abs (x);
			float y_abs = Mathf.Abs (y);

			if(x_abs > y_abs){
				y = 0;
				x = x / x_abs;
			} else {
				x = 0;
				y = y / y_abs;
			}

			direction = new Vector2 (x,y);
		}
	}
}
