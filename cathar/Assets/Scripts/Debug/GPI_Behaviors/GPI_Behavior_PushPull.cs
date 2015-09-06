﻿using UnityEngine;
using System.Collections;

public class GPI_Behavior_PushPull : MonoBehaviour {

	bool isBeingPushed = false;
	bool isBeingPulled = false;
	Rigidbody2D body;
	int speed = 50000;
	Vector2 direction;
	bool isActioning = false;
	PlayerAction playerAction;

	void Start(){
		body = this.GetComponent<Rigidbody2D> ();
		//playerAction = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponentInParent<PlayerAction> ();
	}

	void Update(){

		if (playerAction != null) {
			isActioning = playerAction.getIsActioning ();
			
			if (isActioning && isBeingPushed) {
				isBeingPulled = true;
				isBeingPushed = false;
			} 
			
			if (!isActioning && isBeingPulled) {
				isBeingPulled = false;
				isBeingPushed = true;
			}
			
			if (isBeingPushed) {
				body.AddForce (direction * speed);
			}
			
			if (isBeingPulled) {
				//body.AddForce(-direction * speed);
				//Create a hingejoint?
				SpringJoint2D springJoint = gameObject.AddComponent<SpringJoint2D> () as SpringJoint2D;
			}	

		} else {
			playerAction = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponentInParent<PlayerAction> ();
		}

	}

	void OnCollisionEnter2D(Collision2D aCollision){

		PlayerStat playerStat = aCollision.gameObject.GetComponentInParent<PlayerStat> ();

		if (playerStat != null) {


			isBeingPushed = true;
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


	void OnCollisionExit2D (Collision2D aCollision){
	

		PlayerStat playerStat = aCollision.gameObject.GetComponentInParent<PlayerStat> ();
		
		if (playerStat != null) {

				isBeingPushed = false;
				isBeingPulled = false;

			//If we want a harder stop
			//body.velocity = Vector2.zero;
		}
	}

}
